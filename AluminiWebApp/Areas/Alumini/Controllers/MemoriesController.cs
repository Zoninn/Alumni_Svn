using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class MemoriesController : BaseController
    {
        public MemoriesController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        [HttpGet]
        public ActionResult memories()
        {
            List<Visible> visibleto = new List<Visible>();
            visibleto.Add(new Visible { visibleTo = "To My Batch Mates" });
            visibleto.Add(new Visible { visibleTo = "To All" });
            MemoriesModel model = new MemoriesModel()
            {
                Visibleto = visibleto
            };
            if (Session["UserId"] != null)
            {
                return View(model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        public JsonResult DeleteImagesforMemories(int Memoriesid,int Imageid)
        {
            var Data = MermoriesServices.DeleteMeoryimage(Imageid, Memoriesid);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Conatctus(Alumni_ContactUs Contactus)
        {
            Contactus.CreatedOn = DateTime.Now;
            Contactus.Status = true;
            GenericMethods.ContactUs(Contactus);
            Emails.SendEmails(Contactus.Email, Contactus.Mobile, Contactus.Message, Contactus.Name);
            TempData["Message"] = "Thanks for contact us.we will respond shortly..";
            return RedirectToAction("Conatctus", "Home", new { Area = "Alumni" });
            return View();
        }

        [HttpPost]
        public ActionResult memories(MemoriesModel Model, IEnumerable<HttpPostedFileBase> Image)
        {
            if (Session["UserId"] != null)
            {
                List<Visible> visibleto = new List<Visible>();
                visibleto.Add(new Visible { visibleTo = "To My Batch Mates" });
                visibleto.Add(new Visible { visibleTo = "To All" });
                MemoriesModel model = new MemoriesModel()
                {
                    Visibleto = visibleto
                };

                if (Model.memoriesId != 0)
                {
                    View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(Session["UserId"]));

                    int? VisibleTo = 0;
                    if (Model.visible == "To My Batch Mates")
                    {
                        VisibleTo = Convert.ToInt32(Convert.ToInt32(Userdetails.Batch) - Convert.ToInt32(Userdetails.Years));
                    }
                    db_Memories_Info Mories = new db_Memories_Info()
                    {
                        Heading = Model.Heading,
                        CreatedOn = DateTime.Now,
                        Description = Model.Description,
                        MemoryDate = Model.date,
                        Userid = Convert.ToInt64(Session["UserId"].ToString()),
                        Status = true,
                        VisibleTo = Model.visible,
                        VisibleBatchfrom = VisibleTo,
                        VisibleBatchTo = Userdetails.Batch,
                        MemoriesId = Model.memoriesId
                    };
                    db_Memories_Info InsertMemories = MermoriesServices.UpdateMemories(Mories);

                    if (Image != null)
                    {

                        foreach (var Images in Image)
                        {
                            if (Images != null)
                            {
                                var fileName = Path.GetFileName(Images.FileName);
                                var path = Path.Combine(Server.MapPath("~/UserPostingImages/" + fileName));
                                Images.SaveAs(path);
                                var FilePath = "/UserPostingImages/" + fileName;
                                db_Memories_images images = new db_Memories_images()
                                {
                                    Image = FilePath,
                                    CreatedOn = DateTime.Now,
                                    MemoriesId = InsertMemories.MemoriesId,
                                    Status = true,

                                };
                                MermoriesServices.InsertImages(images);
                            }
                        }


                    }
                    TempData["Message"] = "Memories Updated successfully...";
                    return RedirectToAction("DisplayImages", "Memories", new { area = "Alumini", id = 1 });
                }

                else
                {
                    if (ModelState.IsValid)
                    {


                        View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(Session["UserId"]));

                        int? VisibleTo = 0;
                        if (Model.visible == "To My Batch Mates")
                        {
                            VisibleTo = Convert.ToInt32(Convert.ToInt32(Userdetails.Batch) - Convert.ToInt32(Userdetails.Years));
                        }
                        db_Memories_Info Mories = new db_Memories_Info()
                        {
                            Heading = Model.Heading,
                            CreatedOn = DateTime.Now,
                            Description = Model.Description,
                            MemoryDate = Model.date,
                            Userid = Convert.ToInt64(Session["UserId"].ToString()),
                            Status = true,
                            VisibleTo = Model.visible,
                            VisibleBatchfrom = VisibleTo,
                            VisibleBatchTo = Userdetails.Batch
                        };
                        db_Memories_Info InsertMemories = MermoriesServices.Create(Mories);

                        if (Image != null)
                        {
                            foreach (var Images in Image)
                            {
                                var fileName = Path.GetFileName(Images.FileName);
                                var path = Path.Combine(Server.MapPath("~/UserPostingImages/" + fileName));
                                Images.SaveAs(path);
                                var FilePath = "/UserPostingImages/" + fileName;
                                db_Memories_images images = new db_Memories_images()
                                {
                                    Image = FilePath,
                                    CreatedOn = DateTime.Now,
                                    MemoriesId = InsertMemories.MemoriesId,
                                    Status = true,

                                };
                                MermoriesServices.InsertImages(images);
                            }
                        }
                        TempData["Message"] = "Your memories added successfully...";
                        return RedirectToAction("DisplayImages", "Memories", new { area = "Alumini", id = 1 });
                    }

                }
                return View(model);
            }

            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public ActionResult DisplayImages(int? id)
        {
            if (Session["UserId"] != null)
            {
                if (id == 1)
                {

                    MemoriesModel model = new MemoriesModel()
                    {
                        Memories = MermoriesServices.GetMemories(Convert.ToInt32(Session["UserId"]))
                    };
                    return View(model);
                }
                else if (id == 2)
                {
                    MemoriesModel model = new MemoriesModel()
                    {
                        Memories = MermoriesServices.GetAllMemories(Convert.ToInt32(Session["UserId"]))
                    };
                    return View(model);
                }
                else
                {

                }

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public ActionResult DisplayMemories(int id)
        {
            if (Session["UserId"] != null)
            {

                MemoriesModel model = new MemoriesModel()
                {
                    Memories = MermoriesServices.GetSinglememorys(id)
                };

                return View(model);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public JsonResult GetMemoriesforEdit(int Memoriesid)
        {
            var Data = MermoriesServices.GetSinglememorys(Memoriesid);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteMemories(int id)
        {
            var Memories = MermoriesServices.GetSinglememorys(id);
            var Data = MermoriesServices.Delete(id);
            return Json(Memories, JsonRequestBehavior.AllowGet);

        }

    }
}
