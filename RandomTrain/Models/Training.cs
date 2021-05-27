using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RandomTrain.Models
{
    [Table("Trainings")]
    public class Training
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Plan { get; set; }
        public string Author { get; set; }
        public string ExtraInfo { get; set; }
        public Training()
        {
            Name = "";
            Plan = "";
            Author = "";
            ExtraInfo = "";
        }
        public Training(string n = "", string p = "", string a = "", string ei = "")
        {
            Name = n;
            Plan = p;
            Author = a;
            ExtraInfo = ei;
        }
    }
}
/*
create table trainings(Id int primary key identity,[Name] varchar(max),[Plan] varchar(max),Author nvarchar(max),ExtraInfo varchar(max))
insert into trainings values('TrainingName','Follow the plan','Taras Shevchenko','Go pump your muscles')
*/