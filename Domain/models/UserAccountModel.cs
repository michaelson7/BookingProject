using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class UserAccountModel
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Userid { get; set; }
        public AccountTypeModel accountTypeModel { get; set; }
        public UsersModel UsersModel { get; set; }
    }
}
