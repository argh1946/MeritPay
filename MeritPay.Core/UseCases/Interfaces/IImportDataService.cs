using MeritPay.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases.Interfaces
{
    public interface IImportDataService
    {
        Task<ImportDataVM> ImportAsync(ImportDataInput data,int periodId);
    }
}
