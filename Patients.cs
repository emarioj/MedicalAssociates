using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using MetroFramework.Forms;

namespace Medical_Associate
{
    public partial class Patients : MetroForm
    {
        Patient_Cue pcue = new Patient_Cue();
        Inpateint inp = new Inpateint();
        Patient_List plst = new Patient_List();
        TRAIGE trg = new TRAIGE();


        
        //#################################xml file stream#################################################

        XmlSerializer xs;
        List<Patient_Cue> ls;

        XmlSerializer xsR;
        List<Lab_Test_Requests> lsR;


        XmlSerializer xsu;
        List<UserClass> lsu;


        public Patients()
        {
            InitializeComponent();
            //#################################xml file stream#################################################

            ls = new List<Patient_Cue>();
            xs = new XmlSerializer(typeof(List<Patient_Cue>));

            lsu = new List<UserClass>();
            xsu = new XmlSerializer(typeof(List<UserClass>));

            lsR = new List<Lab_Test_Requests>();
            xsR = new XmlSerializer(typeof(List<Lab_Test_Requests>));


        }

        public void Patients_Load(object sender, EventArgs e)

        {
            populateDataGrid0(); populateCombobox1();
            //#########################populate datagride views####################################

             populateDataGrid1(); populateDataGrid2(); populateDataGrid3(); populateDataGrid0();

            //#####################################################################################

        }

        private void populateDataGrid0()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var patients = from p in db.Patient_List
                                 where p.Age != 2200
                                 select new
                                 {
                                     PatientID = p.Id,
                                     First_Name = p.First_Name,
                                     Second_Name = p.Second_Name,
                                     Gender = p.Gender,
                                     Age = p.Age,
                                     Contact = p.Contact,
                                     Address = p.Address,
                                     Date = p.Date,
                                     Note = p.Note

                                 };

                patients.ToList();


