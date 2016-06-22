using Alumini.Core;
using AluminiRepository.Factories;
using AluminiRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository
{
    public class MemoriesRepository : IMemoriesRepository
    {
        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;

        public MemoriesRepository(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }



        public Alumini.Core.db_Memories_Info Create(Alumini.Core.db_Memories_Info obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.db_Memories_Info.Add(obj);
                context.SaveChanges();
                return obj;
            }
        }

        public List<Images> DeleteMeoryimage(int id, int Memoryid)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_Memories_images memories = context.db_Memories_images.Where(x => x.Imageid == id && x.Status == true).FirstOrDefault();
                memories.Status = false;
                context.SaveChanges();
                List<Images> Images = (from a in context.db_Memories_images
                                       where a.MemoriesId == Memoryid && a.Status == true
                                       select new Images { Imageid = a.Imageid, Image = a.Image }).ToList();
                return Images;
            }
        }

        public Alumini.Core.db_Memories_images InsertImages(db_Memories_images Images)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.db_Memories_images.Add(Images);
                context.SaveChanges();
                return Images;
            }
        }


        public Alumini.Core.db_Memories_Info Get(int id)
        {
            throw new NotImplementedException();
        }

        public Alumini.Core.db_Memories_Info Update(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_Memories_Info memories = context.db_Memories_Info.Where(x => x.MemoriesId == id && x.Status == false).FirstOrDefault();
                memories.Status = true;
                memories.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return memories;
            }
        }


        public Alumini.Core.db_Memories_images UpdateImages(db_Memories_images Images)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_Memories_images memories = context.db_Memories_images.Where(x => x.Imageid == Images.Imageid && x.Status == true).FirstOrDefault();
                memories.Status = true;
                memories.Image = Images.Image;
                memories.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return memories;
            }
        }

        public Alumini.Core.db_Memories_Info UpdateMemories(db_Memories_Info Memories)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_Memories_Info memories = context.db_Memories_Info.Where(x => x.MemoriesId == Memories.MemoriesId && x.Status == true).FirstOrDefault();
                memories.Status = true;
                memories.UpdatedOn = DateTime.Now;
                memories.Heading = Memories.Heading;
                memories.Description = Memories.Description;
                memories.MemoryDate = Memories.MemoryDate;
                memories.VisibleTo = Memories.VisibleTo;
                memories.VisibleBatchfrom = Memories.VisibleBatchfrom;
                memories.VisibleBatchTo = Memories.VisibleBatchTo;
                context.SaveChanges();
                return memories;
            }
        }

        public bool Delete(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_Memories_Info memories = context.db_Memories_Info.Where(x => x.MemoriesId == id && x.Status == true).FirstOrDefault();
                memories.Status = false;
                memories.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }

        public List<Memroeis> EditMemories(int MemoriesId)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<string> MemoriesImages = new List<string>();
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();
                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == true && a.MemoriesId == MemoriesId)
                                           select new Memroeis { Userid = b.Id, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    string MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;

                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();

                    UserdetailsMemories.Add(new Memroeis { Userid = Images.Userid, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }
                return UserdetailsMemories;
            }

        }

        public List<Memroeis> GetMemories(int userid)
        {

            List<string> MemoriesImages = new List<string>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();
                View_UserDetails Batch = context.View_UserDetails.Where(x => x.UserId == userid).FirstOrDefault();

                int? VisibleTo = Convert.ToInt32(Convert.ToInt32(Batch.Batch) - Convert.ToInt32(Batch.Years));

                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == true && (a.VisibleBatchfrom == VisibleTo && a.VisibleBatchTo == Batch.Batch))
                                           select new Memroeis { Userid = b.Id, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    string MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;

                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();

                    UserdetailsMemories.Add(new Memroeis { Userid = Images.Userid, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }
                return UserdetailsMemories;
            }



        }


        public List<Memroeis> GetMemoriesforAdmin(int userid)
        {

            List<string> MemoriesImages = new List<string>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();

                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == true)
                                           select new Memroeis { Title = a.Heading, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    string MemoryImages = "";
                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();
                    if (Count != 0)
                    {
                        MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;
                    }
                    else
                    {
                        MemoryImages = "";
                    }


                    UserdetailsMemories.Add(new Memroeis { Title = Images.Title, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }
                return UserdetailsMemories;
            }



        }




        public List<Memroeis> GetAllMemories(int userid)
        {

            List<string> MemoriesImages = new List<string>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();


                View_UserDetails Batch = context.View_UserDetails.Where(x => x.UserId == userid).FirstOrDefault();

                int? VisibleTo = Convert.ToInt32(Convert.ToInt32(Batch.Batch) - Convert.ToInt32(Batch.Years));

                //List<Memroeis> Memories = (from a in context.db_Memories_Info
                //                           join b in context.UserDetails
                //                           on a.Userid equals b.Id
                //                           where (a.Status == true && (a.VisibleBatchfrom == VisibleTo && a.VisibleBatchTo == Batch.Batch))
                //                           select new Memroeis { Userid = b.Id, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                List<Memroeis> VisibletoAll = (from a in context.db_Memories_Info
                                               join b in context.UserDetails
                                               on a.Userid equals b.Id
                                               where (a.Status == true && a.VisibleTo == "To All")
                                               select new Memroeis { Userid = b.Id, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();



                //foreach (var Images in Memories)
                //{
                //    string MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;

                //    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();

                //    UserdetailsMemories.Add(new Memroeis { Title = Images.Title, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                //}

                foreach (var Images in VisibletoAll)
                {
                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();
                    string MemoryImages = "";
                    if (Count != 0)
                    {
                        MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;
                    }
                    else
                    {
                        MemoryImages = "";
                    }
                    UserdetailsMemories.Add(new Memroeis { Userid = Images.Userid, Title = Images.Title, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }

                return UserdetailsMemories;
            }



        }


        public List<Memroeis> GetAllMemoriesforfaculty()
        {

            List<string> MemoriesImages = new List<string>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();

                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == true && a.VisibleTo == "To All")
                                           select new Memroeis { Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    string MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;

                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();

                    UserdetailsMemories.Add(new Memroeis { Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }
                return UserdetailsMemories;
            }



        }


        public List<Memroeis> GetDeleted()
        {

            List<string> MemoriesImages = new List<string>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();

                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == false)
                                           select new Memroeis { Title = a.Heading, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    string MemoryImages = context.db_Memories_images.FirstOrDefault(x => x.MemoriesId == Images.Memoriesid).Image;

                    int Count = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid).Count();

                    UserdetailsMemories.Add(new Memroeis { Title = Images.Title, Memoriesid = Images.Memoriesid, ImagesCount = Count, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, ImagesFIles = MemoriesImages, PostedbyImages = Images.PostedbyImages, MemroyImage = MemoryImages });

                }
                return UserdetailsMemories;
            }



        }




        public List<Memroeis> GetSinglememorys(int Memoriesid)
        {

            List<Images> Userimagesfor = new List<Images>();
            List<Memroeis> UserImages = new List<Memroeis>();
            using (var context = _dbContextFactory.CreateConnection())
            {
                List<Memroeis> UserdetailsMemories = new List<Memroeis>();




                List<Memroeis> Memories = (from a in context.db_Memories_Info
                                           join b in context.UserDetails
                                           on a.Userid equals b.Id
                                           where (a.Status == true && a.MemoriesId == Memoriesid)
                                           select new Memroeis { visibleTo = a.VisibleTo, Title = a.Heading, Description = a.Description, PostedBy = b.FirstName + "" + b.LastName, Memoriesid = a.MemoriesId, Memoiesdate = a.MemoryDate, PostedOn = a.CreatedOn, PostedbyImages = b.ProfilePicture }).ToList();


                foreach (var Images in Memories)
                {
                    var MemoryImages = context.db_Memories_images.Where(x => x.MemoriesId == Images.Memoriesid && x.Status == true).ToList();

                    foreach (var ImagesCount in MemoryImages)
                    {
                        Userimagesfor.Add(new Images { Image = ImagesCount.Image, Imageid = ImagesCount.Imageid });
                    }
                    UserdetailsMemories.Add(new Memroeis { visibleTo = Images.visibleTo, Title = Images.Title, MemoriesImages = Userimagesfor, Memoriesid = Images.Memoriesid, PostedBy = Images.PostedBy, PostedOn = Images.PostedOn, Memoiesdate = Images.Memoiesdate, Description = Images.Description, PostedbyImages = Images.PostedbyImages });

                }
                return UserdetailsMemories;
            }



        }



    }

    public class Memroeis
    {

        public string Images { get; set; }
        public int Memoriesid { get; set; }
        public string PostedbyImages { get; set; }
        public string PostedBy { get; set; }
        public DateTime? Memoiesdate { get; set; }
        public DateTime? PostedOn { get; set; }
        public string Postedto { get; set; }
        public List<string> ImagesFIles { get; set; }
        public string MemroyImage { get; set; }
        public string Description { get; set; }
        public int ImagesCount { get; set; }
        public List<Images> MemoriesImages { get; set; }
        public string Title { get; set; }

        public long Userid { get; set; }

        public string visibleTo { get; set; }
    }

    public class Images
    {
        public string Image { get; set; }
        public string UserName { get; set; }
        public int Imageid { get; set; }

        public long? Userid { get; set; }
    }
}
