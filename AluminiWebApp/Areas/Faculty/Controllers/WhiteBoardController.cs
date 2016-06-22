using Alumini.Core;
using AluminiService;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Faculty.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class WhiteBoardController : BaseController
    {
        public WhiteBoardController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventCategoryService _eventCategoryservices, IUserPostService _userpostService, IUserPostPicturesService _userPostPictureservice, IUserPostVisibleService _userPostVisibleServices, IGenericMethodsService _genericMethods, ICourseCategoryService _coursecategoryservices, IUserSelectionEventsService _userSelectionServices, IUserselectionBookingsService _userselectionBookingServices, IUserPaymentService _userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(_userDetailsViewService, _userInfoService, _eventCategoryservices, _userpostService, _userPostPictureservice, _userPostVisibleServices, _genericMethods, _coursecategoryservices, _userSelectionServices, _userselectionBookingServices, _userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }

        public ActionResult Index()
        {
            if (Session["UserId"] != null)
            {
                List<Batches> Batches = new List<Batches>();
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                List<YearsforFaculty> Years = new List<YearsforFaculty>();
                if (Userdetails.RoleId == "2")
                {

                    for (int? i = Userdetails.WorkingFrom; i <= Userdetails.WorkingTo; i++)
                    {
                        Years.Add(new YearsforFaculty { Year = Convert.ToString(i) });
                    }
                }
                WhiteBoardModel WhiteBoard = new WhiteBoardModel()
                {
                    Viewdetails = Batches,
                    RoleId = Userdetails.RoleId,
                    Batch = Userdetails.Batch,
                    Stream = Userdetails.CourseName,
                    Events = EventCategoryService.GetCategorys(),
                    yearsList = Years,
                    Coursecategorys = CategoryServices.GetAllCourseCategories(),

                };
                // ViewBag.Userdata = GenericMethods.GetUserdetailsonFaculty(UserId, Convert.ToString(Userdetails.Batch));
                return View(WhiteBoard);

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpPost]
        public ActionResult Index(WhiteBoardModel WhiteBoards, IEnumerable<HttpPostedFileBase> Images, IEnumerable<HttpPostedFileBase> Files)
        {
            if (Session["UserId"] != null)
            {
                List<YearsforFaculty> Years = new List<YearsforFaculty>();
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                List<Batches> Batches = new List<Batches>();
                int PostId = 0;
                if (ModelState.IsValid)
                {

                    if (WhiteBoards.selections == "Visible To All")
                    {
                        UserPost UserPosts = new UserPost()
                        {
                            EventId = WhiteBoards.EventId,
                            ViewBy = WhiteBoards.selections,
                            UserId = UserId,
                            UserMessage = WhiteBoards.Message,
                            Status = true,
                            CreatedOn = DateTime.Now

                        };
                        PostId = UserPostService.InsertUserPosts(UserPosts);

                        UserPosts_Visisble UserPostVisible = new UserPosts_Visisble()
                        {
                            PostId = PostId,
                            Batch = WhiteBoards.selections,
                            Branch = WhiteBoards.Degree,
                            Degreee = Userdetails.CourseId,
                            CreatedOn = DateTime.Now,
                            Status = true,
                            BatchTo = (WhiteBoards.WorkFromTo)



                        };
                        UserPostVisibleServices.Create(UserPostVisible);
                    }
                    else
                    {
                        UserPost UserPosts = new UserPost()
                        {
                            EventId = WhiteBoards.EventId,
                            UserId = UserId,
                            UserMessage = WhiteBoards.Message,
                            ViewBy = WhiteBoards.Batchyear,
                            Status = true,
                            CreatedOn = DateTime.Now

                        };
                        PostId = UserPostService.InsertUserPosts(UserPosts);

                        UserPosts_Visisble UserPostVisible = new UserPosts_Visisble()
                        {
                            PostId = PostId,
                            Batch = WhiteBoards.WorkFromYear,
                            Branch = WhiteBoards.Degree,
                            Degreee = Userdetails.CourseId,

                            CreatedOn = DateTime.Now,
                            Status = true,
                            BatchTo = (WhiteBoards.WorkFromTo)



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
                                var path = Path.Combine(Server.MapPath("~/UserPostingImages/" + fileName));
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
                    ViewBag.Userdata = GenericMethods.GetUserdetailsonFaculty(UserId, Convert.ToString(Userdetails.Batch));
                    return RedirectToAction("Index", "WhiteBoard", new { area = "Faculty" });
                }

                if (Userdetails.RoleId == "2")
                {

                    for (int? i = Userdetails.WorkingFrom; i <= Userdetails.WorkingTo; i++)
                    {
                        Years.Add(new YearsforFaculty { Year = Convert.ToString(i) });
                    }
                }
                WhiteBoardModel WhiteBoard = new WhiteBoardModel()
                {
                    Viewdetails = Batches,
                    RoleId = Userdetails.RoleId,
                    Batch = Userdetails.Batch,
                    Stream = Userdetails.CourseName,
                    Events = EventCategoryService.GetCategorys(),
                    yearsList = Years,
                    Coursecategorys = CategoryServices.GetAllCourseCategories(),

                };
                ViewBag.Userdata = GenericMethods.GetUserdetailsonFaculty(UserId, Convert.ToString(Userdetails.Batch));
                return View(WhiteBoard);

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        public ActionResult EventDetails(int Id)
        {
            if (Session["UserId"] != null)
            {
                return View(GenericMethods.UserPostedFulldetails(Id).FirstOrDefault());
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpPost]
        public JsonResult UserLikesfoPosts(int? PostId)
        {

            UserPost_Likes Likes = new UserPost_Likes()
            {
                PostId = PostId,
                UserId = Convert.ToInt32(Session["UserId"].ToString()),
                CreatedOn = DateTime.Now,
                Status = true,
            };
            var data = GenericMethods.UserPostLikes(Likes);
            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult UserUnLikesfoPosts(int? PostId)
        {

            UserPost_Likes Likes = new UserPost_Likes()
            {
                PostId = PostId,
                UserId = Convert.ToInt32(Session["UserId"].ToString()),
                CreatedOn = DateTime.Now,
                Status = true,
            };
            var data = GenericMethods.UserUnPostPostLikes(Likes);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteComments(int Commentid, int Postid)
        {
            UserPost_Comments UserComments = new UserPost_Comments()
             {
                 PostId = Postid,
             };
            int id = UserPostService.DeleteUserComment(Commentid);
            var data = GenericMethods.UserComments(UserComments);
            return Json(data, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult UserPostComments(int? PostId, string comment)
        {

            UserPost_Comments UserComments = new UserPost_Comments()
            {
                PostId = PostId,
                Comment = comment,
                UserId = Convert.ToInt32(Session["UserId"].ToString()),
                CreatedOn = DateTime.Now,
                Status = true,
            };
            var data = GenericMethods.UserComments(UserComments);
            return Json(data, JsonRequestBehavior.AllowGet);


        }
        public ActionResult DeleteUserPost(int Id)
        {
            if (Session["UserId"] != null)
            {
                UserPostService.Update(Id);
                return RedirectToAction("Index", "WhiteBoard", new { area = "Faculty" });

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
    }
}