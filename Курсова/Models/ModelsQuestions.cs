using System;
using Курсова.Models.ModelsTask;
using Task = Курсова.Models.ModelsTask.Task;
using TaskFactory = Курсова.Models.ModelsTask.TaskFactory;

namespace Курсова.Models
{
    
	public class ModelsQuestions
	{
       public List<Task> tasks = new List<Task>();
        public ModelsQuestions()
        {
            var factory = new TaskFactory();

            // Create a TaskCompliance
            var complianceTask = factory.CreateTask(TaskType.Compliance, "Встановіть відповідність", 15, new ComplianceParameters
            {
                ListQuestion = new List<string> { "Україна", "Франція" },
                ListAnswers = new List<string> { "Київ", "Париж" },
                ListCorrectAnswers = new List<correctAnswer> { new correctAnswer { correct_answer = "Київ" }, new correctAnswer { correct_answer = "Париж" } }
            });

            // Create a TaskMultipleOption
            var multipleOptionTask = factory.CreateTask(TaskType.MultipleOption, "Виберіть міста України:", 5, new MultipleOptionParameters
            {
                AnswerOptions = new List<string> { "Київ", "Львів", "Лондон" },
                Answers = new List<string> { "Київ", "Львів" },
                CountCorrectAnswer = 2
            });

            // Create a TaskOpenQuestion
            var openQuestionTask = factory.CreateTask(TaskType.OpenQuestion, "What is the capital of France?", 4, new OpenQuestionParameters
            {
                CorrectAnswer = "Paris"
            });

            // Create a TaskSuquence
            var sequenceTask = factory.CreateTask(TaskType.Sequence, "Arrange the following in the correct order:", 9, new SequenceParameters
            {
                ListQuestion = new List<string> { "Q1", "Q2", "Q3" },
                ListAnswers = new List<string> { "A3", "A1", "A2" },
                ListCorrectAnswers = new List<correctAnswer>
            {
                new correctAnswer { correct_answer = "A1" },
                new correctAnswer { correct_answer = "A2" },
                new correctAnswer { correct_answer = "A3" }
            }
            });

            // Create a TaskTest
            var testTask = factory.CreateTask(TaskType.Test, "What is the capital of Spain?", 6, new TestParameters
            {
                AnswerOptions = new List<string> { "Madrid", "Barcelona", "Lisbon" },
                Answer = "Madrid"
            });

            // Conduct the tests
            tasks.Add(complianceTask);
            tasks.Add(multipleOptionTask);
            tasks.Add(openQuestionTask);
            tasks.Add(sequenceTask);
            tasks.Add(testTask);
        }//это контруктор, в котором хранятся все вопросы и сохраняются в список tasks
    }
    
}

