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
    class Account
    {
        int accountID;
        int customerID;
        double balance;
        double dailyTransactionTotal;
        double dailyTransactionLimit = 3000.0;
        string dailyTransactionDate;
        Transaction newTransaction;

        public Transaction getNewTransaction()
        {
            return newTransaction;
        }

        ~Account()
        {
            Console.WriteLine("id = " + accountID + " has been destroyed");

        }

        public int withdrawMoney(double amount, double machineCash)
        {
            updateDailyTransaction();
            if (checkDailyTransaction() == false)
                return 1;
            if (verifyDailyTransaction(amount) == false)
                return 2;
            if (verifyAccountBalance(amount) == false)
                return 3;
            if (checkMachineCash(amount, machineCash) == false)
                return 4;
            updateBalance(amount);
            updateDailyTransactionTotal(amount);
            updateAccountData();
            newTransaction = new Transaction(accountID, "Withdraw", amount, -1, -1);
            newTransaction.saveTransaction();
            return 0;
        }

        public int depositMoney(double amount)
        {
            updateDailyTransaction();
            updateBalanceAdd(amount);
            updateAccountData();
            newTransaction = new Transaction(accountID, "Deposit", amount, -1, -1);
            newTransaction.saveTransaction();
            
            return 0;
        }
        
        public int transferMoney(double amount, double machineCash, Account to, Account from)
        {
            updateDailyTransaction();
            if (checkDailyTransaction() == false)
                return 1;
            if (verifyDailyTransaction(amount) == false)
                return 2;
            if (verifyAccountBalance(amount) == false)
                return 3;

            from.updateDailyTransactionTotal(amount);

            //update balance
            from.updateBalance(amount);
            to.updateBalanceAdd(amount);

            //update the account
            from.updateAccountData();
            to.updateAccountData();

            newTransaction = new Transaction(accountID, "Transfer", amount, to.getAccountID(), from.getAccountID());
            newTransaction.saveTransaction();
            updateBalance(amount);

            return 0;
        }

        private void updateAccountData()
        {
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "UPDATE loweAccount SET dailyTransactionDate=@date, dailyTransactionTotal=@total, balance=@newBalance WHERE accountID=@acctID;";
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", dailyTransactionDate);
                cmd.Parameters.AddWithValue("@total", dailyTransactionTotal);
                cmd.Parameters.AddWithValue("@newBalance", balance);
                cmd.Parameters.AddWithValue("@acctID", accountID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            Console.WriteLine("Done.");

        }
        

        private void updateDailyTransactionTotal(double amount)
        {
            dailyTransactionTotal = dailyTransactionTotal + amount;
        }

        private void updateBalance(double amount)
        {
            balance -= amount;
        }
        private void updateBalanceAdd(double amount)
        {
            balance += amount;
        }

        private bool checkMachineCash(double amount, double machineCash)
        {
            if (amount > machineCash)
                return false;
            return true;
        }

        private bool verifyAccountBalance(double amount)
        {
            if (amount > balance)
                return false;
            return true;
        }
        private bool verifyDailyTransaction(double amount)
        {
            if ((dailyTransactionTotal + amount) > 3000.0)
                return false;
            else
                return true;
        }

        public bool checkDailyTransaction()
        {
            if (dailyTransactionTotal >= dailyTransactionLimit)
                return false;
            return true;
        }
        private void updateDailyTransaction()
        {
            string todayDate = DateTime.Now.ToString("yyyy-MM-dd");
            Console.WriteLine("old date: " + dailyTransactionDate);
            Console.WriteLine("new date: " + todayDate);
            if (!dailyTransactionDate.Equals(todayDate))
            {
                dailyTransactionDate = todayDate;
                dailyTransactionTotal = 0.0;
                
                Console.WriteLine("Date being changed.");
            }
        }

        public int getAccountID()
        {
            return accountID;
        }

        public double getBalance()
        {
            return balance;
        }

        public static ArrayList retrieveAccounts(int id)
        {
            ArrayList accountList = new ArrayList();
            //ArrayList eventList = new ArrayList();  //a list to save the events
            //prepare an SQL query to retrieve all the events on the same, specified date
            DataTable myTable = new DataTable();
            string connStr = "server=157.89.28.130;user=ChangK;database=csc340;port=3306;password=Wallace#409;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM loweAccount WHERE customerID=@myID ORDER BY accountID ASC";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@myID", id);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            //convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                Account newAccount = new Account();

                newAccount.accountID = Int32.Parse(row["accountID"].ToString());
                newAccount.customerID = Int32.Parse(row["customerID"].ToString());
                newAccount.balance = Double.Parse(row["balance"].ToString());
                newAccount.dailyTransactionTotal = Double.Parse(row["dailyTransactionTotal"].ToString());
                newAccount.dailyTransactionDate = row["dailyTransactionDate"].ToString();
                //newAccount.countedAmount = Double.Parse(row["countedAmount"].ToString());
                /*
                newEvent.eventDate = row["date"].ToString();
                newEvent.eventStartTime = Int32.Parse(row["startTime"].ToString());
                newEvent.eventEndTime = Int32.Parse(row["endTime"].ToString());
                newEvent.eventContent = row["content"].ToString();
                */
                accountList.Add(newAccount);
            }
            Console.WriteLine("*********" + accountList.Count);
            return accountList;  //return the event list
        }
    }
}
