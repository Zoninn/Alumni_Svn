using Alumini.Core;
using AluminiRepository;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;


namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class EventsController : BaseController
    {

        public EventsController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            List<EventDates> EventStartDates = new List<EventDates>();
            List<EventsAMS> AMS = new List<EventsAMS>();
            for (int i = 1; i <= 12; i++)
            {
                EventStartDates.Add(new EventDates { StartsDate = Convert.ToString(i) });
            }
            AMS.Add(new EventsAMS { AMS = "AM" });
            AMS.Add(new EventsAMS { AMS = "PM" });
            EventModel userEvent = new EventModel()
            {
                EventStartDates = EventStartDates,
                AMS = AMS
            };
            return View(userEvent);
        }

        /// <summary>
        /// This Action Method is used to Register Events by Admin..
        /// </summary>
        /// <param name="userEventModel"></param>
        /// <param name="BannerImage"></param>       
        /// <param name="Gallery"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateEvent(EventModel userEventModel, HttpPostedFileBase BannerImage, IEnumerable<HttpPostedFileBase> Gallery)
        {
            List<EventDates> EventStartDates = new List<EventDates>();
            List<EventsAMS> AMS = new List<EventsAMS>();
            for (int i = 1; i <= 12; i++)
            {
                EventStartDates.Add(new EventDates { StartsDate = Convert.ToString(i) });
            }
            AMS.Add(new EventsAMS { AMS = "AM" });
            AMS.Add(new EventsAMS { AMS = "PM" });
            EventModel userEvent = new EventModel()
            {
                EventStartDates = EventStartDates,
                AMS = AMS
            };
            if (ModelState.IsValid)
            {
                var Tickets = 0;
                foreach (var TicketsQuantity in userEventModel.Quantity)
                {
                    Tickets += TicketsQuantity;
                }
                if (Tickets > userEventModel.TotalNoOfTickets || Tickets < userEventModel.TotalNoOfTickets)
                {
                    TempData["SuccessMessage"] = "Total Tickets must equal to Tickets Quantity";
                    return View(userEvent);
                }
                else
                {
                    var fileName = Path.GetFileName(BannerImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    BannerImage.SaveAs(path);
                    var FilePath = "/UserProfilePictures/" + fileName;
                    Def_Events Events = new Def_Events()
                    {
                        EventName = userEventModel.EventName,
                        BannerImage = FilePath,
                        EventStartdate = userEventModel.EventStartdate,
                        EndDate = userEventModel.EndDate,
                        MobileNumber = userEventModel.MobileNumber,
                        Email = userEventModel.Email,
                        EventVenue = userEventModel.EventVenue,
                        EventDescription = userEventModel.EventDescription,
                        TotalNoOfTickets = userEventModel.TotalNoOfTickets,
                        StartTime = userEventModel.StartTime + userEventModel.StartAfter,
                        EndTime = userEventModel.EndTime + userEventModel.StartEve,
                        Status = true,
                        CreatedOn = DateTime.Now
                    };
                    Def_Events AdminEvents = EventService.Create(Events);
                    if (userEventModel.Heading.Count != 0 && userEventModel.Description.Count != 0 && userEventModel.Price.Count != 0 && userEventModel.Quantity.Count != 0)
                    {


                        for (int i = 0; i < userEventModel.Heading.Count; i++)
                        {
                            for (int j = i; j < userEventModel.Description.Count; j++)
                            {
                                for (int k = i; k < userEventModel.Price.Count; k++)
                                {
                                    for (int l = i; l < userEventModel.Quantity.Count; l++)
                                    {

                                        Event_TicketTypes TicketTypes = new Event_TicketTypes()
                                        {
                                            Heading = userEventModel.Heading[i],
                                            EventId = AdminEvents.EventId,
                                            Price = userEventModel.Price[i],
                                            Description = userEventModel.Description[i],
                                            Quantity = userEventModel.Quantity[i],
                                            Status = true,
                                            CreatedOn = DateTime.Now,
                                        };
                                        EventTicketTypeservices.Create(TicketTypes);

                                        break;
                                    }
                                    break;
                                }
                                break;
                            }
                        }
                    }
                    TempData["SuccessMessage"] = "Event Added successfully";
                    return RedirectToAction("CreateEvent", "Events", new { area = "Admin" });
                }
            }
            return View(userEvent);
        }
        [HttpGet]
        public JsonResult GetEvents()
        {
            var data = GenericMetodsservices.GetAdminEvents();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EventTypes(int Id)
        {
            ViewBag.Events = GenericMetodsservices.EventTypes(Id);
            return View();
        }

        [HttpGet]
        public JsonResult DeleteEvents(int EventId)
        {
            EventService.Delete(EventId);
            var data = GenericMetodsservices.GetAdminEvents();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EventPayments(int? id)
        {
            List<Events> Payments = new List<Events>();
            if (id == 0)
            {
                Payments = UserPaymentsService.GetUserPayments();

            }
            else
            {
                Payments = UserPaymentsService.GetUserPaymentsonEventId(id.Value);
            }
            return View(Payments);
        }

        [HttpGet]
        public JsonResult GetAllPayments()
        {
            var data = UserPaymentsService.GetUserPayments();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetEventDetailsOnid(int EventId)
        {
            var data = GenericMetodsservices.GetUserEvents(EventId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetEventsNames()
        {
            var Data = EventService.GetEVents();
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetEventPaymentsonEVentId(int EventId)
        {
            var Data = UserPaymentsService.GetUserPaymentsonEventId(EventId);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult UpdateEvent(int Eventid)
        {
            List<EventDates> EventStartDates = new List<EventDates>();
            List<EventsAMS> AMS = new List<EventsAMS>();
            for (int i = 1; i <= 12; i++)
            {
                EventStartDates.Add(new EventDates { StartsDate = Convert.ToString(i) });
            }
            AMS.Add(new EventsAMS { AMS = "AM" });
            AMS.Add(new EventsAMS { AMS = "PM" });
            EventModel userEvent = new EventModel()
            {
                EventStartDates = EventStartDates,
                AMS = AMS
            };
            return View(userEvent);
        }
        [HttpPost]
        public ActionResult UpdateEvent(EventModel userEventModel, HttpPostedFileBase BannerImage, IEnumerable<HttpPostedFileBase> Gallery)
        {

            List<EventDates> EventStartDates = new List<EventDates>();
            List<EventsAMS> AMS = new List<EventsAMS>();
            for (int i = 1; i <= 12; i++)
            {
                EventStartDates.Add(new EventDates { StartsDate = Convert.ToString(i) });
            }
            AMS.Add(new EventsAMS { AMS = "AM" });
            AMS.Add(new EventsAMS { AMS = "PM" });
            EventModel userEvent = new EventModel()
            {
                EventStartDates = EventStartDates,
                AMS = AMS
            };

            if (userEventModel.EventId != null)
            {
                var FilePath = "";
                if (BannerImage != null)
                {
                    var fileName = Path.GetFileName(BannerImage.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    BannerImage.SaveAs(path);
                    FilePath = "/UserProfilePictures/" + fileName;
                }

                Def_Events Events = new Def_Events()
                {
                    EventId = userEventModel.EventId,
                    EventName = userEventModel.EventName,
                    BannerImage = FilePath,
                    EventStartdate = userEventModel.EventStartdate,
                    EndDate = userEventModel.EndDate,
                    MobileNumber = userEventModel.MobileNumber,
                    Email = userEventModel.Email,
                    EventVenue = userEventModel.EventVenue,
                    EventDescription = userEventModel.EventDescription,
                    TotalNoOfTickets = userEventModel.TotalNoOfTickets,
                    StartTime = userEventModel.StartTime + userEventModel.StartAfter,
                    EndTime = userEventModel.EndTime + userEventModel.StartEve,
                    Status = true,
                    CreatedOn = DateTime.Now
                };
                EventService.EVentsUpdate(Events);

                if (userEventModel.Heading.Count != 0 && userEventModel.Description.Count != 0 && userEventModel.Price.Count != 0 && userEventModel.Quantity.Count != 0)
                {
                    if (userEventModel.TicketTypeId != null)
                    {
                        if (userEventModel.TicketTypeId.Count != 0)
                        {

                            for (int i = 0; i < userEventModel.Heading.Count; i++)
                            {
                                for (int j = i; j < userEventModel.Description.Count; j++)
                                {
                                    for (int k = i; k < userEventModel.Price.Count; k++)
                                    {
                                        for (int l = i; l < userEventModel.Quantity.Count; l++)
                                        {
                                            for (int m = i; m < userEventModel.TicketTypeId.Count; m++)
                                            {
                                                if (userEventModel.TicketTypeId[m] != 0)
                                                {

                                                    Event_TicketTypes TicketTypes = new Event_TicketTypes()
                                                    {
                                                        Heading = userEventModel.Heading[i],
                                                        EventId = userEventModel.EventId,
                                                        Price = userEventModel.Price[i],
                                                        Description = userEventModel.Description[i],
                                                        Quantity = userEventModel.Quantity[i],
                                                        Status = true,
                                                        TypeId = userEventModel.TicketTypeId[m],
                                                        CreatedOn = DateTime.Now,
                                                    };
                                                    EventTicketTypeservices.UpdateEvent(TicketTypes);

                                                    break;
                                                }
                                                else
                                                {

                                                    Event_TicketTypes TicketTypes = new Event_TicketTypes()
                                                    {
                                                        Heading = userEventModel.Heading[i],
                                                        EventId = userEventModel.EventId,
                                                        Price = userEventModel.Price[i],
                                                        Description = userEventModel.Description[i],
                                                        Quantity = userEventModel.Quantity[i],
                                                        Status = true,
                                                        CreatedOn = DateTime.Now,
                                                    };
                                                    EventTicketTypeservices.Create(TicketTypes);
                                                }
                                            }
                                            break;
                                        }
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        if (userEventModel.Heading.Count != 0 && userEventModel.Description.Count != 0 && userEventModel.Price.Count != 0 && userEventModel.Quantity.Count != 0)
                        {


                            for (int i = 0; i < userEventModel.Heading.Count; i++)
                            {
                                for (int j = i; j < userEventModel.Description.Count; j++)
                                {
                                    for (int k = i; k < userEventModel.Price.Count; k++)
                                    {
                                        for (int l = i; l < userEventModel.Quantity.Count; l++)
                                        {

                                            Event_TicketTypes TicketTypes = new Event_TicketTypes()
                                            {
                                                Heading = userEventModel.Heading[i],
                                                EventId = userEventModel.EventId,
                                                Price = userEventModel.Price[i],
                                                Description = userEventModel.Description[i],
                                                Quantity = userEventModel.Quantity[i],
                                                Status = true,
                                                CreatedOn = DateTime.Now,
                                            };
                                            EventTicketTypeservices.Create(TicketTypes);

                                            break;
                                        }
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            else
            {

            }
            TempData["SuccessMessage"] = "Event Updated Successfully..";
            return RedirectToAction("AdminEvents", "Events", new { area = "Admin" });

        }
        [HttpGet]
        public ActionResult GetUserGoingsforAdmin()
        {

            return View();
        }

        public JsonResult GetdataforAdmins(string Status)
        {
            var Data = EventService.GetUserforAdmin(Status);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AdminEvents()
        {
            List<Goings> AmGoing = new List<Goings>();


            return View();
        }
    }
    public class EventDates
    {
        public string StartsDate { get; set; }
    }
    public class EventsAMS
    {
        public string AMS { get; set; }
    }
    public class Goings
    {
        public string AmGoings { get; set; }
    }
    public class GetGoings
    {
        public string Goings { get; set; }
    }
}