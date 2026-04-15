using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.DTOs;
using QuantityMeasurement.Domain.Exceptions;
using QuantityMeasurement.OperationService.Contracts;

namespace QuantityMeasurement.OperationService.Controllers;

[ApiController]
[Route("api/operations")]
public sealed class OperationsController : ControllerBase
{
    private readonly IQuantityMeasurementService _service;

    public OperationsController(IQuantityMeasurementService service)
    {
        _service = service;
    }

    [HttpPost("compare")]
    public ActionResult<ApiResponse<bool>> Compare([FromBody] CompareRequest request)
    {
        try
        {
            var result = _service.Compare(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity));
            return Ok(new ApiResponse<bool> { Success = true, Message = "Comparison completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<bool> { Success = false, Message = ex.Message, Data = false });
        }
    }

    [HttpPost("convert")]
    public ActionResult<ApiResponse<QuantityDto>> Convert([FromBody] ConvertRequest request)
    {
        try
        {
            var result = _service.Convert(MapToDto(request.SourceQuantity), request.TargetUnit);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Conversion completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("add")]
    public ActionResult<ApiResponse<QuantityDto>> Add([FromBody] ArithmeticRequest request)
    {
        try
        {
            var result = _service.Add(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity), request.TargetUnit);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Addition completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("subtract")]
    public ActionResult<ApiResponse<QuantityDto>> Subtract([FromBody] ArithmeticRequest request)
    {
        try
        {
            var result = _service.Subtract(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity), request.TargetUnit);
            return Ok(new ApiResponse<QuantityDto> { Success = true, Message = "Subtraction completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<QuantityDto> { Success = false, Message = ex.Message });
        }
    }

    [HttpPost("divide")]
    public ActionResult<ApiResponse<double>> Divide([FromBody] ArithmeticRequest request)
    {
        try
        {
            var result = _service.Divide(MapToDto(request.FirstQuantity), MapToDto(request.SecondQuantity));
            return Ok(new ApiResponse<double> { Success = true, Message = "Division completed.", Data = result });
        }
        catch (QuantityMeasurementException ex)
        {
            return BadRequest(new ApiResponse<double> { Success = false, Message = ex.Message });
        }
    }

    [HttpGet("health")]
    public ActionResult<ApiResponse<string>> Health()
    {
        return Ok(new ApiResponse<string> { Success = true, Message = "Service is healthy.", Data = "OK" });
    }

    private static QuantityDto MapToDto(QuantityRequest request)
    {
        return new QuantityDto(request.Value, request.Unit, request.MeasurementType);
    }
}
