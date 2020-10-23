using System;
using System.Collections.Generic;
using System.Text;
using MeritPay.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MeritPay.Infrastructure.Data
{
    public class MeritPayContext : DbContext
    {
        public MeritPayContext(DbContextOptions<MeritPayContext> options) : base(options)
        {            
        }


        public DbSet<Period> Period { get; set; }
        public DbSet<BranchGrouping> BranchGrouping { get; set; }
        public DbSet<MeritPayType> MeritPayType { get; set; }
        public DbSet<MeritPayLimit> MeritPayLimit { get; set; }
        public DbSet<MeritPayFactor> MeritPayFactor { get; set; }
        public DbSet<AdjustmentFactor> AdjustmentFactor { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<BranchGroupingInPeriod> BranchGroupingInPeriod { get; set; }
        public DbSet<GroupingRatio> GroupingRatio { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonInBranch> PersonInBranch { get; set; }
        public DbSet<PersonScore> PersonScore { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<ScoreIndex> ScoreIndex { get; set; }
        public DbSet<ScoreSubIndex> ScoreSubIndex { get; set; }


    }
}
