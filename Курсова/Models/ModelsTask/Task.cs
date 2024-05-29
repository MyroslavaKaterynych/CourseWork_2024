using System;
namespace Курсова.Models.ModelsTask
{
    public abstract class Task
    {
        public string question;
        public double assessment;//оцінка за вірну відповідь
        public string type;
        public List<LetterForTest> lettersForListAnswer = new List<LetterForTest>();//список букв варіантів відповідей

        public Task()
        {
            lettersForListAnswer.Add(new LetterForTest() { letter = 'А' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Б' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'В' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Г' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Д' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Е' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Є' });
            lettersForListAnswer.Add(new LetterForTest() { letter = 'Ж' });
        }
        public virtual double ask_questions()
        {

            return 0;
        }
       
    }


    public struct correctAnswer
    {
        public string correct_question;
        public string correct_answer;


    }
}

