using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RandomTrain.Models
{
    public class TrainingContext : DbContext
    {
        public TrainingContext() : base("TrainingConnection")
        {

        }
        public DbSet<Training> Trainings { get; set; }

    }
}