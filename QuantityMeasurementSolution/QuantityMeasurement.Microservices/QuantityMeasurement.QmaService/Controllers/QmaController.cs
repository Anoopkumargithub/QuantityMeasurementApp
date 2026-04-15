using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using ModelLayer.Entities;
using QuantityMeasurement.Domain.Exceptions;
using QuantityMeasurement.QmaService.Contracts;
using QuantityMeasurement.QmaService.Services;
using RepositoryLayer.Interfaces;

namespace QuantityMeasurement.QmaService.Controllers;

[ApiController]
[Route("api/qma")]
[Authorize]
public sealed class QmaController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;
    private readonly IQuantityMeasurementRepository _repository;
    private readonly IHistoryCacheService _historyCacheService;

    public QmaController(
        IQuantityMeasurementService service,
        IQuantityMeasurementRepository repository,
        IHistoryCacheService historyCacheService)
    {
        _service = service;
        _repository = repository;
        _historyCacheService = historyCacheService;
    }

    [HttpPost("compare")]
    public async Task<ActionResult<ApiResponse<bool>>> Compare([FromBody] CompareRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _service.Compare(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity));
            await _historyCacheService.InvalidateHistoryAsync(cancellationToken);
            return Ok(new ApiResponse<bool> { Success = true, Message = "Comparison completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message, Data = false });
        }
    }

    [HttpPost("convert")]
    public async Task<ActionResult<ApiResponse<QuantityDto>>> Convert([FromBody] ConvertRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _service.Convert(MapToDto(request.SourceQuantity), request.TargetUnit);
            await _historyCacheService.InvalidateHistoryAsync(cancellationToken);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Conversion completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("add")]
    public async Task<ActionResult<ApiResponse<QuantityDto>>> Add([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _service.Add(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity), request.TargetUnit);
            await _historyCacheService.InvalidateHistoryAsync(cancellationToken);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Addition completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("subtract")]
    public async Task<ActionResult<ApiResponse<QuantityDto>>> Subtract([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _service.Subtract(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity), request.TargetUnit);
            await _historyCacheService.InvalidateHistoryAsync(cancellationToken);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Subtraction completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("divide")]
    public async Task<ActionResult<ApiResponse<double>>> Divide([FromBody] ArithmeticRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _service.Divide(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity));
            await _historyCacheService.InvalidateHistoryAsync(cancellationToken);
            return Ok(new ApiResponse<double> { Success = true, Message = "Division completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<double> { Success = false, Message = ex.Message });
        }
    }

    [HttpGet("history")]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<QuantityMeasurementEntity>>>> GetHistory(CancellationToken cancellationToken)
    {
        var cached = await _historyCacheService.GetHistoryAsync(cancellationToken);
        if (cached is not null)
        {
            return Ok(new ApiResponse<IReadOnlyList<QuantityMeasurementEntity>>
            {
                Success = true,
                Message = "History fetched from cache.",
                Data = cached
            });
        }

        var history = _repository.GetAllMeasurements();
        await _historyCacheService.SetHistoryAsync(history, cancellationToken);

        return Ok(new ApiResponse<IReadOnlyList<QuantityMeasurementEntity>>
        {
            Success = true,
            Message = "History fetched from repository.",
            Data = history
        });
    }

    [HttpDelete("history")]
    public async Task<ActionResult<ApiResponse<string>>> ClearHistory(CancellationToken cancellationToken)
    {
        _repository.DeleteAll();
        await _historyCacheService.InvalidateHistoryAsync(cancellationToken);

        return Ok(new ApiResponse<string>
        {
            Success = true,
            Message = "History cleared successfully.",
            Data = "OK"
        });
    }

    [HttpGet("health")]
    [AllowAnonymous]
    public ActionResult<ApiResponse<string>> Health()
    {
        return Ok(new ApiResponse<string> { Success = true, Message = "Service is healthy.", Data = "OK" });
    }

    private static QuantityDto MapToDto(QuantityRequest request)
    {
        return new QuantityDto(request.Value, request.Unit, request.MeasurementType);
    }
}
