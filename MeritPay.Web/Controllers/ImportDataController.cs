using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MeritPay.Core.DTOs;
using MeritPay.Core.UseCases.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace MeritPay.Web.Controllers
{
    public class ImportDataController : BaseController
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IImportDataService _importDataService;
        private readonly ILogger _logger;

        public ImportDataController(IWebHostEnvironment hostingEnvironment, ILogger<HomeController> logger, IImportDataService importData)
        {
            _hostingEnvironment = hostingEnvironment;
            _importDataService = importData;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        private async Task<List<string>> ReadExcel(IFormFile formFile)
        {
            List<string> errorList = new List<string>();
            var list = new List<ImportDataInputVM>();
            using (var stream = new MemoryStream())
            {
                await formFile.CopyToAsync(stream);
                using var package = new ExcelPackage(stream);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 3; row <= rowCount; row++)
                {
                    try
                    {
                        list.Add(new ImportDataInputVM
                        {
                            PersonCode = worksheet.Cells[row, 1].Value != null ? int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()) : 0,
                            BranchCode = worksheet.Cells[row, 4].Value != null ? int.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()) : 0,

                            TaScore = worksheet.Cells[row, 6].Value != null ? int.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()) : 0,
                            TaRankInBranch = worksheet.Cells[row, 7].Value != null ? int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()) : 0,
                            TaRankInZone = worksheet.Cells[row, 8].Value != null ? int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()) : 0,
                            TaRankInBank = worksheet.Cells[row, 9].Value != null ? int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim()) : 0,

                            GroupScore = worksheet.Cells[row, 10].Value != null ? int.Parse(worksheet.Cells[row, 10].Value.ToString().Trim()) : 0,
                            GroupRankInBranch = worksheet.Cells[row, 11].Value != null ? int.Parse(worksheet.Cells[row, 11].Value.ToString().Trim()) : 0,
                            GroupRankInZone = worksheet.Cells[row, 12].Value != null ? int.Parse(worksheet.Cells[row, 12].Value.ToString().Trim()) : 0,
                            GroupRankInBank = worksheet.Cells[row, 13].Value != null ? int.Parse(worksheet.Cells[row, 13].Value.ToString().Trim()) : 0,

                            ArzeshScore = worksheet.Cells[row, 14].Value != null ? int.Parse(worksheet.Cells[row, 14].Value.ToString().Trim()) : 0,
                            ArzeshRankInBranch = worksheet.Cells[row, 15].Value != null ? int.Parse(worksheet.Cells[row, 15].Value.ToString().Trim()) : 0,
                            ArzeshRankInZone = worksheet.Cells[row, 16].Value != null ? int.Parse(worksheet.Cells[row, 16].Value.ToString().Trim()) : 0,
                            ArzeshRankInBank = worksheet.Cells[row, 17].Value != null ? int.Parse(worksheet.Cells[row, 17].Value.ToString().Trim()) : 0,

                            TotalScore = worksheet.Cells[row, 18].Value != null ? int.Parse(worksheet.Cells[row, 18].Value.ToString().Trim()) : 0,
                            TotalRankInBranch = worksheet.Cells[row, 19].Value != null ? int.Parse(worksheet.Cells[row, 19].Value.ToString().Trim()) : 0,
                            TotalRankInZone = worksheet.Cells[row, 20].Value != null ? int.Parse(worksheet.Cells[row, 20].Value.ToString().Trim()) : 0,
                            TotalRankInBank = worksheet.Cells[row, 21].Value != null ? int.Parse(worksheet.Cells[row, 21].Value.ToString().Trim()) : 0,

                            TajhizManabeArzan = worksheet.Cells[row, 22].Value != null ? decimal.Parse(worksheet.Cells[row, 22].Value.ToString().Trim()) : 0,
                            TajhizManabeGeran = worksheet.Cells[row, 23].Value != null ? decimal.Parse(worksheet.Cells[row, 23].Value.ToString().Trim()) : 0,

                            TakhsisManabeTedadTashilat = worksheet.Cells[row, 24].Value != null ? int.Parse(worksheet.Cells[row, 24].Value.ToString().Trim()) : 0,
                            TakhsisManabeMablaghTashilat = worksheet.Cells[row, 25].Value != null ? decimal.Parse(worksheet.Cells[row, 25].Value.ToString().Trim()) : 0,
                            TakhsisManabeTedadZSemanat = worksheet.Cells[row, 26].Value != null ? int.Parse(worksheet.Cells[row, 26].Value.ToString().Trim()) : 0,
                            TakhsisManabeMablaghZemanat = worksheet.Cells[row, 27].Value != null ? decimal.Parse(worksheet.Cells[row, 27].Value.ToString().Trim()) : 0,
                            TakhsisManabeEtebaratAsnadi = worksheet.Cells[row, 28].Value != null ? decimal.Parse(worksheet.Cells[row, 28].Value.ToString().Trim()) : 0,

                            DaramadKarmozdi = worksheet.Cells[row, 29].Value != null ? int.Parse(worksheet.Cells[row, 29].Value.ToString().Trim()) : 0,

                            BankdariElectronicKartHadiye = worksheet.Cells[row, 30].Value != null ? int.Parse(worksheet.Cells[row, 30].Value.ToString().Trim()) : 0,
                            BankdariElectronicIranKart = worksheet.Cells[row, 31].Value != null ? int.Parse(worksheet.Cells[row, 31].Value.ToString().Trim()) : 0,
                            BankdariElectronicKilid = worksheet.Cells[row, 32].Value != null ? int.Parse(worksheet.Cells[row, 32].Value.ToString().Trim()) : 0,

                            SayerAsnadNaghdiVaGheirNaghdi = worksheet.Cells[row, 33].Value != null ? int.Parse(worksheet.Cells[row, 33].Value.ToString().Trim()) : 0,
                            SayerAsnadBarghashti = worksheet.Cells[row, 34].Value != null ? int.Parse(worksheet.Cells[row, 34].Value.ToString().Trim()) : 0,
                            SayerTarifMoshtari = worksheet.Cells[row, 35].Value != null ? int.Parse(worksheet.Cells[row, 35].Value.ToString().Trim()) : 0,
                            SayerEftetahHesab = worksheet.Cells[row, 36].Value != null ? int.Parse(worksheet.Cells[row, 36].Value.ToString().Trim()) : 0,
                            SayerBakhshnameKhani = worksheet.Cells[row, 37].Value != null ? int.Parse(worksheet.Cells[row, 37].Value.ToString().Trim()) : 0,
                            SayerTakhirVaTajil = worksheet.Cells[row, 38].Value != null ? int.Parse(worksheet.Cells[row, 38].Value.ToString().Trim()) : 0,

                            ArzanRankInBranch = worksheet.Cells[row, 39].Value != null ? int.Parse(worksheet.Cells[row, 39].Value.ToString().Trim()) : 0,
                            ArzanRankInZone = worksheet.Cells[row, 40].Value != null ? int.Parse(worksheet.Cells[row, 40].Value.ToString().Trim()) : 0,
                            ArzanRankInBank = worksheet.Cells[row, 41].Value != null ? int.Parse(worksheet.Cells[row, 41].Value.ToString().Trim()) : 0,

                            GeranRankInBranch = worksheet.Cells[row, 42].Value != null ? int.Parse(worksheet.Cells[row, 42].Value.ToString().Trim()) : 0,
                            GeranRankInZone = worksheet.Cells[row, 43].Value != null ? int.Parse(worksheet.Cells[row, 43].Value.ToString().Trim()) : 0,
                            GeranRankInBank = worksheet.Cells[row, 44].Value != null ? int.Parse(worksheet.Cells[row, 44].Value.ToString().Trim()) : 0,

                            TedadTashilatRankInBranch = worksheet.Cells[row, 45].Value != null ? int.Parse(worksheet.Cells[row, 45].Value.ToString().Trim()) : 0,
                            TedadTashilatRankInZone = worksheet.Cells[row, 46].Value != null ? int.Parse(worksheet.Cells[row, 46].Value.ToString().Trim()) : 0,
                            TedadTashilatRankInBank = worksheet.Cells[row, 47].Value != null ? int.Parse(worksheet.Cells[row, 47].Value.ToString().Trim()) : 0,

                            MablaghTashilatRankInBranch = worksheet.Cells[row, 48].Value != null ? int.Parse(worksheet.Cells[row, 48].Value.ToString().Trim()) : 0,
                            MablaghTashilatRankInZone = worksheet.Cells[row, 49].Value != null ? int.Parse(worksheet.Cells[row, 49].Value.ToString().Trim()) : 0,
                            MablaghTashilatRankInBank = worksheet.Cells[row, 50].Value != null ? int.Parse(worksheet.Cells[row, 50].Value.ToString().Trim()) : 0,

                            TedadZemantRankInBranch = worksheet.Cells[row, 51].Value != null ? int.Parse(worksheet.Cells[row, 51].Value.ToString().Trim()) : 0,
                            TedadZemantRankInZone = worksheet.Cells[row, 52].Value != null ? int.Parse(worksheet.Cells[row, 52].Value.ToString().Trim()) : 0,
                            TedadZemantRankInBank = worksheet.Cells[row, 53].Value != null ? int.Parse(worksheet.Cells[row, 53].Value.ToString().Trim()) : 0,

                            MablaghZemantRankInBranch = worksheet.Cells[row, 54].Value != null ? int.Parse(worksheet.Cells[row, 54].Value.ToString().Trim()) : 0,
                            MablaghZemantRankInZone = worksheet.Cells[row, 55].Value != null ? int.Parse(worksheet.Cells[row, 55].Value.ToString().Trim()) : 0,
                            MablaghZemantRankInBank = worksheet.Cells[row, 56].Value != null ? int.Parse(worksheet.Cells[row, 56].Value.ToString().Trim()) : 0,

                            EtebaratAsnadiRankInBranch = worksheet.Cells[row, 57].Value != null ? int.Parse(worksheet.Cells[row, 57].Value.ToString().Trim()) : 0,
                            EtebaratAsnadiRankInZone = worksheet.Cells[row, 58].Value != null ? int.Parse(worksheet.Cells[row, 58].Value.ToString().Trim()) : 0,
                            EtebaratAsnadiRankInBank = worksheet.Cells[row, 59].Value != null ? int.Parse(worksheet.Cells[row, 59].Value.ToString().Trim()) : 0,

                            DaramadKarmozdiRankInBranch = worksheet.Cells[row, 60].Value != null ? int.Parse(worksheet.Cells[row, 60].Value.ToString().Trim()) : 0,
                            DaramadKarmozdiRankInZone = worksheet.Cells[row, 61].Value != null ? int.Parse(worksheet.Cells[row, 61].Value.ToString().Trim()) : 0,
                            DaramadKarmozdiRankInBank = worksheet.Cells[row, 62].Value != null ? int.Parse(worksheet.Cells[row, 62].Value.ToString().Trim()) : 0,

                            KartHadiyeRankInBranch = worksheet.Cells[row, 63].Value != null ? int.Parse(worksheet.Cells[row, 63].Value.ToString().Trim()) : 0,
                            KartHadiyeRankInZone = worksheet.Cells[row, 64].Value != null ? int.Parse(worksheet.Cells[row, 64].Value.ToString().Trim()) : 0,
                            KartHadiyeRankInBank = worksheet.Cells[row, 65].Value != null ? int.Parse(worksheet.Cells[row, 65].Value.ToString().Trim()) : 0,

                            IranKartRankInBranch = worksheet.Cells[row, 66].Value != null ? int.Parse(worksheet.Cells[row, 66].Value.ToString().Trim()) : 0,
                            IranKartRankInZone = worksheet.Cells[row, 67].Value != null ? int.Parse(worksheet.Cells[row, 67].Value.ToString().Trim()) : 0,
                            IranKartRankInBank = worksheet.Cells[row, 68].Value != null ? int.Parse(worksheet.Cells[row, 68].Value.ToString().Trim()) : 0,

                            KilidRankInBranch = worksheet.Cells[row, 69].Value != null ? int.Parse(worksheet.Cells[row, 69].Value.ToString().Trim()) : 0,
                            KilidRankInZone = worksheet.Cells[row, 70].Value != null ? int.Parse(worksheet.Cells[row, 70].Value.ToString().Trim()) : 0,
                            KilidRankInBank = worksheet.Cells[row, 71].Value != null ? int.Parse(worksheet.Cells[row, 71].Value.ToString().Trim()) : 0,

                            AsnadNaghdiVaGhirNaghdiRankInBranch = worksheet.Cells[row, 72].Value != null ? int.Parse(worksheet.Cells[row, 72].Value.ToString().Trim()) : 0,
                            AsnadNaghdiVaGhirNaghdiRankInZone = worksheet.Cells[row, 73].Value != null ? int.Parse(worksheet.Cells[row, 73].Value.ToString().Trim()) : 0,
                            AsnadNaghdiVaGhirNaghdiRankInBank = worksheet.Cells[row, 74].Value != null ? int.Parse(worksheet.Cells[row, 74].Value.ToString().Trim()) : 0,

                            AsnadBarghashtiRankInBranch = worksheet.Cells[row, 75].Value != null ? int.Parse(worksheet.Cells[row, 75].Value.ToString().Trim()) : 0,
                            AsnadBarghashtiRankInZone = worksheet.Cells[row, 76].Value != null ? int.Parse(worksheet.Cells[row, 76].Value.ToString().Trim()) : 0,
                            AsnadBarghashtiRankInBank = worksheet.Cells[row, 77].Value != null ? int.Parse(worksheet.Cells[row, 77].Value.ToString().Trim()) : 0,

                            TarifMoshtariRankInBranch = worksheet.Cells[row, 78].Value != null ? int.Parse(worksheet.Cells[row, 78].Value.ToString().Trim()) : 0,
                            TarifMoshtariRankInZone = worksheet.Cells[row, 79].Value != null ? int.Parse(worksheet.Cells[row, 79].Value.ToString().Trim()) : 0,
                            TarifMoshtariRankInBank = worksheet.Cells[row, 80].Value != null ? int.Parse(worksheet.Cells[row, 80].Value.ToString().Trim()) : 0,

                            EftetahHesabRankInBranch = worksheet.Cells[row, 81].Value != null ? int.Parse(worksheet.Cells[row, 81].Value.ToString().Trim()) : 0,
                            EftetahHesabRankInZone = worksheet.Cells[row, 82].Value != null ? int.Parse(worksheet.Cells[row, 82].Value.ToString().Trim()) : 0,
                            EftetahHesabRankInBank = worksheet.Cells[row, 83].Value != null ? int.Parse(worksheet.Cells[row, 83].Value.ToString().Trim()) : 0,

                            BakhshnameKhaniRankInBranch = worksheet.Cells[row, 84].Value != null ? int.Parse(worksheet.Cells[row, 84].Value.ToString().Trim()) : 0,
                            BakhshnameKhaniRankInZone = worksheet.Cells[row, 85].Value != null ? int.Parse(worksheet.Cells[row, 85].Value.ToString().Trim()) : 0,
                            BakhshnameKhaniRankInBank = worksheet.Cells[row, 86].Value != null ? int.Parse(worksheet.Cells[row, 86].Value.ToString().Trim()) : 0,

                            TakhirVaTajilRankInBranch = worksheet.Cells[row, 87].Value != null ? int.Parse(worksheet.Cells[row, 87].Value.ToString().Trim()) : 0,
                            TakhirVaTajilRankInZone = worksheet.Cells[row, 88].Value != null ? int.Parse(worksheet.Cells[row, 88].Value.ToString().Trim()) : 0,
                            TakhirVaTajilRankInBank = worksheet.Cells[row, 89].Value != null ? int.Parse(worksheet.Cells[row, 89].Value.ToString().Trim()) : 0,
                        });
                        errorList.Add("");
                    }
                    catch (Exception)
                    {
                        errorList.Add(string.Format("داده ردیف {0} دارای اطلاعات ناصحیح می باشد", row));
                    }
                }
            }
            return errorList;
        }

        [HttpPost]
        public async Task<ImportDataOutput> SaveFile()
        {
            try
            {
                _logger.LogInformation("اجرای متد ثبت فایل اکسل");
                var list = new List<ImportDataInputVM>();
                using (var stream = new MemoryStream())
                {
                    using FileStream file = new FileStream(TempData["FileName"].ToString(), FileMode.Open, FileAccess.Read);
                    await file.CopyToAsync(stream);
                    using var package = new ExcelPackage(stream);
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 3; row <= rowCount; row++)
                    {
                        list.Add(new ImportDataInputVM
                        {
                            PersonCode = worksheet.Cells[row, 1].Value != null ? int.Parse(worksheet.Cells[row, 1].Value.ToString().Trim()) : 0,
                            BranchCode = worksheet.Cells[row, 4].Value != null ? int.Parse(worksheet.Cells[row, 4].Value.ToString().Trim()) : 0,

                            TaScore = worksheet.Cells[row, 6].Value != null ? int.Parse(worksheet.Cells[row, 6].Value.ToString().Trim()) : 0,
                            TaRankInBranch = worksheet.Cells[row, 7].Value != null ? int.Parse(worksheet.Cells[row, 7].Value.ToString().Trim()) : 0,
                            TaRankInZone = worksheet.Cells[row, 8].Value != null ? int.Parse(worksheet.Cells[row, 8].Value.ToString().Trim()) : 0,
                            TaRankInBank = worksheet.Cells[row, 9].Value != null ? int.Parse(worksheet.Cells[row, 9].Value.ToString().Trim()) : 0,

                            GroupScore = worksheet.Cells[row, 10].Value != null ? int.Parse(worksheet.Cells[row, 10].Value.ToString().Trim()) : 0,
                            GroupRankInBranch = worksheet.Cells[row, 11].Value != null ? int.Parse(worksheet.Cells[row, 11].Value.ToString().Trim()) : 0,
                            GroupRankInZone = worksheet.Cells[row, 12].Value != null ? int.Parse(worksheet.Cells[row, 12].Value.ToString().Trim()) : 0,
                            GroupRankInBank = worksheet.Cells[row, 13].Value != null ? int.Parse(worksheet.Cells[row, 13].Value.ToString().Trim()) : 0,

                            ArzeshScore = worksheet.Cells[row, 14].Value != null ? int.Parse(worksheet.Cells[row, 14].Value.ToString().Trim()) : 0,
                            ArzeshRankInBranch = worksheet.Cells[row, 15].Value != null ? int.Parse(worksheet.Cells[row, 15].Value.ToString().Trim()) : 0,
                            ArzeshRankInZone = worksheet.Cells[row, 16].Value != null ? int.Parse(worksheet.Cells[row, 16].Value.ToString().Trim()) : 0,
                            ArzeshRankInBank = worksheet.Cells[row, 17].Value != null ? int.Parse(worksheet.Cells[row, 17].Value.ToString().Trim()) : 0,

                            TotalScore = worksheet.Cells[row, 18].Value != null ? int.Parse(worksheet.Cells[row, 18].Value.ToString().Trim()) : 0,
                            TotalRankInBranch = worksheet.Cells[row, 19].Value != null ? int.Parse(worksheet.Cells[row, 19].Value.ToString().Trim()) : 0,
                            TotalRankInZone = worksheet.Cells[row, 20].Value != null ? int.Parse(worksheet.Cells[row, 20].Value.ToString().Trim()) : 0,
                            TotalRankInBank = worksheet.Cells[row, 21].Value != null ? int.Parse(worksheet.Cells[row, 21].Value.ToString().Trim()) : 0,

                            TajhizManabeArzan = worksheet.Cells[row, 22].Value != null ? decimal.Parse(worksheet.Cells[row, 22].Value.ToString().Trim()) : 0,
                            TajhizManabeGeran = worksheet.Cells[row, 23].Value != null ? decimal.Parse(worksheet.Cells[row, 23].Value.ToString().Trim()) : 0,

                            TakhsisManabeTedadTashilat = worksheet.Cells[row, 24].Value != null ? int.Parse(worksheet.Cells[row, 24].Value.ToString().Trim()) : 0,
                            TakhsisManabeMablaghTashilat = worksheet.Cells[row, 25].Value != null ? decimal.Parse(worksheet.Cells[row, 25].Value.ToString().Trim()) : 0,
                            TakhsisManabeTedadZSemanat = worksheet.Cells[row, 26].Value != null ? int.Parse(worksheet.Cells[row, 26].Value.ToString().Trim()) : 0,
                            TakhsisManabeMablaghZemanat = worksheet.Cells[row, 27].Value != null ? decimal.Parse(worksheet.Cells[row, 27].Value.ToString().Trim()) : 0,
                            TakhsisManabeEtebaratAsnadi = worksheet.Cells[row, 28].Value != null ? decimal.Parse(worksheet.Cells[row, 28].Value.ToString().Trim()) : 0,

                            DaramadKarmozdi = worksheet.Cells[row, 29].Value != null ? int.Parse(worksheet.Cells[row, 29].Value.ToString().Trim()) : 0,

                            BankdariElectronicKartHadiye = worksheet.Cells[row, 30].Value != null ? int.Parse(worksheet.Cells[row, 30].Value.ToString().Trim()) : 0,
                            BankdariElectronicIranKart = worksheet.Cells[row, 31].Value != null ? int.Parse(worksheet.Cells[row, 31].Value.ToString().Trim()) : 0,
                            BankdariElectronicKilid = worksheet.Cells[row, 32].Value != null ? int.Parse(worksheet.Cells[row, 32].Value.ToString().Trim()) : 0,

                            SayerAsnadNaghdiVaGheirNaghdi = worksheet.Cells[row, 33].Value != null ? int.Parse(worksheet.Cells[row, 33].Value.ToString().Trim()) : 0,
                            SayerAsnadBarghashti = worksheet.Cells[row, 34].Value != null ? int.Parse(worksheet.Cells[row, 34].Value.ToString().Trim()) : 0,
                            SayerTarifMoshtari = worksheet.Cells[row, 35].Value != null ? int.Parse(worksheet.Cells[row, 35].Value.ToString().Trim()) : 0,
                            SayerEftetahHesab = worksheet.Cells[row, 36].Value != null ? int.Parse(worksheet.Cells[row, 36].Value.ToString().Trim()) : 0,
                            SayerBakhshnameKhani = worksheet.Cells[row, 37].Value != null ? int.Parse(worksheet.Cells[row, 37].Value.ToString().Trim()) : 0,
                            SayerTakhirVaTajil = worksheet.Cells[row, 38].Value != null ? int.Parse(worksheet.Cells[row, 38].Value.ToString().Trim()) : 0,

                            ArzanScore = worksheet.Cells[row, 39].Value != null ? int.Parse(worksheet.Cells[row, 39].Value.ToString().Trim()) : 0,
                            ArzanRankInBranch = worksheet.Cells[row, 40].Value != null ? int.Parse(worksheet.Cells[row, 40].Value.ToString().Trim()) : 0,
                            ArzanRankInZone = worksheet.Cells[row, 41].Value != null ? int.Parse(worksheet.Cells[row, 41].Value.ToString().Trim()) : 0,
                            ArzanRankInBank = worksheet.Cells[row, 42].Value != null ? int.Parse(worksheet.Cells[row, 42].Value.ToString().Trim()) : 0,

                            GeranScore = worksheet.Cells[row, 43].Value != null ? int.Parse(worksheet.Cells[row, 43].Value.ToString().Trim()) : 0,
                            GeranRankInBranch = worksheet.Cells[row, 44].Value != null ? int.Parse(worksheet.Cells[row, 44].Value.ToString().Trim()) : 0,
                            GeranRankInZone = worksheet.Cells[row, 45].Value != null ? int.Parse(worksheet.Cells[row, 45].Value.ToString().Trim()) : 0,
                            GeranRankInBank = worksheet.Cells[row, 46].Value != null ? int.Parse(worksheet.Cells[row, 46].Value.ToString().Trim()) : 0,

                            TedadTashilatScore = worksheet.Cells[row, 47].Value != null ? int.Parse(worksheet.Cells[row, 47].Value.ToString().Trim()) : 0,
                            TedadTashilatRankInBranch = worksheet.Cells[row, 48].Value != null ? int.Parse(worksheet.Cells[row, 48].Value.ToString().Trim()) : 0,
                            TedadTashilatRankInZone = worksheet.Cells[row, 49].Value != null ? int.Parse(worksheet.Cells[row, 49].Value.ToString().Trim()) : 0,
                            TedadTashilatRankInBank = worksheet.Cells[row, 50].Value != null ? int.Parse(worksheet.Cells[row, 50].Value.ToString().Trim()) : 0,

                            MablaghTashilatScore = worksheet.Cells[row, 51].Value != null ? int.Parse(worksheet.Cells[row, 51].Value.ToString().Trim()) : 0,
                            MablaghTashilatRankInBranch = worksheet.Cells[row, 52].Value != null ? int.Parse(worksheet.Cells[row, 52].Value.ToString().Trim()) : 0,
                            MablaghTashilatRankInZone = worksheet.Cells[row, 53].Value != null ? int.Parse(worksheet.Cells[row, 53].Value.ToString().Trim()) : 0,
                            MablaghTashilatRankInBank = worksheet.Cells[row, 54].Value != null ? int.Parse(worksheet.Cells[row, 54].Value.ToString().Trim()) : 0,

                            TedadZemantScore = worksheet.Cells[row, 55].Value != null ? int.Parse(worksheet.Cells[row, 55].Value.ToString().Trim()) : 0,
                            TedadZemantRankInBranch = worksheet.Cells[row, 56].Value != null ? int.Parse(worksheet.Cells[row, 56].Value.ToString().Trim()) : 0,
                            TedadZemantRankInZone = worksheet.Cells[row, 57].Value != null ? int.Parse(worksheet.Cells[row, 57].Value.ToString().Trim()) : 0,
                            TedadZemantRankInBank = worksheet.Cells[row, 58].Value != null ? int.Parse(worksheet.Cells[row, 58].Value.ToString().Trim()) : 0,

                            MablaghZemantScore = worksheet.Cells[row, 59].Value != null ? int.Parse(worksheet.Cells[row, 59].Value.ToString().Trim()) : 0,
                            MablaghZemantRankInBranch = worksheet.Cells[row, 60].Value != null ? int.Parse(worksheet.Cells[row, 60].Value.ToString().Trim()) : 0,
                            MablaghZemantRankInZone = worksheet.Cells[row, 61].Value != null ? int.Parse(worksheet.Cells[row, 61].Value.ToString().Trim()) : 0,
                            MablaghZemantRankInBank = worksheet.Cells[row, 62].Value != null ? int.Parse(worksheet.Cells[row, 62].Value.ToString().Trim()) : 0,

                            EtebaratAsnadiScore = worksheet.Cells[row, 63].Value != null ? int.Parse(worksheet.Cells[row, 63].Value.ToString().Trim()) : 0,
                            EtebaratAsnadiRankInBranch = worksheet.Cells[row, 64].Value != null ? int.Parse(worksheet.Cells[row, 64].Value.ToString().Trim()) : 0,
                            EtebaratAsnadiRankInZone = worksheet.Cells[row, 65].Value != null ? int.Parse(worksheet.Cells[row, 65].Value.ToString().Trim()) : 0,
                            EtebaratAsnadiRankInBank = worksheet.Cells[row, 66].Value != null ? int.Parse(worksheet.Cells[row, 66].Value.ToString().Trim()) : 0,

                            DaramadKarmozdiScore = worksheet.Cells[row, 67].Value != null ? int.Parse(worksheet.Cells[row, 67].Value.ToString().Trim()) : 0,
                            DaramadKarmozdiRankInBranch = worksheet.Cells[row, 68].Value != null ? int.Parse(worksheet.Cells[row, 68].Value.ToString().Trim()) : 0,
                            DaramadKarmozdiRankInZone = worksheet.Cells[row, 69].Value != null ? int.Parse(worksheet.Cells[row, 69].Value.ToString().Trim()) : 0,
                            DaramadKarmozdiRankInBank = worksheet.Cells[row, 70].Value != null ? int.Parse(worksheet.Cells[row, 70].Value.ToString().Trim()) : 0,

                            KartHadiyeScore = worksheet.Cells[row, 71].Value != null ? int.Parse(worksheet.Cells[row, 71].Value.ToString().Trim()) : 0,
                            KartHadiyeRankInBranch = worksheet.Cells[row, 72].Value != null ? int.Parse(worksheet.Cells[row, 72].Value.ToString().Trim()) : 0,
                            KartHadiyeRankInZone = worksheet.Cells[row, 73].Value != null ? int.Parse(worksheet.Cells[row, 73].Value.ToString().Trim()) : 0,
                            KartHadiyeRankInBank = worksheet.Cells[row, 74].Value != null ? int.Parse(worksheet.Cells[row, 74].Value.ToString().Trim()) : 0,

                            IranKartScore = worksheet.Cells[row, 75].Value != null ? int.Parse(worksheet.Cells[row, 75].Value.ToString().Trim()) : 0,
                            IranKartRankInBranch = worksheet.Cells[row, 76].Value != null ? int.Parse(worksheet.Cells[row, 76].Value.ToString().Trim()) : 0,
                            IranKartRankInZone = worksheet.Cells[row, 77].Value != null ? int.Parse(worksheet.Cells[row, 77].Value.ToString().Trim()) : 0,
                            IranKartRankInBank = worksheet.Cells[row, 78].Value != null ? int.Parse(worksheet.Cells[row, 78].Value.ToString().Trim()) : 0,

                            KilidScore = worksheet.Cells[row, 79].Value != null ? int.Parse(worksheet.Cells[row, 79].Value.ToString().Trim()) : 0,
                            KilidRankInBranch = worksheet.Cells[row, 80].Value != null ? int.Parse(worksheet.Cells[row, 80].Value.ToString().Trim()) : 0,
                            KilidRankInZone = worksheet.Cells[row, 81].Value != null ? int.Parse(worksheet.Cells[row, 81].Value.ToString().Trim()) : 0,
                            KilidRankInBank = worksheet.Cells[row, 82].Value != null ? int.Parse(worksheet.Cells[row, 82].Value.ToString().Trim()) : 0,

                            AsnadNaghdiVaGhirNaghdiScore = worksheet.Cells[row, 83].Value != null ? int.Parse(worksheet.Cells[row, 83].Value.ToString().Trim()) : 0,
                            AsnadNaghdiVaGhirNaghdiRankInBranch = worksheet.Cells[row, 84].Value != null ? int.Parse(worksheet.Cells[row, 84].Value.ToString().Trim()) : 0,
                            AsnadNaghdiVaGhirNaghdiRankInZone = worksheet.Cells[row, 85].Value != null ? int.Parse(worksheet.Cells[row, 85].Value.ToString().Trim()) : 0,
                            AsnadNaghdiVaGhirNaghdiRankInBank = worksheet.Cells[row, 86].Value != null ? int.Parse(worksheet.Cells[row, 86].Value.ToString().Trim()) : 0,

                            AsnadBarghashtiScore = worksheet.Cells[row, 87].Value != null ? int.Parse(worksheet.Cells[row, 87].Value.ToString().Trim()) : 0,
                            AsnadBarghashtiRankInBranch = worksheet.Cells[row, 88].Value != null ? int.Parse(worksheet.Cells[row, 88].Value.ToString().Trim()) : 0,
                            AsnadBarghashtiRankInZone = worksheet.Cells[row, 89].Value != null ? int.Parse(worksheet.Cells[row, 89].Value.ToString().Trim()) : 0,
                            AsnadBarghashtiRankInBank = worksheet.Cells[row, 90].Value != null ? int.Parse(worksheet.Cells[row, 90].Value.ToString().Trim()) : 0,

                            TarifMoshtariScore = worksheet.Cells[row, 91].Value != null ? int.Parse(worksheet.Cells[row, 91].Value.ToString().Trim()) : 0,
                            TarifMoshtariRankInBranch = worksheet.Cells[row, 92].Value != null ? int.Parse(worksheet.Cells[row, 92].Value.ToString().Trim()) : 0,
                            TarifMoshtariRankInZone = worksheet.Cells[row, 93].Value != null ? int.Parse(worksheet.Cells[row, 93].Value.ToString().Trim()) : 0,
                            TarifMoshtariRankInBank = worksheet.Cells[row, 94].Value != null ? int.Parse(worksheet.Cells[row, 94].Value.ToString().Trim()) : 0,

                            EftetahHesabScore = worksheet.Cells[row, 95].Value != null ? int.Parse(worksheet.Cells[row, 95].Value.ToString().Trim()) : 0,
                            EftetahHesabRankInBranch = worksheet.Cells[row, 96].Value != null ? int.Parse(worksheet.Cells[row, 96].Value.ToString().Trim()) : 0,
                            EftetahHesabRankInZone = worksheet.Cells[row, 97].Value != null ? int.Parse(worksheet.Cells[row, 97].Value.ToString().Trim()) : 0,
                            EftetahHesabRankInBank = worksheet.Cells[row, 98].Value != null ? int.Parse(worksheet.Cells[row, 98].Value.ToString().Trim()) : 0,

                            BakhshnameKhaniScore = worksheet.Cells[row, 99].Value != null ? int.Parse(worksheet.Cells[row, 99].Value.ToString().Trim()) : 0,
                            BakhshnameKhaniRankInBranch = worksheet.Cells[row, 100].Value != null ? int.Parse(worksheet.Cells[row, 100].Value.ToString().Trim()) : 0,
                            BakhshnameKhaniRankInZone = worksheet.Cells[row, 101].Value != null ? int.Parse(worksheet.Cells[row, 101].Value.ToString().Trim()) : 0,
                            BakhshnameKhaniRankInBank = worksheet.Cells[row, 102].Value != null ? int.Parse(worksheet.Cells[row, 102].Value.ToString().Trim()) : 0,

                            TakhirVaTajilScore = worksheet.Cells[row, 103].Value != null ? int.Parse(worksheet.Cells[row, 103].Value.ToString().Trim()) : 0,
                            TakhirVaTajilRankInBranch = worksheet.Cells[row, 104].Value != null ? int.Parse(worksheet.Cells[row, 104].Value.ToString().Trim()) : 0,
                            TakhirVaTajilRankInZone = worksheet.Cells[row, 105].Value != null ? int.Parse(worksheet.Cells[row, 105].Value.ToString().Trim()) : 0,
                            TakhirVaTajilRankInBank = worksheet.Cells[row, 106].Value != null ? int.Parse(worksheet.Cells[row, 106].Value.ToString().Trim()) : 0,
                        });
                    }
                }
                var r = await _importDataService.ImportAsync(new ImportDataInput(list), 1);

                TempData["FileName"] = null;
                return new ImportDataOutput(true, null, "عملیات ثبت اطلاعات با موفقیت انجام شد");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"خطا در ثبت فایل اکسل");
                return new ImportDataOutput(false, null, "خطا در ثبت فایل اکسل");
            }
           
        }

        [HttpPost]
        public async Task<ActionResult> Import(IFormFile formFile, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("اجرای متد انتخاب فایل اکسل");
                if (formFile == null || formFile.Length <= 0)
                {
                    ModelState.AddModelError("Msg", "لطفا یک فایل اکسل انتخاب نمایید");
                }

                if (!Path.GetExtension(formFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    ModelState.AddModelError("Msg", "فایل با فرمت اکسل انتخاب نمایید");
                }
                var errList = await ReadExcel(formFile);
                string str = "";
                if (errList.Count(d => d != "") > 0)
                {
                    return View("Index", new ImportDataOutput(false, null, string.Join(" - ", errList)));
                }

                var id = Guid.NewGuid();
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "UploadFiles");
                var filePath = Path.Combine(uploads, id + ".xlsx");
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }
                TempData["FileName"] = filePath;
                str = string.Format("تعداد {0} رکورد از فایل اکسل با موفقیت خوانش شد", errList.Count());
                return View("Index", new ImportDataOutput(true, null, str));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"خطا در انتخاب فایل اکسل");
                return View("Index", new ImportDataOutput(false, null, "خطا در انتخاب فایل اکسل"));
            }
        }

        //public ActionResult GridData()
        //{
        //    List<UserInfo> list = new List<UserInfo>();
        //    var u = new UserInfo() { UserName = "ali", Age = 1 };
        //    var u1 = new UserInfo() { UserName = "ali1", Age = 1 };
        //    var u2 = new UserInfo() { UserName = "ali2", Age = 1 };
        //    list.Add(u);
        //    list.Add(u1);
        //    list.Add(u2);
        //    return Json(new { data = list, total = 3 });

        //}

    //    [HttpGet]
    //    public async Task<DemoResponse<string>> Export(CancellationToken cancellationToken)
    //    {
    //        string folder = _hostingEnvironment.WebRootPath;
    //        string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
    //        string downloadUrl = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, excelName);
    //        FileInfo file = new FileInfo(Path.Combine(folder, excelName));
    //        if (file.Exists)
    //        {
    //            file.Delete();
    //            file = new FileInfo(Path.Combine(folder, excelName));
    //        }

    //        // query data from database  
    //        await Task.Yield();

    //        var list = new List<UserInfo>()
    //{
    //    new UserInfo { UserName = "catcher", Age = 18 },
    //    new UserInfo { UserName = "james", Age = 20 },
    //};

    //        using (var package = new ExcelPackage(file))
    //        {
    //            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
    //            workSheet.Cells.LoadFromCollection(list, true);
    //            package.Save();
    //        }

    //        return DemoResponse<string>.GetResult(0, "OK", downloadUrl);
    //    }
    }

}
