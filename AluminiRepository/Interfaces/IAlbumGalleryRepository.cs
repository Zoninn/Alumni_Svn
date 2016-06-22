using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IAlbumGalleryRepository : GenericCRUDRepository<Album_Gallery>
    {
        Album_Gallery_Images CreateImagesAndVideos(Album_Gallery_Images obj);
        List<UserAlbumGallery> AlbumImages();
    }
}
