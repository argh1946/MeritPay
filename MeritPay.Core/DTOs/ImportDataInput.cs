using SampleProject.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeritPay.Core.DTOs
{
    public class ImportDataInput
    {
        public List<ImportDataInputVM> Data { get; set; }
        public ImportDataInput(List<ImportDataInputVM> data)
        {
            Data = data;
        }
    }


    public class ImportDataInputVM
    {
        public int PersonCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BranchCode { get; set; }

        public int TaScore { get; set; }
        public int TaRankInBranch { get; set; }
        public int TaRankInZone { get; set; }
        public int TaRankInBank { get; set; }

        public int GroupScore { get; set; }
        public int GroupRankInBranch { get; set; }
        public int GroupRankInZone { get; set; }
        public int GroupRankInBank { get; set; }

        public int ArzeshScore { get; set; }
        public int ArzeshRankInBranch { get; set; }
        public int ArzeshRankInZone { get; set; }
        public int ArzeshRankInBank { get; set; }

        public int TotalScore { get; set; }
        public int TotalRankInBranch { get; set; }
        public int TotalRankInZone { get; set; }
        public int TotalRankInBank { get; set; }
        
        public decimal TajhizManabeArzan { get; set; }
        public decimal TajhizManabeGeran { get; set; }
        
        public int TakhsisManabeTedadTashilat { get; set; }
        public decimal TakhsisManabeMablaghTashilat { get; set; }
        public int TakhsisManabeTedadZSemanat { get; set; }
        public decimal TakhsisManabeMablaghZemanat { get; set; }
        public decimal TakhsisManabeEtebaratAsnadi { get; set; }

        public int DaramadKarmozdi { get; set; }

        public int BankdariElectronicKartHadiye { get; set; }
        public int BankdariElectronicIranKart { get; set; }
        public int BankdariElectronicKilid { get; set; }

        public int SayerAsnadNaghdiVaGheirNaghdi { get; set; }
        public int SayerAsnadBarghashti { get; set; }
        public int SayerTarifMoshtari { get; set; }
        public int SayerEftetahHesab { get; set; }
        public int SayerBakhshnameKhani { get; set; }
        public int SayerTakhirVaTajil { get; set; }

        public int ArzanScore { get; set; }
        public int ArzanRankInBranch { get; set; }
        public int ArzanRankInZone { get; set; }
        public int ArzanRankInBank { get; set; }

        public int GeranScore { get; set; }
        public int GeranRankInBranch { get; set; }
        public int GeranRankInZone { get; set; }
        public int GeranRankInBank { get; set; }

        public int TedadTashilatScore { get; set; }
        public int TedadTashilatRankInBranch { get; set; }
        public int TedadTashilatRankInZone { get; set; }
        public int TedadTashilatRankInBank { get; set; }

        public int MablaghTashilatScore { get; set; }
        public int MablaghTashilatRankInBranch { get; set; }
        public int MablaghTashilatRankInZone { get; set; }
        public int MablaghTashilatRankInBank { get; set; }

        public int TedadZemantScore { get; set; }
        public int TedadZemantRankInBranch { get; set; }
        public int TedadZemantRankInZone { get; set; }
        public int TedadZemantRankInBank { get; set; }

        public int MablaghZemantScore { get; set; }
        public int MablaghZemantRankInBranch { get; set; }
        public int MablaghZemantRankInZone { get; set; }
        public int MablaghZemantRankInBank { get; set; }

        public int EtebaratAsnadiScore { get; set; }
        public int EtebaratAsnadiRankInBranch { get; set; }
        public int EtebaratAsnadiRankInZone { get; set; }
        public int EtebaratAsnadiRankInBank { get; set; }

        public int DaramadKarmozdiScore { get; set; }
        public int DaramadKarmozdiRankInBranch { get; set; }
        public int DaramadKarmozdiRankInZone { get; set; }
        public int DaramadKarmozdiRankInBank { get; set; }

        public int KartHadiyeScore { get; set; }
        public int KartHadiyeRankInBranch { get; set; }
        public int KartHadiyeRankInZone { get; set; }
        public int KartHadiyeRankInBank { get; set; }

        public int IranKartScore { get; set; }
        public int IranKartRankInBranch { get; set; }
        public int IranKartRankInZone { get; set; }
        public int IranKartRankInBank { get; set; }

        public int KilidScore { get; set; }
        public int KilidRankInBranch { get; set; }
        public int KilidRankInZone { get; set; }
        public int KilidRankInBank { get; set; }

        public int AsnadNaghdiVaGhirNaghdiScore { get; set; }
        public int AsnadNaghdiVaGhirNaghdiRankInBranch { get; set; }
        public int AsnadNaghdiVaGhirNaghdiRankInZone { get; set; }
        public int AsnadNaghdiVaGhirNaghdiRankInBank { get; set; }

        public int AsnadBarghashtiScore { get; set; }
        public int AsnadBarghashtiRankInBranch { get; set; }
        public int AsnadBarghashtiRankInZone { get; set; }
        public int AsnadBarghashtiRankInBank { get; set; }

        public int TarifMoshtariScore { get; set; }
        public int TarifMoshtariRankInBranch { get; set; }
        public int TarifMoshtariRankInZone { get; set; }
        public int TarifMoshtariRankInBank { get; set; }

        public int EftetahHesabScore { get; set; }
        public int EftetahHesabRankInBranch { get; set; }
        public int EftetahHesabRankInZone { get; set; }
        public int EftetahHesabRankInBank { get; set; }

        public int BakhshnameKhaniScore { get; set; }
        public int BakhshnameKhaniRankInBranch { get; set; }
        public int BakhshnameKhaniRankInZone { get; set; }
        public int BakhshnameKhaniRankInBank { get; set; }

        public int TakhirVaTajilScore { get; set; }
        public int TakhirVaTajilRankInBranch { get; set; }
        public int TakhirVaTajilRankInZone { get; set; }
        public int TakhirVaTajilRankInBank { get; set; }

    }
}
