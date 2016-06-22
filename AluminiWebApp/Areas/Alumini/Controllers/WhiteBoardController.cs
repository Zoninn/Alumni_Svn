using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class WhiteBoardController : BaseController
    {
        public WhiteBoardController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventCategoryService _eventCategoryservices, IUserPostService _userpostService, IUserPostPicturesService _userPostPictureservice, IUserPostVisibleService _userPostVisibleServices, IGenericMethodsService _genericMethods, IUserSelectionEventsService _userSelectionServices, IUserselectionBookingsService _userselectionBookingServices, IUserPaymentService _userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(_userDetailsViewService, _userInfoService, _eventCategoryservices, _userpostService, _userPostPictureservice, _userPostVisibleServices, _genericMethods, _userSelectionServices, _userselectionBookingServices, _userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }

        [HttpGet]
        public ActionResult Index(int? Type)
        {
            var defaultPageSize = 5;
            int? page = 1;
            List<Batches> batches = new List<Batches>();
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                switch (Convert.ToInt32(Userdetails.RoleId))
                {
                    case 1:
                        batches.Add(new Batches { Batch = UtilitiesClass.BatchVisibleToAll, BatchName = UtilitiesClass.BatchVisibleToAll });
                        batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = UtilitiesClass.BatchName });
                        break;
                    case 2:
                        batches.Add(new Batches { Batch = UtilitiesClass.BatchVisibleToAll, BatchName = UtilitiesClass.BatchVisibleToAll });
                        batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = UtilitiesClass.BatchName });
                        break;
                }
                if (Userdetails.RoleId == "4")
                {

                }
                WhiteBoardModel WhiteBoard = new WhiteBoardModel()
                {
                    Viewdetails = batches,
                    RoleId = Userdetails.RoleId,
                    Batch = Userdetails.Batch,
                    Stream = Userdetails.CourseName,
                    Events = EventCategoryService.GetCategorys()
                };

                if (Type == 1)
                {
                    if ((Userdetails.RoleId == Convert.ToString(1) && Userdetails.Batch == Userdetails.Batch) || (Userdetails.RoleId == Convert.ToString(1) && Convert.ToString(Userdetails.Batch) == UtilitiesClass.BatchVisibleToAll))
                    {
                        ViewBag.Userdata = GenericMethods.GetUserDataserach(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName, Type);
                    }
                }
                else if (Type == 2)
                {
                    if ((Userdetails.RoleId == Convert.ToString(1) && Userdetails.Batch == Userdetails.Batch) || (Userdetails.RoleId == Convert.ToString(1) && Convert.ToString(Userdetails.Batch) == UtilitiesClass.BatchVisibleToAll))
                    {
                        ViewBag.Userdata = GenericMethods.GetUserDataserach(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName, Type);
                    }
                }
                else
                {
                    if ((Userdetails.RoleId == Convert.ToString(1) && Userdetails.Batch == Userdetails.Batch) || (Userdetails.RoleId == Convert.ToString(1) && Convert.ToString(Userdetails.Batch) == UtilitiesClass.BatchVisibleToAll))
                    {

                        ViewBag.Userdata = GenericMethods.GetUserPostsonId(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName, page, defaultPageSize);

                    }
                }

                return View(WhiteBoard);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        public JsonResult GetPostsonLazyLoad(int? page)
        {
            var defaultPageSize = 5;

            int UserId = Convert.ToInt32(Session["UserId"]);
            View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
            if ((Userdetails.RoleId == Convert.ToString(1) && Userdetails.Batch == Userdetails.Batch) || (Userdetails.RoleId == Convert.ToString(1) && Convert.ToString(Userdetails.Batch) == UtilitiesClass.BatchVisibleToAll))
            {

                var Data = GenericMethods.GetUserPostsonId(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName, page, defaultPageSize);
                return Json(Data, JsonRequestBehavior.AllowGet);

            }
            else
            {
                return null;
            }
        }


        [HttpPost]
        public ActionResult Index(WhiteBoardModel WhiteBoards, IEnumerable<HttpPostedFileBase> Images, IEnumerable<HttpPostedFileBase> Files)
        {
            if (Session["UserId"] != null)
            {
                TempData["DeletedMessage"] = "your post successfully posted";
                int UserId = Convert.ToInt32(Session["UserId"]);

                List<Batches> batches = new List<Batches>();
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                switch (Convert.ToInt32(Userdetails.RoleId))
                {
                    case 1:
                        batches.Add(new Batches { Batch = UtilitiesClass.BatchVisibleToAll, BatchName = UtilitiesClass.BatchVisibleToAll });
                        batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = UtilitiesClass.BatchName });
                        break;
                    case 2:
                        batches.Add(new Batches { Batch = UtilitiesClass.BatchVisibleToAll, BatchName = UtilitiesClass.BatchVisibleToAll });
                        batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = UtilitiesClass.BatchName });
                        break;
                }
                if (Userdetails.RoleId == "4")
                {

                }
                WhiteBoardModel WhiteBoard = new WhiteBoardModel()
                {
                    Viewdetails = batches,
                    RoleId = Userdetails.RoleId,
                    Batch = Userdetails.Batch,
                    Stream = Userdetails.CourseName,
                    Events = EventCategoryService.GetCategorys()
                };

                if (ModelState.IsValid)
                {
                    int? EventId = null;
                    if (WhiteBoards.EventId == null || WhiteBoards.EventId == 0)
                    {
                        EventId = 5;
                    }
                    else
                    {
                        EventId = WhiteBoards.EventId;
                    }
                    UserPost UserPosts = new UserPost()
                    {
                        EventId = EventId,
                        UserId = UserId,
                        UserMessage = WhiteBoards.Message,
                        ViewBy = WhiteBoards.Batchyear,
                        Status = true,
                        CreatedOn = DateTime.Now


                    };
                    int PostId = UserPostService.InsertUserPosts(UserPosts);
                    string Batch = "";
                    Int64 BatchFrom = 0;
                    Int64 BatchTo = 0;
                    if (WhiteBoards.Batchyear == UtilitiesClass.BatchVisibleToAll)
                    {
                        Batch = UtilitiesClass.BatchVisibleToAll;
                    }
                    else
                    {

                        BatchFrom = Convert.ToInt64(WhiteBoards.Batchyear);
                        BatchTo = Convert.ToInt64(WhiteBoards.Batchyear) - (Convert.ToInt64(Userdetails.Years));

                    }
                    if (Batch == UtilitiesClass.BatchVisibleToAll)
                    {
                        UserPosts_Visisble UserPostVisible = new UserPosts_Visisble()
                        {
                            PostId = PostId,
                            Batch = Batch,
                            Branch = Userdetails.CourseCategoryName,
                            Degreee = Userdetails.CourseId,
                            CreatedOn = DateTime.Now,
                            Status = true


                        };
                        UserPostVisibleServices.Create(UserPostVisible);
                    }
                    else
                    {
                        UserPosts_Visisble UserPostVisible = new UserPosts_Visisble()
                        {
                            PostId = PostId,
                            Batch = Convert.ToString(BatchTo),
                            BatchTo = Convert.ToString(BatchFrom),
                            Branch = Userdetails.CourseCategoryName,
                            Degreee = Userdetails.CourseId,
                            CreatedOn = DateTime.Now,
                            Status = true


                        };
                        UserPostVisibleServices.Create(UserPostVisible);
                    }


                    if (Images != null)
                    {
                        foreach (var Pictures in Images)
                        {
                            if (Pictures != null)
                            {
                                var fileName = Path.GetFileName(Pictures.FileName);
                                var path = Path.Combine(Server.MapPath(UtilitiesClass.ImagePath + fileName));
                                Pictures.SaveAs(path);
                                var FilePath = "/UserPostingImages/" + fileName;
                                UserPost_Images UserImages = new UserPost_Images()
                                {
                                    PostId = PostId,
                                    ImagePath = FilePath,
                                    CreatedOn = DateTime.Now,
                                    Status = true
                                };
                                UserpostPictureServices.Create(UserImages);

                            }
                        }
                    }

                    return RedirectToAction(AlumniWhiteBoard.Index, AlumniWhiteBoard.WhiteBoard, new { area = AreasforAlumni.Alumini });
                }
                return View(WhiteBoard);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public ActionResult Display()
        {

            return View();
        }


        public ActionResult EventDetails(int Id)
        {
            if (Session["UserId"] != null)
            {
                return View(GenericMethods.UserPostedFulldetails(Id).FirstOrDefault());
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        public ActionResult DeleteUserPost(int Id)
        {
            if (Session["UserId"] != null)
            {

                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                UserPostService.Update(Id);
                TempData["DeletedMessage"] = UtilitiesClass.DeleteMessage;
                switch (Convert.ToInt32(Userdetails.RoleId))
                {
                    case 1:
                        return RedirectToAction(AlumniWhiteBoard.Index, AlumniWhiteBoard.WhiteBoard, new { area = AreasforAlumni.Alumini });
                        break;
                    case 2:
                        return RedirectToAction(AlumniWhiteBoard.Index, AlumniWhiteBoard.WhiteBoard, new { area = AreasforAlumni.Faculty });
                        break;
                    default:
                        return RedirectToAction(AlumniWhiteBoard.Index, AlumniWhiteBoard.WhiteBoard, new { area = AreasforAlumni.AlumniFaculty });
                        break;
                }

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
    }

}