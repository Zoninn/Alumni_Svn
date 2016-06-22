using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AluminiWebApp.Areas.Alumini.Controllers
{
    public class AlumniEventsController : BaseController
    {
        public AlumniEventsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice, IUser_JobPostingService _userJobPostingservice, IEventService _eventService, INewsRoomService _newsRoomService, IMemoriesServices _mermoriesServices, IDonationService _donationService, IProfessionalDetailsService _ProfessionalDetails, ISaluationService _SaluationServices, IEducationalDetailService _educationalDetailService, ICourseCategoryService _courseCategoryService, IFacultyWorkInfoService _facultyWorkInfoService)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, userSelectionServices, userselectionBookingServices, userPaymentservice, _userJobPostingservice, _eventService, _newsRoomService, _mermoriesServices, _donationService, _ProfessionalDetails, _SaluationServices, _educationalDetailService, _courseCategoryService, _facultyWorkInfoService)
        {

        }
        public ActionResult Events(string EvenName)
        {
            if (Session["UserId"] != null)
            {
                if (EvenName != null)
                {
                    MyEventsModel events = new MyEventsModel()
                    {

                        UserPurchasedEvents = GenericMethods.GetAdminEventsforsearch(EvenName)
                    };
                    return View(events);
                }
                else
                {
                    MyEventsModel events = new MyEventsModel()
                    {

                        UserPurchasedEvents = GenericMethods.GetAdminEvents()
                    };
                    return View(events);
                }


            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }

        [HttpPost]
        [ActionName("MyTickets")]
        public ActionResult PrintTickets(int id)
        {
            int userId = Convert.ToInt32(Session["UserId"].ToString());      
            var data =  GenericMethods.UserBookedEvents(userId, Convert.ToInt32(id));

            GridView gridview = new GridView();
            gridview.DataSource = data;
            gridview.DataBind();

            Response.ClearContent();
            Response.Buffer = true;

            Response.AddHeader("content-disposition", "attachment; filename=itfunda.doc"); Response.ContentType = "application/ms-word";
            Response.Charset = "";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    gridview.RenderControl(htw);
                    Response.Flush();
                    Response.End();
                }
            }
            return View();
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


        public JsonResult AuctocompleteEventsearch(string EventTitle)
        {
            var data = GenericMethods.GetAllEventsonserach(EventTitle);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SingleEvent(AlumniEventsModel alumniBookTickets, int? EventId)
        {
            if (alumniBookTickets.EventTypeId == null && alumniBookTickets.TicketsQuantity == null && alumniBookTickets.TicketAmount == null)
            {
                TempData["ErrorMessage"] = "Please select the Tickets and click book now";

                return RedirectToAction("SingleEvent", "AlumniEvents", new { Area = "Alumini", EventId = EventId });
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
                        return RedirectToAction("BookEvent", "AlumniEvents", new { area = "Alumini", id = eventselections.EventSelectionId });
                    }
                }
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public JsonResult GetEventTypesOnId(int EventId)
        {
            var data = GenericMethods.EventTypes(EventId);
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
                return RedirectToAction("Events", "AlumniEvents", new { area = "Alumini" });
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