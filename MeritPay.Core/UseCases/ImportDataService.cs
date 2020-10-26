using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeritPay.Core.Contracts;
using MeritPay.Core.DTOs;
using MeritPay.Core.Entities;
using MeritPay.Core.UseCases.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MeritPay.Core.UseCases
{
    public class ImportDataService : IImportDataService
    {
        private readonly IUnitOfWork _uow;
        public ImportDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<ImportDataVM> ImportAsync(ImportDataInput data, int periodId)
        {
            var res = await _uow.ReportRepository.DeleteAllPersonInPeriodReportAsync(periodId);
            var res1 = await _uow.PersonScoreRepository.DeleteAllPersonScoreInPeriodAsync(periodId);
            //await Task.Yield();
            var branchList = await _uow.BranchRepository.GetAllAsync();
            var personList = await _uow.PersonRepository.GetAllAsync();
            var personInBranchList = await _uow.PersonInBranchRepository.GetAllAsync();

            var meritPayFactorList = await _uow.MeritPayFactorRepository.GetMeritPayFactorByPeriodIdAsync(periodId);
            var meritPayFactorBranchList = meritPayFactorList.Where(d => d.MeritPayTypeId == 1);
            var TarakoneshAmalkardiId = meritPayFactorBranchList.FirstOrDefault(d => d.Title == "تراکنش های عملکردی").Id;
            var EmtiazGrohiShobeId = meritPayFactorBranchList.FirstOrDefault(d => d.Title == "امتیاز گروهی").Id;
            var ArzeshYabiShobeId = meritPayFactorBranchList.FirstOrDefault(d => d.Title == "ارزشیابی فردی").Id;

            var meritPayFactorSetadList = meritPayFactorList.Where(d => d.MeritPayTypeId == 2);
            var EmtiazGrohiSetadId = meritPayFactorSetadList.FirstOrDefault(d => d.Title == "امتیاز گروهی").Id;
            var ArzeshYabiSetadId = meritPayFactorSetadList.FirstOrDefault(d => d.Title == "ارزشیابی فردی").Id;

            foreach (ImportDataInputVM item in data.Data)
            {
                var branchId = branchList.FirstOrDefault(d => d.BranchCode == item.BranchCode).Id;
                var personId = personList.FirstOrDefault(d => d.PersonCode == item.PersonCode).Id;
                var personInBranch = personInBranchList.FirstOrDefault(x => x.BranchId == branchId && x.PersonId == personId);
                int personInBranchId = 0;
                if (personInBranch == null)
                {
                    //insert to db
                    personInBranchId = 1;
                }
                else personInBranchId = personInBranch.Id;
                if (item.BranchCode != 21000)
                {
                    Report report1 = new Report();
                    report1.MeritPayFactorId = TarakoneshAmalkardiId;
                    report1.PersonInBranchId = personInBranchId;
                    report1.Score = item.TaScore;
                    report1.RankInBranch = item.TaRankInBranch;
                    report1.RankInZone = item.TaRankInZone;
                    report1.RankInBank = item.TaRankInBank;
                    await _uow.ReportRepository.AddAsync(report1);

                    Report report2 = new Report();
                    report2.MeritPayFactorId = EmtiazGrohiShobeId;
                    report2.PersonInBranchId = personInBranchId;
                    report2.Score = item.GroupScore;
                    report2.RankInBranch = item.GroupRankInBranch;
                    report2.RankInZone = item.GroupRankInZone;
                    report2.RankInBank = item.GroupRankInBank;
                    await _uow.ReportRepository.AddAsync(report2);

                    Report report3 = new Report();
                    report3.MeritPayFactorId = ArzeshYabiShobeId;
                    report3.PersonInBranchId = personInBranchId;
                    report3.Score = item.ArzeshScore;
                    report3.RankInBranch = item.ArzeshRankInBranch;
                    report3.RankInZone = item.ArzeshRankInZone;
                    report3.RankInBank = item.ArzeshRankInBank;
                    await _uow.ReportRepository.AddAsync(report3);

                    //ثبت تراکنش های عملکردی
                    //ارزان قیمت
                    PersonScore ps1 = new PersonScore();
                    ps1.PersonInBranchId = personInBranchId;
                    ps1.ScoreSubIndexId = 1;
                    ps1.Value = item.TajhizManabeArzan;
                    ps1.RankInBranch = item.ArzanRankInBranch;
                    ps1.RankInZone = item.ArzanRankInZone;
                    ps1.RankInBank = item.ArzanRankInBank;
                    ps1.Score = item.ArzanScore;
                    await _uow.PersonScoreRepository.AddAsync(ps1);

                    //گران قیمت
                    PersonScore ps2 = new PersonScore();
                    ps2.PersonInBranchId = personInBranchId;
                    ps2.ScoreSubIndexId = 2;
                    ps2.Value = item.TajhizManabeGeran;
                    ps2.RankInBranch = item.GeranRankInBranch;
                    ps2.RankInZone = item.GeranRankInZone;
                    ps2.RankInBank = item.GeranRankInBank;
                    ps2.Score = item.GeranScore;
                    await _uow.PersonScoreRepository.AddAsync(ps2);

                    // تسهیلات
                    PersonScore ps3 = new PersonScore();
                    ps3.PersonInBranchId = personInBranchId;
                    ps3.ScoreSubIndexId = 3;
                    ps3.Value = item.TakhsisManabeMablaghTashilat;
                    ps3.RankInBranch = item.MablaghTashilatRankInBranch;
                    ps3.RankInZone = item.MablaghTashilatRankInZone;
                    ps3.RankInBank = item.MablaghTashilatRankInBank;
                    ps3.Score = item.MablaghTashilatScore;
                    await _uow.PersonScoreRepository.AddAsync(ps3);

                    //ضمانتنامه
                    PersonScore ps4 = new PersonScore();
                    ps4.PersonInBranchId = personInBranchId;
                    ps4.ScoreSubIndexId = 4;
                    ps4.Value = item.TakhsisManabeMablaghZemanat;
                    ps4.RankInBranch = item.MablaghZemantRankInBranch;
                    ps4.RankInZone = item.MablaghZemantRankInZone;
                    ps4.RankInBank = item.MablaghZemantRankInBank;
                    ps4.Score = item.MablaghZemantScore;
                    await _uow.PersonScoreRepository.AddAsync(ps4);

                    //اعتبار اسنادی
                    PersonScore ps5 = new PersonScore();
                    ps5.PersonInBranchId = personInBranchId;
                    ps5.ScoreSubIndexId = 5;
                    ps5.Value = item.TakhsisManabeEtebaratAsnadi;
                    ps5.RankInBranch = item.EtebaratAsnadiRankInBranch;
                    ps5.RankInZone = item.EtebaratAsnadiRankInZone;
                    ps5.RankInBank = item.EtebaratAsnadiRankInBank;
                    ps5.Score = item.EtebaratAsnadiScore;
                    await _uow.PersonScoreRepository.AddAsync(ps5);

                    //درآمد کارمزدی
                    PersonScore ps6 = new PersonScore();
                    ps6.PersonInBranchId = personInBranchId;
                    ps6.ScoreSubIndexId = 7;
                    ps6.Value = item.DaramadKarmozdi;
                    ps6.RankInBranch = item.DaramadKarmozdiRankInBranch;
                    ps6.RankInZone = item.DaramadKarmozdiRankInZone;
                    ps6.RankInBank = item.DaramadKarmozdiRankInBank;
                    ps6.Score = item.DaramadKarmozdiScore;
                    await _uow.PersonScoreRepository.AddAsync(ps6);

                    //کارت هدیه
                    PersonScore ps7 = new PersonScore();
                    ps7.PersonInBranchId = personInBranchId;
                    ps7.ScoreSubIndexId = 8;
                    ps7.Value = item.BankdariElectronicKartHadiye;
                    ps7.RankInBranch = item.KartHadiyeRankInBranch;
                    ps7.RankInZone = item.KartHadiyeRankInZone;
                    ps7.RankInBank = item.KartHadiyeRankInBank;
                    ps7.Score = item.KartHadiyeScore;
                    await _uow.PersonScoreRepository.AddAsync(ps7);

                    //ایران کارت
                    PersonScore ps8 = new PersonScore();
                    ps8.PersonInBranchId = personInBranchId;
                    ps8.ScoreSubIndexId = 9;
                    ps8.Value = item.BankdariElectronicIranKart;
                    ps8.RankInBranch = item.IranKartRankInBranch;
                    ps8.RankInZone = item.IranKartRankInZone;
                    ps8.RankInBank = item.IranKartRankInBank;
                    ps8.Score = item.IranKartScore;
                    await _uow.PersonScoreRepository.AddAsync(ps8);

                    //کیلید
                    PersonScore ps9 = new PersonScore();
                    ps9.PersonInBranchId = personInBranchId;
                    ps9.ScoreSubIndexId = 10;
                    ps9.Value = item.BankdariElectronicKilid;
                    ps9.RankInBranch = item.KilidRankInBranch;
                    ps9.RankInZone = item.KilidRankInZone;
                    ps9.RankInBank = item.KilidRankInBank;
                    ps9.Score = item.KilidScore;
                    await _uow.PersonScoreRepository.AddAsync(ps9);

                    //اسناد نقدی و غیر نقدی
                    PersonScore ps10 = new PersonScore();
                    ps10.PersonInBranchId = personInBranchId;
                    ps10.ScoreSubIndexId = 12;
                    ps10.Value = item.SayerAsnadNaghdiVaGheirNaghdi;
                    ps10.RankInBranch = item.AsnadNaghdiVaGhirNaghdiRankInBranch;
                    ps10.RankInZone = item.AsnadNaghdiVaGhirNaghdiRankInZone;
                    ps10.RankInBank = item.AsnadNaghdiVaGhirNaghdiRankInBank;
                    ps10.Score = item.AsnadNaghdiVaGhirNaghdiScore;
                    await _uow.PersonScoreRepository.AddAsync(ps10);

                    //اسناد برگشتی
                    PersonScore ps11 = new PersonScore();
                    ps11.PersonInBranchId = personInBranchId;
                    ps11.ScoreSubIndexId = 13;
                    ps11.Value = item.SayerAsnadBarghashti;
                    ps11.RankInBranch = item.AsnadBarghashtiRankInBranch;
                    ps11.RankInZone = item.AsnadBarghashtiRankInZone;
                    ps11.RankInBank = item.AsnadBarghashtiRankInBank;
                    ps11.Score = item.AsnadBarghashtiScore;
                    await _uow.PersonScoreRepository.AddAsync(ps11);

                    //تعریف مشتری
                    PersonScore ps12 = new PersonScore();
                    ps12.PersonInBranchId = personInBranchId;
                    ps12.ScoreSubIndexId = 14;
                    ps12.Value = item.SayerTarifMoshtari;
                    ps12.RankInBranch = item.TarifMoshtariRankInBranch;
                    ps12.RankInZone = item.TarifMoshtariRankInZone;
                    ps12.RankInBank = item.TarifMoshtariRankInBank;
                    ps12.Score = item.TarifMoshtariScore;
                    await _uow.PersonScoreRepository.AddAsync(ps12);

                    //افتتاح حساب
                    PersonScore ps13 = new PersonScore();
                    ps13.PersonInBranchId = personInBranchId;
                    ps13.ScoreSubIndexId = 15;
                    ps13.Value = item.SayerEftetahHesab;
                    ps13.RankInBranch = item.EftetahHesabRankInBranch;
                    ps13.RankInZone = item.EftetahHesabRankInZone;
                    ps13.RankInBank = item.EftetahHesabRankInBank;
                    ps13.Score = item.EftetahHesabScore;
                    await _uow.PersonScoreRepository.AddAsync(ps13);

                    //بخش نامه خوانی
                    PersonScore ps14 = new PersonScore();
                    ps14.PersonInBranchId = personInBranchId;
                    ps14.ScoreSubIndexId = 16;
                    ps14.Value = item.SayerBakhshnameKhani;
                    ps14.RankInBranch = item.BakhshnameKhaniRankInBranch;
                    ps14.RankInZone = item.BakhshnameKhaniRankInZone;
                    ps14.RankInBank = item.BakhshnameKhaniRankInBank;
                    ps14.Score = item.BakhshnameKhaniScore;
                    await _uow.PersonScoreRepository.AddAsync(ps14);

                    //تاخیر و تاجیل
                    PersonScore ps15 = new PersonScore();
                    ps15.PersonInBranchId = personInBranchId;
                    ps15.ScoreSubIndexId = 17;
                    ps15.Value = item.SayerTakhirVaTajil;
                    ps15.RankInBranch = item.TakhirVaTajilRankInBranch;
                    ps15.RankInZone = item.TakhirVaTajilRankInZone;
                    ps15.RankInBank = item.TakhirVaTajilRankInBank;
                    ps15.Score = item.TakhirVaTajilScore;
                    await _uow.PersonScoreRepository.AddAsync(ps15);
                }
                else
                {
                    Report report4 = new Report();
                    report4.MeritPayFactorId = EmtiazGrohiSetadId;
                    report4.PersonInBranchId = personInBranchId;
                    report4.Score = item.GroupScore;
                    report4.RankInBranch = item.GroupRankInBranch;
                    report4.RankInZone = item.GroupRankInZone;
                    report4.RankInBank = item.GroupRankInBank;
                    await _uow.ReportRepository.AddAsync(report4);

                    Report report5 = new Report();
                    report5.MeritPayFactorId = ArzeshYabiSetadId;
                    report5.PersonInBranchId = personInBranchId;
                    report5.Score = item.ArzeshScore;
                    report5.RankInBranch = item.ArzeshRankInBranch;
                    report5.RankInZone = item.ArzeshRankInZone;
                    report5.RankInBank = item.ArzeshRankInBank;
                    await _uow.ReportRepository.AddAsync(report5);
                }

                



            }
            await _uow.CommitAsync();
            return null;

        }




    }
}
