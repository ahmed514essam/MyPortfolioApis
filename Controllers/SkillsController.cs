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

    public class SkillsController : ControllerBase
    {
        private readonly SkillsServices services;
        public SkillsController(SkillsServices services)
        {
            this.services = services;
        }


        [HttpGet]
        public async Task<IActionResult> GetSkillsInf()
        {
            var result = await services.GetSkillInfo();
            return Ok(result);
        }




        [HttpPost]
        public async Task<IActionResult> AddSkillsInfo([FromForm] MySkillsDto dto)
        {
            var result = await services.AddSkillInfo(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSkillsInfo([FromForm] MySkillsDto dto, [FromRoute] int id )
        {
            var result = await services.UpdateSkillInfo(dto,id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteSkillsInfo(int id)
        {
            var result = await services.DeleteSkillInfo(id);
            if (!result)
                return NotFound();
            return Ok(new { message = "Skill deleted successfully" });

        }
    }
}
