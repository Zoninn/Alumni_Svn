using Alumini.Core;
using AluminiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiService.Interfaces
{
    public interface IAlbumGalleryService : GenericCRUDService<Album_Gallery>
    {
        Album_Gallery_Images CreateImagesAndVideos(Album_Gallery_Images obj);
        List<UserAlbumGallery> AlbumImages();
    }
}
