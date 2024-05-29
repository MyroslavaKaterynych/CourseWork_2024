using System;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Курсова.Models.ModelsTask;

namespace Курсова.Models
{
	public class DataBase
	{
        string path = "server=localhost;user=root;database=coursework;password=Toortoor14;charset=utf8;";

        public bool AddNewUser(RegisterModel user)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(path))
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO users (name, surname, login, password) VALUES ('" + user.name + "','" + user.surname + "','" + user.login + "','" + user.password + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        command.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public User SearchUser(LoginModel model)
        {
            User user = null;
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sqlQuery = "SELECT id, name, surname, login, password FROM users WHERE (login='"+model.login+"' AND password='"+model.password+"');";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                user = new User();
                user.name = reader["name"].ToString();
                user.surname = reader["surname"].ToString();
                user.login = reader["login"].ToString();
                user.password = reader["password"].ToString();
            }
            connection.Close();
            return user;

        }
        public User GetUserByLogin(string login)
        {
            User user = null;
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sqlQuery = "SELECT id, name, surname, login, password FROM users WHERE (login='" + login + "');";
            MySqlCommand command = new MySqlCommand(sqlQuery, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                user = new User();
                user.id = Convert.ToInt32(reader["id"]);
                user.name = reader["name"].ToString();
                user.surname = reader["surname"].ToString();
                user.login = reader["login"].ToString();
                user.password = reader["password"].ToString();
            }
            connection.Close();
            return user;

        }
        public int AddNewTest(int id_user, string name, string description)
        {
            int result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(path))
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO test (name, description, id_user) VALUES ('" +name + "','" + description + "', '"+id_user+"')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту

                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public int AddNewOpenQuestion(int test_id, string question, string answer, int assessment)
        {

            int result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(path))
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO task_open_question (test_id, question, answer, assessment) VALUES ('" + test_id + "','" + question + "','" + answer + "','" + assessment + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public int AddNewCompliance(int test_id,string question, string list_question, string answer, int assessment)
        {

            int result = 0;
            try
            {
                MySqlConnection connection = new MySqlConnection(path);
              
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO task_compliance (test_id, question, list_question, answer, assessment) VALUES ('" + test_id + "','" + question + "','" + list_question + "','" + answer + "','" + assessment + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
           
            return result;
        }
        public int AddNewSuquence(int test_id, string question, string answer, int assessment)
        {

            int result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(path))
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO task_suquence (test_id, question, answer, assessment) VALUES ('" + test_id + "','" + question + "','" + answer + "','" + assessment + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public  int AddNewTest(int test_id, string question, string answer, int assessment)
        {

            int result = 0;
            try
            {
                MySqlConnection connection = new MySqlConnection(path);
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO task_test (test_id, question, answer, assessment) VALUES ('" + test_id + "','" + question + "','" + answer + "','" + assessment + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public int AddNewMultipleOptionTest(int test_id, string question, string answer, int count_correct_answer, int assessment)
        {

            int result = 0;
            try
            {
                MySqlConnection connection = new MySqlConnection(path);
                {
                    connection.Open();

                    // SQL запит для вставки даних в базу даних
                    string sql = "INSERT INTO task_multiple_option (test_id, question, answer, count_correct_answer, assessment) VALUES ('" + test_id + "','" + question + "','" + answer + "','" + count_correct_answer + "','" + assessment + "')";

                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {


                        // Виконання запиту
                        int rowsAffected = command.ExecuteNonQuery();

                        // Перевіряємо, чи було вставлено хоча б один рядок
                        if (rowsAffected > 0)
                        {
                            // Отримуємо ідентифікатор останнього вставленого запису
                            result = Convert.ToInt32(command.LastInsertedId);
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            return result;
        }
        public bool DeleteTest(int test_id, string tableName)
        {
            MySqlConnection connection = new MySqlConnection(path);
            {
                try
                {
                    connection.Open();

                    string sql = $"DELETE FROM {tableName} WHERE id = @testId";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@testId", test_id);

                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0; // Повертає true, якщо хоча б один рядок був видалений
                }
                catch (MySqlException ex)
                {
                    // Обробка помилки підключення або виконання запиту
                    Console.WriteLine("MySQL Error: " + ex.Message);
                    return  false;
                }
            }
        }
        public List<Test> GetMyTests(int user_id)
        {
            List<Test> tests = new List<Test>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT id, name, description, id_user FROM test WHERE id_user = '"+user_id+"';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                Test a = new Test();
                a.id = Convert.ToInt32(reader[0]);
                a.name = reader[1].ToString();
                a.description = reader[2].ToString();
                a.id_user = Convert.ToInt32(reader[3]);
                tests.Add(a);

            }
            connection.Close();
            return tests;

        }
        public Test GetTest(int id)
        {
            Test a=null;
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT id, name, description, id_user FROM test WHERE id = '" + id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                 a = new Test();
                a.id = Convert.ToInt32(reader[0]);
                a.name = reader[1].ToString();
                a.description = reader[2].ToString();
                a.id_user = Convert.ToInt32(reader[3]);


            }
            connection.Close();
            return a;

        }
        public List<TaskCompliance> list_TaskCompliance(int test_id)
        {
            List<TaskCompliance> list = new List<TaskCompliance>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM task_compliance WHERE test_id = '" + test_id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                TaskCompliance a = new TaskCompliance();
                a.question = reader["question"].ToString();
                a.assessment =Convert.ToInt32( reader["assessment"]);
                string list_ques= reader["list_question"].ToString();
                string list_answ = reader["answer"].ToString();

                string[] question = list_ques.Split(new char[] { '\n' });
                for (int i = 0; i < question.Length; i++)
                {
                    question[i] = question[i].Substring(3);
                    question[i] = question[i].Replace("\r", "");
                }
                string[] answer = list_answ.Split(new char[] { '\n' });
                for (int i = 0; i < question.Length; i++)
                {
                    answer[i] = answer[i].Substring(3);
                    answer[i] = answer[i].Replace("\r", "");
                }

                a.list_quastion = question.ToList();
                List<string> NE_SORT_ASNWER = new List<string>();
                for (int i = 0; i < question.Length; i++)
                {
                    correctAnswer c = new correctAnswer();
                    
                    c.correct_question = question[i];
                    c.correct_answer = answer[i];
                    a.list_correctAnswer .Add(c);
                    NE_SORT_ASNWER.Add(c.correct_answer);
                }
                a.list_answers = new List<string>(NE_SORT_ASNWER);
                a.list_answers.Sort();
              

                list.Add(a);

            }
            connection.Close();
            return list;
        }
        public List<TaskMultipleOption> list_TaskMultipleOption(int test_id)
        {
            List<TaskMultipleOption> list = new List<TaskMultipleOption>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM task_multiple_option WHERE test_id = '" + test_id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                TaskMultipleOption a = new TaskMultipleOption();
                a.question = reader["question"].ToString();
                a.assessment = Convert.ToInt32(reader["assessment"]);
                string list_answ = reader["answer"].ToString();

                string[] answer = list_answ.Split(new char[] { '\n' });
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = answer[i].Substring(3);
                    answer[i] = answer[i].Replace("\r", "");
                }

              
                a.answers = new List<string>();
                int count_correct_answer = Convert.ToInt32(reader["count_correct_answer"]);
                for (int i = 0; i < count_correct_answer; i++)
                {

                    a.answers.Add(answer[i]);
                }
                a.answer_options = new List<string>(answer.ToList());
                a.answer_options.Sort();
                list.Add(a);

            }
            connection.Close();
            return list;
        }
        public List<TaskOpenQuestion> list_TaskOpenQuestion(int test_id)
        {
            List<TaskOpenQuestion> list = new List<TaskOpenQuestion>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM task_open_question WHERE test_id = '" + test_id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                TaskOpenQuestion a = new TaskOpenQuestion();
                a.question = reader["question"].ToString();
                a.assessment = Convert.ToInt32(reader["assessment"]);
                a.corect_answer = reader["answer"].ToString();

               
                list.Add(a);

            }
            connection.Close();
            return list;
        }
        public List<TaskSuquence> list_TaskSuquence(int test_id)
        {
            List<TaskSuquence> list = new List<TaskSuquence>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM task_suquence WHERE test_id = '" + test_id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                TaskSuquence a = new TaskSuquence();
                a.question = reader["question"].ToString();
                a.assessment = Convert.ToInt32(reader["assessment"]);
                string list_answ = reader["answer"].ToString();

                string[] answer = list_answ.Split(new char[] { '\n' });
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = answer[i].Substring(3);
                    answer[i] = answer[i].Replace("\r", "");
                }
                a.list_correct_answer = answer.ToList();
                a.list_answers = new List<string>(a.list_correct_answer);
                a.list_answers.Sort();

                list.Add(a);

            }
            connection.Close();
            return list;
        }
        public List<TaskTest> list_TaskTest(int test_id)
        {
            List<TaskTest> list = new List<TaskTest>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM task_test WHERE test_id = '" + test_id + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                TaskTest a = new TaskTest();
                a.question = reader["question"].ToString();
                a.assessment = Convert.ToInt32(reader["assessment"]);
                string list_answ = reader["answer"].ToString();
                string[] answer = list_answ.Split(new char[] { '\n' });
                for (int i = 0; i < answer.Length; i++)
                {
                    answer[i] = answer[i].Substring(3);
                    answer[i] = answer[i].Replace("\r", "");
                }
                a.answer = answer[0];
                a.answer_options = new List<string>(answer.ToList());
                a.answer_options.Sort();



                list.Add(a);

            }
            connection.Close();
            return list;
        }
        public void SaveTestResult(User user, int id_test, string test_name, double assessment, string date)
        {
            using (MySqlConnection connection = new MySqlConnection(path))
            {
                connection.Open();

                // SQL запит для вставки даних в базу даних
                string sql = "INSERT INTO test_result (name, surname, login, id_test,test_name, assessment,date) VALUES ('" + user.name + "','" + user.surname + "','" + user.login + "','" + id_test + "','" + test_name + "','" + assessment + "','" + date + "')";

                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {


                    // Виконання запиту
                    command.ExecuteNonQuery();
                   
                }
            }

        }
        public List<Statistic_Test > GetStatistic_Test(int id_test, string login)
        {
            List<Statistic_Test> list = new List<Statistic_Test>();
            MySqlConnection connection = new MySqlConnection(path);
            connection.Open();

            string sql_zapros1 = "SELECT * FROM test_result WHERE login = '" + login + "';";
            MySqlCommand command = new MySqlCommand(sql_zapros1, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Statistic_Test a = new Statistic_Test();
                a.avtor_test = login;
                a.test_name = reader["test_name"].ToString();
              
                a.id_test = Convert.ToInt32(reader["id_test"]);
                a.assessment = reader["assessment"].ToString();
                a.login = reader["login"].ToString();
                
                a.user_name = reader["name"].ToString();
                a.user_surname  = reader["surname"].ToString();
                a.date = reader["date"].ToString();
              


                list.Add(a);

            }
            connection.Close();
            return list;
        }

        

    }
}

