using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class BookingsModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public ServicesModel ServiceModel { get; set; }
        public UsersModel UsersModel { get; set; }
    }
}
