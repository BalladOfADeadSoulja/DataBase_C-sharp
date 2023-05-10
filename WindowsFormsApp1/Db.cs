using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace WindowsFormsApp1.connection
{
    public class StudentsInfo
    {
        public int Id { get; set; }
        public int course { get; set; }
        public string firstname { get; set; }
        public string secondname { get; set; }
        public string surname { get; set; }
      
    }

    internal class Db
    {
        public static List<StudentsInfo> StudentsList;
        public static void Conect()
        {
            string connString = "Server=localhost;Port=5432;User Id=postgres; Password=admin; Database=Lab;";
            var conn = new NpgsqlConnection(connString);
            var sql = "select  * from student";
            var cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            var reader = cmd.ExecuteReader(); 
            StudentsList = new List<StudentsInfo>();
            while (reader.Read())
            {
                var student = new StudentsInfo()
                {
                    Id = reader.GetInt32(0),
                    firstname = reader.GetString(1),//second name
                    secondname = reader.GetString(2), //GOOD
                    surname = reader.GetString(3),//sourse
                    course = reader.GetInt32(6)
                };
                StudentsList.Add(student);
            }
            conn.Close();
    }

        public static void Delete(string val)
        {
            string connString = "Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Lab;";
            var conn = new NpgsqlConnection(connString);
            var sql = "select  * from delete_student(" + val + ")";
            var cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public static void Read(string sql, string name, string secondname, string surname, string course)
        {
            string connString = "Server=localhost;Port=5432;User Id=postgres;Password=admin;Database=Lab;";
            var conn = new NpgsqlConnection(connString);
            var cmd = new NpgsqlCommand(sql, conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            StudentsList = new List<StudentsInfo>();
            while (reader.Read())
            {
                var student = new StudentsInfo()
                {
                    Id = reader.GetInt32(0),
                    firstname = reader.GetString(1),//second name
                    secondname = reader.GetString(2), //GOOD
                    surname = reader.GetString(3),//sourse
                    course = reader.GetInt32(6)
                };
                StudentsList.Add(student);
            }
            conn.Close();
        }

        public static void Add(string firstname, string secondname, string surname, int course)//id_group)
        {
            string sql = "Server = localhost ; Port = 5432; User Id = postgres; Password = admin; Database = Lab";
            NpgsqlConnection con = new NpgsqlConnection(sql);
            con.Open();
            NpgsqlCommand com = new NpgsqlCommand("INSERT INTO student(name, middle_name, surname, id_group, cource) VALUES(@p1, @p2, @p3, @p4, @p5)", con);

            var a = new NpgsqlParameter("@p1", NpgsqlTypes.NpgsqlDbType.Varchar);
            var b = new NpgsqlParameter("@p2", NpgsqlTypes.NpgsqlDbType.Varchar);
            var c = new NpgsqlParameter("@p3", NpgsqlTypes.NpgsqlDbType.Varchar);
            var d = new NpgsqlParameter("@p4", NpgsqlTypes.NpgsqlDbType.Bigint);
            var e = new NpgsqlParameter("@p5", NpgsqlTypes.NpgsqlDbType.Bigint);

            a.Value = firstname;
            b.Value = secondname;
            c.Value = surname;
            d.Value = 2;
            e.Value = course;

            com.Parameters.Add(a);
            com.Parameters.Add(b);
            com.Parameters.Add(c);
            com.Parameters.Add(d);
            com.Parameters.Add(e);
            com.ExecuteNonQuery();
            con.Close();
        }
        }
}
