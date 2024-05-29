using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Курсова.Models.ModelsTask
{
    public class TaskFactory
    {
        public Task CreateTask(TaskType type, string question, int assessment, params object[] parameters)
        {
            switch (type)
            {
                case TaskType.Compliance:
                    var complianceParameters = parameters.Cast<ComplianceParameters>().Single();
                    return new TaskCompliance(question, complianceParameters.ListQuestion, complianceParameters.ListAnswers, complianceParameters.ListCorrectAnswers, assessment);

                case TaskType.MultipleOption:
                    var multipleOptionParameters = parameters.Cast<MultipleOptionParameters>().Single();
                    return new TaskMultipleOption(question, multipleOptionParameters.AnswerOptions, multipleOptionParameters.Answers, assessment, multipleOptionParameters.CountCorrectAnswer);

                case TaskType.OpenQuestion:
                    var openQuestionParameters = parameters.Cast<OpenQuestionParameters>().Single();
                    return new TaskOpenQuestion(question, openQuestionParameters.CorrectAnswer, assessment);

                case TaskType.Sequence:
                    var sequenceParameters = parameters.Cast<SequenceParameters>().Single();
                    return new TaskSuquence(question, sequenceParameters.ListQuestion, sequenceParameters.ListAnswers, sequenceParameters.ListCorrectAnswers, assessment);

                case TaskType.Test:
                    var testParameters = parameters.Cast<TestParameters>().Single();
                    return new TaskTest(question, testParameters.AnswerOptions, testParameters.Answer, assessment);

                default:
                    throw new ArgumentException("Invalid task type.");
            }
        }
    }

    public enum TaskType
    {
        Compliance,
        MultipleOption,
        OpenQuestion,
        Sequence,
        Test
    }

    // Parameters for different task types
    public class ComplianceParameters
    {
        public List<string> ListQuestion { get; set; }
        public List<string> ListAnswers { get; set; }
        public List<correctAnswer> ListCorrectAnswers { get; set; }
    }

    public class MultipleOptionParameters
    {
        public List<string> AnswerOptions { get; set; }
        public List<string> Answers { get; set; }
        public int CountCorrectAnswer { get; set; }
    }

    public class OpenQuestionParameters
    {
        public string CorrectAnswer { get; set; }
    }

    public class SequenceParameters
    {
        public List<string> ListQuestion { get; set; }
        public List<string> ListAnswers { get; set; }
        public List<correctAnswer> ListCorrectAnswers { get; set; }
    }

    public class TestParameters
    {
        public List<string> AnswerOptions { get; set; }
        public string Answer { get; set; }
    }

}
