using Alumini.Core;
using AluminiRepository;
using AluminiRepository.Interfaces;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService
{
    public class AlbumGalleryService : IAlbumGalleryService
    {
        private readonly Alumini.Logger.ILogger _logger;
        private readonly IAlbumGalleryRepository _courseCategoryRepo;

        public AlbumGalleryService(Alumini.Logger.ILogger _logger, IAlbumGalleryRepository _courseCategoryRepo)
        {
            this._logger = _logger;
            this._courseCategoryRepo = _courseCategoryRepo;
        }

        public Alumini.Core.Album_Gallery Create(Alumini.Core.Album_Gallery obj)
        {
            return _courseCategoryRepo.Create(obj);
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
            return _courseCategoryRepo.Delete(id);
        }
        public Album_Gallery_Images CreateImagesAndVideos(Album_Gallery_Images obj)
        {
            return _courseCategoryRepo.CreateImagesAndVideos(obj);
        }
        public List<UserAlbumGallery> AlbumImages()
        {
            return _courseCategoryRepo.AlbumImages();
        }
    }
}
