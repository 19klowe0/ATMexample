using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ATMexample
{
    
    class Bank
    {
        double bankMoney;

        public Bank(int id)
        {

            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM lowebank WHERE bankID=@myID";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@myID", id);
                MySqlDataReader myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {

                    bankMoney = (double)myReader["bankMoney"];
                }
                myReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine(bankMoney);
            Console.WriteLine("Done.");

        }

        public void updateBankMoney(String transType, double amount)
        {
            if (transType.Equals("withdrawal"))
            {
                updateBalance(amount);
            }
            if (transType.Equals("deposit"))
            {
                updateBalanceAdd(amount);
            }


            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE lowebank SET bankMoney=@newbankMoney WHERE bankID= 1";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@newbankMoney", bankMoney);
                //cmd.Parameters.AddWithValue("@myID", bankID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");
            Console.WriteLine(bankMoney);

        }
        private void updateBalance(double amount)
        {
            bankMoney -= amount;
        }
        private void updateBalanceAdd(double amount)
        {
            bankMoney += amount;
        }
        public double getBankMoney()
        {
            return bankMoney;
        }
    }
}
