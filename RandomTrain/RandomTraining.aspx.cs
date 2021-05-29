using RandomTrain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RandomTrain
{
    public partial class RandomTraining1 : System.Web.UI.Page
    {
        List<Training> trainings;
        TrainingContext db = new TrainingContext();
        List<Button> editionButtons = new List<Button>();
        const string mailAddr = "michelotakuwatson@gmail.com";//Separately for controllers
        const string bridgeAddr = "smtp.gmail.com";
        int DBC = 240;
        protected void Page_Load(object sender, EventArgs e)
        {
            trainings = db.Trainings.ToList();
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
        }
        void AddInDb(string Name, string Plan, string Breaks, string Muscles, string Author, string ExtraInfo)
        {
            trainings.Add(new Training(Name, Plan, Breaks, Author, ExtraInfo));
            db.SaveChanges();
        }
        void AddRow(int Id, string Name, string Plan, string Breaks, string Muscles, string Author, string ExtraInfo)
        {
            TableRow row = new TableRow();
            Button b = new Button();
            b.Click += btnSetForEdit_Click;
            b.Text = "✔";
            b.Width = 25;
            b.Height = 25;
            b.CssClass = "markButton";
            b.BackColor = Color.FromArgb(1, 240, 240, 240);
            TableCell cell1 = new TableCell(), cell2 = new TableCell(), cell3 = new TableCell(), cell4 = new TableCell(), cell5 = new TableCell(), cell6 = new TableCell(), cell7 = new TableCell(), cell8 = new TableCell();
            cell1.Text = Id.ToString();
            cell2.Text = Name.ToString();
            cell3.Text = Plan.ToString();
            cell4.Text = Breaks.ToString();
            cell5.Text = Muscles.ToString();
            cell6.Text = Author.ToString();
            cell7.Text = ExtraInfo.ToString();
            cell8.Controls.Add(b);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            row.Cells.Add(cell7);
            row.Cells.Add(cell8);
            trainingTable.Rows.Add(row);
            editionButtons.Add(b);
        }
        void AddTitle(string idtitle, string nametitle, string plantitle, string breakstitle, string muscles, string authortitle, string infotitle)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell(), cell2 = new TableCell(), cell3 = new TableCell(), cell4 = new TableCell(), cell5 = new TableCell(), cell6 = new TableCell(), cell7 = new TableCell(), cell8 = new TableCell();
            cell1.Text = idtitle.ToString();
            cell2.Text = nametitle.ToString();
            cell3.Text = plantitle.ToString();
            cell4.Text = breakstitle.ToString();
            cell5.Text = muscles.ToString();
            cell6.Text = authortitle.ToString();
            cell7.Text = infotitle.ToString();
            cell8.Text = string.Empty;
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            row.Cells.Add(cell7);
            row.Cells.Add(cell8);
            trainingTable.Rows.Add(row);
        }
        void AddRow(Training training)
        {
            AddRow(training.Id, training.Name, training.Plan, training.Breaks, training.Muscles, training.Author, training.ExtraInfo);
        }
        string BuildStrList(Training t)
        {
            return "ID:" + t.Id + Environment.NewLine + "Name:" + t.Name + Environment.NewLine + "Plan:" + t.Plan + Environment.NewLine + "Breaks:" + t.Muscles + Environment.NewLine + t.Breaks + Environment.NewLine + "Author:" + t.Author + Environment.NewLine + "Extra info:" + t.ExtraInfo;
        }
        List<string> BuildList(Training t)
        {
            List<string> liTraining = new List<string>();
            if (t.Breaks == string.Empty) t.Breaks = "Lesss gooo";
            if (t.Author == string.Empty) t.Author = "Unknown Arnold";
            if (t.ExtraInfo == string.Empty) t.ExtraInfo = "Nothing more to say";
            if (t.Muscles == string.Empty) t.Muscles = "Random muscles";
            liTraining.Add("ID: " + t.Id.ToString());
            liTraining.Add("Name: " + t.Name);
            liTraining.Add("Breaks: " + t.Breaks);
            liTraining.Add("Author: " + t.Author);
            liTraining.Add("Extra info: " + t.ExtraInfo);
            if (t.Muscles.Contains(';'))
            {
                string[] muscles = t.Muscles.Split(';');
                liTraining.Add("---MUSCLES INFO---");
                for (int i = 0; i < muscles.Length; i++)
                    if (muscles[i].Trim(' ').Length != 0)
                        liTraining.Add((i + 1) + ") " + muscles[i]);
            }
            else liTraining.Add("Muscles info: " + t.Muscles);
            if (t.Plan.Contains(';'))
            {
                string[] plan = t.Plan.Split(';');
                liTraining.Add("---PLAN---");
                for (int i = 0; i < plan.Length; i++)
                    if (plan[i].Trim(' ').Length != 0)
                        liTraining.Add((i + 1) + ") " + plan[i]);
            }
            else liTraining.Add("Plan: " + t.Plan);
            return liTraining;
        }
        Training RandomTraining()
        {
            int trainingIndex = new Random().Next(0, trainings.Count);
            //ResponseAlert(trainingIndex + " of " + trainings.Count + " on Id " + trainings[trainingIndex].Id + " -> " + trainings[trainingIndex].Author + " -> " + trainings[trainingIndex].Name + " -> " + trainings[trainingIndex].Plan + " -> " + trainings[trainingIndex].ExtraInfo);
            return trainings[trainingIndex];
        }
        void Alert(string text)
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", "alert('" + text + "')", true);
        }
        void ResponseAlert(string text)
        {
            Response.Write("<script>alert('" + text + "')</script>");
        }
        void UpdateTable()
        {
            trainingTable.Rows.Clear();
            AddTitle("Id", "Name", "Plan", "Breaks", "Muscles", "Author", "Extra Info");
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
        }
        TrainingContext LoadDb()//Load database to the table
        {
            trainingTable.Rows.Clear();
            AddTitle("Id", "Name", "Plan", "Breaks", "Muscles", "Author", "Extra Info");
            List<Training> trainings;
            TrainingContext db = new TrainingContext();
            trainings = db.Trainings.ToList();
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
            return db;
        }
        string TrimMultiStr(string str, char separator = ';')
        {
            string[] ms = str.Split(separator);
            string multistr = string.Empty;
            for (int i = 0; i < ms.Length; i++)
                multistr += ms[i].Trim(' ') + separator;
            return multistr.Trim(separator);
        }
        protected void btnAddTraining_Click(object sender, EventArgs e)
        {
            Training t = new Training();
            t.Author = tbAuthor.Text;
            t.ExtraInfo = tbExtraInfo.Text;
            t.Name = tbName.Text;
            t.Breaks = tbBreaks.Text;
            t.Plan = TrimMultiStr(tbPlan.Text);
            t.Muscles = TrimMultiStr(tbMuscles.Text);
            if (tbPlan.Text.Trim(' ') != string.Empty && tbName.Text.Trim(' ') != string.Empty)
            {
                db.Trainings.Add(t);
                db.SaveChanges();
                ResponseAlert("New element added.");
            }
            else ResponseAlert("Input does not match requirements.");
            tbAuthor.Text = string.Empty;
            tbExtraInfo.Text = string.Empty;
            tbName.Text = string.Empty;
            tbPlan.Text = string.Empty;
            tbBreaks.Text = string.Empty;
            tbMuscles.Text = string.Empty;
            LoadDb();
        }

        int GetButtonIndex(object sender)
        {
            Button b = (Button)sender;
            for (int i = 0; i < editionButtons.Count; i++)
            {
                if (editionButtons[i] == b) return i;
            }
            return -1;
        }

        protected void btnSetForEdit_Click(object sender, EventArgs e)
        {
            if ((sender as Button).BackColor == Color.FromArgb(1, DBC, DBC, DBC))
            {
                Training t = trainings[GetButtonIndex(sender)];
                for (int i = 0; i < editionButtons.Count; i++)
                    editionButtons[i].BackColor = Color.FromArgb(1, DBC, DBC, DBC);
                (sender as Button).BackColor = Color.FromArgb(1, 162, 255, 209);
                lblId.Text = t.Id.ToString();
                tbName.Text = t.Name;
                tbPlan.Text = t.Plan;
                tbBreaks.Text = t.Breaks;
                tbMuscles.Text = t.Muscles;
                tbAuthor.Text = t.Author;
                tbExtraInfo.Text = t.ExtraInfo;
            }
            else
            {
                for (int i = 0; i < editionButtons.Count; i++)
                    editionButtons[i].BackColor = Color.FromArgb(1, DBC, DBC, DBC);
                lblId.Text = "ID";
                ClearEntityFields();
            }
        }
        void ClearEntityFields()
        {
            lblId.Text = "ID";
            tbName.Text = string.Empty;
            tbPlan.Text = string.Empty;
            tbBreaks.Text = string.Empty;
            tbMuscles.Text = string.Empty;
            tbAuthor.Text = string.Empty;
            tbExtraInfo.Text = string.Empty;
        }
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (IsNum(lblId.Text.Trim(' ')) && tbPlan.Text.Trim(' ') != string.Empty && tbName.Text.Trim(' ') != string.Empty)
            {
                Training t = trainings.Where(x => x.Id == int.Parse(lblId.Text)).FirstOrDefault();
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                t.Name = tbName.Text;
                t.Author = tbAuthor.Text;
                t.Breaks = tbBreaks.Text;
                t.Muscles = tbMuscles.Text;
                t.ExtraInfo = tbExtraInfo.Text;
                t.Plan = TrimMultiStr(tbPlan.Text);
                db.SaveChanges();
                ResponseAlert("Element on ID " + t.Id + " has been updated.");
                ClearEntityFields();
            }
            else ResponseAlert("The element to edit hasn't been established.");
            LoadDb();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsNum(lblId.Text.Trim(' ')))
            {
                Training t = trainings.Where(x => x.Id == int.Parse(lblId.Text)).FirstOrDefault();
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.Trainings.Remove(t);
                db.SaveChanges();
                ResponseAlert("Plan called \"" + t.Name + "\" is now deleted. Now quantity is: " + db.Trainings.Count());
                ClearEntityFields();
            }
            else ResponseAlert("The element to remove hasn't been established.");
            LoadDb();
        }

        protected void btnGetTraining_Click(object sender, EventArgs e)
        {
            tblPlan.Rows.Clear();
            Training t = RandomTraining();
            List<string> trainingParts = BuildList(t);
            for (int i = 0; i < trainingParts.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = trainingParts[i];
                TableRow row = new TableRow();
                row.Cells.Add(cell);
                tblPlan.Rows.Add(row);
            }
        }
        private bool IsNum(string num)
        {
            int temp;
            return int.TryParse(num, out temp);
        }
        protected void btnBackup_Click(object sender, EventArgs e)
        {
            string query = "create table trainings(Id int primary key identity,[Name] varchar(max),[Plan] varchar(max),Breaks varchar(max),Muscles varchar(max),Author nvarchar(max),ExtraInfo varchar(max))" + Environment.NewLine;
            for (int i = 0; i < trainings.Count; i++)
            {
                query += "/*" + trainings[i].Id + "*/ insert into Trainings values('" + trainings[i].Name + "','" + trainings[i].Plan + "','" + trainings[i].Breaks + "','" + trainings[i].Muscles + "','" + trainings[i].Author + "','" + trainings[i].ExtraInfo + "')" + Environment.NewLine;
            }
            SendMailMessage(mailAddr, mailAddr, "Trainings backup", query);
            ResponseAlert("Backup successful.");
        }
        void SendMailMessage(string from, string to, string topic, string message)
        {
            using (MailMessage mail = new MailMessage(from, to))
            {
                using (SmtpClient bridge = new SmtpClient(bridgeAddr, 587))
                {
                    bridge.UseDefaultCredentials = true;
                    bridge.EnableSsl = true;
                    bridge.Credentials = new NetworkCredential(from, "I.am.the.sauce lord");//HARDER PASSWORD, Secured acc
                    mail.Subject = topic;
                    mail.Body = message;
                    bridge.Send(mail);
                }
            }
        }

    }
}