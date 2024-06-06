using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;

namespace WpfApp1
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public String Date { get; set; }
        public String AccountNumber { get; set; }
        public String Client { get; set; }
        public SqlMoney Summa { get; set; }
        static SqlConnection connection;


        public Notice()
        {
            // Получение строки подключения из файла конфигурации
            var connString = ConfigurationManager
            .ConnectionStrings["DemoConnection"]
            .ConnectionString;
            // Создание объекта подключения
            connection = new SqlConnection(connString);
        }
        static Notice()
        {
            // Получение строки подключения из файла конфигурации
            var connString = ConfigurationManager
            .ConnectionStrings["DemoConnection"]
            .ConnectionString;
            // Создание объекта подключения
            connection = new SqlConnection(connString);
        }

        /// Переопределение метода ToString()

        public override string ToString()
        {
            return String.Format("id={0} - Date: {1} - AccountNumber: {2} - Client: {3} -Summa: {4} ", NoticeId, Date, AccountNumber, Client, Summa);
        }


        /// <summary>
        /// Получение списка всех платежек
        /// </summary>
        /// <returns>IEnumerable<Notice></returns>
        public static IEnumerable<Notice> GetAllNotices()
        {
            var commandString = "SELECT * FROM PaymentSlip";
            SqlCommand getAllCommand = new SqlCommand(commandString, connection);
            connection.Open();
            var reader = getAllCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var noticeId = reader.GetInt32(0);
                    var date = reader.GetString(1);
                    var accountNumber = reader.GetString(2);
                    var client = reader.GetString(3);
                    var summa = reader.GetSqlMoney(4);
                    var notice = new Notice
                    {
                        NoticeId = noticeId,
                        Date = date,
                        AccountNumber = accountNumber,
                        Client = client,
                        Summa = summa
                    };
                    yield return notice;
                }
            };
            connection.Close();
        }

        /// <summary>
        /// Добавление новой записи в базу данных
        /// </summary>
        public void Insert()
        {
            var commandString = "INSERT INTO PaymentSlip (Date, AccountNumber, Client, Summa)"
                    + "VALUES (@date, @accountNumber, @client, @summa)";
            SqlCommand insertCommand = new SqlCommand(commandString, connection);
            insertCommand.Parameters.AddRange(new SqlParameter[] {
                new SqlParameter("date", Date),
                new SqlParameter("accountNumber", AccountNumber),
                new SqlParameter("client", Client),
                new SqlParameter("summa", Summa),
            });
            connection.Open();
            insertCommand.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Получение записи с заданным id
        /// </summary>
        /// <param clientName="id">Значение NoticeId для поиска</param>
        /// <returns>объект класса Notice</returns>
        public static Notice GetClient(int noticeId)
        {
            foreach (var notice in GetAllNotices())
            {
                if (notice.NoticeId == noticeId)
                    return notice;
            }
            return null;
        }


        /// <summary>
        /// Изменение текущей записи в базе данных
        /// </summary>
        /*public void Update()
        {
            var commandString = "UPDATE PaymentSlip SET Date=@date,AccountNumber = @accountNumber, Client = @client, Summa = @summa WHERE(ClientId = @id)";
            SqlCommand updateCommand = new SqlCommand(commandString,
            connection);
            updateCommand.Parameters.AddRange(new SqlParameter[] {
                new SqlParameter("date", Date),
                new SqlParameter("accountNumber", AccountNumber),
                new SqlParameter("client", Client),
                new SqlParameter("summa", Summa)
            });
            connection.Open();
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }*/


        public void Update()
        {
            var commandString = "UPDATE PaymentSlip SET Date=@date, AccountNumber=@accountNumber, Client=@client, Summa=@summa WHERE AccountNumber=@accountNumber";
           
                SqlCommand updateCommand = new SqlCommand(commandString, connection);
                updateCommand.Parameters.AddRange(new SqlParameter[] {
            new SqlParameter("@date", Date),
            new SqlParameter("@accountNumber", AccountNumber),
            new SqlParameter("@client", Client),
            new SqlParameter("@summa", Summa),
           
        });
                connection.Open();
                updateCommand.ExecuteNonQuery();
                connection.Close();
           

        }







        /// <summary>
        /// Удаление записи из базы данных
        /// </summary>
        /// <param name="id">Значение NoticeId для удаляемой записи</param>
        public static void Delete(int id)
        {
            /* var commandString = "DELETE FROM PaymentSlip WHERE(NoticeId = @id)";
             SqlCommand deleteCommand = new SqlCommand(commandString, connection);
             deleteCommand.Parameters.AddWithValue("id", id);
             connection.Open();
             deleteCommand.ExecuteNonQuery();
             connection.Close();*/

            var commandString = "DELETE FROM PaymentSlip WHERE id = @id";
            SqlCommand deleteCommand = new SqlCommand(commandString, connection);
            deleteCommand.Parameters.AddWithValue("@id", id); 

            connection.Open();
            deleteCommand.ExecuteNonQuery();
            connection.Close();


        }







    }
}
