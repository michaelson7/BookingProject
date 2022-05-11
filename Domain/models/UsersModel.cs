using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class UsersModel
    {
        public int Id { get; set; }
        public String Names { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
    }
}
