using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Drawing.Printing;
using System.Drawing;

namespace Medical_Associate
{

    public partial class Accounts : MetroForm
    {
        //###################PRINT VARIABLES
        PrintDocument printdoc1 = new PrintDocument();
        PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        Panel pannel = null;
        //###################END OF PRINT VARIABLES

        ReportDocument crypt = new ReportDocument();
        SqlDataAdapter sda;

        Invoice inv = new Invoice();
        Invoice inv2 = new Invoice();
        public Accounts()
        {
            InitializeComponent();
            //##################declare event handler for printing in constructor
            printdoc1.PrintPage += new PrintPageEventHandler(printdoc1_PrintPage);
        }

        private void Accounts_Load(object sender, EventArgs e)
        {
            //    string connectionString = GetConnectionString();

            //    using (SqlConnection connection = new SqlConnection())
            //    {
            //        connection.ConnectionString = connectionString;

            //        connection.Open();

            //        Console.WriteLine("State: {0}", connection.State);
            //        Console.WriteLine("ConnectionString: {0}",
            //            connection.ConnectionString);

            //    sda = new SqlDataAdapter("select * from Invoice where ID > 0", connection);

            //    DataSet dst = new DataSet();
            //    sda.Fill(dst,"InvoiceDST");
            //    crypt.Load("Service Invoice.rpt");
            //    crypt.SetDataSource(dst);
            //    crystalReportViewer1.ReportSource = crypt;
            popgridview();
        }

        private void Button5_Click(object sender, EventArgs e)
        {

            if (metroTextBox8.Text == "" && metroTextBox1.Text == "")
            {
                popgridview();

            }
            else
            {
                var fname = metroTextBox8.Text.ToString();
                var sname = metroTextBox1.Text.ToString();
                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    var Plist = from p in db.Patient_Cue
                                where p.First_Name == fname || p.Second_Name == sname || p.First_Name == sname || p.Second_Name == fname
                                select new
                                {
                                    S_No = p.Service_Number,
                                    First_Name = p.First_Name,
                                    Second_Name = p.Second_Name
                                };

                    Plist.ToList();
                    int num = Plist.Count();
                    if (num < 1)
                    {
                        MetroFramework.MetroMessageBox.Show(this, "There is No patient With that name in your database please register as new");

                        // metroGrid1.DataSource = Plist.ToList();
                    }
                    else
                    {
                        dataGridView1.DataSource = Plist.ToList();


                    }
                }
            }
        }

        public void popgridview()
        {

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var pcue = from p in db.Patient_Cue
                           where p.Status != "Clear"
                           select new
                           {
                               S_No = p.Service_Number,
                               First_Name = p.First_Name,
                               Second_Name = p.Second_Name
                           };

                dataGridView1.DataSource = pcue.ToList();
            }
        }
        public void Poplategrid()
        {
            int s_noi = 0;
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                if (metroLabel9.Text != ". . . . . ")
                {
                    s_noi = Convert.ToInt32(metroLabel9.Text);
                }
                else
                {

                    MetroFramework.MetroMessageBox.Show(this, "PLease select patient to view invoice", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                var invoice = from p in db.Invoices
                              where p.Service_Number == s_noi
                              select new
                              {
                                  Item = p.Item,
                                  Description = p.Description,
                                  Amount = p.Amount
                              };

                dataGridView4.DataSource = invoice.ToList();

            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.CurrentRow.Index != -1)
            {
                //pcue.PatientID = Convert.ToInt32(metroGrid2.CurrentRow.Cells["Patient_ID"].Value);
                var s_no = Convert.ToInt32(dataGridView1.CurrentRow.Cells["S_No"].Value);
                metroLabel9.Text = Convert.ToInt32(dataGridView1.CurrentRow.Cells["S_No"].Value).ToString();
                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    var Ptests = from p in db.Lab_Test_Requests
                                 where p.ServiceID == s_no
                                 select new
                                 {
                                     Test = p.Test,
                                     Amount = p.Price
                                 };
                    var Pconsh = from p in db.Prescription_List
                                 where p.Service_Number == s_no
                                 select new
                                 {
                                     Item = p.Remedy_Treatment,
                                     Amount = p.Amount
                                 };
                    //var Proced = from p in db.procedures
                    //             where p.Service_Number == s_no
                    //             select new
                    //             {
                    //                 Item = p.Remedy_Treatment,
                    //                 Amount = p.Amount
                    //             }; patient procedures cariedout delete this line

                    dataGridView2.DataSource = Ptests.ToList();
                    dataGridView3.DataSource = Pconsh.ToList();
                    //dataGridView4.DataSource = Ptests.ToList();
                    //dataGridView2.DataSource = Proced.ToList();

                    //##############sumin datagrids
                    int sum = 0;
                    for (int i = 0; i < dataGridView2.Rows.Count; ++i)
                    {
                        sum += Convert.ToInt32(dataGridView2.Rows[i].Cells[1].Value);
                    }
                    metroLabel6.Text = sum.ToString();

                    int sum2 = 0;
                    for (int i = 0; i < dataGridView3.Rows.Count; ++i)
                    {
                        sum2 += Convert.ToInt32(dataGridView3.Rows[i].Cells[1].Value);
                    }
                    metroLabel7.Text = sum2.ToString();

                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            int sno = 0;
            if (metroLabel9.Text != "")
            {
                sno = Convert.ToInt32(metroLabel9.Text);
            }
            else
            {

                MetroFramework.MetroMessageBox.Show(this, "PLease select patient to view invoice", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            inv.Item = metroTextBox2.Text;
            inv.Service_Number = sno;
            inv.Description = metroTextBox3.Text;
            inv.Amount = Convert.ToInt32(metroTextBox4.Text);
            using (medicalAssociatesEntitiesBeta db2 = new medicalAssociatesEntitiesBeta())
            {
                db2.Invoices.Add(inv);
                db2.SaveChanges();
                Poplategrid();
            }
            //static private string GetConnectionString()
            //{
            //    // To avoid storing the connection string in your code,
            //    // you can retrieve it from a configuration file.
            //    return "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=medicalAssociates;"
            //        + "Integrated Security=true;";
            //}
        }
        //###########################################################START OF PRINT CODE
        Bitmap MemoryImage;
        public void GetPrintArea(Panel pnl)
        {
            MemoryImage = new Bitmap(pnl.Width, pnl.Height);
            pnl.DrawToBitmap(MemoryImage, new Rectangle(0, 0, pnl.Width, pnl.Height));
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (MemoryImage != null)
            {
                e.Graphics.DrawImage(MemoryImage, 0, 0);
                base.OnPaint(e);
            }
        }
        void printdoc1_PrintPage(object sender, PrintPageEventArgs e)
        {
            Rectangle pagearea = e.PageBounds;
            e.Graphics.DrawImage(MemoryImage, (pagearea.Width / 2) - (this.panel1.Width / 2), this.panel1.Location.Y);
        }
        public void Print(Panel pnl)
        {
            pannel = pnl;
            GetPrintArea(pnl);
            previewdlg.Document = printdoc1;
            previewdlg.ShowDialog();
        }
        //###########################################################END OF PRINT
        private void Button1_Click(object sender, EventArgs e)
        {
            //##########CALL TO PRINT METHOD
            Print(this.panel1);
        }
    }
}