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
                await _uow.CommitAsync();
            }

            return null;

        }
    }
}
