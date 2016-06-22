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


namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class MemoriesController : BaseController
    {
        public MemoriesController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService coursecategoryservices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, coursecategoryservices, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }
        [HttpGet]
        public ActionResult memories()
        {
            List<Visible> visibleto = new List<Visible>();
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

        [HttpPost]
        public ActionResult memories(MemoriesModel Model, IEnumerable<HttpPostedFileBase> Image)
        {
            if (Session["UserId"] != null)
            {
                List<Visible> visibleto = new List<Visible>();
                visibleto.Add(new Visible { visibleTo = "To All" });
                MemoriesModel model = new MemoriesModel()
                {
                    Visibleto = visibleto
                };



                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(Session["UserId"]));


                if (Model.memoriesId != 0)
                {

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
                    return RedirectToAction("DisplayImages", "Memories", new { area = "Faculty" });
                }

                else
                {
                    if (ModelState.IsValid)
                    {

                        db_Memories_Info Mories = new db_Memories_Info()
                        {
                            Heading = Model.Heading,
                            CreatedOn = DateTime.Now,
                            Description = Model.Description,
                            MemoryDate = Model.date,
                            Userid = Convert.ToInt64(Session["UserId"].ToString()),
                            Status = true,
                            VisibleTo = Model.visible,
                        };

                        db_Memories_Info InsertMemories = MermoriesServices.Create(Mories);

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

                            TempData["Message"] = "Your memories added successfully...";
                            return RedirectToAction("DisplayImages", "Memories", new { area = "Faculty" });
                        }
                    }
                    return View(model);
                }


            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpGet]
        public ActionResult DisplayImages(int? id)
        {
            if (Session["UserId"] != null)
            {

                MemoriesModel model = new MemoriesModel()
                {
                    Memories = MermoriesServices.GetAllMemoriesforfaculty()
                };
                return View(model);

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
