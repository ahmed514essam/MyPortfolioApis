using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Services;

namespace MyPortfolioApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class HomeInfoController : ControllerBase
    {
        private readonly HomeServices services;
        public HomeInfoController(HomeServices services)
        {
            this.services = services;
        }


        [HttpGet]
        public async Task<IActionResult> GetHomeInfo()
        {
            var result = await services.GetHomeInfo();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddHomeInfo([FromForm] HomeDto dto)
        {
            var result = await services.AddHomeInfo(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateHomeInfo([FromForm] HomeDto dto)
        {
            var result = await services.UpdateHomeInfo(dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

    }
}
