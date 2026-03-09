using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis.Data;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Services
{
    public class HomeServices
    {
        private readonly AppDbContext context;
        private readonly CloudinaryService cloudinary;
        public HomeServices(AppDbContext context, CloudinaryService cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
        }


        public async Task<List<Home>> GetHomeInfo()
        {
            var home = await context.Home
                .Include(a => a.Images)
                .ToListAsync();

            return home;
        }




        public async Task<List<Home>> AddHomeInfo(HomeDto dto)
        {
            var homeInfo = new Home
            {
                subTitle = dto.subTitle,
                Summary = dto.Summary,
              
                FacebookLink = dto.FacebookLink,
                LinkedinLink = dto.LinkedinLink,
                GithubLink = dto.GithubLink,
                WhatsLink = dto.WhatsLink,
                InstagramLink = dto.InstagramLink,
                Images = new List<MyImages>()
            };

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);
                    homeInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = homeInfo.Id,
                        EntityType = "Home"
                    });
                }
            }
            context.Home.Add(homeInfo);
            await context.SaveChangesAsync();
            return await GetHomeInfo();
        }



        public async Task<Home> UpdateHomeInfo(HomeDto dto )
        {
            var homeInfo = await context.Home
                .Include(a => a.Images)
                .FirstOrDefaultAsync();

            if (homeInfo == null)
                return null;

            homeInfo.subTitle = dto.subTitle;
            homeInfo.Summary = dto.Summary;
          
            homeInfo.FacebookLink = dto.FacebookLink;
            homeInfo.LinkedinLink = dto.LinkedinLink;
            homeInfo.GithubLink = dto.GithubLink;
            homeInfo.WhatsLink = dto.WhatsLink;
            homeInfo.InstagramLink = dto.InstagramLink;

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                if (homeInfo.Images != null)
                {
                    foreach (var img in homeInfo.Images)
                    {
                        await cloudinary.DeleteImage(img.PublicId);
                    }

                    context.MyImages.RemoveRange(homeInfo.Images);
                }

                homeInfo.Images = new List<MyImages>();

                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);

                    homeInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = homeInfo.Id,
                        EntityType = "Home"
                    });
                }
            }

            await context.SaveChangesAsync();

            return  homeInfo;
        }



    }
}
