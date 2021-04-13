using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ATMexample
{
    class Customer
    {
        int id = 0;

        public Customer(int num)//pin
        {
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM lowecustomer WHERE pinNumber=@myPIN";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@myPIN", num);
                MySqlDataReader myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {

                    id = (int)myReader["id"];
                }
                myReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine(num);
            Console.WriteLine("Done.");
            //id = num;
            if (id <= 0)
            {
                id = 0;
            }
            Console.WriteLine(id);
           

        }

        ~Customer()
        {
            Console.WriteLine("id = "+ id+ " has been destroyed");
        }
       

        public int getID()
        {
            return id;
        }
       
    }
}
