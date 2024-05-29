using System;
namespace Курсова.Models.ModelsTask
{
	public class TaskCompliance : Task
	{
        public List<string> list_quastion;
        public List<string> list_answers;
		public	List<correctAnswer> list_correctAnswer= new List<correctAnswer>();
        public List<string> list_our_answers = new List<string>();
        public TaskCompliance() { type = "Compliance";}
        public TaskCompliance(string question, List<string> list_quastion, List<string> list_answers, List<correctAnswer> list_correctAnswer, int assessment): base()
		{
            this.question = question;
			this.list_quastion = list_quastion;
			this.list_answers = list_answers;
			this.list_correctAnswer = list_correctAnswer;
			this.assessment = assessment;
            type = "Compliance";
        }

        public override double ask_questions()
        {
            Console.WriteLine("Питання: "+question);
            Console.WriteLine();
            for (int i = 0; i < list_quastion.Count; i++)
            {
                Console.WriteLine((i+1) +": "+ list_quastion[i]);
            }
            Console.WriteLine();
            for (int i = 0; i < list_answers.Count; i++)
            {
                Console.WriteLine(lettersForListAnswer[i].letter + ": " + list_answers[i]);
            }
            Console.WriteLine("Введіть відповідність");
            for (int i = 0; i < list_quastion .Count; i++)
            {
                Console.Write((i + 1) + ": " + list_quastion[i] +" - ");
                string result = Console.ReadLine();
                int index = -1;
                for (int h = 0; h < lettersForListAnswer.Count; h++)
                {
                    if (result.ToUpper() == lettersForListAnswer[h].letter.ToString())
                    {
                        index = h; break;
                    }

                }

                list_our_answers.Add(list_answers[index]);
            }
            int correct_our_answer = 0;
            for (int i = 0; i < list_correctAnswer.Count; i++)
            {
                if (list_correctAnswer[i].correct_answer.ToUpper() == list_our_answers[i].ToUpper())
                {
                    correct_our_answer++;
                }
            }
            double for_one_correct_answer = assessment / list_correctAnswer.Count;
            return for_one_correct_answer * correct_our_answer;
        }

    }
	
}

