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
        public string Breaks { get; set; }
        public string Muscles { get; set; }
        public string Author { get; set; }
        public string ExtraInfo { get; set; }
        public Training()
        {
            Name = "";
            Plan = "";
            Breaks = "";
            Muscles = "";
            Author = "";
            ExtraInfo = "";
        }
        public Training(string n = "", string p = "", string b = "", string m = "", string a = "", string ei = "")
        {
            Name = n;
            Plan = p;
            Breaks = b;
            Muscles = m;
            Author = a;
            ExtraInfo = ei;
        }
    }
}
/*
create table trainings(Id int primary key identity,[Name] varchar(max),[Plan] varchar(max),Breaks varchar(max),Author nvarchar(max),ExtraInfo varchar(max))
insert into trainings values('TrainingName','Follow the plan','Non-stop','Taras Shevchenko','Go pump your muscles')
*/