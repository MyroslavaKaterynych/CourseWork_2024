using System;
using System.ComponentModel.DataAnnotations;

namespace Курсова.Models
{
	public class RegisterModel
	{
        [Required(ErrorMessage = "Не вказано імʼя")]
        public string name { get; set; }

        [Required(ErrorMessage = "Не вказано прізвище")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Не вказано логін")]
        public string login { get; set; }

        [Required(ErrorMessage = "Не вказано пароль")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "Пароль введено невірно")]
        public string confirmPassword { get; set; }
        
    }
}

