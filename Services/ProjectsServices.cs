using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis.Data;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Services
{
    public class ProjectsServices
    {
        private readonly AppDbContext context;
        private readonly CloudinaryService cloudinary;
        public ProjectsServices(AppDbContext context, CloudinaryService cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
        }


        public async Task<List<MyProjects>> GetProjectInfo()
        {
            var project = await context.Projects
                .Include(a => a.Images)
                .ToListAsync();

            return project;
        }


        public async Task<MyProjects> GetProjectInfoById(int id)
        {
            var project = await context.Projects
                .Include(a => a.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            return project;
        }



        public async Task<List<MyProjects>> AddProjectInfo(MyProjectsDto dto)
        {
            var projectInfo = new MyProjects
            {
                Name = dto.Name,
                SubTitle = dto.SubTitle,

                Description = dto.Description,
                DemoLink = dto.DemoLink,
                RepoLink = dto.RepoLink,

                Images = new List<MyImages>()
            };

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);
                    projectInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = projectInfo.Id,
                        EntityType = "Projects"
                    });
                }
            }
            context.Projects.Add(projectInfo);
            await context.SaveChangesAsync();
            return await GetProjectInfo();
        }



        public async Task<MyProjects> UpdateProjectInfo(MyProjectsDto dto , int id)
        {
            var projectInfo = await context.Projects
                .Include(a => a.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (projectInfo == null)
                return null;

            projectInfo.Name = dto.Name;
            projectInfo.SubTitle = dto.SubTitle;

            projectInfo.Description = dto.Description;
            projectInfo.DemoLink = dto.DemoLink;
            projectInfo.RepoLink = dto.RepoLink;
        
            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                if (projectInfo.Images != null)
                {
                    foreach (var img in projectInfo.Images)
                    {
                        await cloudinary.DeleteImage(img.PublicId);
                    }

                    context.MyImages.RemoveRange(projectInfo.Images);
                }

                projectInfo.Images = new List<MyImages>();

                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);

                    projectInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = projectInfo.Id,
                        EntityType = "Projects"
                    });
                }
            }

            await context.SaveChangesAsync();

            return projectInfo;
        }





        public async Task<bool> DeleteProjectInfo(int id)
        {
            var projectInfo = await context.Projects
                .Include(a => a.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (projectInfo == null)
                return false;
            if (projectInfo.Images != null)
            {
                foreach (var img in projectInfo.Images)
                {
                    await cloudinary.DeleteImage(img.PublicId);
                }
                context.MyImages.RemoveRange(projectInfo.Images);
            }
            context.Projects.Remove(projectInfo);
            await context.SaveChangesAsync();
            return true;




        }


        }
}
