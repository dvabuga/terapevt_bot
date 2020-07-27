using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Medcin> Medcins { get; set; }
        public DbSet<MedcinParam> MedcinParams { get; set; }
        public DbSet<Param> Params { get; set; }
        public DbSet<ParamValue> ParamValues { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTree> QuestionTrees { get; set; }
        public DbSet<Recept> Recepts { get; set; }
        public DbSet<ReceptParam> ReceptParams { get; set; }
        public DbSet<ReceptRow> ReceptRows { get; set; }
        public DbSet<Scenario> Scenarios { get; set; }
        public DbSet<QuestionTreeHistory> QuestionTreeHistories { get; set; }
        public DbSet<ReceptTemplate> ReceptTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var converterParamType = new EnumToStringConverter<ParamType>();
            modelBuilder
               .Entity<Param>()
               .Property(e => e.Type)
               .HasConversion(converterParamType);


            //var converterUnitType = new EnumToStringConverter<UnitType>();
            //modelBuilder
            //   .Entity<ParamValue>()
            //   .Property(e => e.Unit)
            //   .HasConversion(converterUnitType);


            var converterQuestionType = new EnumToStringConverter<QuestionType>();
            modelBuilder
               .Entity<Question>()
               .Property(e => e.Type)
               .HasConversion(converterQuestionType);


            var converterQuestionScenarioType = new EnumToStringConverter<QuestionScenarioType>();
            modelBuilder
               .Entity<Question>()
               .Property(e => e.ScenarioType)
               .HasConversion(converterQuestionScenarioType);


            var converterScenarioType = new EnumToStringConverter<ScenarioType>();
            modelBuilder
               .Entity<Scenario>()
               .Property(e => e.Type)
               .HasConversion(converterScenarioType);

        }
    }
}
