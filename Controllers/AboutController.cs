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

    public class AboutController : ControllerBase
    {
        private readonly AboutServices services;
        public AboutController(AboutServices services)
        {
            this.services = services;
        }


        [HttpGet]
        public async Task<IActionResult> GetAboutInf()
        {
            var result = await services.GetAboutInfo();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddAboutInfo([FromForm] AboutDto dto)
        {
            var result = await services.AddAboutInfo(dto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAboutInfo( [FromForm] AboutDto dto)
        {
            var result = await services.UpdateAboutInfo( dto);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}
