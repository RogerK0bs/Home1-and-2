using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Home1_and_2
{
    internal class Program
    {
        static SqlConnection OpenConnection() // Открыть соединение
        {
            string ConnectString = @"
                                    Data Source=localhost\SQLEXPRESS;
                                    Initial Catalog=Users_db;
                                    Integrated Security=SSPI;";
            SqlConnection sqlConnection = new SqlConnection(ConnectString);
            sqlConnection.Open();
            return sqlConnection;
        }
        static void Add_user(string Login, string Password, string Country) // Добавление записи
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConnection();
                string cmd = $"INSERT INTO Us_t (Login, Password, Country) VALUES ('{Login}', '{Password}', '{Country}')";
                SqlCommand adding = new SqlCommand(cmd, conn);
                int Result = adding.ExecuteNonQuery();
                if (Result != 1)
                {
                    Console.WriteLine("Failed Insert");
                }
                else
                {
                    Console.WriteLine("Add new User");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message} User is not insert;");

            }
            finally { conn.Close(); }
        }
        static void List_result(SqlDataReader reader)
        {
            for (int i = 0; i < reader.FieldCount - 1; i++)
            {
                Console.Write($"{reader.GetName(i)} - ");
            }
            Console.WriteLine(reader.GetName(reader.FieldCount - 1));
            bool checkRows = true;
            while (reader.Read())
            {
                checkRows = false;
                for (int i = 0; i < reader.FieldCount - 1; i++)
                {
                    Console.Write($"{reader[i]} - ");
                }
                Console.WriteLine(reader[reader.FieldCount - 1]);
            }
            if (checkRows)
            {
                Console.WriteLine("No rows, for result view;");
            }
        }
        static void Select_list()
        {

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = OpenConnection();
                SqlCommand query = new SqlCommand("SELECT * FROM Us_t", conn);
                reader = query.ExecuteReader();
                List_result(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message}");
            }
            finally
            {
                conn?.Close();
                reader?.Close();
            }
        }
        static void Select_id(int id)
        {

            SqlConnection conn = null;
            SqlDataReader reader = null;
            try
            {
                conn = OpenConnection();
                SqlCommand query = new SqlCommand($"SELECT * FROM Us_t WHERE id={id}", conn);
                reader = query.ExecuteReader();
                List_result(reader);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message} Failed ID");
            }
            finally
            {
                conn?.Close();
                reader?.Close();
            }
        }
        static void Delete_users(int id)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConnection();
                string cmdString = $"DELETE FROM Us_t WHERE id = {id};";
                SqlCommand cmd = new SqlCommand(cmdString, conn);
                int rowsAffect = cmd.ExecuteNonQuery();
                if (rowsAffect != 1)
                {
                    Console.WriteLine($"Delete failed ({rowsAffect})");
                }
                else
                { 
                    Console.WriteLine("Successfully deleted"); 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message} User is not delete");
            }
            finally
            {
                conn?.Close();
            }
        }
        static void Update_seres(string Login, string Password, string Country, int id)
        {
            SqlConnection conn = null;
            try
            {
                conn = OpenConnection();
                string cmd = $"UPDATE Us_t SET Login='{Login}', Password='{Password}', Country='{Country}' where id={id}";
                SqlCommand adding = new SqlCommand(cmd, conn);
                int Result = adding.ExecuteNonQuery();
                if (Result != 1)
                {
                    Console.WriteLine("Failed Update");
                }
                else
                {
                    Console.WriteLine("Update new User");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed: {ex.Message} User is not update;");

            }
            finally { conn.Close(); }

        }


        static void Main(string[] args)
        {
           Add_user("New", "NewPassword", "Country");
           Select_list();
          //Delete_users(4);
           Select_list();
           Console.ReadKey();
           Console.Clear();
           Select_id(6);
           Update_seres("q1", "q2", "q3", 6);
           Select_id(6);
            Console.ReadKey();
        }


    }
}

            
        
    

