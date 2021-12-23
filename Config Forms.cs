using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Medical_Associate
{
    public partial class Config_Forms : MetroForm
    {
        Lab_Tests labModel = new Lab_Tests();
        X_Rays xry = new X_Rays();
        Consumable cons = new Consumable();
        public Config_Forms()
        {
            InitializeComponent();
        }

        private void Config_Forms_Load(object sender, EventArgs e)
        {
            populateDataGrid0(); populateDataGrid1(); populateDataGrid2();
            // TODO: This line of code loads data into the 'medicalAssociatesDataSet2.Consumables' table. You can move, or remove it, as needed.
            //this.consumablesTableAdapter.Fill(this.medicalAssociatesDataSet2.Consumables);
            // TODO: This line of code loads data into the 'medicalAssociatesDataSet1._X_Rays' table. You can move, or remove it, as needed.
            //this.x_RaysTableAdapter.Fill(this.medicalAssociatesDataSet1._X_Rays);
            // TODO: This line of code loads data into the 'medicalAssociatesDataSet.Lab_Tests' table. You can move, or remove it, as needed.
            //this.lab_TestsTableAdapter.Fill(this.medicalAssociatesDataSet.Lab_Tests);

        }

        private void populateDataGrid0()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Conb = from p in db.Consumables
                          where p.Id > 0
                          select new
                          {
                              ID = p.Id,
                              Item = p.Name,
                              Price = p.Price,
                              Current_stock = p.Current_stock
                              //BuyingPrice = p.Buying_Price
                          };

                Conb.ToList();


                metroGrid3.DataSource = Conb.ToList();


            }

        }

        private void populateDataGrid1()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Xry = from p in db.X_Rays
                          where p.Id > 0
                          select new
                          {
                              Item = p.Name,
                              Price = p.Price

                          };

                Xry.ToList();


                metroGrid2.DataSource = Xry.ToList();


            }

        }

        private void populateDataGrid2()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Lab = from p in db.Lab_Tests
                          where p.Id > 0
                          select new
                          {
                              Test = p.Name,
                              Price = p.Price

                          };

                Lab.ToList();


                metroGrid1.DataSource = Lab.ToList();


            }

        }
        private void button5_Click(object sender, EventArgs e)
        {
            labModel.Name = metroTextBox8.Text.ToString();
            labModel.Price = Convert.ToInt32(metroTextBox1.Text);

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                db.Lab_Tests.Add(labModel);
                db.SaveChanges();
            }
            populateDataGrid2();
            // TODO: This line of code loads data into the 'medicalAssociatesDataSet.Lab_Tests' table. You can move, or remove it, as needed.
            //this.lab_TestsTableAdapter.Fill(this.medicalAssociatesDataSet.Lab_Tests);
            MessageBox.Show("Has been Updated Succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            metroTextBox8.Text = metroTextBox1.Text = "";

        }

        private void button6_Click(object sender, EventArgs e)
        {
            xry.Name = metroTextBox3.Text.ToString();
            xry.Price = Convert.ToInt32(metroTextBox2.Text);

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                db.X_Rays.Add(xry);
                db.SaveChanges();
            }

            // TODO: This line of code loads data into the 'medicalAssociatesDataSet1._X_Rays' table. You can move, or remove it, as needed.
            //this.x_RaysTableAdapter.Fill(this.medicalAssociatesDataSet1._X_Rays);
            MessageBox.Show("Has been Registered Succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            metroTextBox3.Text = metroTextBox2.Text = "";

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (metroTextBox7.Text != "")
            {
                cons.Current_stock = Convert.ToInt32(metroTextBox6.Text) + Convert.ToInt32(metroTextBox7.Text);
            }
            
            else
            {
                cons.Current_stock = Convert.ToInt32(metroTextBox6.Text);
            }
            cons.Name = metroTextBox5.Text;
            if (metroLabel1.Text != ". . .")
            {
                cons.Id = Convert.ToInt32(metroLabel1.Text);
            }
            
            cons.Price = Convert.ToInt32(metroTextBox4.Text);
            cons.Buying_Price = Convert.ToInt32(metroTextBox9.Text);

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                if (cons.Name == "")
                {
                    db.Consumables.Add(cons);
                }
                else
                { 
                    db.Entry(cons).State = EntityState.Modified;
                }                
               
                db.SaveChanges();
            }
            populateDataGrid0();
            // TODO: This line of code loads data into the 'medicalAssociatesDataSet2.Consumables' table. You can move, or remove it, as needed.
            //this.consumablesTableAdapter.Fill(this.medicalAssociatesDataSet2.Consumables);
            MessageBox.Show("Has been Registered Succefully", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            metroTextBox5.Text = metroTextBox6.Text = metroTextBox4.Text = "";

        }

        private void metroGrid3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            metroLabel1.Text = metroGrid3.CurrentRow.Cells["ID"].Value.ToString();
            metroTextBox5.Text = metroGrid3.CurrentRow.Cells["Item"].Value.ToString();
            metroTextBox4.Text = metroGrid3.CurrentRow.Cells["Price"].Value.ToString();
            metroTextBox6.Text = metroGrid3.CurrentRow.Cells["Current_stock"].Value.ToString();
            button9.Text = "Update";
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox5_Click(object sender, EventArgs e)
        {
            button9.Text = "Save";
        }
    }
}
