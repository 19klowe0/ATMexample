using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATMexample
{
    public partial class Form1 : Form
    {
        Customer currentCustomer;
        ArrayList accountList = new ArrayList();
        Account selectedAccount;
        Bank id = new Bank(1);
        double machineCash;
        int withdrawCode = -1;
        String transType = "";
        int transferCode = -1;
        Account toAccount = null;
        Account fromAccount = null;

        public Form1()
        {
            InitializeComponent();
            machineCash = id.getBankMoney();
            //accountList = Account.retrieveAccounts(currentCustomer.getID());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)//withdrawal
        {
            label1.Text = "You are in the process of withdrawal";
            transType = "withdrawal";

            listBox1.Items.Clear();
            Account tempAccount;
            Console.WriteLine("number of account: " + accountList.Count);
            for (int i = 0; i < accountList.Count; i++)
            {
                tempAccount = (Account)accountList[i];
                listBox1.Items.Add( tempAccount.getAccountID());
            }
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;


        }

        private void button4_Click_1(object sender, EventArgs e)//deposit
        {
            label1.Text = "You are in the process of Deposit";
            transType = "deposit";
           

            listBox1.Items.Clear();
            Account tempAccount;
            Console.WriteLine("number of account: " + accountList.Count);//setting up the accounts for customer view
            for (int i = 0; i < accountList.Count; i++)
            {
                tempAccount = (Account)accountList[i];
                listBox1.Items.Add(tempAccount.getAccountID());
            }
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)//transfer
        {
            label1.Text = "You are in the process of Transfering Money";
            transType = "transfer";
            label2.Text = "Select an Account to transfer TO: ";
     

            listBox1.Items.Clear();
            Account tempAccount;
            Console.WriteLine("number of account: " + accountList.Count);
            for (int i = 0; i < accountList.Count; i++)
            {
                tempAccount = (Account)accountList[i];
                listBox1.Items.Add(tempAccount.getAccountID());
            }
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = false;
            signOutMessage.Visible = true;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel2.Visible = false;
            label1.Text = "Welcome to ZZZ Bank ATM Machine";
            label2.Text = "Select an Account:";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e) //when you select an account
        {
            //tableLayoutPanel2.Visible = false;
            //tableLayoutPanel3.Visible = true;

            selectedAccount = (Account)accountList[listBox1.SelectedIndex];  // what the selected account equals **************
            fromAccount = (Account)accountList[listBox1.SelectedIndex];
            
            if (transType.Equals("Withdrawal") && selectedAccount.checkDailyTransaction() == false)
            {
                withdrawCode = 1;
                label5.Text = "The transactions of this account have exceeded the max limit $3000 for today.\n"
                     + "Please select another account.";
                tableLayoutPanel2.Visible = false;
                tableLayoutPanel7.Visible = true;
            }
            else if (transType.Equals("checkBalance"))
            {
                label5.Text = "Your balance is\n " + "$"
                        + selectedAccount.getBalance() + "\n" + "From account: "
                        + selectedAccount.getAccountID();

                tableLayoutPanel2.Visible = false;
                tableLayoutPanel7.Visible = true;

            }
            else if (transType.Equals("transfer") && selectedAccount.checkDailyTransaction() == false)
            {
                transferCode = 1;
                label5.Text = "The transactions of this account have exceeded the max limit $3000 for today.\n"
                     + "Please select another account.";
                tableLayoutPanel2.Visible = false;
                tableLayoutPanel7.Visible = true;
            }

            else if (transType.Equals("transfer") && selectedAccount.checkDailyTransaction() == true)
            {
                

                listBox2.Items.Clear();
                Account tempAccount;
                Console.WriteLine("number of account: " + accountList.Count);
                for (int i = 0; i < accountList.Count; i++)
                {
                    tempAccount = (Account)accountList[i];
                    listBox2.Items.Add(tempAccount.getAccountID());


                }
                tableLayoutPanel2.Visible = false;
                tableLayoutPanel13.Visible = true;


            }

            else //if (transType.Equals(""))
            {
                label4.Text = "0";
                tableLayoutPanel2.Visible = false;
                tableLayoutPanel3.Visible = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)//check balance
        {
            label1.Text = "You are in the Process of Checking your Balance";
            transType = "checkBalance";
            
            listBox1.Items.Clear();
            Account tempAccount;
            Console.WriteLine("number of account: " + accountList.Count);
            for (int i = 0; i < accountList.Count; i++)
            {
                tempAccount = (Account)accountList[i];
               
                     listBox1.Items.Add(tempAccount.getAccountID());
            }
            tableLayoutPanel1.Visible = false;
            tableLayoutPanel2.Visible = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        private void button20_Click_1(object sender, EventArgs e)
        {
            tableLayoutPanel3.Visible = false;
            tableLayoutPanel2.Visible = true;

        }

        private void button18_Click(object sender, EventArgs e) //clear button
        {
            label4.Text = "0";

        }

        private void button7_Click(object sender, EventArgs e) //add 1
        {
            //label4.Text += "1";
            if (label4.Text == "0")
                label4.Text = "1";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "1";
        }

        private void button8_Click(object sender, EventArgs e) //2
        {
            if (label4.Text == "0")
                label4.Text = "2";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "2";
        }

        private void button9_Click(object sender, EventArgs e) //3
        {
            if (label4.Text == "0")
                label4.Text = "3";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "3";
        }

        private void button10_Click(object sender, EventArgs e) //4
        {
            if (label4.Text == "0")
                label4.Text = "4";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "4"; 
        }

        private void button11_Click(object sender, EventArgs e)//5
        {
            if (label4.Text == "0")
                label4.Text = "5";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "5";
        }

        private void button16_Click(object sender, EventArgs e)//6
        {
            if (label4.Text == "0")
                label4.Text = "6";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "6";
        }

        private void button15_Click(object sender, EventArgs e)//7
        {
            if (label4.Text == "0")
                label4.Text = "7";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "7";
        }

        private void button14_Click(object sender, EventArgs e)//8
        {
            if (label4.Text == "0")
                label4.Text = "8";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "8";
        }

        private void button13_Click(object sender, EventArgs e)//9
        {
            if (label4.Text == "0")
                label4.Text = "9";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "9";
        }

        private void button12_Click(object sender, EventArgs e)//0
        {
            if (label4.Text != "0" && label4.Text.Length <= 3)
                label4.Text = label4.Text + "0";
        }

        private void button17_Click(object sender, EventArgs e) //delete
        {
            if (label4.Text.Length - 1 != 0)
            {
                label4.Text = label4.Text.Substring(0, label4.Text.Length - 1);
            }
           
        }

        private void button19_Click(object sender, EventArgs e) //process button
        {
            double amount = Double.Parse(label4.Text);
            if (transType.Equals("withdrawal"))//withdrawal 
            {             
                withdrawCode = selectedAccount.withdrawMoney(amount, machineCash);
                id.updateBankMoney(transType, amount);
                if (withdrawCode == 0)
                {
                    label5.Text = "Please take the money.\n Transaction number: "
                        + selectedAccount.getNewTransaction().getTransNum() + "\n" + "Withdrawal amount: $"
                        + selectedAccount.getNewTransaction().getAmount() + "\n" + "From account: "
                        + selectedAccount.getAccountID();

                }
                else if (withdrawCode == 1)
                {
                    label5.Text = "The transactions of this account have exceeded the max limit $3000 for today.\n"
                        + "Please select another account.";
                }
                else if (withdrawCode == 2)
                {
                    label5.Text = "The amount will make the transactions of this account exceed the max limit $3000 for today.\n"
                        + "Please enter a smaller amount.";
                }
                else if (withdrawCode == 3)
                {
                    label5.Text = "The amount you entered is greater than the balance of the selected account.\n"
                        + "Please enter a smaller amount.";
                }
                else if (withdrawCode == 4)
                {
                    label5.Text = "The machine doesn't have enough cash for your withdrawal.\n"
                        + "Please enter a smaller amount.";
                }
                tableLayoutPanel3.Visible = false;
                tableLayoutPanel7.Visible = true;
            }

            if (transType.Equals("deposit")) //deposit
            {
                selectedAccount.depositMoney(amount);
                id.updateBankMoney(transType, amount);

                label5.Text = "Deposit was a Success.\n Transaction number: "
                        + selectedAccount.getNewTransaction().getTransNum() + "\n" + "Deposit amount: $"
                        + selectedAccount.getNewTransaction().getAmount() + "\n" + "To account: "
                        + selectedAccount.getAccountID();

                tableLayoutPanel3.Visible = false;
                tableLayoutPanel7.Visible = true;
            }

            else if (transType.Equals("transfer1")) //transfer
            {
                
                transferCode = toAccount.transferMoney(amount, machineCash, fromAccount, toAccount);

                if (transferCode == 0)
                {
                    label5.Text = "\n Transaction number: "
                                     + toAccount.getNewTransaction().getTransNum() + "\n" + "transfer amount: $"
                                    + toAccount.getNewTransaction().getAmount() + "\n" + "From account: "
                                    + toAccount.getAccountID() + "\n" + "To account: " 
                                    + fromAccount.getAccountID();
                }
                else if (transferCode == 1)
                {
                    label5.Text = "The transactions of this account have exceeded the max limit $3000 for today.\n"
                        + "Please select another account.";
                }
                else if (transferCode == 2)
                {
                    label5.Text = "The amount will make the transactions of this account exceed the max limit $3000 for today.\n"
                        + "Please enter a smaller amount.";
                }
                else if (transferCode == 3)
                {
                    label5.Text = "The amount you entered is greater than the balance of the selected from account.\n"
                        + "Please enter a smaller amount.";
                }
                
                
                tableLayoutPanel3.Visible = false;
                tableLayoutPanel7.Visible = true;


            }

            else if (transType.Equals("checkBalance"))
            {
                tableLayoutPanel3.Visible = false;
                tableLayoutPanel7.Visible = true;
            }

        }

        private void button21_Click(object sender, EventArgs e) //button for message panel change this to fit your requirements!
        {
            if (transType.Equals("pinError"))
            {
                tableLayoutPanel7.Visible = false;
                SignINScreen.Visible = true;
                label9.Text = "Pin";
                transType = "";

            }
            else if (transType.Equals("checkBalance"))
            {
                tableLayoutPanel7.Visible = false;
                tableLayoutPanel2.Visible = true;
            }
            else
            {
                tableLayoutPanel7.Visible = false;
                tableLayoutPanel2.Visible = true;
                transType = "transfer";
            }
            
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {
            EnterPin.Visible = true;
            SignINScreen.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //EnterPin.Visible = false;
            //tableLayoutPanel1.Visible = true;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            thankYouMessage.Visible = true;
            signOutMessage.Visible = false;
        }

        private void button40_Click(object sender, EventArgs e) //signout stuff
        {
            //Application.Exit();
            currentCustomer = null;
            for(int i = 0; i < accountList.Count; i++)
            {
                accountList[i] = null;
            }
            thankYouMessage.Visible = false;
            SignINScreen.Visible = true;
            

            

        }

        private void button39_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            signOutMessage.Visible = false;
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            toAccount = (Account)accountList[listBox2.SelectedIndex];

            tableLayoutPanel13.Visible = false;
            tableLayoutPanel3.Visible = true;
            transType = "transfer1";

        }

        private void button41_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Visible = true;
            tableLayoutPanel13.Visible = false;
            label1.Text = "Welcome to ZZZ Bank ATM Machine";
            label2.Text = "Select an Account:";
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "1";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "1";

        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "2";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "2";
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "3";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "3";
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "4";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "4";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "5";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "5";
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "6";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "6";
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "7";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "7";
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "8";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "8";
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (label9.Text == "Pin")
                label9.Text = "9";
            else if (label9.Text.Length <= 3)
                label9.Text = label9.Text + "9";
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (label4.Text == "Pin")
                label4.Text = "0";
            else if (label4.Text.Length <= 3)
                label4.Text = label4.Text + "0";
        }

        private void button35_Click(object sender, EventArgs e) //delete for pin
        {
            if (label9.Text.Length - 1 != 0)
            {
                label9.Text = label9.Text.Substring(0, label9.Text.Length - 1);
            }
        }

        private void button37_Click(object sender, EventArgs e)//clear all for pin
        {
            label9.Text = "Pin";
        }

        private void button36_Click(object sender, EventArgs e) //process for pin 
        {
            int pin = int.Parse(label9.Text);
            currentCustomer = new Customer(pin);
            if (currentCustomer.getID() == 0)
            {
                EnterPin.Visible = false;
                tableLayoutPanel7.Visible = true;
                tableLayoutPanel2.Visible = false;
                transType = "pinError";
                label5.Text = "You have entered an invalid pin please restart process";

            }
            else
                accountList = Account.retrieveAccounts(currentCustomer.getID());


            EnterPin.Visible = false;
            tableLayoutPanel1.Visible = true;
            label9.Text = "Pin";
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
