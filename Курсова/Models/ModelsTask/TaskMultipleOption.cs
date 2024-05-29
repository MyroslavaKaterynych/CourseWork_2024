using System;
namespace Курсова.Models.ModelsTask
{
    public class TaskMultipleOption : Task
    {
        public int count_correct_answer;
        public List<string> answer_options;//варіанти відповідей на тест
        public List<string> answers;//вірна відповідь(а-відповідь)
        public TaskMultipleOption() { type = "MultipleOption"; }
        public TaskMultipleOption(string question, List<string> answer_options, List<string> answers, int assessment, int count_correct_answer):base()
        {
            this.question = question;
            this.answer_options = answer_options;
            this.answers = answers;
            this.assessment = assessment;
            this.count_correct_answer = count_correct_answer;
            type = "MultipleOption";
        }
        public override double ask_questions()
        {
            Console.WriteLine("Питання: " + question);
           
            for (int i = 0; i < answer_options.Count; i++)
            {

                Console.WriteLine(lettersForListAnswer[i].letter + " - " + answer_options[i]);

            }
            Console.WriteLine("Виберіть вірні варіанти відповідей(По черзі кожен варіант відповіді)");


            List<string> list_our_answers = new List<string>();
            for (int i = 0; i < answer_options.Count && i < count_correct_answer; i++)
            {
                string result = "";
                result = Console.ReadLine();
                if (result != ""   )
                    list_our_answers.Add(result.ToUpper());
                else break;
            }

            List<string> letter_correct_answer = new List<string>();
            for (int i = 0; i < answer_options.Count; i++)
            {
                for (int j = 0; j < answers.Count; j++)
                {
                    if (answer_options[i].ToUpper() == answers[j].ToUpper())
                    {
                        letter_correct_answer.Add(lettersForListAnswer[i].letter.ToString().ToUpper());
                    }

                }
            }


            int commonCount = list_our_answers.Intersect(letter_correct_answer).Count();
            int distinctCount = list_our_answers.Count - commonCount;

            double price_one_answer = assessment / letter_correct_answer.Count;
            double result_assesmant = price_one_answer * commonCount;

            return result_assesmant;
        }

    }
}


