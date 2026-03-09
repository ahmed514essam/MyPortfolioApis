using CloudinaryDotNet;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioApis.Data;
using MyPortfolioApis.Dtos;
using MyPortfolioApis.Models;

namespace MyPortfolioApis.Services
{
    public class AboutServices
    {
        private readonly AppDbContext context;
            private readonly CloudinaryService cloudinary;
        public AboutServices(AppDbContext context , CloudinaryService cloudinary)
        {
            this.context = context;
            this.cloudinary = cloudinary;
        }


        public async Task<List<About>> GetAboutInfo()
        {
            var aboutInfo = await context.About.Include(a => a.Images).Include(a => a.MyCertificates).ToListAsync();
            if (aboutInfo == null)
            {
               return new List<About>();
            }
            return aboutInfo ;
        }





        public async Task<List<About>> AddAboutInfo(AboutDto dto)
        {
            var aboutInfo = new About
            {
                Name = dto.Name,
                BirthDate = dto.BirthDate,
                Phone = dto.Phone,
                From = dto.From,
                Address = dto.Address,
                Email = dto.Email,
                FacebookLink = dto.FacebookLink,
                LinkedinLink = dto.LinkedinLink,
                GithubLink = dto.GithubLink,
                WhatsLink = dto.WhatsLink,
                InstagramLink = dto.InstagramLink,
                WhoAmI = dto.WhoAmI,
                Images = new List<MyImages>(),
                MyCertificates = new List<MyCertificates>()
            };


            if (dto.MyCertificates != null && dto.MyCertificates.Count > 0)
            {
                foreach (var cer in dto.MyCertificates)
                {
                    aboutInfo.MyCertificates.Add(new MyCertificates
                    {
                        Name = cer.Name,
                        CertificatesLink = cer.CertificatesLink,
                    });
                }
            }


            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);
                    aboutInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                       EntityId = aboutInfo.Id,
                        EntityType = "About"

                    });
                }
            }
            context.About.Add(aboutInfo);
            await context.SaveChangesAsync();
            return await GetAboutInfo();
        }



        public async Task<About> UpdateAboutInfo(AboutDto dto)
        {
            var aboutInfo = await context.About
                .Include(a => a.Images)
                .FirstOrDefaultAsync();

            if (aboutInfo == null)
                return null;

            aboutInfo.Name = dto.Name;
            aboutInfo.BirthDate = dto.BirthDate;
            aboutInfo.Phone = dto.Phone;
            aboutInfo.From = dto.From;
            aboutInfo.Address = dto.Address;
            aboutInfo.Email = dto.Email;
            aboutInfo.FacebookLink = dto.FacebookLink;
            aboutInfo.LinkedinLink = dto.LinkedinLink;
            aboutInfo.GithubLink = dto.GithubLink;
            aboutInfo.WhatsLink = dto.WhatsLink;
            aboutInfo.InstagramLink = dto.InstagramLink;
            aboutInfo.WhoAmI = dto.WhoAmI;

            if (dto.ImageFiles != null && dto.ImageFiles.Count > 0)
            {
                if (aboutInfo.Images != null)
                {
                    foreach (var img in aboutInfo.Images)
                    {
                        await cloudinary.DeleteImage(img.PublicId);
                    }

                    context.MyImages.RemoveRange(aboutInfo.Images);
                }

                aboutInfo.Images = new List<MyImages>();

                foreach (var file in dto.ImageFiles)
                {
                    var upload = await cloudinary.UploadImage(file);

                    aboutInfo.Images.Add(new MyImages
                    {
                        ImageUrl = upload.SecureUrl.ToString(),
                        PublicId = upload.PublicId,
                        EntityId = aboutInfo.Id,
                        EntityType = "About"

                    });
                }
            }

            if (dto.MyCertificates != null && dto.MyCertificates.Count > 0)
            {
                foreach (var cer in dto.MyCertificates)
                {
                    aboutInfo.MyCertificates.Add(new MyCertificates
                    {
                        Name = cer.Name,
                        CertificatesLink = cer.CertificatesLink,
                    });
                }
            }

            await context.SaveChangesAsync();

            return aboutInfo;
        }


        public async Task<bool> DeleteAboutInfo(int id)
        {
            var aboutInfo = await context.About
                .Include(a => a.Images)
                .Include(a => a.MyCertificates)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (aboutInfo == null)
                return false;


            if (aboutInfo.Images != null)
            {
                foreach (var img in aboutInfo.Images)
                {
                    await cloudinary.DeleteImage(img.PublicId);
                }
            }
            if (aboutInfo.MyCertificates != null)
            {
                context.MyCertificates.RemoveRange(aboutInfo.MyCertificates);
            }
            context.About.Remove(aboutInfo);
            await context.SaveChangesAsync();
            return true;


        }


        }
}
