using System;
using static System.Net.Mime.MediaTypeNames;

namespace Курсова.Models.ModelsTask
{
  public class TaskTest : Task
    {
        public List<string> answer_options;//варіанти відповідей на тест
        public string answer;//вірна відповідь(а-відповідь)

        public TaskTest() { type = "Test"; }

        public TaskTest(string question, List<string> answer_options, string answer, int assessment):base()
        {
            this.question = question;
            this.answer_options = answer_options;
            this.answer = answer;
            this.assessment = assessment;
            type = "Test";
        }
        public override double ask_questions()
        {
            Console.WriteLine("Питання: " + question);
            for (int i = 0; i < answer_options.Count; i++)
            {
                Console.WriteLine(lettersForListAnswer[i].letter +" - "+ answer_options[i]);

            }
            Console.WriteLine("Виберіть варіант відповіді(вкажіть букву)");
            string result = Console.ReadLine().ToUpper();
            int number_correct_answer = -1;
            for (int i = 0; i < answer_options.Count; i++)
            {
                if (answer_options[i] == answer)
                {
                    number_correct_answer = i;
                    break;
                }
            }
            int number_our_answer = -1;
            for (int i = 0; i < lettersForListAnswer.Count; i++)
            {
                if (lettersForListAnswer[i].letter.ToString() == result)
                {
                    number_our_answer = i;
                    break;
                }
            }
            if (number_correct_answer == number_our_answer)
            {
                return assessment;
            }
            else
                return 0;
        }
        
    }

}

