using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Services;

namespace MyPortfolioApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectsController : ControllerBase
    {
        private readonly ProjectsServices services;
        public ProjectsController(ProjectsServices services)
        {
            this.services = services;
        }


        [HttpGet]
        public async Task<IActionResult> GetProjectInf()
        {
            var result = await services.GetProjectInfo();
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectInfById([FromRoute] int id)
        {
            var result = await services.GetProjectInfoById(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddAboutInfo([FromForm] MyProjectsDto dto)
        {
            var result = await services.AddProjectInfo(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAboutInfo([FromForm] MyProjectsDto dto , [FromRoute] int id)
        {
            var result = await services.UpdateProjectInfo(dto,id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteProjectInfo([FromRoute] int id)
        {
            var result = await services.DeleteProjectInfo(id);
            if (!result)
                return NotFound();
            return Ok();


        }
        }
}
