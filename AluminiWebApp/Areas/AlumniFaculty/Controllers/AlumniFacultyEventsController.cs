using Alumini.Core;
using AluminiService.Interfaces;
using AluminiWebApp.Areas.Alumini.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.AlumniFaculty.Controllers
{
    public class AlumniFacultyEventsController : BaseController
    {
        public AlumniFacultyEventsController(IUserDetailsViewService userDetailsViewService, IUserInfoService userInfoService, IEventCategoryService eventCategoryservices, IUserPostService userpostService, IUserPostPicturesService userPostPictureservice, IUserPostVisibleService userPostVisibleServices, IGenericMethodsService genericMethods, ICourseCategoryService CategoryServices, IUserSelectionEventsService userSelectionServices, IUserselectionBookingsService userselectionBookingServices, IUserPaymentService userPaymentservice)
            : base(userDetailsViewService, userInfoService, eventCategoryservices, userpostService, userPostPictureservice, userPostVisibleServices, genericMethods, CategoryServices, userSelectionServices, userselectionBookingServices, userPaymentservice)
        {

        }
        public ActionResult Events()
        {
            if (Session["UserId"] != null)
            {
                ViewBag.GetEvents = GenericMethods.GetAdminEvents();

                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpGet]
        public ActionResult SingleEvent(int? id)
        {
            if (Session["UserId"] != null)
            {
                if (id != null) ViewBag.EventsOnId = GenericMethods.GetUserEvents(id.Value);
                return View();
            }
            return RedirectToAction(LoginPages.Login, LoginPages.Account, new { area = "" });
        }
        [HttpPost]
        public ActionResult SingleEvent(AlumniEventsModel alumniBookTickets, int id)
        {
            if (alumniBookTickets.EventTypeId == null && alumniBookTickets.TicketsQuantity == null && alumniBookTickets.TicketAmount == null)
            {
                TempData["ErrorMessage"] = "Please select Tickets";
                return RedirectToAction("Events", "AlumniFacultyEvents", new { area = "AlumniFaculty" });
            }
            else
            {
                if (Session["UserId"] != null)
                {
                    Event_UserSelections eventselections = new Event_UserSelections()
                    {
                        EventId = id,
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
                        return RedirectToAction("BookEvent", "AlumniFacultyEvents", new { area = "AlumniFaculty", id = eventselections.EventSelectionId });
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
                return RedirectToAction("Events", "AlumniFacultyEvents", new { area = "AlumniFaculty" });
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
    }
}