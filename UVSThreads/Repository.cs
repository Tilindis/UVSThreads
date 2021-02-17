using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace UVSThreads
{
    class Repository : Controler
    {
        public void AddNewValue(string threadID, string data)
        {

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            
            try
            {
                sqlEil = $"INSERT INTO `symbols` (`id`, `threadid`, `time`, `data`) VALUES (NULL, '{threadID}', '{DateTime.Now}', \"{data}\")";
                MySqlCommand command = new MySqlCommand(sqlEil, connection);
                command.ExecuteReader();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

        }

    }
}
