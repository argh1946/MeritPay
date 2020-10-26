using MeritPay.Core.DTOs;
using MeritPay.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeritPay.Core.UseCases.Interfaces
{
    public interface IPersonScoreService
    {
        Task<List<PersonScore>> GetPersonScoreByPersonCodeAsync(int periodId, int personCode);

    }
}
