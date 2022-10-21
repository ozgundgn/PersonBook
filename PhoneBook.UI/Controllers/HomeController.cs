using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhoneBook.UI.Models;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ServiceConnectUtils;
using ServiceConnectUtils.BaseModels;
using ServiceConnectUtils.Enums;
using System.Diagnostics;

namespace PhoneBook.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Persons
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PersonList(GetAllPersonsQuery request)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/getall", request);
            if (result != null && result.Object.Count() > 0)
            {
                var res = new { data = result.Object };
                return new JsonResult(res);
            }
            return Ok();
        }

        public IActionResult PersonSave(CreatePersonCommand request)
        {

            if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Surname))
            {
                return BadRequest();
            }
            var apiMetod = request.Id > 0 ? "persons/update" : "persons/create";
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, apiMetod, request);
            return Ok(result);
        }

        public IActionResult PersonDelete(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/delete", new DeleteContactCommand(id));
            return Ok();
        }
        #endregion

        #region Contacts
        public IActionResult Contact(GetAllContactsQuery request)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/getall", request);
            if (result != null && result.Object != null && result.Object.Any())
            {
                ViewBag.Persons = result.Object.Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FullName
                }).ToList();
            }
            return View();
        }
        public IActionResult ContactList(int personId)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/getall", new GetAllContactsQuery());

            if (personId > 0 && result != null && result.Object != null && result.Object.Any())
            {
                var resPersons = new { data = result.Object.Where(x => x.PersonId == personId).ToList() };
                return new JsonResult(resPersons);
            }
            var res = new { data = result.Object };
            return new JsonResult(res);
        }


        public IActionResult ContactSave(CreateContactCommand request)
        {
            if (string.IsNullOrEmpty(request.Location) || string.IsNullOrEmpty(request.PhoneNumber) || request.PersonId <= 0)
            {
                return BadRequest();
            }
            var apiMetod = request.Id > 0 ? "contacts/update" : "contacts/create";
            ServiceConnect.Get(ServiceTypeEnum.ContactService, apiMetod, request);
            return Ok();
        }

        public IActionResult ContactDelete(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/delete", new DeleteContactCommand(id));
            return Ok();
        }
        //#endregion

        //#region Reports
        public IActionResult Report()
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/getall", new GetAllContactsQuery());
            if (result != null && result.Object != null && result.Object.Any())
            {
                var a = result.Object.GroupBy(x => x.Location).Select(grp => grp.ToList());

                ViewBag.Locations = result.Object.GroupBy(x => x.Location).ToList().Select(x => new SelectListItem
                {
                    Value = x.Key,
                    Text = x.Key
                }).ToList();

            }
            return View();
        }

        public IActionResult ReportList(string location)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/getall", new GetAllReportsQuery());
            if (result != null && result.Object != null && result.Object.Any())
            {
                if (!string.IsNullOrWhiteSpace(location))
                {
                    var resReports = new { data = result.Object.Where(x => x.Path == location).ToList() };
                    return new JsonResult(resReports);
                }
                var res = new { data = result.Object };
                return new JsonResult(res);
            }

            return Ok();
        }


        public IActionResult CreateReport(CreateReportCommand request)
        {
            ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/create", request);
            return Ok();
        }


        public IActionResult DeleteReport(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/delete", new DeleteReportCommand(id));
            return Ok();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}