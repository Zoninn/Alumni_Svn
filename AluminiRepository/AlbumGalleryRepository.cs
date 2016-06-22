using Alumini.Core;
using AluminiRepository.Interfaces;
using AluminiRepository.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class AlbumGalleryRepository : IAlbumGalleryRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public AlbumGalleryRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }


        public Alumini.Core.Album_Gallery Create(Alumini.Core.Album_Gallery obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Album_Gallery.Add(obj);
                context.SaveChanges();
                return obj;
            }
        }

        public Album_Gallery_Images CreateImagesAndVideos(Album_Gallery_Images obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Album_Gallery_Images.Add(obj);
                context.SaveChanges();
                return obj;
            }
        }

        public Alumini.Core.Album_Gallery Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.Album_Gallery Update(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {

                Album_Gallery Images = context.Album_Gallery.Where(x => x.Galleryid == id).FirstOrDefault();
                Images.Status = false;
                context.SaveChanges();
                return true;
            }
        }
        public List<UserAlbumGallery> AlbumImages()
        {
            List<UserAlbumGallery> UserImagesforalbum = new List<UserAlbumGallery>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<UserAlbumGallery> Images = (from a in context.Album_Gallery
                                                 where a.Status == true
                                                 select new UserAlbumGallery { Type = a.AlbumType, galleryid = a.Galleryid, Description = a.Description, AlbumHeading = a.AlbumName, AldumDate = a.GalleryDate }).ToList();

                foreach (var ImageAlbums in Images)
                {
                    List<Album_Gallery_Images> UserAlbumPics = context.Album_Gallery_Images.Where(x => x.AlbumId == ImageAlbums.galleryid && x.Status == true).ToList();

                    UserImagesforalbum.Add(new UserAlbumGallery { Type = ImageAlbums.Type, galleryid = ImageAlbums.galleryid, Description = ImageAlbums.Description, AlbumHeading = ImageAlbums.AlbumHeading, AldumDate = ImageAlbums.AldumDate, AlbumImages = UserAlbumPics });
                }

                return UserImagesforalbum;
            }

        }


    }

    public class UserAlbumGallery
    {
        public int galleryid { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTime? AldumDate { get; set; }
        public List<Album_Gallery_Images> AlbumImages { get; set; }
        public string AlbumHeading { get; set; }
    }
    public class UserImagesImages
    {
        public int ImageId { get; set; }
        public string Url { get; set; }
    }
}
