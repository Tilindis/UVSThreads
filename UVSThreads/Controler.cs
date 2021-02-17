using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UVSThreads
{
    class Controler
    {
        protected string connectionDuom;
        protected string sqlEil;
        protected MySqlConnection connection;
        
        public Controler()
        {
            connectionDuom = "Server=localhost;Database=uvsuzduotis;Uid=root;Pwd=";
            connection = new MySqlConnection(connectionDuom);
        }

    }
}
