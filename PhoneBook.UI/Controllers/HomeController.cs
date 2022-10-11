using ContactService.Application.Contacts.Commands;
using ContactService.Application.Contacts.Queries;
using ContactService.Application.Persons.Commands;
using ContactService.Application.Persons.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PhoneBook.UI.Models;
using ReportService.Application.Reports.Commands;
using ReportService.Application.Reports.Queries;
using ServiceConnectUtils;
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

        public IActionResult PersonList()
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/getall");
            if (result != null && result.Count() > 0)
            {
                var persons = JsonConvert.DeserializeObject<List<PersonDto>>(result);
                var res = new { data = persons };
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
            HttpMethod method = request.Id > 0 ? HttpMethod.Put : HttpMethod.Post;
            ServiceConnect.Get(ServiceTypeEnum.ContactService, apiMetod, method, request);
            return Ok();
        }

        public IActionResult PersonDelete(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/" + id, HttpMethod.Delete, id);
            return Ok();
        }
        #endregion

        #region Contacts
        public IActionResult Contact()
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "persons/getall");
            if (result.Any())
            {
                var persons = JsonConvert.DeserializeObject<List<PersonDto>>(result);
                if (persons != null && persons.Count() > 0)
                {
                    ViewBag.Persons = persons.Select(x => new SelectListItem
                    {
                        Value = x.Id.ToString(),
                        Text = x.FullName
                    }).ToList();
                }
            }
            return View();
        }
        public IActionResult ContactList(int personId)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/getall");
            if (result.Any())
            {
                var contacts = JsonConvert.DeserializeObject<List<ContactDto>>(result);
                if (personId > 0 && contacts != null && contacts.Any())
                {
                    var resPersons = new { data = contacts.Where(x => x.PersonId == personId).ToList() };
                    return new JsonResult(resPersons);
                }
                var res = new { data = contacts };
                return new JsonResult(res);
            }

            return Ok();
        }
        public IActionResult ContactSave(CreateContactCommand request)
        {
            if (string.IsNullOrEmpty(request.Location) || string.IsNullOrEmpty(request.PhoneNumber) || request.PersonId <= 0)
            {
                return BadRequest();
            }
            var apiMetod = request.Id > 0 ? "contacts/update" : "contacts/create";
            HttpMethod method = request.Id > 0 ? HttpMethod.Put : HttpMethod.Post;
            ServiceConnect.Get(ServiceTypeEnum.ContactService, apiMetod, method, request);

            return Ok();
        }

        public IActionResult ContactDelete(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/" + id, HttpMethod.Delete, id);
            return Ok();
        }
        #endregion

        #region Reports
        public IActionResult Report()
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ContactService, "contacts/getall");
            if (result.Any())
            {
                var locations = JsonConvert.DeserializeObject<List<ContactDto>>(result);
                if (locations != null && locations.Count() > 0)
                {

                    var a = locations.GroupBy(x => x.Location).Select(grp => grp.ToList());

                    ViewBag.Locations = locations.GroupBy(x => x.Location).ToList().Select(x => new SelectListItem
                    {
                        Value = x.Key,
                        Text = x.Key
                    }).ToList();
                }
            }
            return View();
        }

        public IActionResult ReportList(string location)
        {
            var result = ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/getall");
            if (result.Any())
            {
                var reports = JsonConvert.DeserializeObject<List<ReportDto>>(result);
                if (!string.IsNullOrWhiteSpace(location))
                {
                    var resReports = new { data = reports.Where(x => x.Path == location).ToList() };
                    return new JsonResult(resReports);
                }
                var res = new { data = reports };
                return new JsonResult(res);
            }

            return Ok();
        }


        public IActionResult CreateReport(CreateReportCommand request)
        {
            ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/create", HttpMethod.Post, request);
            return Ok();
        }


        public IActionResult DeleteReport(int id)
        {
            if (id <= 0)
                return BadRequest();

            ServiceConnect.Get(ServiceTypeEnum.ReportService, "reports/" + id, HttpMethod.Delete, id);
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