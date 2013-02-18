using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient; //Our collection of classes/methods for doing work with SQL databases

namespace DatabaseTest
{
    class Program
    {
        /*To better understand some possible topics for future employment, I decided to write up an explaination for myself
         * and others to better understand opening, executing commands, and closing SQL database connections. The original posted code was found
         * at http://www.daniweb.com/software-development/csharp/threads/21636/how-do-you-connect-to-a-sql-database-using-c# posted by user Paladine. All
         * Credit for this code should really go to that person. I commented extensively the code to make sense of what was going on since the original had no comments and one
         * of my current prosepctive employers needs code to connect to various databases. 
         * 
         * Any programmer will tell you that part of your time in a development environment will be about maintaining someone else's code. I decided as a learning exercise
         * to use MSDN and other resources to make some sense of this code. 
         * 
         * I learned C# pretty much in an applied way, and my comments might mix some terminology (like 'instances', which is a JAVA programming term, not really used in C#), but the points
         * should be otherwise clear.
         * 
         * As I have linked above, this code was originally posted elsewhere, but not really explained in any fashion. I am not responsible for the code.*/

        static void Main(string[] args)
        {
            //Code to open an SQL Connection
            //Comments where appropriate to explain new concepts

            //going to attempt to open an SQL Connection below
            try
            {
                /*The following code does this: It creates an instance of the SqlConnection object
                 * It calls the TCP/IP stack with the DBMSSOCN function in the network libraries
                 * Source can be local or remote. Local code would be Data Source=(local) Remote needs IPV4 to work
                 * Datbase refers to database name (Northwind is a sample SQL server in VS2010 resources)
                 * Additional info in the string can be User/Password or authentication for the database
                 */
                SqlConnection mySQL = new SqlConnection(@"Network Library=DBMSSOCN;Data Source=192.168.11.7,2345;database=Northwind;User id=;Password=;");

                //The glory of C# .NET--to start a connection with the connection SQL object we created, it as simple as running a Open() method
                mySQL.Open();
                //We could just end with a connection close method call, but let's do something
                //We're going to Read Data from an SQL database using a Query

                //Creating a command to parse through the SQL data connection
                SqlCommand myCommand = mySQL.CreateCommand();
                //That line creates a command object for the connection
                //We now want to send a SQL command through the query
                myCommand.CommandText = "SELECT CustomerID, CompanyName FROM Customers";
                //The above line creates the SQL statement. This particular one asks for data from the Customers
                //Table in the database for the CustomerID and CompanyName columns. If the connection is successful
                //it will return the CustomerID and CompanyName data.

                //We now need to 'Execute' the command
                SqlDataReader myReader = myCommand.ExecuteReader();

                //The code below will write to the terminal the formatted column data of each item in the Customer ID
                // and CompanyName in the Customers table in the Northwind database. It will continue until a null
                // terminator (probably) is returned from the SqlDataReader.Read() function.
                while (myReader.Read())
                {
                    Console.WriteLine("\t{0}\t{1}", myReader["CustomerID"], myReader["CompanyName"]);
                }

                //Time to close some connections. Remember to close the most recently created object first, then work
                //backwards

                myReader.Close();
                mySQL.Close();

                //End of Try case
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                //If the SQL connection cannot be established, this will execute. Because the server and other configuration
                //information is wrong, this will probably execute once I debug and run it. This code is just to explain and show the
                //basics of an SQL connection
            }



        }
    }
}
