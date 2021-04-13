using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ATMexample
{
    class Transaction
    {
        int transactionID;
        int acctID;
        string transactionType;
        double amount;
        int fromAccountID;
        int toAccountID;
        DateTime date;

        public Transaction(int accNum, string transTy, double amountMoney, int fromAcc, int toAcc)
        {
            acctID = accNum;
            transactionType = transTy;
            amount = amountMoney;
            fromAccountID = fromAcc;
            toAccountID = toAcc;
            transactionID = 0;
        }

        public int getTransNum()
        {
            return transactionID;
        }

        public double getAmount()
        {
            return amount;
        }

        public void saveTransaction()
        {
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //string sql = "SELECT MAX(TransNum) FROM changTransaction WHERE accountNum=@num";
                string sql = "SELECT MAX(transactionID) FROM lowetransaction;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@num", accountNum);
                MySqlDataReader myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    transactionID = Int32.Parse(myReader[0].ToString());
                    Console.WriteLine("newTrans number" + transactionID);
                }
                myReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

            if (transactionID == -1)
            {
                Console.WriteLine("Error:  Cannot find and assign a new transaction number.");
            }
            else
            {
                transactionID = transactionID + 1;
                connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
                conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    string sql = "INSERT INTO lowetransaction (transactionID, transactionType, acctID, amount, fromAccountID, toAccountID, date)" +
                        " VALUES (@tNum,  @tType, @aNum, @amo, @fAcc, @tAcc, @dT)";
                    MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tNum", transactionID);
                    cmd.Parameters.AddWithValue("@tType", transactionType);
                    cmd.Parameters.AddWithValue("@aNum", acctID);
                    cmd.Parameters.AddWithValue("@amo", amount);
                    cmd.Parameters.AddWithValue("@fAcc", toAccountID);
                    cmd.Parameters.AddWithValue("@tAcc", fromAccountID);
                    date = DateTime.Now;
                    cmd.Parameters.AddWithValue("@dT", date);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
                Console.WriteLine("Done.");
            }
        }

    }
}
