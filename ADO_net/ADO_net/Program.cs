using System;
using System.Data.SqlClient;

namespace ADO_net
{
    class Program
    {
        public static void CreateTable(SqlConnection con)
        {
            string query =
            @"CREATE TABLE girls
                (
                    id int IDENTITY(1,1) NOT NULL,
                    name nvarchar(50) NULL,
                    eye_color nvarchar(50) NULL,
                    age int NOT NULL,
                    is_available bit NOT NULL,
                    first_date date NULL,
                    CONSTRAINT pk_id PRIMARY KEY (ID)
                );";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Table Created Successfully");
                Console.WriteLine();
            }
        }

        public static void SelectInformation(SqlConnection con)
        {
            string selectQuery = @"Select * From girls";
            using (SqlCommand cmd = new SqlCommand(selectQuery, con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
                reader.Close();
                Console.WriteLine();
            }
        }

        public static void SearchGirl(SqlConnection con, string search)
        {
            string searchQuery = $@"Select * From girls
                                   where Name like '%{search}%'
                                   or eye_color like '%{search}%'";
            using (SqlCommand cmd = new SqlCommand(searchQuery, con))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("\t{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
                        reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
                reader.Close();
                Console.WriteLine();
            }
        }

        public static void InsertGirlInformation(SqlConnection con)
        {
            string insertQuery = @"INSERT INTO girls(name, eye_color, age, is_available, first_date )
                                  VALUES('April', 'grey', 28, 1, '2021-01-30'),
                                  ('Judith', 'blue', 23, 1, '2020-06-06'),
                                  ('Olivia', 'green', 29, 1, '2020-08-07'),
                                  ('Magdalena', 'grey', 19, 1, '2020-10-30'),
                                  ('Dorothy', 'blue', 32, 1, '2020-09-06'),
                                  ('Dolly', 'brown', 18, 1, '2020-10-07')";
            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("A Girl Have Inserted Successfully");
                Console.WriteLine();
            }
        }

        public static void DeleteRow(SqlConnection con)
        {
            string dropQuery = @"Delete FROM girls WHERE Id = 4;";
            using (SqlCommand cmd = new SqlCommand(dropQuery, con))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("The Row Have Deleted Successfully");
                Console.WriteLine();
                SelectInformation(con);
                Console.WriteLine();
            }
        }
        public static void DropTable(SqlConnection con)
        {
            string dropQuery = @"Drop table girls";
            using (SqlCommand cmd = new SqlCommand(dropQuery, con))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("The Table Droped Successfully");
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Life;Integrated Security=True");


            try
            {
                con.Open();
                CreateTable(con);
                InsertGirlInformation(con);
                SelectInformation(con);
                SearchGirl(con, "a");
                DeleteRow(con);
                DropTable(con);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close();
            }
        }
    }
}
