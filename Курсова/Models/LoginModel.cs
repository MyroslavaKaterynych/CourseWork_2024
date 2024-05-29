using System;
using System.ComponentModel.DataAnnotations;

namespace Курсова.Models
{
	public class LoginModel
	{
        [Required(ErrorMessage = "Не вказано логін")]
        public string login { get; set; }

        [Required(ErrorMessage = "Не вказано пароль")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}

