using System;
using Курсова.Models.ClassCheckTask;
using Курсова.Models.ModelsTask;

namespace Курсова.Models
{
	public static class CheckTestUser
	{
		public static double Check(List<CheckTask> task)
		{
            DataBase db = new DataBase();
            int id_test = task[0].id_test;
            List<TaskCompliance> taskCompliance = db.list_TaskCompliance(id_test);
            List<TaskMultipleOption> taskMultipleOption = db.list_TaskMultipleOption(id_test);
            List<TaskOpenQuestion> taskOpenQuestion = db.list_TaskOpenQuestion(id_test);
            List<TaskSuquence> taskSuquence = db.list_TaskSuquence(id_test);
            List<TaskTest> taskTest = db.list_TaskTest(id_test);
            double assessment=0;
            for (int i = 0; i < task.Count; i++)
            {


                if (task[i].type=="Test")
                {
                    TaskTest test = taskTest.FirstOrDefault(t => t.question == task[i].question);

                   
                    if (test.answer.ToLower() == task[i].answer.ToLower())
                    {
                        assessment += test.assessment;
                    }

                }
                else if (task[i].type == "MultipleOption")
                {
                    TaskMultipleOption test = taskMultipleOption.FirstOrDefault(t => t.question == task[i].question);
                    List<string> user_anwer = task[i].answer.Replace(" ","").Split(new char[] { ',' }).ToList();

                    double assessment_for_one_asnwer = test.assessment / test.answers.Count;
                    int count_user_corr_answer = 0;

                    for (int q = 0; q < user_anwer.Count; q++)
                    {
                        for (int w = 0; w < test.answers.Count; w++)
                        {
                            if (user_anwer[q] == test.answers[w])
                            {
                                count_user_corr_answer++;
                            }
                        }
                    }
                    assessment += (count_user_corr_answer*assessment_for_one_asnwer);



                }
                else if (task[i].type == "Suquence")
                {
                    TaskSuquence test = taskSuquence.FirstOrDefault(t => t.question == task[i].question);
                    List<string> user_anwer = task[i].answer.Replace(" ", "").Split(new char[] { ',' }).ToList();

                    double assessment_for_one_asnwer = test.assessment / test.list_correct_answer.Count;
                    int count_user_corr_answer = 0;

                    for (int q = 0; q < user_anwer.Count; q++)
                    {
                        //for (int w = 0; w < test.list_correct_answer.Count; w++)
                        {
                            if (user_anwer[q] == test.list_correct_answer[q])
                            {
                                count_user_corr_answer++;
                            }
                        }
                    }
                    assessment += (count_user_corr_answer * assessment_for_one_asnwer);


                }

                else if (task[i].type == "Compliance")
                {
                    TaskCompliance test = taskCompliance.FirstOrDefault(t => t.question == task[i].question);

                   
                    double assessment_for_one_asnwer = test.assessment / test.list_correctAnswer.Count;
                    int count_user_corr_answer = 0;

                    for (int q = 0; q < task[i].questions.Count; q++)
                    {
                        for (int w = 0; w < test.list_correctAnswer.Count; w++)
                        {
                            if (task[i].questions[q].answer == test.list_correctAnswer[w].correct_answer)
                            {
                                count_user_corr_answer++;
                            }
                        }
                    }
                    assessment += (count_user_corr_answer * assessment_for_one_asnwer);
                }

                else if (task[i].type == "OpenQuestion")
                {
                    TaskOpenQuestion test = taskOpenQuestion.FirstOrDefault(t => t.question == task[i].question);

                    if (test.corect_answer.ToLower().Replace(",","").Replace(".","").Replace(" ","") == task[i].answer.ToLower().Replace(",", "").Replace(".", "").Replace(" ", ""))
                    {
                        assessment += test.assessment;
                    }
                }


            }



            return assessment;
		}
      
	}
}

