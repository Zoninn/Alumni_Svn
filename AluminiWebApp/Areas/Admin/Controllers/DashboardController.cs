using Alumini.Core;
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
    public class DashboardController : BaseController
    {
        public DashboardController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult DeleteBanners(int id)
        {

            EventService.DeletBannerImages(id);
            TempData["Message"] = "Deleted successfully";
            return RedirectToAction("BannerImages", "Dashboard", new { area = "Admin", id = 0 });
        }

        [HttpGet]
        public ActionResult BannerImages(int id)
        {
            if (id == 0)
            {
                BannerImages Images = new BannerImages()
                {
                    HomeBannerImages = EventService.HomeBannerImages()
                };
                return View(Images);
            }
            else
            {
                var Image = EventService.EditBannerImages(id);
                BannerImages Images = new BannerImages()
                {
                    Image = Image.Image,
                    Text = Image.Text,
                    Tags = Image.Tages,
                    id = Image.Id,
                    HomeBannerImages = EventService.HomeBannerImages()
                };
                return View(Images);
            }
        }
        [HttpPost]
        public ActionResult BannerImages(BannerImages Bannrs, HttpPostedFileBase Image)
        {

            var FilePath = "";
            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                Image.SaveAs(path);
                FilePath = "/UserProfilePictures/" + fileName;
            }


            if (Bannrs.id != 0)
            {
                Banner_Imags HomeBanners = new Banner_Imags()
                {
                    Text = Bannrs.Text,
                    Image = FilePath,
                    Tages = Bannrs.Tags,
                    CreatedOn = DateTime.Now,
                    Status = true,
                    Id = Bannrs.id

                };
                EventService.UpdateBannerImages(HomeBanners);
                TempData["Message"] = "Updated successfully";
            }
            else
            {
                if (ModelState.IsValid)
                {

                    Banner_Imags HomeBanners = new Banner_Imags()
                    {
                        Text = Bannrs.Text,
                        Image = FilePath,
                        Tages = Bannrs.Tags,
                        CreatedOn = DateTime.Now,
                        Status = true

                    };
                    EventService.BannerImages(HomeBanners);
                    TempData["Message"] = "Saved successfully";
                }
                else
                {
                    return View();
                }
            }

            return RedirectToAction("BannerImages", "Dashboard", new { area = "Admin", id = 0 });
        }

        [HttpGet]
        public ActionResult ExecutiveBoard(int? id)
        {
            if (id == null || id == 0)
            {
                ExecutiveBoard ExecutiveBoardmems = new ExecutiveBoard()
                {
                    ExecutiveBoardDetail = EventService.ExecutiveBoardList()
                };
                return View(ExecutiveBoardmems);
            }
            else
            {
                var Detail = EventService.GetExecutiveBoardDetails(id.Value);
                ExecutiveBoard ExecutiveBoar = new ExecutiveBoard()
                {
                    Email = Detail.Email,
                    Mobile = Detail.Mobile,
                    Role = Detail.ROle,
                    Name = Detail.Name,
                    Image = Detail.Image,
                    id = Detail.id,
                    ExecutiveBoardDetail = EventService.ExecutiveBoardList()
                };
                return View(ExecutiveBoar);
            }


        }
        [HttpGet]
        public ActionResult DeleteExecutiveBoard(int id)
        {
            EventService.DeleteExecutiveBoardDetails(id);
            TempData["Message"] = "Deleted Successfully";
            return RedirectToAction("ExecutiveBoard", "Dashboard", new { area = "Admin" });
        }
        [HttpPost]
        public ActionResult ExecutiveBoard(ExecutiveBoard ExeBoard, HttpPostedFileBase Image)
        {
            var FilePath = "";
            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                Image.SaveAs(path);
                FilePath = "/UserProfilePictures/" + fileName;
            }

            if (ExeBoard.id != 0)
            {
                Executive_board UpdateExecutiveBoardmems = new Executive_board()
                {
                    Email = ExeBoard.Email,
                    CreatedOn = DateTime.Now,
                    Image = FilePath,
                    Mobile = ExeBoard.Mobile,
                    Name = ExeBoard.Name,
                    ROle = ExeBoard.Role,
                    Status = true,
                    id = ExeBoard.id
                };
                EventService.RegisterExe(UpdateExecutiveBoardmems);
                TempData["Message"] = "Update Successfully";
                return RedirectToAction("ExecutiveBoard", "Dashboard", new { area = "Admin", id = 0 });
            }
            else
            {
                Executive_board ExecutiveBoardmems = new Executive_board()
                {
                    Email = ExeBoard.Email,
                    CreatedOn = DateTime.Now,
                    Image = FilePath,
                    Mobile = ExeBoard.Mobile,
                    Name = ExeBoard.Name,
                    ROle = ExeBoard.Role,
                    Status = true,
                };
                EventService.RegisterExe(ExecutiveBoardmems);
                TempData["Message"] = "Save Successfully";
                return RedirectToAction("ExecutiveBoard", "Dashboard", new { area = "Admin" });
            }


        }
    }
}