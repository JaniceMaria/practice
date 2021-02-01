using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace MiniProjectNew
{
    class Program
    {
        public DataTable dt = new DataTable();

        public string CompID;//{ get; set; }

        public static DataTable ImportCSV(string strFilePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(strFilePath))
            {
                string[] header = sr.ReadLine().Split(',');
                foreach (string hdr in header)
                {
                    dt.Columns.Add(hdr);
                }
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < header.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
        }

        public void userQuery1(DataTable dt, string year)
        {
            foreach (DataColumn var in dt.Columns)
                if (var.ColumnName == "Date received")
                    foreach (DataRow dr in dt.Rows)
                        if (dr[var.ColumnName].ToString().Contains(year))
                            Console.WriteLine("Year: " + year + "Complaint associated to: " + dr[3].ToString());
        }

        public void userQuery2(DataTable dt, string NameoftheBank)
        {

            foreach (DataColumn a in dt.Columns)
            {
                if (a.ColumnName == "Company")
                {
                    foreach (DataRow dr in dt.Rows)
                        if (dr[a.ColumnName].ToString() == NameoftheBank)
                            Console.WriteLine("Bank Name:" + dr[5].ToString() + " " + "has complaint regarding to:" + dr[3].ToString());

                }
            }
        }

        public void userQuery3(DataTable dt, string CompID)
        {
            //this.CompID = CompID;
            foreach (DataColumn a in dt.Columns)
            {
                if (a.ColumnName == "Complaint ID")
                {
                    foreach (DataRow dr in dt.Rows)
                        if (dr[a.ColumnName].ToString() == CompID)
                            Console.WriteLine("Complaints related to:" + dr[3].ToString());

                }
            }

        }


        public void userQuery4()
        {
            DateTime datereceived;
            DateTime datesent;
            foreach (DataRow dr in dt.Rows)
                if (dr["Complaint ID"].ToString() == CompID)
                {
                    Console.Write(dr["Complaint ID"] + "    ");
                    datereceived = DateTime.ParseExact(dr["Date received"].ToString(), "MM/dd/yyyy", null);
                    datesent = DateTime.ParseExact(dr["Date sent to company"].ToString(), "MM/dd/yyyy", null);
                    TimeSpan Diff_dates = datesent.Subtract(datereceived);
                    Console.WriteLine("Days:" + Diff_dates.Days);
                }


        }


        public void userQuery5(DataTable dt)
        {
            foreach (DataColumn a in dt.Columns)
                if (a.ColumnName == "Company response to consumer")
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[a.ColumnName].ToString() == "Closed" || dr[a.ColumnName].ToString() == "Closed with explanation")
                            Console.WriteLine("Complaints that are closed: " + dr[3].ToString());
                    }


        }

        public void userQuery6(DataTable dt)
        {
            foreach (DataColumn a in dt.Columns)
                if (a.ColumnName == "Timely response?")
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[a.ColumnName].ToString() == "Yes")
                            Console.WriteLine("Complaint that recieved a timely response: " + dr[3].ToString());
                    }


        }



        static void Main(string[] args)
        {

            Program prog = new Program();

            DataTable dt = ImportCSV("C:\\Users\\Janice\\Downloads\\complaints1.csv");

            Console.WriteLine("1:Display all the complaints based on the year provided by the user");
            Console.WriteLine("2:Display all the complaints based on the name of the bank provided by the user");
            Console.WriteLine("3:Display complaints based on the complaint id provided by the user");
            Console.WriteLine("4:Display number of days took by the Bank to close the complaint");
            Console.WriteLine("5:Display all the complaints closed/closed with explanation");
            Console.WriteLine("6:Display all the complaints which received a timely response ");

            Console.WriteLine("\n");

            Console.WriteLine("Enter the Number:");
            int num = Convert.ToInt32(Console.ReadLine());

            switch (num)
            {
                case 1:
                    Console.WriteLine("Enter the years:");
                    string year = Console.ReadLine();
                    prog.userQuery1(dt, year);
                    break;
                case 2:
                    Console.WriteLine("Enter the Name of the Bank:");
                    string BankName = Console.ReadLine();
                    prog.userQuery2(dt, BankName);
                    break;
                case 3:
                    Console.WriteLine("Enter the Complaint ID of customer:");
                    string CompID = Console.ReadLine();
                    prog.userQuery3(dt, CompID);
                    break;
                case 4:
                    prog.userQuery4();
                    break;
                case 5:
                    prog.userQuery5(dt);
                    break;
                case 6:
                    prog.userQuery6(dt);
                    break;

                default:
                    Console.WriteLine("Invalid!");
                    break;

            }
            Console.Read();

        }

    }
}
