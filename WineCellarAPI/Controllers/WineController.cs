using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Common.Models;
using Common.Enums;
using Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace WineCellarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WineController : ControllerBase
    {
        public readonly IWineService _wineServices;
        public WineController(IWineService wineServices)
        {
            _wineServices = wineServices;
        }

        [HttpGet]
        [Route("WineStock")]
        public IActionResult GetAvailableWines([FromQuery] Variety? variety)
        {
            if (variety.HasValue)
            {
                return Ok(_wineServices.GetWinesByVariety(variety.Value));
            }

            return Ok(_wineServices.GetAvailableWines());
        }

        [HttpPost]
        [Route("Wine")]
        [Authorize]
        public IActionResult CreateWine([FromBody] CreateWineDTO wineDTO)
        {
            if (wineDTO == null)
            {
                return BadRequest("The request's body is null.");
            }
            try
            {
                _wineServices.CreateWine(wineDTO);
            }
            catch (InvalidOperationException)
            {
                return BadRequest($"A wine with the name {wineDTO.Name.ToUpper()} already exists and can't store duplicates");
            }
            return Created("Location", "Resource");
        }

        [HttpPut]
        [Route("Wine/{id}")]
        [Authorize]
        public IActionResult UpdateWineStock(int id, [FromBody] int stock)
        {
            _wineServices.UpdateWineStockById(id, stock);
            return Ok();
        }
    }
}