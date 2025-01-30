using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace hotel_management_system_with_SQL_database
{
    internal class usersData
    {
        private string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\gelic\\OneDrive\\Documents\\hotel-database.mdf;Integrated Security=True;Connect Timeout=30";

        public int ID { set; get; }
        public string Username { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
        public string Status { set; get; }
        public string DateReg { set; get; }

        public List<usersData> listUsersData()
        {
            List<usersData> listDate = new List<usersData>();

            using (SqlConnection connect = new SqlConnection(conn))
            {
                connect.Open();

                string selectData = "SELECT * FROM users";

                using (SqlCommand cmd = new SqlCommand(selectData, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        usersData uData = new usersData();

                        uData.ID = (int)reader["id"];
                        uData.Username = reader["username"].ToString();
                        uData.Password = reader["password"].ToString();
                        uData.Role = reader["role"].ToString();

                        // Assign 'status' and 'date_register' as strings
                        uData.Status = reader["status"]?.ToString();
                        uData.DateReg = reader["date_register"]?.ToString();

                        listDate.Add(uData); // Use 'Add' method on 'listDate'
                    }
                }
            }

            return listDate; // Return 'listDate'
        }
    }
}
