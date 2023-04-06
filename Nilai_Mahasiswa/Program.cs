using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nilai_Mahasiswa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi ke Database\n");
                    Console.WriteLine("Masukan Server :");
                    string server = Console.ReadLine();
                    Console.WriteLine("Masukan User ID : ");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukan Password : ");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukan database tujuan : ");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                MySqlConnection conn;
                                string connectionString;
                                connectionString = "SERVER=" + server + ";DATABASE=" + db + ";UserID=" + user + ";PASSWORD=" + pass + ";Port=3306";

                                conn = new MySqlConnection(connectionString);
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Keluar");
                                        Console.WriteLine("\nEnter your Choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch) 
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA MAHASISWA\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA MAHASISWA\n");
                                                    Console.WriteLine("Masukan NIM : ");
                                                    string NIM = Console.ReadLine();
                                                    Console.WriteLine("Masukan NIM Mahasiswa : ");
                                                    string NmaMhs = Console.ReadLine();
                                                    Console.WriteLine("Masukan Nama Mahasiswa : ");
                                                    string TglLahir = Console.ReadLine();
                                                    Console.WriteLine("Masukan Tanggal Lahir Mahasiswa : ");

                                                    try
                                                    {
                                                        pr.insert(NIM, NmaMhs, TglLahir, conn);
                                                        conn.Close();
                                                    }
                                                    catch { }
                                                }
                                                break;
                                            case '3':
                                                {
                                                    conn.Close();
                                                    return;
                                                }
                                        }
                                    }
                                    catch 
                                    { 
                                    }
                                }
                            }

                    }
               
                }
                catch 
                {
                }
            }
        }
        public void baca(MySqlConnection con)
        {
            MySqlDataAdapter cmd = new MySqlDataAdapter("Select * From ProdiTI.Mahasiswa", con);
            DataSet ds = new DataSet();
            cmd.Fill(ds, "Mahasiswa");
            DataTable dt = ds.Tables["Mahasiswa"];

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn col in dt.Columns)
                {
                    Console.WriteLine(row[col]);
                }
                Console.Write("\n");
            }
        }
        public void insert(string NIM, string NmaMhs, string TglLahir, MySqlConnection con)
        {
            string str;
            str = "insert into ProdiTI.MAHASISWA (NIM, NamaMhs, TglLahir)" + " values(@nim,nma,TglLahir)";
            MySqlCommand cmd = new MySqlCommand(str, con);
            cmd.CommandText = str;

            cmd.Parameters.Add(new MySqlParameter("nim", NIM));
            cmd.Parameters.Add(new MySqlParameter("nma", NmaMhs));
            cmd.Parameters.Add(new MySqlParameter("tglLahir", TglLahir));
     
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
