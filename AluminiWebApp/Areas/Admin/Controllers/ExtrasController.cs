using Alumini.Core;
using AluminiService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AluminiWebApp.Areas.Admin.Controllers
{
    public class ExtrasController : BaseController
    {
        public ExtrasController(IUserDetailsViewService _userDetailsViewService, IUserInfoService _userInfoService, IEventService _eventServices, IEventTicketTypesServices _eventTicketTypeservices, IGenericMethodsService _genericMetodsservices, IUserPaymentService _userPaymentsService, IUser_JobPostingService _userJobPostingservice, IUserPostService _userPostService, INewsRoomService _newsRoomservice, IMemoriesServices memoriesservices, IDonationService _donationservice, IAlbumGalleryService _albumGalleryserice)
            : base(_userDetailsViewService, _userInfoService, _eventServices, _eventTicketTypeservices, _genericMetodsservices, _userPaymentsService, _userJobPostingservice, _userPostService, _newsRoomservice, memoriesservices, _donationservice, _albumGalleryserice)
        {

        }
        public ActionResult EditTemplate()
        {
            TemplateModel Model = new TemplateModel()
            {
                GetTemplates = GenericMetodsservices.GetTemplates()
            };

            return View(Model);
        }

        public JsonResult GetTemplateonid(int id)
        {
            var Data = GenericMetodsservices.GetTmplatesonid(id);
            return Json(Data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TemplateEdit()
        {

            return View();
        }
        [HttpPost]
        public ActionResult EditTemplate(TemplateModel Model, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {


                var FilePath = "";
                if (Image != null)
                {
                    var fileName = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/UserProfilePictures/" + fileName));
                    Image.SaveAs(path);
                    FilePath = "/UserProfilePictures/" + fileName;
                }
                if (Model.Templateid != null)
                {
                    Custome_Templates custom = new Custome_Templates()
                    {
                        Heading = Model.Heading,
                        Subject = Model.Subject,
                        Description = Model.Description,
                        Status = true,
                        createdOn = DateTime.Now,
                        Images = FilePath,
                        id = Model.Templateid
                    };
                    TempData["Message"] = "Updated successfully...";
                    GenericMetodsservices.InsertTemplates(custom);
                }
                else
                {
                    Custome_Templates custom = new Custome_Templates()
                    {
                        Heading = Model.Heading,
                        Subject = Model.Subject,
                        Description = Model.Description,
                        Status = true,
                        createdOn = DateTime.Now,
                        Images = FilePath
                    };
                    TempData["Message"] = "Saved successfully...";
                    GenericMetodsservices.InsertTemplates(custom);
                }

                return RedirectToAction("EditTemplate", "Extras", new { Area = "Admin" });
            }
            return View();
        }

    }
    public partial class TemplateModel
    {
        [Required]
        public string Heading { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Description { get; set; }
        public string Image { get; set; }
        public List<Custome_Templates> GetTemplates { get; set; }
        public int Templateid { get; set; }
    }
}