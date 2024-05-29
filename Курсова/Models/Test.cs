using System;
using Курсова.Models.ModelsTask;

namespace Курсова.Models
{
	public class Test 
	{
		public int id;
		public string name;
		public string description;
		public int id_user;

		public List<TaskTest> taskTests;
		public List<TaskSuquence> taskSuquences;
		public List<TaskOpenQuestion> taskOpenQuestions;
        public List<TaskMultipleOption> taskMultipleOptions;
        public List<TaskCompliance> taskCompliances;

    }
	
}

