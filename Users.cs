using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Medical_Associate
{
    public partial class Users : MetroForm
    {
        User users = new User();
        public Users()
        {
            InitializeComponent();
        }

        private void Users_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dBAssociatesDataSet.Users' table. You can move, or remove it, as needed.
            //this.usersTableAdapter.Fill(this.dBAssociatesDataSet.Users);
            populateDataGrid0();

        }

        private void populateDataGrid0()
        {
            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                var Usr = from p in db.Users
                               where p.Id > 0
                               select new
                               {
                                   UserID = p.Id,
                                   UserName = p.User_Name,
                                   Roles = p.Roles

                               };

                metroGrid2.DataSource = Usr.ToList();


            }

        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

            users.User_Name = metroTextBox1.Text.ToString();
            users.Password = metroTextBox2.Text.ToString();
            users.Roles = metroComboBox1.Text.ToString();



            using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
            {
                db.Users.Add(users);
                db.SaveChanges();
            }
            MetroFramework.MetroMessageBox.Show(this, "User Has been Registered Succefully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            populateDataGrid0();    
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (metroGrid2.CurrentRow.Index != -1)
            {

                users.Id = Convert.ToInt32(metroGrid2.CurrentRow.Cells["UserID"].Value);


                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    db.Users.Remove(users);
                    db.SaveChanges();
                }
                MetroFramework.MetroMessageBox.Show(this, "User Has been Deleted Succefully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                populateDataGrid0();

            }

        }

        private void metroGrid2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (metroGrid2.CurrentRow.Index != -1)
            {
                users.Id = Convert.ToInt32(metroGrid2.CurrentRow.Cells["UserID"].Value);
                int id = users.Id;
                using (medicalAssociatesEntitiesBeta db = new medicalAssociatesEntitiesBeta())
                {
                    users = db.Users.Where(x => x.Id == id).FirstOrDefault();

                    

                }
            }
        }
    }
}
