using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace hotel_management_system_with_SQL_database
{
    public partial class staff_bookRoom : UserControl
    {
        public staff_bookRoom()
        {
            InitializeComponent();
            displayRooms();
        }

        public void refreshData()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }
            displayRooms();
        }

        public void displayRooms()
        {
            roomsData rData = new roomsData();

            List<roomsData> listData = rData.roomsDataList();

            dataGridView2.DataSource = listData;
        }

        private int getID = 0;
        private decimal regPrice = 0;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView2.Rows[e.RowIndex];

                getID = (int)row.Cells[0].Value;
                bookRoom_roomID.Text = row.Cells[1].Value.ToString();
                bookRoom_roomType.Text = row.Cells[2].Value.ToString();
                bookRoom_roomName.Text = row.Cells[3].Value.ToString();
                bookRoom_regPrice.Text = (Convert.ToInt32(row.Cells[4].Value)).ToString("0.00");

                regPrice = Convert.ToDecimal(row.Cells[4].Value);

                bookRoom_imageView.ImageLocation = row.Cells[5].Value.ToString();

                bookRoom_status.Text = row.Cells[6].Value.ToString();
            }
        }

        private void bookRoom_schedBtn_Click(object sender, EventArgs e)
        {
            DateTime fromDate = bookRoom_from.Value;
            DateTime toDate = bookRoom_to.Value;

            TimeSpan countDays = toDate - fromDate;

            int days = countDays.Days;

            decimal totalPrice = (days * regPrice);

            if (totalPrice < 0)
            {
                MessageBox.Show("Something went wrong", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);

                bookRoom_total.Text = "0.00";
            }
            else if(totalPrice == 0)
            {
                bookRoom_total.Text = regPrice.ToString("0.00");
            }
            else
            {
                bookRoom_total.Text = (days * regPrice).ToString("0.00");
            }
        }

        private void bookRoom_bookBtn_Click(object sender, EventArgs e)
        {
            if (regPrice == 0 || bookRoom_total.Text == "0.00")
            {
                MessageBox.Show("Please fill all info correctly", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(bookRoom_status.Text != "Active")
            {
                MessageBox.Show("The room is unavailable!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                hotelData.roomID = bookRoom_roomID.Text;
                hotelData.fromDate = bookRoom_from.Value;
                hotelData.toDate = bookRoom_to.Value;
                hotelData.price = bookRoom_total.Text;

                Form formbg = new Form();

                try
                {
                    using (clientInfo ciForm = new clientInfo())
                    {
                        ciForm.Owner = formbg;
                        ciForm.ShowDialog();

                        formbg.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    formbg.Dispose();
                }
            }
        }

        public void clearFields()
        {
            bookRoom_roomID.Text = "_______";
            bookRoom_roomType.Text = "_______";
            bookRoom_roomName.Text = "_______";
            bookRoom_status.Text = "_______";
            bookRoom_total.Text = "0.00";
            bookRoom_regPrice.Text = "0.00";

            bookRoom_imageView.Image = null;
        }

        private void bookRoom_clearBtn_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }
}
