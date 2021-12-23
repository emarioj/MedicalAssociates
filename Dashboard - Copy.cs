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
    public partial class Dashboard : MetroForm
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
        //    tabContral.HideTab(tabLab);
        //    tabContral.HideTab(tabPatients);
        //    tabContral.HideTab(tabPrescribe);
        //    tabContral.HideTab(tabTraige);
        }

        private void newPrescritionBtn_Click(object sender, EventArgs e)
        {
            Prescription pr = new Prescription();
            pr.Show();
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Lab lb = new Lab();
            lb.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            TRIAGE tr = new TRIAGE();
            tr.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            New_Patient pt = new New_Patient();
            pt.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            tabContral.ShowTab(tabLab);
            tabContral.SelectTab(3);

        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Invoice Inv = new Invoice();
            Inv.Show();
        }
    }
}
