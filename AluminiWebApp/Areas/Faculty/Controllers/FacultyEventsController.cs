using Alumini.Core;
using AluminiService.Interfaces;
using System;
using System.Web.Mvc;
using AluminiWebApp.Areas.Alumini.Models;

namespace AluminiWebApp.Areas.Faculty.Controllers
{
    public class FacultyEventsController : BaseController
    {
        public FacultyEventsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService coursecategoryservices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, coursecategoryservices, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices)
        {

        }

        public ActionResult Events()
        {
            if (Session["UserId"] != null)
            {
                MyEventsModel events = new MyEventsModel()
                {

                    UserPurchasedEvents = GenericMethods.GetAdminEvents()
                };
                return View(events);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public ActionResult SingleEvent(int? EventId)
        {
            if (Session["UserId"] != null)
            {
                AlumniEventsModel eventModel = new AlumniEventsModel()
                {
                    EventId = EventId.Value
                };
                if (EventId != null) ViewBag.EventsOnId = GenericMethods.GetUserEvents(EventId.Value);
                return View(eventModel);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpPost]
        public ActionResult SingleEvent(AlumniEventsModel alumniBookTickets, int EventId)
        {
            if (alumniBookTickets.EventTypeId == null && alumniBookTickets.TicketsQuantity == null && alumniBookTickets.TicketAmount == null)
            {
                TempData["ErrorMessage"] = "Please select the Tickets and click book now";
                return RedirectToAction("SingleEvent", "FacultyEvents", new { Area = "Faculty", EventId = EventId });
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    Event_UserSelections eventselections = new Event_UserSelections()
                    {
                        EventId = EventId,
                        UserId = Convert.ToInt32(Session["UserId"].ToString()),
                        CreatedOn = DateTime.Now,
                        CreatedBy = Convert.ToInt32(Session["UserId"].ToString()),
                        Status = true
                    };


                    Event_UserSelections eventSelection = UserSelectionServices.Create(eventselections);
                    if (alumniBookTickets.EventTypeId.Count != null && alumniBookTickets.TicketsQuantity.Count != null && alumniBookTickets.TicketAmount.Count != null)
                    {
                        for (int i = 0; i < alumniBookTickets.EventTypeId.Count; i++)
                        {
                            for (int j = i; j < alumniBookTickets.TicketAmount.Count; j++)
                            {
                                for (int k = i; k < alumniBookTickets.TicketsQuantity.Count; k++)
                                {
                                    decimal totalAmount = ((alumniBookTickets.TicketsQuantity[i]) * (alumniBookTickets.TicketAmount[i]));
                                    Events_UserBookings bookings = new Events_UserBookings()
                                    {
                                        TicketTypeId = alumniBookTickets.EventTypeId[i],
                                        EventSelectionId = eventSelection.EventSelectionId,
                                        TotalAmount = totalAmount,
                                        TotalTickets = alumniBookTickets.TicketsQuantity[i],
                                        CreatedOn = DateTime.Now,
                                        CreatedBy = Convert.ToInt32(Session["UserId"].ToString()),
                                        Status = true

                                    };

                                    UserselectionBookingServices.Create(bookings);

                                    break;
                                }
                                break;
                            }
                        }
                        return RedirectToAction("BookEvent", "FacultyEvents", new { area = "Faculty", id = eventselections.EventSelectionId });
                    }
                }
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public JsonResult GetEventTypesOnId(int eventId)
        {
            var data = GenericMethods.EventTypes(eventId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult BookEvent(int? id)
        {
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                ViewBag.BookingEvents = GenericMethods.UserBookedEvents(userId, Convert.ToInt32(id));
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpPost]
        [ActionName("BookEvent")]
        public ActionResult FinalPayment(int? id)
        {
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                var data = GenericMethods.UserBookedEvents(userId, Convert.ToInt32(id));
                Event_UserPayments payments = new Event_UserPayments()
                {
                    UserId = userId,
                    UserSelectionId = id.Value,
                    Status = true,
                    CreatedOn = DateTime.Now,
                    PaymentStatus = "Paid",
                    ConfirmationDate = DateTime.Now,
                };
                UserpaymentsService.Create(payments);

                foreach (var paymentdetails in data.Useselections)
                {
                    GenericMethods.EventUpdateTickets(paymentdetails.EventId, paymentdetails.TicketTypeId, paymentdetails.TotalNoOfTickets.Value);
                }
                TempData["DisplayMessage"] = "Your booking was successfully completed";
                return RedirectToAction("Events", "FacultyEvents", new { area = "Faculty" });
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });


        }


        [HttpGet]
        public ActionResult MyEvents()
        {
            if (Session["UserId"] != null)
            {

                MyEventsModel Events = new MyEventsModel()
                {
                    UserPurchasedEvents = UserSelectionServices.MyUserEvents(Convert.ToInt32(Session["UserId"].ToString()))
                };

                return View(Events);
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });

        }
        [HttpGet]
        public ActionResult MyTickets(int id)
        {
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                ViewBag.BookingEvents = GenericMethods.UserBookedEvents(userId, Convert.ToInt32(id));
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });

        }

        [HttpGet]
        public JsonResult EventGoing(int Type, string Status, int EventId)
        {

            Event_AttendingStatus Attending = new Event_AttendingStatus()
            {
                UserId = Convert.ToInt64(Session["UserId"].ToString()),
                UserGoingStatus = Status,
                Status = true,
                CreatedOn = DateTime.Now,
                EventId = EventId

            };
            EventService.EventGoingInsert(Convert.ToInt32(Session["UserId"].ToString()), Attending);
            var data = EventService.EventGoingList(EventId, Convert.ToInt32(Session["UserId"]));


            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUserEventGoingCounts(int EventId)
        {
            var data = EventService.EventGoingList(EventId, Convert.ToInt32(Session["UserId"]));
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }

}