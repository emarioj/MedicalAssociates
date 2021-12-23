using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Medical_Associate
{
    public partial class New_Patient : MetroForm
    {
        Patient_List pcnt = new Patient_List();

        public New_Patient()
        {
            InitializeComponent();
        }

        private void New_Patient_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var gen = "";
            if (metroRadioButton1.Checked)
            {
                gen = "Male";
            }
            else {
                gen = "Female";
            }
            pcnt.First_Name = metroTextBox8.Text;
            pcnt.Second_Name = metroTextBox1.Text;
            pcnt.Gender = gen;
            pcnt.Age = Convert.ToInt32(metroTextBox2.Text);
            pcnt.Contact = Convert.ToInt32(metroTextBox3.Text);
            pcnt.Address = metroTextBox4.Text;
            pcnt.Date = Convert.ToDateTime(metroDateTime1.Text);
            pcnt.Status = metroTextBox6.Text;
            pcnt.Note = metroTextBox5.Text;




            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                db.Patient_List.Add(pcnt);
                db.SaveChanges();
            }
            MetroFramework.MetroMessageBox.Show(this, "Patient Has been Registered Succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


            metroTextBox1.Text = metroTextBox2.Text = metroTextBox3.Text = metroTextBox4.Text = metroTextBox5.Text = metroTextBox6.Text = metroTextBox8.Text = "";
            metroRadioButton1.Checked = false;
            metroRadioButton2.Checked = false;





        }

        private void New_Patient_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
