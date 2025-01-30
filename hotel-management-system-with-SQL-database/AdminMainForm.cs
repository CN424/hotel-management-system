﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hotel_management_system_with_SQL_database
{
    public partial class AdminMainForm : Form
    {
        public AdminMainForm()
        {
            InitializeComponent();
        }

        private void logout_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirmation Message"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Form1 loginForm = new Form1();
                loginForm.Show();

                this.Hide();
            }
        }

        private void close_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Confirmation Message"
                , MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dashboard_btn_Click(object sender, EventArgs e)
        {
            admin_dashboard1.Visible = true;
            admin_addUser1.Visible = false;
            admin_rooms1.Visible = false;
            admin_customers1.Visible = false;

            admin_dashboard adDashboard = admin_dashboard1 as admin_dashboard;

            if(adDashboard != null)
            {
                adDashboard.refreshData();
            }
        }

        private void addUser_btn_Click(object sender, EventArgs e)
        {
            admin_dashboard1.Visible = false;
            admin_addUser1.Visible = true;
            admin_rooms1.Visible = false;
            admin_customers1.Visible = false;

            admin_addUser adAddUser = admin_addUser1 as admin_addUser;

            if (adAddUser != null)
            {
                adAddUser.refreshData();
            }
        }

        private void rooms_btn_Click(object sender, EventArgs e)
        {
            admin_dashboard1.Visible = false;
            admin_addUser1.Visible = false;
            admin_rooms1.Visible = true;
            admin_customers1.Visible = false;

            admin_rooms adRooms = admin_rooms1 as admin_rooms;

            if (adRooms != null)
            {
                adRooms.refreshData();
            }
        }

        private void customers_btn_Click(object sender, EventArgs e)
        {
            admin_dashboard1.Visible = false;
            admin_addUser1.Visible = false;
            admin_rooms1.Visible = false;
            admin_customers1.Visible = true;

            admin_customers adCustomers = admin_customers1 as admin_customers;

            if (adCustomers != null)
            {
                adCustomers.refreshData();
            }
        }
    }
}
