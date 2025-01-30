using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hotel_management_system_with_SQL_database
{
    public partial class RegistrationForm : Form
    {
        private string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\gelic\\OneDrive\\Documents\\hotel-database.mdf;Integrated Security=True;Connect Timeout=30";
        public RegistrationForm()
        {
            InitializeComponent();
        }

        private void reg_signInbtn_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();

            this.Hide();
        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void reg_showPass_CheckedChanged(object sender, EventArgs e)
        {
            reg_pass.PasswordChar = reg_showPass.Checked ? '\0' : '*';
            reg_confirmPass.PasswordChar = reg_showPass.Checked ? '\0' : '*';
        }

        private void reg_signUpbtn_Click(object sender, EventArgs e)
        {
            if (reg_username.Text == "" || reg_pass.Text == "" || reg_confirmPass.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                using (SqlConnection connect = new SqlConnection(conn))
                {
                    connect.Open();

                    string checkUsername = "SELECT username FROM users WHERE username = @username";

                    using (SqlCommand checkU = new SqlCommand(checkUsername, connect))
                    {
                        checkU.Parameters.AddWithValue("@username", reg_username.Text.Trim());

                        SqlDataAdapter adapter = new SqlDataAdapter(checkU);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count != 0)
                        {
                            string tempEmail = reg_username.Text.Substring(0, 1).ToUpper() + reg_username.Text.Substring(1);
                            MessageBox.Show($"{tempEmail} already exists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (reg_pass.Text.Length < 8)
                        {
                            MessageBox.Show("Invalid Password. Password must be at least 8 characters.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (reg_pass.Text != reg_confirmPass.Text)
                        {
                            MessageBox.Show("Passwords do not match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            string query = "INSERT INTO users (username, password, role, status, date_register) " +
                                "VALUES(@username, @pass, @role, @status, @regDate)";

                            using (SqlCommand cmd = new SqlCommand(query, connect))
                            {
                                cmd.Parameters.AddWithValue("@username", reg_username.Text.Trim());
                                cmd.Parameters.AddWithValue("@pass", reg_pass.Text.Trim());
                                cmd.Parameters.AddWithValue("@role", "Staff"); // Correct parameter
                                cmd.Parameters.AddWithValue("@status", "Active"); // Correct parameter

                                DateTime today = DateTime.Today;
                                cmd.Parameters.AddWithValue("@regDate", today);

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Registered successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                Form1 loginForm = new Form1();
                                loginForm.Show();
                                this.Hide();
                            }
                        }
                    }
                }
            }
        }

        private void reg_signInBtn_Click_1(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Hide();
        }
    }
}
