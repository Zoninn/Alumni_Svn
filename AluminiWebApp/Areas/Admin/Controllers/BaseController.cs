using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public readonly IUserDetailsViewService UserDetailsViewService;
        public readonly IUserInfoService UserInfoService;
        public readonly IEventService EventService;
        public readonly IEventTicketTypesServices EventTicketTypeservices;
        public readonly IGenericMethodsService GenericMetodsservices;
        public readonly IUserPaymentService UserPaymentsService;
        public readonly IUser_JobPostingService UserJobPostingservice;
        public readonly IUserPostService UserPostService;
        public readonly INewsRoomService NewsRoomService;
        public readonly IMemoriesServices Memoriesservices;
        public readonly IDonationService Donationservice;
        public readonly IAlbumGalleryService AlbumGalleryService;

        public BaseController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMethodservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices _memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryService)
        {
            UserDetailsViewService = _userDetailsViewService;
            UserInfoService = _userInfoService;
            EventService = _eventServices;
            EventTicketTypeservices = _eventTicketTypeservices;
            GenericMetodsservices = _genericMethodservices;
            UserPaymentsService = _userPaymentsService;
            UserJobPostingservice = _userJobPostingservice;
            UserPostService = _userPostService;
            NewsRoomService = _newsRoomservice;
            Memoriesservices = _memoriesservices;
            Donationservice = _donationservice;
            AlbumGalleryService = _albumGalleryService;
        }
    }
}