using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis.Data;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Services
{
    public class SkillsServices
    {
        private readonly AppDbContext context;
        private readonly CloudinaryService cloudinary;
        public SkillsServices(AppDbContext context, CloudinaryService cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
        }


        public async Task<List<MySkills>> GetSkillInfo()
        {
            var skills = await context.MySkills
                .Include(a => a.Images)
                .ToListAsync();

            return skills;
        }




        public async Task<List<MySkills>> AddSkillInfo(MySkillsDto dto)
        {
            var skillsInfo = new MySkills
            {
                Name = dto.Name,
                Type = dto.Type,



                Images = new List<MyImages>()
            };

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);
                    skillsInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = skillsInfo.Id,
                        EntityType = "Skill"
                    });
                }
            }
            context.MySkills.Add(skillsInfo);
            await context.SaveChangesAsync();
            return await GetSkillInfo();
        }



        public async Task<MySkills> UpdateSkillInfo(MySkillsDto dto, int id)
        {
            var skillsInfo = await context.MySkills
                .Include(a => a.Images)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (skillsInfo == null)
                return null;

            skillsInfo.Name = dto.Name;

            skillsInfo.Name = dto.Name;
            skillsInfo.Type = dto.Type;

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                if (skillsInfo.Images != null)
                {
                    foreach (var img in skillsInfo.Images)
                    {
                        await cloudinary.DeleteImage(img.PublicId);
                    }

                    context.MyImages.RemoveRange(skillsInfo.Images);
                }

                skillsInfo.Images = new List<MyImages>();

                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);

                    skillsInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = skillsInfo.Id,
                        EntityType = "Skill"

                    });
                }
            }

            await context.SaveChangesAsync();

            return skillsInfo;
        }

        public async Task<bool> DeleteSkillInfo(int id)
        {
            var skillsInfo = await context.MySkills
                .Include(a => a.Images)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (skillsInfo == null)
                return false;
            if (skillsInfo.Images != null)
            {
                foreach (var img in skillsInfo.Images)
                {
                    await cloudinary.DeleteImage(img.PublicId);
                }
                context.MyImages.RemoveRange(skillsInfo.Images);
            }
            context.MySkills.Remove(skillsInfo);
            await context.SaveChangesAsync();
            return true;

        }

        }
}
