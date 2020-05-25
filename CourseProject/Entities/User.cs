using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.Entities
{
    public class User
    {
        public string Login { get; set; }
        public int Id { get; set; }
        public int Day { get; set; }
        public User(string Login, int Id, int Day)
        {
            this.Login = Login;
            this.Id = Id;
            this.Day = Day;
        }
    }


}
