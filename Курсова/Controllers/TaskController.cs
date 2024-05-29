using System.Diagnostics;
using System.Xml.Linq;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Курсова.Models;
using Курсова.Models.ClassCheckTask;
using Курсова.Models.ModelsTask;
using static System.Net.Mime.MediaTypeNames;
using Task = System.Threading.Tasks.Task;

namespace Курсова.Controllers;

public class TaskController : Controller

{
    DataBase db = new DataBase();
    private readonly ILogger<TaskController> _logger;

    public TaskController(ILogger<TaskController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    [Authorize]
    public IActionResult PassTest(string str)
    {
        int id_test = Convert.ToInt32(str.Substring(0, str.IndexOf('_')));
        List<TaskCompliance> taskCompliance = db.list_TaskCompliance(id_test);
        List<TaskMultipleOption> taskMultipleOption = db.list_TaskMultipleOption(id_test);
        List<TaskOpenQuestion> taskOpenQuestion = db.list_TaskOpenQuestion(id_test);
        List<TaskSuquence> taskSuquence = db.list_TaskSuquence(id_test);
        List<TaskTest> taskTest = db.list_TaskTest(id_test);
        if (taskCompliance != null && taskCompliance.Count > 0)
        {
            ViewBag.list_taskCompliance = taskCompliance;
        }
        else
        {
            ViewBag.list_taskCompliance = new List<TaskCompliance>();
        }
        if (taskMultipleOption != null && taskMultipleOption.Count > 0)
        {
            ViewBag.List_taskMultipleOption = taskMultipleOption;
        }
        else
        {
            ViewBag.List_taskMultipleOption = new List<TaskMultipleOption>();
        }
        if (taskOpenQuestion != null && taskOpenQuestion.Count > 0)
        {
            ViewBag.list_taskOpenQuestion = taskOpenQuestion;
        }
        else
        {
            ViewBag.list_taskOpenQuestion = new List<TaskOpenQuestion>();
        }
        if (taskSuquence != null && taskSuquence.Count > 0)
        {
            ViewBag.list_taskSuquence = taskSuquence;
        }
        else
        {
            ViewBag.list_taskSuquence = new List<TaskSuquence>();
        }
        if (taskTest != null && taskTest.Count > 0)
        {
            ViewBag.list_taskTest = taskTest;
        }
        else
        {
            ViewBag.list_taskTest = new List<TaskTest>();
        }
        return View(id_test);

    }
    [HttpGet]
    public string CheckThisTask(string str)
    {
        List<CheckTask> myDeserializedClass = JsonConvert.DeserializeObject<List<CheckTask>>(str);
       double assessment = CheckTestUser.Check(myDeserializedClass);
        db.SaveTestResult(db.GetUserByLogin(User.Identity.Name), myDeserializedClass[0].id_test,db.GetTest(myDeserializedClass[0].id_test).name,assessment,DateTime.Now.ToString("dd MM yyyy HH:mm"));
        return (assessment.ToString());
    }

    public IActionResult Statistic(int id_test)
    {
        List<Statistic_Test> tests = db.GetStatistic_Test(id_test, User.Identity.Name);
        
        return View(tests);
    }
    public IActionResult Main()
    {
        db.list_TaskTest(59);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [HttpGet]
    [Authorize]
    public IActionResult AddNewTest()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public IActionResult AddNewTest(string name, string description)//перегрузка
    {
        User user = db.GetUserByLogin(User.Identity.Name.ToString());
        int id = db.AddNewTest(user.id,name,description);
        ViewBag.id = id;
        ViewBag.name = name;
        ViewBag.description = description;


        return View("SettingTest");

    }

    [Authorize]
    public IActionResult MyTests()
    {

        User user = db.GetUserByLogin(User.Identity.Name.ToString());

        List<Test> myTests = db.GetMyTests(user.id);

        return View(myTests);
    }
    [Authorize]
    public IActionResult GetMyTestInfo(int id)
    {
        User user = db.GetUserByLogin(User.Identity.Name.ToString());
        Test t = db.GetTest(id);
        if(t.id_user!=user.id)
        {
            return View("Error","Цей тест не є  вашим");
        }
        ViewBag.id = t.id;
        ViewBag.name = t.name;
        ViewBag.description = t.description;
        ViewBag.avtor = user.id;
        return View("SettingTest");

    }


    [HttpGet]
    public IActionResult PartialOpenQuestion(int id_test)
    {
        ViewBag.id_test = id_test;
        return PartialView("Partial/PartialOpenQuestion");
    }
    [HttpGet]
    public IActionResult PartialCompliance(int id_test)
    {
        ViewBag.id_test = id_test;
        return PartialView("Partial/PartialCompliance");
    }
    [HttpGet]
    public IActionResult PartialSuquence(int id_test)
    {
        ViewBag.id_test = id_test;
        return PartialView("Partial/PartialSuquence");
    }
    [HttpGet]
    public IActionResult PartialTest(int id_test)
    {
        ViewBag.id_test = id_test;
        return PartialView("Partial/PartialTest");
    }
    [HttpGet]
    public IActionResult PartialMultipleOption(int id_test)
    {
        ViewBag.id_test = id_test;
        return PartialView("Partial/PartialMultipleOption");
    }
    [HttpPost]

    public string PartialAddNewTaskOpenQuestion(int test_id, string question, string corect_answer, int assessment )
    {


        int id_new_test = db.AddNewOpenQuestion(test_id, question, corect_answer, assessment);

        return "<div><button type='button' id = '" + id_new_test + "' onclick=\"DeleteTest('" + id_new_test + "', 'task_open_question')\">Видалити</button></div>";
    }
    [HttpPost]

    public string PartialAddNewTaskCompliance(int test_id, string list_question,string question, string list_answer, int assessment)
    {
        //string[] question = list_question.Split(new char[] { '\n' });
        //for (int i = 0; i < question.Length; i++)
        //{            
        //    question[i] = question[i].Substring(3);
        //    question[i] = question[i].Replace("\r", "");
        //}
        //string[] answer = list_answer.Split(new char[] { '\n' });
        //for (int i = 0; i < question.Length; i++)
        //{
        //    answer[i] = answer[i].Substring(3);
        //    answer[i] = answer[i].Replace("\r", "");
        //}

        int id_new_test = db.AddNewCompliance(test_id,question, list_question, list_answer, assessment);
        return "<div><button type='button' id = '" + id_new_test + "' onclick=\"DeleteTest('" + id_new_test + "', 'task_compliance')\">Видалити</button></div>";
    }
    [HttpPost]

    public string PartialAddNewTaskSuquence(int test_id, string list_question, string list_answer, int assessment)
    {
        //string[] question = list_question.Split(new char[] { '\n' });
        //for (int i = 0; i < question.Length; i++)
        //{            
        //    question[i] = question[i].Substring(3);
        //    question[i] = question[i].Replace("\r", "");
        //}
        //string[] answer = list_answer.Split(new char[] { '\n' });
        //for (int i = 0; i < question.Length; i++)
        //{
        //    answer[i] = answer[i].Substring(3);
        //    answer[i] = answer[i].Replace("\r", "");
        //}

        int id_new_test = db.AddNewSuquence(test_id, list_question, list_answer, assessment);

        return "<div><button type='button' id = '" + id_new_test + "' onclick=\"DeleteTest('" + id_new_test + "', 'task_suquence')\">Видалити</button></div>";
    }

    [HttpPost]
    public  string PartialAddNewTaskTest(int test_id, string question, string list_answer, int assessment)
    {
        int id_new_test =  db.AddNewTest(test_id, question, list_answer, assessment);

        return "<div><button type='button' id = '" + id_new_test + "' onclick=\"DeleteTest('" + id_new_test + "', 'task_test')\">Видалити</button></div>";
    }
    
        [HttpPost]
    public string PartialAddNewTaskMultipleOption(int test_id, string question, string list_answer, int count_correct_answer, int assessment)
    {
        int id_new_test = db.AddNewMultipleOptionTest(test_id, question, list_answer, count_correct_answer, assessment);

        return "<div><button type='button' id = '" + id_new_test + "' onclick=\"DeleteTest('" + id_new_test + "', 'task_multiple_option')\">Видалити</button></div>";
    }


    [HttpPost]
    public  bool DeleteTest(int id_test, string table_name)
    {

          return  db.DeleteTest(id_test,table_name);
    }



}

