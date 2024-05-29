using System;
namespace Курсова.Models.ClassCheckTask
{
    public class Question
    {
        public string question { get; set; }
        public string answer { get; set; }
    }

    public class CheckTask
    {
        public string type { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int id_test {get;set;}
        public List<Question> questions { get; set; }
    }
}

