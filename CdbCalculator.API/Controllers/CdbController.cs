using CdbCalculator.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CdbCalculator.API.Controllers;

[ApiController]
[Route("api/v1/cdb")]
public class CdbController(ICdbService cdbService) : ControllerBase
{
    private readonly ICdbService _cdbService = cdbService;

    /// <summary>
    /// Calcula o valor bruto e líquido do valor investido no CDB no período de meses informados.
    /// </summary>
    /// <param name="initialValue">O valor inicial investido no CDB.</param>
    /// <param name="months">Meses investidos.</param>
    /// <returns>Um objeto com o rendimento bruto e líquido calculado</returns>
    [HttpGet("calculate")]
    [ProducesResponseType(typeof((decimal GrossYield, decimal NetYield)), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Calculate(decimal initialValue, int months)
    {
        var result = _cdbService.CalculateYield(initialValue, months);

        if (result.IsSuccess)
        {
            return Ok(new
            {
                GrossYield = Math.Round(result.Value.GrossYield, 2, MidpointRounding.ToEven),
                NetYield = Math.Round(result.Value.NetYield, 2, MidpointRounding.ToEven)
            });
        }
        else
            return BadRequest(result.Errors);
    }
}
