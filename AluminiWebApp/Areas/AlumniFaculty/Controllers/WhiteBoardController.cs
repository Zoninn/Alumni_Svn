using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AluminiWebApp.Areas.AlumniFaculty.Models;
using Alumini.Core;
using AluminiService.Interfaces;
using System.IO;

namespace AluminiWebApp.Areas.AlumniFaculty.Controllers
{
    public class WhiteBoardController : BaseController
    {
        public WhiteBoardController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService CategoryServices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, CategoryServices, userSelectionServices, userselectionBookingServices, userPaymentservice)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var defaultPageSize = 5;
            int? Page = 1;
            List<Batches> Batches = new List<Batches>();
            List<YearsforFaculty> Years = new List<YearsforFaculty>();
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));

                if (Userdetails.RoleId == "4")
                {
                    Batches.Add(new Batches { Batch = "Visible To All", BatchName = "Visible To All" });
                    Batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = "My BatchMates" });

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
                ViewBag.Userdata = GenericMethods.AlumniAdnFacultyData(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName);
                return View(WhiteBoard);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpPost]
        public ActionResult Index(WhiteBoardModel WhiteBoards, IEnumerable<HttpPostedFileBase> Images, IEnumerable<HttpPostedFileBase> Files)
        {
            var defaultPageSize = 5;
            int? Page = 1;
            List<Batches> Batches = new List<Batches>();
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                if (Userdetails.RoleId == "4")
                {
                    Batches.Add(new Batches { Batch = "Visible To All", BatchName = "Visible To All" });
                    Batches.Add(new Batches { Batch = Convert.ToString(Userdetails.Batch), BatchName = "My BatchMates" });
                }
                WhiteBoardModel WhiteBoard = new WhiteBoardModel()
                    {
                        Viewdetails = Batches,
                        RoleId = Userdetails.RoleId,
                        Batch = Userdetails.Batch,
                        Stream = Userdetails.CourseName,
                        Events = EventCategoryService.GetCategorys()
                    };


                if (ModelState.IsValid)
                {
                    int? EventId = null;
                    if (WhiteBoard.EventId == null || WhiteBoard.EventId == 0)
                    {
                        EventId = 5;
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
                    if (WhiteBoards.Batchyear == "Visible To All")
                    {
                        Batch = "Visible To All";
                    }
                    else
                    {

                        BatchFrom = Convert.ToInt64(WhiteBoards.Batchyear);
                        BatchTo = Convert.ToInt64(WhiteBoards.Batchyear) - (Convert.ToInt64(Userdetails.Years));

                    }
                    if (Batch == "Visible To All")
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
                    ViewBag.Userdata = GenericMethods.GetUserPostsonId(UserId, Convert.ToString(Userdetails.Batch), Convert.ToInt32(Userdetails.Years), Userdetails.CourseCategoryName, Page, defaultPageSize);
                    return RedirectToAction("Index", "WhiteBoard", new { area = "AlumniFaculty" });


                }
                return View(WhiteBoard);

            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpPost]
        public ActionResult PostasFaculty(WhiteBoardModel WhiteBoards, IEnumerable<HttpPostedFileBase> Images, IEnumerable<HttpPostedFileBase> Files)
        {
            int PostId = 0;
            if (Session["UserId"] != null)
            {
                int UserId = Convert.ToInt32(Session["UserId"]);
                View_UserDetails Userdetails = UserDetailsViewService.GetUserByUserId(Convert.ToInt32(UserId));
                if (WhiteBoards.selections == "Visible To All")
                {
                    UserPost UserPosts = new UserPost()
                    {
                        EventId = WhiteBoards.EventId,
                        UserId = UserId,
                        UserMessage = WhiteBoards.Message,
                        ViewBy = WhiteBoards.selections,
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
                return RedirectToAction("Index", "WhiteBoard", new { area = "AlumniFaculty" });
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
    }
}