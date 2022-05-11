using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class BookingReviewsModel
    {
        public int Id { get; set; }
        public String Reviews { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public UsersModel usersModel { get; set; }
        public ServicesModel ServicesModel { get; set; }
    }
}
