using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{

    public class GalleryController : BaseController
    {
        public GalleryController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }

        [HttpGet]
        public ActionResult Gallery()
        {
            GalleryModel Model = new GalleryModel()
            {
                Albums = AlbumGalleryService.AlbumImages()
            };

            return View(Model);
        }
        [HttpGet]
        public ActionResult Images(int id)
        {
            return View();
        }
        public ActionResult DeleteAlbum(int id)
        {

            AlbumGalleryService.Delete(id);
            TempData["Message"] = "Album Deleted successfully..";
            return RedirectToAction("Gallery", "Gallery", new { area = "Admin" });
        }

        [HttpPost]
        public ActionResult Gallery(GalleryModel Gallery, IEnumerable<HttpPostedFileBase> Images, HttpPostedFileBase Videos)
        {
            if (ModelState.IsValid)
            {
                if (Gallery.selectionType == 2)
                {


                    var fileName = Path.GetFileName(Videos.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    Videos.SaveAs(path);
                    var FilePath = "/UserProfilePictures/" + fileName;
                    Album_Gallery Album = new Album_Gallery()
                    {
                        CreateOn = DateTime.Now,
                        AlbumName = Gallery.Title,
                        GalleryDate = Gallery.GalleryDate,
                        Description = Gallery.Description,
                        AlbumType = "Videos",
                        Status = true
                    };
                    Album_Gallery gallery = AlbumGalleryService.Create(Album);
                    Album_Gallery_Images ImagesGallery = new Album_Gallery_Images()
                    {
                        Image = FilePath,
                        AlbumId = gallery.Galleryid,
                        CreatedOn = DateTime.Now,
                        AlbumType = "Videos",
                        Status = true
                    };

                    AlbumGalleryService.CreateImagesAndVideos(ImagesGallery);

                }
                else if (Gallery.selectionType == 1)
                {

                    Album_Gallery Album = new Album_Gallery()
                    {
                        CreateOn = DateTime.Now,
                        AlbumName = Gallery.Title,
                        GalleryDate = Gallery.GalleryDate,
                        Description = Gallery.Description,
                        AlbumType = "Images",
                        Status = true
                    };
                    Album_Gallery gallery = AlbumGalleryService.Create(Album);

                    foreach (var GalleryImages in Images)
                    {
                        var fileName = Path.GetFileName(GalleryImages.FileName);
                        var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                        GalleryImages.SaveAs(path);
                        var FilePath = "/UserProfilePictures/" + fileName;

                        Album_Gallery_Images ImagesGallery = new Album_Gallery_Images()
                        {
                            Image = FilePath,
                            AlbumId = gallery.Galleryid,
                            CreatedOn = DateTime.Now,
                            AlbumType = "Images",
                            Status = true
                        };

                        AlbumGalleryService.CreateImagesAndVideos(ImagesGallery);

                    }
                }
                TempData["Message"] = "Album Saved successfully..";
                return RedirectToAction("Gallery", "Gallery", new { area = "Admin" });
            }
            return View();
        }

    }
}