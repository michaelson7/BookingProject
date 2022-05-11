using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.models
{
    public class ServicesModel
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public string ImagePath { get; set; }
        public decimal Cost { get; set; }
        public DateTime DateTime { get; set; }
        public IFormFile ImageFile { get; set; }
        public List<BookingReviewsModel> bookingReviewsModel { get; set; }
    }
}
