using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace University.DAO
{
    public class DAOConnect
    {
        public MySqlConnection Connection { get; set; }
        public void Connect()
        {
            MySqlConnection Connection = DAOUtils.GetDBConnection();
            Connection.Open();
        }
        public void Disconnect()
        {
            Connection.Close();
        }
    }
}