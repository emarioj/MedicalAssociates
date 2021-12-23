using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Medical_Associate
{
    public partial class Dispensary : MetroForm
    {
        public Dispensary()
        {
            InitializeComponent();
        }

        private void Dispensary_Load(object sender, EventArgs e)
        {
            Consumable consbl = new Consumable();

            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {

                var cons = from p in db.Consumables
                           where p.Id >= 0
                           select new
                           {
                               ID = p.Id,
                               Name = p.Name,
                               Current_Stock = p.Current_stock

                           };

                cons.ToList();


                metroGrid3.DataSource = cons.ToList();
            }
        }

        Bitmap bitmap;//bitmap print
        private void button1_Click(object sender, EventArgs e)
        {
            //bitmap print
            button1.Visible = false;
            this.Controls.Add(panel1);
            Graphics grp = panel1.CreateGraphics();
            Size formsize = this.ClientSize;
            bitmap = new Bitmap(formsize.Width, formsize.Height, grp);
            grp = Graphics.FromImage(bitmap);

            Point panLocation = PointToScreen(panel1.Location);
            grp.CopyFromScreen(panLocation.X, panLocation.Y, 5, 5, formsize);
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
            printPreviewDialog1.ShowDialog();
            button1.Visible = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            //bitmap print
            e.Graphics.DrawImage(bitmap,0 ,0);
        }
    }
}
