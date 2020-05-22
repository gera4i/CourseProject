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
        public User(string Login, int Id)
        {
            this.Login = Login;
            this.Id = Id;
        }
    }


}
