﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DBProject
{
    class DataConnection
    {
        private static DataConnection instance;
        public string connectionstring;
        private SqlConnection connection;

        DataConnection()
        {

        }

        public static DataConnection get_instance()
        {
            if (instance == null)
            {
                instance = new DataConnection();
            }
            return instance;
        }


        public SqlConnection Getconnection()
        {
            connection = new SqlConnection(connectionstring);
            if (connection.State != System.Data.ConnectionState.Open)
            {
                connection.Open();
            }
            return connection;
        }

        public void close()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        public SqlDataReader Getdata(string query)
        {
            connection = Getconnection();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader data = cmd.ExecuteReader();
            return data;

        }

        public int Executequery(string query)
        {
            connection = Getconnection();
            SqlCommand cmd = new SqlCommand(query, connection);
            int row = cmd.ExecuteNonQuery();
            return row;
        }

        public void Closeconnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }
    }
}
