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
    public partial class Dashboard : MetroForm
    {
        Users users = new Users();  

        XmlSerializer xs;
        List<UserClass> ls;

        DoctorsStatu dst = new DoctorsStatu();
        public Dashboard()
        {
            InitializeComponent();

            ls = new List<UserClass>();
            xs = new XmlSerializer(typeof(List<UserClass>));
        }

        //Use timer class

        Timer tmr;
        //Timer tmr2;

        private void Dasboard_Load(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;
            tmr = new Timer();
            //tmr2 = new Timer();

            //set time interval 10 sec

            tmr.Interval = 1000;
           // tmr2.Interval = 30000;

            //starts the timer
            //tmr2.Start();
            tmr.Start();

            //tmr2.Tick += tmr_Tick2;
            tmr.Tick += tmr_Tick;

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];
            userLogtxt.Text = "Welcome Back: " + u.User.ToString();

        }
        void tmr_Tick2(object sender, EventArgs e)

        {
            //if (radioButton4.Checked == true)
            //{
            //    dst.Dr_Busy = "";
            //}
            //else if (radioButton5.Checked == true)
            //{
            //    dst.Dr_IN = "";
            //}
            //else {
            //    dst.Dr_OUT = "";
            //}
        }
        void tmr_Tick(object sender, EventArgs e)

        {

            //update time label
            label3.Text = DateTime.Now.ToString();
            label4.Text = TimeZone.CurrentTimeZone.DaylightName.ToString();

            

        }

        private void Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;
            Patients np = new Patients();
            np.Show();
            pictureBox3.Visible = false;

            //FileStream fs = new FileStream("D:\\UserLog.xml", FileMode.Open, FileAccess.Read);

            //ls = (List<UserClass>)xs.Deserialize(fs);

            //UserClass u = ls[0];

            //if (u.Role != "Dr")
            //{
            //    button5.Enabled = false;

            //    MetroFramework.MetroMessageBox.Show(this, "Access Denied", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];

            if (u.Role == "Accounts" || u.Role == "Admin" )
            {
                Accounts acc = new Accounts();
                acc.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Sorry you do not have access to this resource ", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];

            if (u.Role == "Lab" || u.Role == "Admin" || u.Role == "Doctor")
            {

                pictureBox3.Visible = true;
                Lab lb = new Lab();
                lb.Show();
                pictureBox3.Visible = false;}
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Sorry you do not have access to this resource ", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];

            if (u.Role == "Lab" || u.Role == "Admin" || u.Role == "Doctor")
            {
                Coming_Soon cmg = new Coming_Soon();
                cmg.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Sorry you do not have access to this resource ", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Config_Forms conf = new Config_Forms();
            conf.Show();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
        }

        //private void radioButton4_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        if (radioButton6.Checked == true)
        //        {
        //            dst.Dr_IN = "False";
        //            dst.Dr_Busy = "False";
        //            dst.Dr_OUT = "True";
        //        }
        //        else if (radioButton5.Checked == true)
        //        {
        //            dst.Dr_IN = "True";
        //            dst.Dr_Busy = "False";
        //            dst.Dr_OUT = "False";

        //        }
        //        else
        //        {
        //            dst.Dr_IN = "True";
        //            dst.Dr_Busy = "False";
        //            dst.Dr_OUT = "False";
        //        }
        //        using (medicalAssociatesEntities db = new medicalAssociatesEntities())
        //        {
        //            dst = db.DoctorsStatus.Where(x => x.Id == 1).FirstOrDefault();
        //            db.Entry(dst).State = EntityState.Modified;
        //            db.SaveChanges();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        //tmr2.Stop();
        //        MessageBox.Show("Sorry an error occured trying to update Doctors Status"+ex+"");

        //        //throw;
        //    }

        //}

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {

            

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                dst = db.DoctorsStatus.Where(x => x.Id == 1).FirstOrDefault();
                db.Entry(dst).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {


            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                dst = db.DoctorsStatus.Where(x => x.Id == 1).FirstOrDefault();
                db.Entry(dst).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {


            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                dst = db.DoctorsStatus.Where(x => x.Id == 1).FirstOrDefault();
                db.Entry(dst).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private void metroButton4_Click(object sender, EventArgs e)
        {
            //Server sv = new Server();
            //sv.Show();
            imageserver ims = new imageserver();
            ims.Show();
        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            //Client cl = new Client();
            //cl.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];

            if (u.Role == "Admin" || u.Role == "Dispensary")
            {
                Dispensary dip = new Dispensary();
                dip.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Sorry you do not have access to this resource ", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void invoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
                pictureBox3.Visible = true;
           
            
            Medical_Invoice medinv = new Medical_Invoice();
            medinv.Show();
            pictureBox3.Visible = false; 

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("C:\\XML\\UserLog.xml", FileMode.Open, FileAccess.Read);

            ls = (List<UserClass>)xs.Deserialize(fs);

            UserClass u = ls[0];

            if (u.Role == "Admin")
            {
                Users usr = new Users();
                usr.Show();
            }
            else
            {
                MetroFramework.MetroMessageBox.Show(this, "Sorry you do not have access to this resource ", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void userLogtxt_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
 