                metroGrid1.DataSource = patients.ToList();


            }

        }
        private void populateDataGrid1()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Inpatients = from p in db.Patient_Cue
                                 where p.Status == "Admited"
                                 select new
                                 {
                                     PatientID = p.PatientID,
                                     PatientFname = p.First_Name,
                                     PatientSname = p.Second_Name,
                                     //Pstatus = p.Status

                                 };

                Inpatients.ToList();


                metroGrid3.DataSource = Inpatients.ToList();


            }

        }
        public void populateDataGrid2()
        {

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            lsu = (List<UserClass>)xsu.Deserialize(fs);
            UserClass uu = lsu[0];
            String Dr = uu.User.ToString();

           

            // ##########gets the logedin user and displays patients posted to that user/dr#######################
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                

                var PatientCue = from p in db.Patient_Cue
                                 where p.Status != "Cleared" && p.Presiding_Dr == Dr
                                 select new
                                 {
                                     PatientID = p.PatientID,
                                     PatientFname = p.First_Name,
                                     PatientSname = p.Second_Name,
                                     Pstatus = p.Status,
                                     PservicerNO = p.Service_Number

                                 };

                PatientCue.ToList();


                metroGrid2.DataSource = PatientCue.ToList();
            }
        }
        private void populateDataGrid3()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var PatientCue = from p in db.Patient_Cue
                                 where p.Status != "Discharged"
                                 select new
                                 {
                                     PatientID = p.PatientID,
                                     Fname = p.First_Name,
                                     Sname = p.Second_Name,
                                     Status = p.Status,
                                     PservceID = p.Service_Number

                                 };


                    PatientCue.ToList();


                    metroGrid4.DataSource = PatientCue.ToList();
                
            }
        }
        private void populateDataGrid4()
        {
            var serviceID = Convert.ToInt32(metroGrid4.CurrentRow.Cells["PservceID"].Value);
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Presctn = from p in db.Prescription_List
                              where p.Service_Number == serviceID
                              select new
                              {
                                  ID = p.Id,
                                  Treatment = p.Remedy_Treatment,
                                  Diagnosis = p.Diagnosis,
                                  Dosage = p.Dosage,
                                  No_Dates = p.Days,
                                  Amount = p.Amount,
                                  Note = p.Note

                              };

                Presctn.ToList();


                metroGrid5.DataSource = Presctn.ToList();
            }
        }
        public void populateCombobox1()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var docx = from p in db.Users
                              where p.Roles == "Doctor"
                              select new
                              {
                                  p.User_Name

                              };
                comboBox1.DisplayMember = "User_Name";
                comboBox1.ValueMember = "User_Name";
                comboBox1.DataSource = docx.ToList();
                //######################having some error !!!!!!
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            New_Patient np = new New_Patient();
            if (np.Visible != true)
            {

                np.Show();

            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this,"Window Already active", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Patients_Activated(object sender, EventArgs e)
        {

            // TODO: This line of code loads data into the 'medicalAssociatesDataSet3.Patient_List' table. You can move, or remove it, as needed.
            //this.patient_ListTableAdapter.Fill(this.medicalAssociatesDataSet3.Patient_List);
        }

        private void metroGrid1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (metroGrid1.CurrentRow.Index != -1)
            //{
            //    //metroLabel6.Text = Convert.ToInt32(metroGrid1.CurrentRow.Cells["Service_Number"].Value).ToString();
            //    pcue.PatientID = Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value);
            //    metroLabel11.Text = metroGrid1.CurrentRow.Cells["firstNameData"].Value.ToString();
            //    metroLabel12.Text = metroGrid1.CurrentRow.Cells["secondNameData"].Value.ToString();
            //    //metroLabel9.Text = Convert.ToInt32(metroGrid1.CurrentRow.Cells["ageData"].Value).ToString();
            //    pcue.Date = DateTime.Today;

            //    using (medicalAssociatesEntities db = new medicalAssociatesEntities())
            //    {
            //        db.Patient_Cue.Add(pcue);
            //        db.SaveChanges();
            //    }
            //    MessageBox.Show("Patient Has been added to the CUE Succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);


            //}
        }



        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void metroGrid3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && metroTabControl1.SelectedTab.Text == "Patients")
            {

                if (metroGrid1.CurrentRow.Index != -1)
                {
                    //    metroLabel6.Text = Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value).ToString();
                    pcue.PatientID = Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value);
                    pcue.First_Name = metroGrid1.CurrentRow.Cells["First_Name"].Value.ToString();
                    pcue.Second_Name = metroGrid1.CurrentRow.Cells["Second_Name"].Value.ToString();
                    pcue.Age = Convert.ToInt32(metroGrid1.CurrentRow.Cells["age"].Value);
                    pcue.Date = DateTime.Today;
                    pcue.Presiding_Dr = comboBox1.Text;

                    using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                    {
                        db.Patient_Cue.Add(pcue);
                        db.SaveChanges();
                    }

                    // TODO: This line of code loads data into the 'medicalAssociatesDataSet3.Patient_Cue' table. You can move, or remove it, as needed.
                    //this.patient_CueTableAdapter.Fill(this.medicalAssociatesDataSet3.Patient_Cue);

                    MetroFramework.MetroMessageBox.Show(this, "Patient "+ Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value) +" Has been added to the CUE Succefully to Dr. " + pcue.Presiding_Dr.ToString() + "", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //MetroFramework.MetroMessageBox.Show(null,"Patient Has been added to the CUE Succefully");


                    //##################################xml file stream url for xml file################################################
                    //#############xml patient cache for the triag #################################################
                    FileStream fs = new FileStream("C:\\XML\\PatientLog.xml", FileMode.Create, FileAccess.Write);
                    pcue.PatientID = Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value);
                    pcue.Service_Number = Convert.ToInt32(metroGrid1.CurrentRow.Cells["PatientID"].Value);

                    ls.Add(pcue);
                    xs.Serialize(fs, ls);

                    fs.Close();

                    //#################################xml file stream#################################################
                    TRIAGE tr = new TRIAGE();
                    tr.Show();

                }
            }
            else {

                MetroFramework.MetroMessageBox.Show(this, "Please Go to the Patients tab and Select Doctor For This Patient" ,"info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            inp.Service_Number = Convert.ToInt32(metroLabel53.Text);
            inp.Status = "Discharged";
            pcue.Service_Number = Convert.ToInt32(metroLabel53.Text);
            pcue.Status = "Discharged";
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                db.Entry(inp).State = EntityState.Modified;
                db.SaveChanges();
            }
        }  

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Prescription pr = new Prescription();
            pr.Show();
        }

        private void metroGrid4_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            populateDataGrid4();
        }

        private void metroGrid5_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (metroGrid5.CurrentRow.Index != -1)
            {
                metroLabel39.Text = metroGrid5.CurrentRow.Cells["Diagnosis"].Value.ToString();
                metroLabel42.Text = metroGrid5.CurrentRow.Cells["Treatment"].Value.ToString();
                metroLabel44.Text = metroGrid5.CurrentRow.Cells["Dosage"].Value.ToString();
                metroLabel48.Text = metroGrid5.CurrentRow.Cells["No_Dates"].Value.ToString();
                metroLabel50.Text = metroGrid5.CurrentRow.Cells["Amount"].Value.ToString();
                metroLabel46.Text = metroGrid5.CurrentRow.Cells["Note"].Value.ToString();

            }
        }

        private void metroGrid4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            populateDataGrid4();
        }

        private void metroGrid2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (metroGrid2.CurrentRow.Index != -1)
                {
                    pcue.PatientID = Convert.ToInt32(metroGrid2.CurrentRow.Cells["Patient_ID"].Value);

                    plst.Id = Convert.ToInt32(metroGrid2.CurrentRow.Cells["Patient_ID"].Value);
                    try
                    {
                        trg.PatientID = Convert.ToInt32(metroGrid2.CurrentRow.Cells["Patient_ID"].Value);
                    }
                    catch (Exception)
                    {
                        metroLabel9.Visible = true;

                    }
                    var pID = trg.PatientID = Convert.ToInt32(metroGrid2.CurrentRow.Cells["Patient_ID"].Value);

                    using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                    {
                        pcue = db.Patient_Cue.Where(x => x.PatientID == pcue.PatientID).FirstOrDefault();
                        plst = db.Patient_List.Where(x => x.Id == plst.Id).FirstOrDefault();

                        metroLabel6.Text = pcue.Service_Number.ToString();
                        metroLabel52.Text = pcue.PatientID.ToString();
                        metroLabel11.Text = pcue.First_Name.ToString();
                        metroLabel12.Text = pcue.Second_Name.ToString();
                        //metroLabel9.Text = pcue.Age.ToString();
                        //metroLabel23.Text = pcue.Date.ToString();

                        metroLabel7.Text = plst.Address.ToString();
                        metroLabel10.Text = "0" + plst.Contact.ToString();
                        metroTextBox2.Text = plst.Note.ToString();
                        metroLabel29.Text = plst.Status.ToString();

                        try
                        {
                            trg = db.TRAIGEs.Where(x => x.PatientID == trg.PatientID).FirstOrDefault();

                            if (trg != null)
                            {
                                //metroLabel24.Text = trg.Weight.ToString();
                                //metroLabel26.Text = trg.Height.ToString();
                                //metroLabel25.Text = trg.Bod_temp.ToString();
                                //metroLabel21.Text = trg.Blood_pressure.ToString();
                                //metroLabel20.Text = trg.Respiratory_rate.ToString();

                                if (trg.Weight.ToString() != null)
                                {
                                    metroLabel24.Text = trg.MUAC.ToString();
                                }
                                else
                                {
                                    metroLabel24.Text = "Null";
                                }
                                if (trg.Height.ToString() != null)
                                {
                                    metroLabel26.Text = trg.Height.ToString();
                                }
                                else
                                {
                                    metroLabel26.Text = "Null";
                                }
                                if (trg.Bod_temp.ToString() != null)
                                {
                                    metroLabel25.Text = trg.Bod_temp.ToString();
                                }
                                else
                                {
                                    metroLabel25.Text = "Null";
                                }
                                if (trg.Blood_pressure.ToString() != null)
                                {
                                    metroLabel21.Text = trg.Blood_pressure.ToString();
                                }
                                else
                                {
                                    metroLabel21.Text = "Null";
                                }
                                if (trg.Respiratory_rate.ToString() != null)
                                {
                                    metroLabel20.Text = trg.Respiratory_rate.ToString();
                                }
                                else
                                {
                                    metroLabel20.Text = "Null";
                                }

                                metroLabel9.Visible = false;
                            }
                            else
                            {

                                metroLabel9.Visible = true;
                            }
                        }
                        catch (NullReferenceException)
                        {

                            metroLabel9.Visible = true;
                        }

                    }
                }
            }
            catch (NullReferenceException) {

                MetroFramework.MetroMessageBox.Show(this,"Something went wrong somewhere but dont worry it maybe a minor error Or The paptient is missing some information","Ooops",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Prescription prct = new Prescription();
            prct.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchPatients();
        }

        private void searchPatients()
        {
            if (metroTextBox8.Text == "" && metroTextBox1.Text == "")
            {
                populateDataGrid0();
            }
            else
            {
                var fname = metroTextBox8.Text.ToString();
                var sname = metroTextBox1.Text.ToString();
                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    var Plist = from p in db.Patient_List
                                where p.First_Name == fname || p.Second_Name == sname || p.First_Name == sname || p.Second_Name == fname
                                select new
                                {
                                    PatientID = p.Id,
                                    First_Name = p.First_Name,
                                    Second_Name = p.Second_Name,
                                    Gender = p.Gender,
                                    Age = p.Age,
                                    Contact = p.Contact,
                                    Address = p.Address,
                                    Date = p.Date,
                                    Note = p.Note

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




                        metroGrid1.DataSource = Plist.ToList();
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (metroLabel52.Text != ". . . .")
            {
                inp.PatientId = Convert.ToInt32(metroLabel52.Text);
                inp.DOA = DateTime.Now.Date;
                inp.Status = "Admited";
                pcue.Status = "Admited";


                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {

                    db.Entry(pcue).State = EntityState.Modified;
                    db.Inpateints.Add(inp);
                    db.SaveChanges();
                }
                populateDataGrid1();

                MetroFramework.MetroMessageBox.Show(this,"Patient Has been admited Succefully ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //MetroFramework.MetroMessageBox.Show(null,"Patient Has been added to the CUE Succefully");
                TRIAGE tr = new TRIAGE();
                tr.Show();
            }
            else {
                //MessageBox.Show("");
                MetroFramework.MetroMessageBox.Show(this, "Please Select Patient To be admited");
            }
            
        }

        private void metroGrid3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (metroGrid3.CurrentRow.Index != -1)
            {
                inp.PatientId = Convert.ToInt32(metroGrid3.CurrentRow.Cells["PatientID"].Value);
                metroLabel31.Text = metroGrid3.CurrentRow.Cells["PatientID"].Value.ToString();
                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    inp = db.Inpateints.Where(x => x.PatientId == inp.PatientId).FirstOrDefault();


                    metroLabel34.Text = inp.DOA.ToString();
                    metroLabel36.Text = inp.DOD.ToString();
                    metroLabel37.Text = inp.Status.ToString();
                    metroLabel14.Text = inp.Ward_No.ToString();
                    metroTextBox3.Text = inp.Note.ToString();
                    
                }
            }
        }

        private void metroGrid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            var num = 0;
            if (metroLabel6.Text != ". . . .")
            {
                num = Convert.ToInt32(metroLabel6.Text);

                //##################################xml file stream   and xml file path  #########################
                FileStream fs = new FileStream("C:\\XML\\PatientLog.xml", FileMode.Create, FileAccess.Write);
                Lab_Test_Requests us = new Lab_Test_Requests();
                us.ServiceID = num;
                
                lsR.Add(us);
                xsR.Serialize(fs, lsR);

                fs.Close();
                

                //#################################xml file stream#################################################

                Test_Result tr = new Test_Result();
                tr.Name = "treat";
                tr.Show(); 

            }
            else 
            {
                MetroFramework.MetroMessageBox.Show(this, "Please select patient before checking for test results", "Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } 
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            metroLabel56.Text = comboBox1.Text.ToString();
        }
    }
}
