using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alumini.Core;
using AluminiRepository.Interfaces;
using NLog;
using AluminiRepository.Factories;

namespace AluminiRepository
{
    public class GenericMethods : IGenericMethodsRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        #region Contsructor
        public GenericMethods(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }
        #endregion EndConstructor

        #region Methods
        public IEnumerable<Country> GetAllCountries()
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    return context.Countries.ToList();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        public IEnumerable<EventsId> GetAllEventsonserach(string EVents)
        {
            List<EventsId> Events = new List<EventsId>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    Events = (from a in context.Def_Events
                              where a.EventName.Contains(EVents) && a.Status == true
                              select new EventsId { EventId = a.EventId, EventName = a.EventName }).ToList();
                    return Events;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<CourseCategory> GetAllCourseCategories()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<CourseCategory> Coursescategorysdata = context.CourseCategories.Where(x => x.Status == true).ToList();
                return Coursescategorysdata;

            }
        }



        public Custome_Templates GetTmplatesonid(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                Custome_Templates Template = context.Custome_Templates.Where(x => x.id == id && x.Status == true).FirstOrDefault();
                return Template;
            }
        }

        public Custome_Templates InsertTemplates(Custome_Templates Template)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                var Image = "";
                if (Template.id == null)
                {
                    context.Custome_Templates.Add(Template);
                    context.SaveChanges();
                }
                else
                {
                    Custome_Templates Templates = context.Custome_Templates.Where(x => x.id == Template.id && x.Status == true).FirstOrDefault();
                    Templates.Heading = Template.Heading;
                    Templates.Subject = Template.Subject;
                    Templates.Description = Template.Description;
                    Templates.UpdateOn = DateTime.Now;
                    Templates.Status = true;
                    if (Template.Images != "")
                    {
                        Templates.Images = Template.Images;
                    }
                    context.SaveChanges();

                }
                return Template;
            }
        }

        public List<Custome_Templates> GetTemplates()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {

                List<Custome_Templates> GetData = context.Custome_Templates.Where(x => x.Status == true).ToList();
                return GetData;
            }
        }
        public Alumni_ContactUs ContactUs(Alumni_ContactUs ContactUs)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Alumni_ContactUs.Add(ContactUs);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
            return ContactUs;
        }


        public ActivitiesCounts GetActivities()
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    ActivitiesCounts Activities = new ActivitiesCounts()
                    {
                        Events = context.Def_Events.Where(x => x.Status == true).Count(),
                        Members = context.View_UserDetails.Where(x => x.UserStatus == 1).Count(),
                        Jobs = context.UserPosting_Jobs.Where(x => x.Status == 1).Count(),
                        News = context.db_NewsRooms.Where(x => x.Status == true).Count()
                    };
                    return Activities;
                }

            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }




        public List<Alumni_Gallery> UserGallery(int? id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                string Album = "";
                if (id == 1)
                {
                    Album = "Images";
                }
                else
                {
                    Album = "videos";
                }
                List<Alumni_Gallery> GalleryImages = new List<Alumni_Gallery>();
                List<Album_Gallery> Gallery = context.Album_Gallery.Where(x => x.Status == true && x.AlbumType == Album).ToList();
                foreach (var details in Gallery)
                {
                    int count = context.Album_Gallery_Images.Where(x => x.AlbumId == details.Galleryid).Count();
                    string Images = "";
                    if (count != 0)
                    {
                        Images = context.Album_Gallery_Images.First(x => x.AlbumId == details.Galleryid).Image;
                    }
                    else
                    {
                        Images = "";
                    }
                    GalleryImages.Add(new Alumni_Gallery { AlbumId = details.Galleryid, Image = Images, AlbumName = details.AlbumName, AlbumType = details.AlbumType });
                }
                return GalleryImages;
            }

        }


        public IEnumerable<UserPostsData> GetUserDataserach(int UserId, string Batch, int UserYears, string Course, int? Type)
        {
            List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                if (Type == 1)
                {
                    List<UserdetailsDTO> UserData = (from a in context.UserDetails
                                                     join b in context.UserPosts
                                                     on a.Id equals b.UserId
                                                     join c in context.EventCategorys
                                                     on b.EventId equals c.Id
                                                     join e in context.UserPosts_Visisble
                                                     on b.PostId equals e.PostId
                                                     where (((a.Id != UserId) && ((e.Batch == "Visible To All")) && b.Status == true))
                                                     orderby b.PostId descending
                                                     select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    foreach (var User in UserData)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                        List<string> PostImagesforusers = new List<string>();

                        List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                join b in context.UserPost_Comments
                                                                on a.Id equals b.UserId
                                                                where b.PostId == User.PostId && b.Status == true
                                                                orderby b.CommentId descending
                                                                select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                        List<UserComments> UserPostComments = new List<UserComments>();
                        int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                        foreach (var UserPostCooments in UserComments)
                        {
                            UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                        }

                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }

                        int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                        int? COunt = null;
                        if (UserLikeCOunt != 0)
                        {
                            COunt = 1;
                        }

                        UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                    }


                }
                else if (Type == 2)
                {
                    Int64 BatchFrom = (Convert.ToInt64(Batch) - Convert.ToInt64(UserYears));
                    Int64 BatchTo = Convert.ToInt64(Batch);
                    string BatchF = Convert.ToString(BatchFrom);
                    string BatchT = Convert.ToString(BatchTo);
                    List<UserPosts_Visisble> YearsCount = context.UserPosts_Visisble.Where(x => x.Batch == BatchF && x.BatchTo == BatchT).ToList();
                    foreach (var Years in YearsCount)
                    {


                        List<UserdetailsDTO> UserPostDetails = (from a in context.UserDetails
                                                                join b in context.UserPosts
                                                                on a.Id equals b.UserId
                                                                join c in context.EventCategorys
                                                                on b.EventId equals c.Id
                                                                join e in context.UserPosts_Visisble
                                                                on b.PostId equals e.PostId
                                                                where (Years.PostId == b.PostId && b.Status == true) && (e.Branch == Course)
                                                                orderby b.PostId descending
                                                                select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name, }).ToList();

                        foreach (var User in UserPostDetails)
                        {

                            List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                            List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                            int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                            int? COunt = null;
                            if (UserLikeCOunt != 0)
                            {
                                COunt = 1;
                            }
                            List<string> PostImagesforusers = new List<string>();

                            List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                    join b in context.UserPost_Comments
                                                                    on a.Id equals b.UserId
                                                                    where b.PostId == User.PostId && b.Status == true
                                                                    orderby b.CommentId descending
                                                                    select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                            int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                            List<UserComments> UserPostComments = new List<UserComments>();
                            foreach (var UserPostCooments in UserComments)
                            {
                                UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                            }

                            foreach (var PostingImages in PostImages)
                            {
                                PostImagesforusers.Add(PostingImages.ImagePath);
                            }
                            UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });

                        }
                    }
                }
                return UserPostsDisplay.OrderByDescending(i => i.PostId); ;
            }

        }

        public IEnumerable<UserPostsData> GetUserPostsonId(int UserId, string Batch, int UserYears, string Course, int? page, int Pagesize)
        {
            try
            {
                var PageIndex = page - 1 ?? 0;

                List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;

                    List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                      join b in context.UserPosts
                                                      on a.Id equals b.UserId
                                                      join c in context.EventCategorys
                                                      on b.EventId equals c.Id
                                                      join d in context.EducationalDetails
                                                      on a.Id equals d.UserId
                                                      orderby b.PostId descending
                                                      where (a.Id == UserId) && b.Status == true
                                                      select new UserdetailsDTO { Batch = b.ViewBy, PostDate = b.CreatedOn, UserId = b.UserId, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).OrderByDescending(x => x.PostId).Skip(PageIndex * Pagesize).Take(Pagesize).ToList();
                    List<UserdetailsDTO> UserData = (from a in context.UserDetails
                                                     join b in context.UserPosts
                                                     on a.Id equals b.UserId
                                                     join c in context.EventCategorys
                                                     on b.EventId equals c.Id
                                                     join e in context.UserPosts_Visisble
                                                     on b.PostId equals e.PostId
                                                     where (((a.Id != UserId) && ((e.Batch == "Visible To All")) && b.Status == true))
                                                     orderby b.PostId descending
                                                     select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).OrderByDescending(x => x.PostId).Skip(PageIndex * Pagesize).Take(Pagesize).ToList();


                    List<UserPosts_Visisble> UserVisibleYears = context.UserPosts_Visisble.Where(x => x.Degreee == null && x.Batch != "Visible To All").ToList();

                    foreach (var UsersYears in UserVisibleYears)
                    {
                        for (int i = Convert.ToInt32(UsersYears.Batch); i <= Convert.ToInt32(UsersYears.BatchTo); i++)
                        {
                            if (i == Convert.ToInt32(Batch))
                            {
                                List<UserdetailsDTO> UserYearPostDetails = (from a in context.UserDetails
                                                                            join b in context.UserPosts
                                                                            on a.Id equals b.UserId
                                                                            join c in context.EventCategorys
                                                                            on b.EventId equals c.Id
                                                                            join e in context.UserPosts_Visisble
                                                                            on b.PostId equals e.PostId
                                                                            where (b.PostId == UsersYears.PostId && b.Status == true && e.Branch == Course)
                                                                            select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                                foreach (var User in UserYearPostDetails)
                                {

                                    List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                                    List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                                    int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                                    int? COunt = null;
                                    if (UserLikeCOunt != 0)
                                    {
                                        COunt = 1;
                                    }
                                    List<string> PostImagesforusers = new List<string>();

                                    List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                            join b in context.UserPost_Comments
                                                                            on a.Id equals b.UserId
                                                                            where b.PostId == User.PostId && b.Status == true
                                                                            orderby b.CommentId descending
                                                                            select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                                    int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                                    List<UserComments> UserPostComments = new List<UserComments>();
                                    foreach (var UserPostCooments in UserComments)
                                    {
                                        UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                                    }

                                    foreach (var PostingImages in PostImages)
                                    {
                                        PostImagesforusers.Add(PostingImages.ImagePath);
                                    }
                                    UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });

                                }
                            }
                        }

                    }
                    Int64 BatchFrom = (Convert.ToInt64(Batch) - Convert.ToInt64(UserYears));
                    Int64 BatchTo = Convert.ToInt64(Batch);
                    string BatchF = Convert.ToString(BatchFrom);
                    string BatchT = Convert.ToString(BatchTo);
                    List<UserPosts_Visisble> YearsCount = context.UserPosts_Visisble.Where(x => x.Batch == BatchF && x.BatchTo == BatchT).ToList();
                    foreach (var Years in YearsCount)
                    {


                        List<UserdetailsDTO> UserPostDetails = (from a in context.UserDetails
                                                                join b in context.UserPosts
                                                                on a.Id equals b.UserId
                                                                join c in context.EventCategorys
                                                                on b.EventId equals c.Id
                                                                join e in context.UserPosts_Visisble
                                                                on b.PostId equals e.PostId
                                                                where (b.UserId != UserId) && (Years.PostId == b.PostId && b.Status == true) && (e.Branch == Course)
                                                                orderby b.PostId descending
                                                                select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name, }).OrderByDescending(x => x.PostId).Skip(PageIndex * Pagesize).Take(Pagesize).ToList();

                        foreach (var User in UserPostDetails)
                        {

                            List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                            List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                            int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                            int? COunt = null;
                            if (UserLikeCOunt != 0)
                            {
                                COunt = 1;
                            }
                            List<string> PostImagesforusers = new List<string>();

                            List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                    join b in context.UserPost_Comments
                                                                    on a.Id equals b.UserId
                                                                    where b.PostId == User.PostId && b.Status == true
                                                                    orderby b.CommentId descending
                                                                    select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                            int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                            List<UserComments> UserPostComments = new List<UserComments>();
                            foreach (var UserPostCooments in UserComments)
                            {
                                UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                            }

                            foreach (var PostingImages in PostImages)
                            {
                                PostImagesforusers.Add(PostingImages.ImagePath);
                            }
                            UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });

                        }
                    }
                    foreach (var User in UserData)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                        List<string> PostImagesforusers = new List<string>();

                        List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                join b in context.UserPost_Comments
                                                                on a.Id equals b.UserId
                                                                where b.PostId == User.PostId && b.Status == true
                                                                orderby b.CommentId descending
                                                                select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                        List<UserComments> UserPostComments = new List<UserComments>();
                        int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                        foreach (var UserPostCooments in UserComments)
                        {
                            UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                        }

                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }

                        int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                        int? COunt = null;
                        if (UserLikeCOunt != 0)
                        {
                            COunt = 1;
                        }

                        UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                    }

                    foreach (var User in UserPosts)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                join b in context.UserPost_Comments
                                                                on a.Id equals b.UserId
                                                                where b.PostId == User.PostId && b.Status == true
                                                                orderby b.CommentId descending
                                                                select new UserPostsComments { CommentId = b.CommentId, CommentedByFirstName = a.FirstName, CommentedByLastName = a.LastName, PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                        List<UserComments> UserPostComments = new List<UserComments>();
                        int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId && x.Status == true).Count();
                        foreach (var UserPostCooments in UserComments)
                        {
                            UserPostComments.Add(new UserComments { CommentId = UserPostCooments.CommentId, CommentedByFirstName = UserPostCooments.CommentedByFirstName, CommentedByLastName = UserPostCooments.CommentedByLastName, ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                        }

                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }

                        int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                        int? COunt = null;
                        if (UserLikeCOunt != 0)
                        {
                            COunt = 1;
                        }


                        UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserId = User.UserId, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                    }

                    return UserPostsDisplay.OrderByDescending(x => x.PostId).ToList();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public IEnumerable<UserPostsData> GetUserdetailsonFaculty(int UserId, string Batch)
        {
            try
            {
                List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                      join b in context.UserPosts
                                                      on a.Id equals b.UserId
                                                      join c in context.EventCategorys
                                                      on b.EventId equals c.Id
                                                      join d in context.FacultyWorkInfoes
                                                      on a.Id equals d.FacultyUserId
                                                      where (a.Id == UserId) && b.Status == true
                                                      orderby b.PostId descending
                                                      select new UserdetailsDTO { Batch = b.ViewBy, PostDate = b.CreatedOn, UserId = b.UserId, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    List<UserdetailsDTO> UserData = (from a in context.UserDetails
                                                     join b in context.UserPosts
                                                     on a.Id equals b.UserId
                                                     join c in context.EventCategorys
                                                     on b.EventId equals c.Id
                                                     join e in context.UserPosts_Visisble
                                                     on b.PostId equals e.PostId
                                                     where ((a.Id != UserId) && (e.Batch == Batch)) || ((a.Id != UserId) && (e.Batch == "Visible To All")) && b.Status == true
                                                     select new UserdetailsDTO { Batch = e.Batch, PostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    foreach (var User in UserData)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                        List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                join b in context.UserPost_Comments
                                                                on a.Id equals b.UserId
                                                                where b.PostId == User.PostId && b.Status == true
                                                                select new UserPostsComments { PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        List<int> UserPostsLikes = new List<int>();
                        List<UserComments> UserPostComments = new List<UserComments>();
                        int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId).Count();
                        foreach (var UserPostCooments in UserComments)
                        {
                            UserPostComments.Add(new UserComments { ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                        }

                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }
                        int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                        int? COunt = null;
                        if (UserLikeCOunt != 0)
                        {
                            COunt = 1;
                        }


                        UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                    }

                    foreach (var User in UserPosts)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        List<int> UserPostsLikes = new List<int>();
                        List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                                join b in context.UserPost_Comments
                                                                on a.Id equals b.UserId
                                                                where b.PostId == User.PostId && b.Status == true
                                                                select new UserPostsComments { PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                        List<UserComments> UserPostComments = new List<UserComments>();
                        int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId).Count();
                        foreach (var UserPostCooments in UserComments)
                        {
                            UserPostComments.Add(new UserComments { ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                        }

                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }
                        int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                        int? COunt = null;
                        if (UserLikeCOunt != 0)
                        {
                            COunt = 1;
                        }
                        UserPostsDisplay.Add(new UserPostsData { BatchVisible = User.Batch, UserPostDate = User.PostDate, UserLikecheck = COunt, UserId = User.UserId, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                    }
                    return UserPostsDisplay.OrderByDescending(i => i.PostId);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<UserPostsData> AlumniAdnFacultyData(int UserId, string Batch, int UserYears, string Course)
        {
            List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.Configuration.ProxyCreationEnabled = false;
                List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                  join b in context.UserPosts
                                                  on a.Id equals b.UserId
                                                  join c in context.EventCategorys
                                                  on b.EventId equals c.Id
                                                  join d in context.FacultyWorkInfoes
                                                  on a.Id equals d.FacultyUserId
                                                  where (a.Id == UserId) && b.Status == true
                                                  orderby b.PostId descending
                                                  select new UserdetailsDTO { UserId = b.UserId, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                List<UserdetailsDTO> UserData = (from a in context.UserDetails
                                                 join b in context.UserPosts
                                                 on a.Id equals b.UserId
                                                 join c in context.EventCategorys
                                                 on b.EventId equals c.Id
                                                 join e in context.UserPosts_Visisble
                                                 on b.PostId equals e.PostId
                                                 where ((a.Id != UserId) && (e.Batch == Batch)) || ((a.Id != UserId) && (e.Batch == "Visible To All")) && b.Status == true
                                                 select new UserdetailsDTO { UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                foreach (var User in UserData)
                {

                    List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                    List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                    List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                            join b in context.UserPost_Comments
                                                            on a.Id equals b.UserId
                                                            where b.PostId == User.PostId && b.Status == true
                                                            select new UserPostsComments { PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                    List<string> PostImagesforusers = new List<string>();
                    List<int> UserPostsLikes = new List<int>();
                    List<UserComments> UserPostComments = new List<UserComments>();
                    int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId).Count();
                    foreach (var UserPostCooments in UserComments)
                    {
                        UserPostComments.Add(new UserComments { ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                    }

                    foreach (var PostingImages in PostImages)
                    {
                        PostImagesforusers.Add(PostingImages.ImagePath);
                    }
                    int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                    int? COunt = null;
                    if (UserLikeCOunt != 0)
                    {
                        COunt = 1;
                    }


                    UserPostsDisplay.Add(new UserPostsData { UserLikecheck = COunt, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                }

                foreach (var User in UserPosts)
                {

                    List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                    List<UserPost_Likes> UserPostLikes = context.UserPost_Likes.Where(x => x.PostId == User.PostId && x.Status == true).ToList();
                    List<string> PostImagesforusers = new List<string>();
                    List<int> UserPostsLikes = new List<int>();
                    List<UserPostsComments> UserComments = (from a in context.UserDetails
                                                            join b in context.UserPost_Comments
                                                            on a.Id equals b.UserId
                                                            where b.PostId == User.PostId && b.Status == true
                                                            select new UserPostsComments { PostId = b.PostId, Comment = b.Comment, UserImage = a.ProfilePicture }).ToList();
                    List<UserComments> UserPostComments = new List<UserComments>();
                    int CommentsCount = context.UserPost_Comments.Where(x => x.PostId == User.PostId).Count();
                    foreach (var UserPostCooments in UserComments)
                    {
                        UserPostComments.Add(new UserComments { ProfilePic = UserPostCooments.UserImage, Comment = UserPostCooments.Comment });
                    }

                    foreach (var PostingImages in PostImages)
                    {
                        PostImagesforusers.Add(PostingImages.ImagePath);
                    }
                    int UserLikeCOunt = context.UserPost_Likes.Where(x => x.UserId == UserId && x.Status == true && x.PostId == User.PostId).Count();
                    int? COunt = null;
                    if (UserLikeCOunt != 0)
                    {
                        COunt = 1;
                    }
                    UserPostsDisplay.Add(new UserPostsData { UserLikecheck = COunt, UserId = User.UserId, UserImage = User.UserImage, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName, UserPostsCount = UserPostLikes.Count(), UserComments = UserPostComments, UserCommentCount = CommentsCount });
                }
            }
            return UserPostsDisplay.OrderByDescending(i => i.PostId);
        }


        public IEnumerable<UserPostsData> GetUsersonBatches(int UserId, string Batch)
        {
            try
            {
                List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                      join b in context.UserPosts
                                                      on a.Id equals b.UserId
                                                      join c in context.EventCategorys
                                                      on b.EventId equals c.Id
                                                      join d in context.EducationalDetails
                                                      on a.Id equals d.UserId
                                                      join e in context.UserPosts_Visisble
                                                      on b.PostId equals e.PostId
                                                      where (a.Id != UserId) && (e.Batch == Batch) && b.Status == true
                                                      select new UserdetailsDTO { PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    foreach (var User in UserPosts)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }
                        UserPostsDisplay.Add(new UserPostsData { UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName });
                    }
                    return UserPostsDisplay;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public IEnumerable<UserPostsData> UserPostedFulldetails(int PostId)
        {
            try
            {
                List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                      join b in context.UserPosts
                                                      on a.Id equals b.UserId
                                                      join c in context.EventCategorys
                                                      on b.EventId equals c.Id
                                                      join e in context.UserPosts_Visisble
                                                      on b.PostId equals e.PostId
                                                      where (b.PostId == PostId && b.Status == true)
                                                      select new UserdetailsDTO { PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    foreach (var User in UserPosts)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }
                        UserPostsDisplay.Add(new UserPostsData { UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName });
                    }
                    return UserPostsDisplay;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }




        }
        public IEnumerable<UserPostsData> UserPostedFulldetailsforAdminDashBorad()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<UserPostsData> UserPosts = (from a in context.UserDetails
                                                 join b in context.UserPosts
                                                 on a.Id equals b.UserId
                                                 where (b.Status == true)
                                                 select new UserPostsData { UserPostDate = b.CreatedOn, UserImage = a.ProfilePicture, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage }).ToList();
                return UserPosts.OrderByDescending(x => x.PostId);
            }
        }

        public IEnumerable<UserPostsData> UserPostedFulldetailsforAdmin()
        {
            try
            {
                List<UserPostsData> UserPostsDisplay = new List<UserPostsData>();
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<UserdetailsDTO> UserPosts = (from a in context.UserDetails
                                                      join b in context.UserPosts
                                                      on a.Id equals b.UserId
                                                      join c in context.EventCategorys
                                                      on b.EventId equals c.Id
                                                      join e in context.UserPosts_Visisble
                                                      on b.PostId equals e.PostId
                                                      where (b.Status == true)
                                                      select new UserdetailsDTO { Branch = e.Branch, BatchFrom = e.Batch, BatchTo = e.BatchTo, PostId = b.PostId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.UserMessage, EventName = c.Name }).ToList();

                    foreach (var User in UserPosts)
                    {

                        List<UserPost_Images> PostImages = context.UserPost_Images.Where(x => x.PostId == User.PostId).ToList();
                        List<string> PostImagesforusers = new List<string>();
                        foreach (var PostingImages in PostImages)
                        {
                            PostImagesforusers.Add(PostingImages.ImagePath);
                        }
                        UserPostsDisplay.Add(new UserPostsData { Branch = User.Branch, BatchFrom = User.BatchFrom, BatchTo = User.BatchTo, UserPostMessage = User.UserMessage, PostId = User.PostId, imageUrls = PostImagesforusers, UserName = User.UserName, LastName = User.LastName, EventName = User.EventName });
                    }
                    return UserPostsDisplay.OrderByDescending(x => x.PostId);
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }




        }

        public int UserPostLikes(UserPost_Likes Obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.UserPost_Likes.Add(Obj);
                    context.SaveChanges();
                    return context.UserPost_Likes.Where(x => x.PostId == Obj.PostId).Count();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }

        public int UserUnPostPostLikes(UserPost_Likes Obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    var Count = context.UserPost_Likes.Where(x => x.PostId == Obj.PostId).Count();
                    if (Count != 0)
                    {
                        UserPost_Likes Likes = context.UserPost_Likes.Where(x => x.PostId == Obj.PostId && x.UserId == Obj.UserId).First();
                        context.UserPost_Likes.Remove(Likes);
                        context.SaveChanges();
                    }
                    return context.UserPost_Likes.Where(x => x.PostId == Obj.PostId).Count();
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }


        public List<UserdetailsDTO> UserComments(UserPost_Comments Obj)
        {
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    if (Obj.Comment != null)
                    {
                        context.UserPost_Comments.Add(Obj);
                        context.SaveChanges();
                    }
                    List<UserdetailsDTO> Userdetails = (from a in context.UserDetails
                                                        join b in context.UserPost_Comments
                                                        on a.Id equals b.UserId
                                                        where b.PostId == Obj.PostId && b.Status == true
                                                        orderby b.CommentId descending
                                                        select new UserdetailsDTO { CommentId = b.CommentId, UserName = a.FirstName, LastName = a.LastName, UserMessage = b.Comment, UserImage = b.Image, UserPic = a.ProfilePicture }).ToList();
                    return Userdetails;
                }
            }
            catch (Exception ex)
            {
                _Logger.Error(ex.Message, ex);
                throw ex;
            }
        }
        /// <summary>
        /// This Methods Retrives all events when admin Logins
        /// </summary>
        public List<Events> GetAdminEvents()
        {
            List<Events> EventsforUsrs = new List<Events>();
            List<EventTypes> EventTyp = new List<EventTypes>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<Def_Events> Events = context.Def_Events.Where(x => x.Status == true).OrderByDescending(x => x.EventId).ToList();
                    foreach (Def_Events events in Events)
                    {
                        List<Event_TicketTypes> TicketTypes = context.Event_TicketTypes.Where(x => x.EventId == events.EventId).ToList();

                        List<EventGoingCount> IwillJoin = (from a in context.Event_UserSelections
                                                           join b in context.Event_UserPayments
                                                             on a.EventSelectionId equals b.UserSelectionId
                                                           where (a.EventId == events.EventId && b.PaymentStatus == "Paid")
                                                           select new EventGoingCount { EventId = b.PaymentId }).ToList();

                        foreach (Event_TicketTypes TicketType in TicketTypes)
                        {
                            if (events.EventId == TicketType.EventId)
                            {
                                EventTyp.Add(new EventTypes { TypeId = TicketType.TypeId, Heading = TicketType.Heading, EventId = TicketType.EventId, Description = TicketType.Description, Price = TicketType.Price, Quantity = TicketType.Quantity, CreatedOn = TicketType.CreatedOn });

                            }

                        }
                        EventsforUsrs.Add(new Events { StartTime = events.StartTime, EndTime = events.EndTime, EventId = events.EventId, EventName = events.EventName, EventTypeforAdmin = EventTyp, BannerImage = events.BannerImage, EventStartdate = events.EventStartdate, EndDate = events.EndDate, MobileNumber = events.MobileNumber, Email = events.Email, EventVenue = events.EventVenue, EventDescription = events.EventDescription, TotalNoOfTickets = events.TotalNoOfTickets, CreatedOn = events.CreatedOn, TicketsPaidCount = IwillJoin.Count() });
                    }

                }

            }
            catch (SystemException ex)
            {

            }
            return EventsforUsrs;
        }


        public List<Events> GetAdminEventsforsearch(string Name)
        {
            List<Events> EventsforUsrs = new List<Events>();
            List<EventTypes> EventTyp = new List<EventTypes>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<Def_Events> Events = context.Def_Events.Where(x => x.Status == true && x.EventName == Name).OrderByDescending(x => x.EventId).ToList();
                    foreach (Def_Events events in Events)
                    {
                        List<Event_TicketTypes> TicketTypes = context.Event_TicketTypes.Where(x => x.EventId == events.EventId).ToList();

                        List<EventGoingCount> IwillJoin = (from a in context.Event_UserSelections
                                                           join b in context.Event_UserPayments
                                                             on a.EventSelectionId equals b.UserSelectionId
                                                           where (a.EventId == events.EventId && b.PaymentStatus == "Paid")
                                                           select new EventGoingCount { EventId = b.PaymentId }).ToList();

                        foreach (Event_TicketTypes TicketType in TicketTypes)
                        {
                            if (events.EventId == TicketType.EventId)
                            {
                                EventTyp.Add(new EventTypes { TypeId = TicketType.TypeId, Heading = TicketType.Heading, EventId = TicketType.EventId, Description = TicketType.Description, Price = TicketType.Price, Quantity = TicketType.Quantity, CreatedOn = TicketType.CreatedOn });

                            }

                        }
                        EventsforUsrs.Add(new Events { StartTime = events.StartTime, EndTime = events.EndTime, EventId = events.EventId, EventName = events.EventName, EventTypeforAdmin = EventTyp, BannerImage = events.BannerImage, EventStartdate = events.EventStartdate, EndDate = events.EndDate, MobileNumber = events.MobileNumber, Email = events.Email, EventVenue = events.EventVenue, EventDescription = events.EventDescription, TotalNoOfTickets = events.TotalNoOfTickets, CreatedOn = events.CreatedOn, TicketsPaidCount = IwillJoin.Count() });
                    }

                }

            }
            catch (SystemException ex)
            {

            }
            return EventsforUsrs;
        }
        public List<EventTypes> EventTypes(int Id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<EventTypes> Events = (from a in context.Event_TicketTypes
                                           where (a.EventId == Id && a.Status == true)
                                           select new EventTypes { TypeId = a.TypeId, Heading = a.Heading, EventId = a.EventId, Description = a.Description, Price = a.Price, Quantity = a.Quantity, CreatedOn = a.CreatedOn }).ToList();
                return Events;
            }
        }


        public List<Events> GetUserEvents(int EventId)
        {
            List<Events> EventsforUsrs = new List<Events>();
            List<EventTypes> EventTyp = new List<EventTypes>();
            List<EventsId> Eventids = new List<EventsId>();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    List<Def_Events> Events = context.Def_Events.Where(x => x.Status == true && x.EventId == EventId).OrderByDescending(x => x.EventId).ToList();

                    List<Def_Events> UserUpcoming = context.Def_Events.Where(x => x.Status == true && x.EventId != EventId).OrderByDescending(x => x.EventId).Take(5).ToList();


                    foreach (var UpcomingEventIds in UserUpcoming)
                    {
                        DateTime dt1 = Convert.ToDateTime(UpcomingEventIds.EndDate);
                        DateTime dt2 = DateTime.Now;


                        if ((dt1.Date >= dt2.Date))
                        {

                            Eventids.Add(new EventsId { img = UpcomingEventIds.BannerImage, EventId = UpcomingEventIds.EventId, EventName = UpcomingEventIds.EventName, EventStartDate = UpcomingEventIds.EventStartdate.Value });
                        }

                    }
                    foreach (Def_Events events in Events)
                    {
                        List<Event_TicketTypes> TicketTypes = context.Event_TicketTypes.Where(x => x.EventId == events.EventId).ToList();
                        foreach (Event_TicketTypes TicketType in TicketTypes)
                        {
                            if (events.EventId == TicketType.EventId)
                            {
                                EventTyp.Add(new EventTypes { TypeId = TicketType.TypeId, Heading = TicketType.Heading, EventId = TicketType.EventId, Description = TicketType.Description, Price = TicketType.Price, Quantity = TicketType.Quantity, CreatedOn = TicketType.CreatedOn });

                            }

                        }
                        DateTime EndDate = Convert.ToDateTime(events.EndDate);
                        DateTime PresentDate = DateTime.Now.Date;
                        int Expired;
                        if (EndDate.Date <= PresentDate.Date)
                        {
                            Expired = 1;
                        }
                        else
                        {
                            Expired = 0;
                        }
                        EventsforUsrs.Add(new Events { EventExpired = Expired, EndTime = events.EndTime, StartTime = events.StartTime, EventIds = Eventids, EventId = events.EventId, EventName = events.EventName, EventTypeforAdmin = EventTyp, BannerImage = events.BannerImage, EventStartdate = events.EventStartdate, EndDate = events.EndDate, MobileNumber = events.MobileNumber, Email = events.Email, EventVenue = events.EventVenue, EventDescription = events.EventDescription, TotalNoOfTickets = events.TotalNoOfTickets, CreatedOn = events.CreatedOn });
                    }

                }

            }
            catch (SystemException ex)
            {

            }
            return EventsforUsrs;
        }


        public List<FunactionalAreasforjobs> FunactionalAreasforjobs()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<FunactionalAreasforjobs> Areas = (from a in context.Job_FunctionalArea
                                                       where a.Status == true
                                                       select new FunactionalAreasforjobs { FunactionalId = a.FunctionalId, Name = a.Name }).ToList();
                return Areas;
            }
        }

        public Events UserBookedEvents(int UserId, int SelectionId)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Events> EventBookings = new List<Events>();
                int EventId = context.Event_UserSelections.Single(x => x.EventSelectionId == SelectionId && x.UserId == UserId && x.Status == true).EventId.Value;
                Def_Events EventsDef = context.Def_Events.Where(x => x.EventId == EventId && x.Status == true).FirstOrDefault();

                List<UserEventSelections> EventBookingsbyUser = (from a in context.Event_TicketTypes
                                                                 join b in context.Events_UserBookings
                                                                     on a.TypeId equals b.TicketTypeId
                                                                 where b.EventSelectionId == SelectionId && b.Status == true
                                                                 select new UserEventSelections { TicketTypeId = a.TypeId, EventId = a.EventId, TicketTypeHeading = a.Heading, TotalAmount = b.TotalAmount.Value, TotalNoOfTickets = b.TotalTickets }).ToList();
                int TotalQuantity = 0;
                decimal TotalAmount = 0;
                foreach (var Bookings in EventBookingsbyUser)
                {
                    TotalQuantity += Bookings.TotalNoOfTickets.Value;
                    TotalAmount += Bookings.TotalAmount;
                }

                Events Eve = new Events()
                {
                    BannerImage = EventsDef.BannerImage,
                    EventVenue = EventsDef.EventVenue,
                    EventName = EventsDef.EventName,
                    Useselections = EventBookingsbyUser,
                    TotalNoOfTickets = TotalQuantity,
                    TotalAmount = TotalAmount

                };
                return Eve;

            }

        }
        public Event_TicketTypes EventUpdateTickets(int EventId, int TicketTypeId, int TicketsPurchased)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                int TotalTickets = context.Event_TicketTypes.Single(x => x.TypeId == TicketTypeId && x.EventId == EventId && x.Status == true).Quantity.Value;
                int SubTickets = (TotalTickets - TicketsPurchased);
                Event_TicketTypes TicketTypes = context.Event_TicketTypes.Where(x => x.TypeId == TicketTypeId && x.EventId == EventId && x.Status == true).FirstOrDefault();
                TicketTypes.Quantity = SubTickets;
                TicketTypes.UpdatedOn = DateTime.Now;
                context.SaveChanges();

                return TicketTypes;
            }


        }

        public List<Donation_Details> GetAdminDonations()
        {
            List<Donation_Details> DonationsforUsrs = new List<Donation_Details>();
            Donation_Details DonationDetails = new Donation_Details();
            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    DonationsforUsrs = context.Donation_Details.Where(x => x.Status == true).OrderByDescending(x => x.Donation_ID).ToList();
                    foreach (Donation_Details Donation in DonationsforUsrs)
                    {
                        DonationDetails.Donor_Details = context.Donor_Details.Where(x => x.Donation_ID == Donation.Donation_ID && x.Donation_Status == "Success").ToList();
                    }
                }
            }
            catch (SystemException ex)
            {

            }
            return DonationsforUsrs;
        }

        public Donation_Details GetUserDonations(int DonationId)
        {
            Donation_Details DonationDetails = new Donation_Details();

            try
            {
                using (var context = _dbContextFactory.CreateConnection())
                {
                    context.Configuration.ProxyCreationEnabled = false;
                    DonationDetails = context.Donation_Details.Where(x => x.Status == true && x.Donation_ID == DonationId).First();
                    DonationDetails.Donor_Details = context.Donor_Details.Where(x => x.Donation_ID == DonationId && x.Donation_Status == "Success").ToList();
                }

            }
            catch (SystemException ex)
            {

            }
            return DonationDetails;
        }
        public dashboardCounts GetdashboardData()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {

                int TotalUsers = context.UserDetails.Where(x => x.UserStatus == 1).Count();
                int TotalEvents = context.Def_Events.Where(x => x.Status == true).Count();
                int TotalJobs = context.UserPosting_Jobs.Where(x => x.Status == 1).Count();
                int PendigUsers = context.View_UserDetails.Where(x => x.UserStatus == 0).Count();
                int RejectedUsers = context.View_UserDetails.Where(x => x.UserStatus == 2).Count();
                //  decimal Amounts = context.Event_UserPayments.Where(x => x.PaymentStatus == "Paid").Sum(x=>x.);
                dashboardCounts Countsdata = new dashboardCounts()
                {
                    TotalUsers = TotalUsers,
                    TotalEvents = TotalEvents,
                    TotalJobs = TotalJobs,
                    PendingUsers = PendigUsers,
                    RejectedUsers = RejectedUsers
                    //TotalAmount = Amounts

                };
                return Countsdata;
            }

        }

    }
        #endregion EndMethods
    #region properties

    public class FunactionalAreasforjobs
    {
        public int FunactionalId { get; set; }
        public string Name { get; set; }
    }

    public class UserPostsData : UserPostsImages
    {
        public long? UserId { get; set; }
        public string EventName { get; set; }
        public string UserPostMessage { get; set; }
        public int PostId { get; set; }
        public List<string> imageUrls { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string CourseName { get; set; }
        public int UserPostsCount { get; set; }
        public List<UserComments> UserComments { get; set; }
        public string UserImage { get; set; }
        public int? UserLikecheck { get; set; }
        public int UserCommentCount { get; set; }
        public DateTime? UserPostDate { get; set; }
        public string BatchVisible { get; set; }

        public string Branch { get; set; }

        public string BatchTo { get; set; }

        public string BatchFrom { get; set; }

        public string UserMessage { get; set; }
    }
    public class UserComments
    {
        internal object CommentedByLastName;

        public int CommentId { get; set; }
        public string Comment { get; set; }
        public string ProfilePic { get; set; }
        public object CommentedByFirstName { get; internal set; }
    }
    public class UserPostsImages
    {
        public int ImageId { get; set; }
        public string ImagePath { get; set; }
    }

    public class UserPostsComments : UserPost_Comments
    {
        public string UserImage { get; set; }
        public string CommentedByFirstName { get; set; }
        public string CommentedByLastName { get; set; }
    }

    public partial class UserdetailsDTO
    {
        public int PostId { get; set; }
        public string UserMessage { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string CourseName { get; set; }
        public string EventName { get; set; }
        public string Batch { get; set; }
        public string UserImage { get; set; }
        public int CommentId { get; set; }
        public List<UserComments> UserComments { get; set; }
        public string UserPic { get; set; }
        public long? UserId { get; set; }
        public DateTime? PostDate { get; set; }
        public string BatchFrom { get; set; }
        public string BatchTo { get; set; }
        public string Branch { get; set; }
        public string WorkingTO { get; set; }
    }
    public class Events
    {
        public List<EventsId> EventIds { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string BannerImage { get; set; }
        public Nullable<System.DateTime> EventStartdate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string MobileNumber { get; set; }
        public string Landline { get; set; }
        public string Email { get; set; }
        public string EventVenue { get; set; }
        public string EventDescription { get; set; }
        public Nullable<int> VisitorsCount { get; set; }
        public Nullable<int> TotalNoOfTickets { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Status { get; set; }
        public int TotalTickets { get; set; }
        public decimal TotalAmount { get; set; }
        public string TicketTypeHeading { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string LName { get; set; }
        public string Mobile { get; set; }
        public List<UserEventSelections> Useselections { get; set; }
        public int EventSelectionId { get; set; }
        public int ActualQuantity { get; set; }
        public decimal ActualTicketAmount { get; set; }
        public string EmailId { get; set; }
        public string UserImage { get; set; }
        public string Userimage { get; set; }
        public int? Batch { get; set; }
        public string Branch { get; set; }
        public string Course { get; set; }
        public List<EventTypes> EventTypeforAdmin { get; set; }
        public DateTime? EventGoingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int paymentId { get; set; }
        public int Count { get; set; }

        public string OnlyStartTime { get; set; }
        public string OnlyStartTimeNoon { get; set; }

        public string OnlyEndTime { get; set; }
        public string OnlyEndTimeNoon { get; set; }

        public int? WorkingTO { get; set; }

        public Def_Events events { get; set; }
        public int TicketsPaidCount { get; set; }
        public int EventExpired { get; set; }
    }

    public class EventGoingCount
    {
        public int Going { get; set; }
        public int EventId { get; set; }
        public long? Userid { get; set; }
    }
    public class EventsId
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventStartDate { get; set; }
        public string img { get; set; }
    }

    public class EventTypes
    {
        public int TypeId { get; set; }
        public string Heading { get; set; }
        public int EventId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool Status { get; set; }
        public string EventEmail { get; set; }

    }

    public class UserEventSelections
    {
        public string TicketTypeHeading { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<int> TotalNoOfTickets { get; set; }

        public int EventId { get; set; }

        public int TicketTypeId { get; set; }
    }

    public class Donations
    {
        public int DonationID { get; set; }
        public string DonationTitle { get; set; }
        public string DonationDescription { get; set; }
        public decimal DonationAmount { get; set; }
        public string DonationBanner { get; set; }
        public long DonorID { get; set; }
        public string DonorName { get; set; }
        public DateTime DonationDate { get; set; }
        public string DonationStatus { get; set; }

    }

    #endregion properties

    public class GalleryImages
    {
        public int ImageId { get; set; }
        public string Image { get; set; }
    }
    public class ActivitiesCounts
    {
        public int Events { get; set; }
        public int Members { get; set; }
        public int News { get; set; }
        public int Jobs { get; set; }
    }

    public class dashboardCounts
    {
        public decimal TotalAmount { get; set; }
        public int TotalUsers { get; set; }
        public int TotalJobs { get; set; }
        public int TotalEvents { get; set; }
        public int PendingUsers { get; set; }
        public int RejectedUsers { get; set; }
    }
}
