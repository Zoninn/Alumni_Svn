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
    public class NewsRoomRepositroy : INewsRoomRepository
    {

        private readonly Alumini.Logger.ILogger _Logger;
        private readonly IDbConnectionFactory _dbContextFactory;
        public NewsRoomRepositroy(Alumini.Logger.ILogger _Logger, IDbConnectionFactory _DbContextFactory)
        {
            this._Logger = _Logger;
            this._dbContextFactory = _DbContextFactory;
        }

        public Alumini.Core.db_NewsRooms Create(Alumini.Core.db_NewsRooms obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                context.db_NewsRooms.Add(obj);
                context.SaveChanges();
                return obj;
            }
        }

        public Alumini.Core.db_NewsRooms UpdateNewRooms(Alumini.Core.db_NewsRooms obj)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_NewsRooms News = context.db_NewsRooms.Where(x => x.NewRoomId == obj.NewRoomId && x.Status == true).FirstOrDefault();
                News.Title = obj.Title;
                if (obj.Image != "")
                {
                    News.Image = obj.Image;
                }
                News.Description = obj.Description;
                News.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return obj;
            }
        }

        public Alumini.Core.db_NewsRooms Get(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_NewsRooms News = context.db_NewsRooms.Where(x => x.NewRoomId == id && x.Status == true).FirstOrDefault();

                return News;
            }
        }

        public List<NewsRooms> GetNews()
        {
            using (var context = _dbContextFactory.CreateConnection())
            {

                List<NewsRooms> NewsRooms = (from a in context.db_NewsRooms
                                             where a.Status == true
                                             orderby a.NewRoomId descending
                                             select new NewsRooms { NewsId = a.NewRoomId, Image = a.Image, Description = a.Description, Title = a.Title, PostedOn = a.CreatedOn }).Take(6).ToList();

                return NewsRooms;
            }
        }


        public Alumini.Core.db_NewsRooms Update(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_NewsRooms NewsRooms = context.db_NewsRooms.Where(x => x.Status == false && x.NewRoomId == id).FirstOrDefault();
                NewsRooms.Status = true;
                NewsRooms.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return NewsRooms;
            }
        }


        public bool Delete(int id)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                db_NewsRooms NewsRooms = context.db_NewsRooms.Where(x => x.Status == true && x.NewRoomId == id).FirstOrDefault();
                NewsRooms.Status = false;
                NewsRooms.UpdatedOn = DateTime.Now;
                context.SaveChanges();
                return true;
            }
        }

        public List<NewsRooms> GetNewsRooms(int Status)
        {
            using (var context = _dbContextFactory.CreateConnection())
            {
                bool NewsStatus = Convert.ToBoolean(Status);
                List<NewsRooms> NewsRooms = (from a in context.db_NewsRooms
                                             where a.Status == NewsStatus
                                             orderby a.NewRoomId descending
                                             select new NewsRooms { NewsId = a.NewRoomId, Image = a.Image, Description = a.Description, Title = a.Title, PostedOn = a.CreatedOn }).ToList();

                return NewsRooms;
            }
        }
    }

    public class NewsRooms
    {
        public int NewsId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime? PostedOn { get; set; }
    }
}
