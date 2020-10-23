using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MeritPay.Core.DTOs;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MeritPay.Web.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IPersonService _personService;
        private readonly IMeritPayFactorService _meritPayFactorService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ReportController(ILogger<HomeController> logger, IPersonService personService, IReportService reportService, IMeritPayFactorService meritPayFactorService, IMapper mapper)
        {
            _logger = logger;
            _reportService = reportService;
            _personService = personService;
            _meritPayFactorService = meritPayFactorService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ReportDataOutput> GetReportData()
        {
            try
            {
                _logger.LogInformation("اجرای متد بازیابی اطلاعات کارنامه");
                var personData = await _personService.GetPersonByUesrIdAsync(2207);
                var reportData = await _reportService.GetReportByPersonCodeAsync(1, personData.PersonCode);
                //var result = _mapper.Map<Person, ReportDataVM>(personData);
                ReportDataVM dataVM = new ReportDataVM();
                dataVM.PersonCode = personData.PersonCode;
                dataVM.FirstName = personData.FirstName;
                dataVM.LastName = personData.LastName;
                dataVM.BirthDate = personData.BirthDate;
                dataVM.EmployeeDate = personData.EmployeeDate;
                dataVM.StudyBranch = personData.StudyBranch;
                dataVM.StudyJob = personData.StudyJob;
                dataVM.Grade = personData.Grade;

                foreach (var item in reportData)
                {
                    var txt = await _meritPayFactorService.GetMeritPayByIdAsync(item.MeritPayFactorId);
                    switch (txt.Title)
                    {
                        case "تراکنش های عملکردی":
                            dataVM.TaScore = item.Score;
                            dataVM.TaRankInBranch = item.RankInBranch;
                            dataVM.TaRankInZone = item.RankInZone;
                            dataVM.TaRankInBank = item.RankInBank;
                            break;
                        case "امتیاز گروهی":
                            dataVM.GroupScore = item.Score;
                            dataVM.GroupRankInBranch = item.RankInBranch;
                            dataVM.GroupRankInZone = item.RankInZone;
                            dataVM.GroupRankInBank = item.RankInBank;
                            break;
                        case "ارزشیابی فردی":
                            dataVM.ArzeshScore = item.Score;
                            dataVM.ArzeshRankInBranch = item.RankInBranch;
                            dataVM.ArzeshRankInZone = item.RankInZone;
                            dataVM.ArzeshRankInBank = item.RankInBank;
                            break;
                        default:
                            break;
                    }

                }
                return new ReportDataOutput(true, dataVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"خطا در بازیابی اطلاعات کارنامه");
                return new ReportDataOutput(false, null, "خطا در بازیابی اطلاعات کارنامه");
            }
        }

    }
}
