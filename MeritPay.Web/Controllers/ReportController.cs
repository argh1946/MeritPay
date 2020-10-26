using System;
using System.Collections.Generic;
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
        private readonly IPersonArzeshyabiService _personArzeshyabiService;
        private readonly IPersonScoreService _personScoreService;
        private readonly IMeritPayFactorService _meritPayFactorService;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public ReportController(ILogger<HomeController> logger, IPersonArzeshyabiService personArzeshyabiService, IPersonScoreService personScoreService,
            IReportService reportService, IMeritPayFactorService meritPayFactorService, IMapper mapper)
        {
            _logger = logger;
            _reportService = reportService;
            _personArzeshyabiService = personArzeshyabiService;
            _personScoreService = personScoreService;
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
                int periodId = 1;

                _logger.LogInformation("اجرای متد بازیابی اطلاعات کارنامه");
                var personData = await _personArzeshyabiService.GetPersonArzeshyabiByPersonCodeAsync(periodId, 13137);
                //var result = _mapper.Map<Person, ReportDataVM>(personData);
                ReportDataVM dataVM = new ReportDataVM();
                dataVM.PersonCode = personData.PersonInBranch.Person.PersonCode;
                dataVM.FirstName = personData.PersonInBranch.Person.FirstName;
                dataVM.LastName = personData.PersonInBranch.Person.LastName;
                dataVM.BirthDate = personData.PersonInBranch.Person.BirthDate;
                dataVM.EmployeeDate = personData.PersonInBranch.Person.EmployeeDate;
                dataVM.StudyBranch = personData.PersonInBranch.Person.StudyBranch;
                dataVM.StudyJob = personData.PersonInBranch.Person.StudyJob;
                dataVM.Grade = personData.PersonInBranch.Person.Grade;
                dataVM.BranchCode = personData.PersonInBranch.Branch.BranchCode;
                dataVM.BranchName = personData.PersonInBranch.Branch.BranchName;
                dataVM.ZoneName = personData.PersonInBranch.Branch.ZoneName;
                dataVM.Arzyab1 = personData.Arzyab1.ToString();
                dataVM.Arzyab2 = personData.Arzyab2.ToString();
                dataVM.MaghtaArzeshyabi = personData.ArzyabiDate;

                var reportTaData = await _personScoreService.GetPersonScoreByPersonCodeAsync(periodId, personData.PersonInBranch.Person.PersonCode);
                List<ReportTaDataVM> listTa = new List<ReportTaDataVM>();
                foreach (var item in reportTaData)
                {
                    ReportTaDataVM ta = new ReportTaDataVM();
                    ta.ScoreIndexTitle = item.ScoreSubIndex.ScoreIndex.Title;
                    ta.ScoreSubIndexTitle = item.ScoreSubIndex.Title;
                    ta.Value = item.Value;
                    ta.Score = item.Score;
                    ta.RankInBranch = item.RankInBranch;
                    ta.RankInZone = item.RankInZone;
                    ta.RankInBank = item.RankInBank;
                    listTa.Add(ta);
                }
                dataVM.ReportTaDataList = listTa;

                var reportData = await _reportService.GetReportByPersonCodeAsync(periodId, personData.PersonInBranch.Person.PersonCode);
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
                _logger.LogError(ex, "خطا در بازیابی اطلاعات کارنامه");
                return new ReportDataOutput(false, null, "خطا در بازیابی اطلاعات کارنامه");
            }
        }

    }
}
