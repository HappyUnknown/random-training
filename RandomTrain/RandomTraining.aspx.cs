using RandomTrain.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            trainings = db.Trainings.ToList();
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
        }
        void AddInDb(string Name, string Plan, string Author, string ExtraInfo)
        {
            trainings.Add(new Training(Name, Plan, Author, ExtraInfo));
            db.SaveChanges();
        }
        void AddRow(int Id, string Name, string Plan, string Author, string ExtraInfo)
        {
            TableRow row = new TableRow();
            Button b = new Button();
            b.Click += btnSetForEdit_Click;
            b.Text = "✔";
            b.Width = 25;
            b.Height = 25;
            b.CssClass = "markButton";
            TableCell cell1 = new TableCell(), cell2 = new TableCell(), cell3 = new TableCell(), cell4 = new TableCell(), cell5 = new TableCell(), cell6 = new TableCell();
            cell1.Text = Id.ToString();
            cell2.Text = Name.ToString();
            cell3.Text = Plan.ToString();
            cell4.Text = Author.ToString();
            cell5.Text = ExtraInfo.ToString();
            cell6.Controls.Add(b);
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            trainingTable.Rows.Add(row);
            editionButtons.Add(b);
        }
        void AddTitle(string idtitle, string nametitle, string plantitle, string authortitle, string infotitle)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell(), cell2 = new TableCell(), cell3 = new TableCell(), cell4 = new TableCell(), cell5 = new TableCell(), cell6 = new TableCell();
            cell1.Text = idtitle.ToString();
            cell2.Text = nametitle.ToString();
            cell3.Text = plantitle.ToString();
            cell4.Text = authortitle.ToString();
            cell5.Text = infotitle.ToString();
            cell6.Text = string.Empty;
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            row.Cells.Add(cell3);
            row.Cells.Add(cell4);
            row.Cells.Add(cell5);
            row.Cells.Add(cell6);
            trainingTable.Rows.Add(row);
        }
        void AddRow(Training training)
        {
            AddRow(training.Id, training.Name, training.Plan, training.Author, training.ExtraInfo);
        }
        string BuildStrList(Training t)
        {
            return "ID:" + t.Id + Environment.NewLine + "Name:" + t.Name + Environment.NewLine + "Plan:" + t.Plan + Environment.NewLine + "Author:" + t.Author + Environment.NewLine + "Extra info:" + t.ExtraInfo;
        }
        List<string> BuildList(Training t)
        {
            List<string> liTraining = new List<string>();
            if (t.Author == string.Empty) t.Author = "Unknown Arnold";
            if (t.ExtraInfo == string.Empty) t.ExtraInfo = "Nothing more to say";
            liTraining.Add("ID: " + t.Id.ToString());
            liTraining.Add("Name: " + t.Name);
            liTraining.Add("Author: " + t.Author);
            liTraining.Add("Extra info: " + t.ExtraInfo);
            liTraining.Add("---PLAN---");
            string[] plan = t.Plan.Split(';');
            for (int i = 0; i < plan.Length; i++)
            {
                if (plan[i].Trim(' ').Length != 0)
                    liTraining.Add((i + 1) + ") " + plan[i]);
            }
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
            AddTitle("Id", "Name", "Plan", "Author", "Extra Info");
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
        }
        TrainingContext LoadDb()//Load database to the table
        {
            trainingTable.Rows.Clear();
            AddTitle("Id", "Name", "Plan", "Author", "Extra Info");
            List<Training> trainings;
            TrainingContext db = new TrainingContext();
            trainings = db.Trainings.ToList();
            for (int i = 0; i < trainings.Count; i++)
            {
                AddRow(trainings[i]);
            }
            return db;
        }
        protected void btnAddTraining_Click(object sender, EventArgs e)
        {
            Training t = new Training();
            t.Author = tbAuthor.Text;
            t.ExtraInfo = tbExtraInfo.Text;
            t.Name = tbName.Text;
            t.Plan = tbPlan.Text;
            if (tbPlan.Text.Trim(' ') != string.Empty && tbName.Text.Trim(' ') != string.Empty)
            {
                db.Trainings.Add(t);
                db.SaveChanges();
                ResponseAlert("New element added.");
            }
            else ResponseAlert("Input does not match requirements.");
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
            Training t = trainings[GetButtonIndex(sender)];
            for (int i = 0; i < editionButtons.Count; i++)
                editionButtons[i].BackColor = Color.FromArgb(222, 222, 222);
            (sender as Button).BackColor = Color.FromArgb(162, 255, 209);
            tbId.Text = t.Id.ToString();
            tbAuthor.Text = t.Author;
            tbExtraInfo.Text = t.ExtraInfo;
            tbName.Text = t.Name;
            tbPlan.Text = t.Plan;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (tbId.Text.Trim(' ') != string.Empty && tbPlan.Text.Trim(' ') != string.Empty && tbName.Text.Trim(' ') != string.Empty)
            {
                Training t = trainings.Where(x => x.Id == int.Parse(tbId.Text)).FirstOrDefault();
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                t.Name = tbName.Text;
                t.Author = tbAuthor.Text;
                t.ExtraInfo = tbExtraInfo.Text;
                t.Plan = tbPlan.Text;
                db.SaveChanges();
                ResponseAlert("Element on ID " + t.Id + " has been updated.");
            }
            else ResponseAlert("The element to edit hasn't been established.");
            LoadDb();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (tbId.Text.Trim(' ') != string.Empty && tbPlan.Text.Trim(' ') != string.Empty && tbName.Text.Trim(' ') != string.Empty)
            {
                Training t = trainings.Where(x => x.Id == int.Parse(tbId.Text)).FirstOrDefault();
                db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                db.Trainings.Remove(t);
                db.SaveChanges();
                ResponseAlert("Plan called \"" + t.Name + "\" is now deleted. Now quantity is: " + db.Trainings.Count());
            }
            else ResponseAlert("The element to remove hasn't been established.");
            LoadDb();
        }

        protected void btnGetTraining_Click(object sender, EventArgs e)
        {
            tblPlan.Rows.Clear();
            Training t = RandomTraining();
            List<string> planParts = BuildList(t);
            for (int i = 0; i < planParts.Count; i++)
            {
                TableCell cell = new TableCell();
                cell.Text = planParts[i];
                TableRow row = new TableRow();
                row.Cells.Add(cell);
                tblPlan.Rows.Add(row);
            }
        }
    }

}