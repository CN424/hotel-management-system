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
using System.Runtime.Remoting.Contexts;

namespace hotel_management_system_with_SQL_database
{
    public partial class clientInfo : Form
    {
        private string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\gelic\\OneDrive\\Documents\\hotel-database.mdf;Integrated Security=True;Connect Timeout=30";

        public clientInfo()
        {
            InitializeComponent();

            displayBookID();
        }

        private void close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public void displayBookID()
        {
            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();

                int getBookID = 0;

                string selectBID = "SELECT COUNT(id) FROM customer";

                using(SqlCommand cmd = new SqlCommand(selectBID, connect))
                {
                    getBookID = Convert.ToInt32(cmd.ExecuteScalar());

                    if(getBookID == 0)
                    {
                        getBookID = 1;
                    }
                    else
                    {
                        getBookID += 1;
                    }
                }
                clientInfo_bookID.Text = $"BID-{getBookID}";
            }
        }

        private void clientInfo_bookNowBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to book now?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (clientInfo_fullName.Text == "" || clientInfo_gender.SelectedIndex == -1
                    || clientInfo_address.Text == "" || clientInfo_email.Text == "" ||
                    clientInfo_number.Text == "" || hotelData.roomID == "")
                {
                    MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    using (SqlConnection connect = new SqlConnection(conn))
                    {
                        connect.Open();

                        string insertData = "INSERT INTO customer " +
                            "(book_id, full_name, email, contact, gender, address, room_id, price, status_payment, status" +
                            ", date_from, date_to, date_book) " +
                            "VALUES(@bookID, @fullname, @email, @contact, @gender, @address, @roomID," +
                            "@price, @statusP, @status, @dateFrom, @dateTo, @dateBook)";

                        using (SqlCommand cmd = new SqlCommand(insertData, connect))
                        {
                            cmd.Parameters.AddWithValue("@bookID", clientInfo_bookID.Text);
                            cmd.Parameters.AddWithValue("@fullname", clientInfo_fullName.Text);
                            cmd.Parameters.AddWithValue("@email", clientInfo_email.Text);
                            cmd.Parameters.AddWithValue("@contact", clientInfo_number.Text);
                            cmd.Parameters.AddWithValue("@gender", clientInfo_gender.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@address", clientInfo_address.Text);
                            cmd.Parameters.AddWithValue("@roomID", hotelData.roomID);
                            cmd.Parameters.AddWithValue("@price", hotelData.price);
                            cmd.Parameters.AddWithValue("@statusP", "Paid");
                            cmd.Parameters.AddWithValue("@status", "Checked In");
                            cmd.Parameters.AddWithValue("@dateFrom", hotelData.fromDate);
                            cmd.Parameters.AddWithValue("@dateTo", hotelData.toDate);

                            DateTime today = DateTime.Today;

                            cmd.Parameters.AddWithValue("@dateBook", today);

                            cmd.ExecuteNonQuery();

                            updateRoomStatus();

                            MessageBox.Show("Booked successfully!", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.Hide();
                        }
                    }
                }
            }
        }

        public void updateRoomStatus()
        {
            using(SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();

                string updateStatus = "UPDATE rooms SET status = @status WHERE room_id = @roomID";

                using(SqlCommand cmd = new SqlCommand(updateStatus, connect))
                {
                    cmd.Parameters.AddWithValue("@status", "Unavailable");
                    cmd.Parameters.AddWithValue("@roomID", hotelData.roomID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void clientInfo_clearBtn_Click(object sender, EventArgs e)
        {
            clientInfo_fullName.Text = "";
            clientInfo_email.Text = "";
            clientInfo_number.Text = "";
            clientInfo_gender.SelectedIndex = -1;
            clientInfo_address.Text = "";
        }

        private void clientInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
