using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Application.interfaces;
using Domain.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Bookings
{
    public class MyBookingsModel : PageModel
    {
        private readonly IDataService _db;
        private readonly IWebHostEnvironment _env;

        public ImageHandler _imageHandler = new ImageHandler();
        public List<BookingsModel> _BookingsModel = new List<BookingsModel>();
        public List<UsersModel> _usersModel = new List<UsersModel>();
        public List<ServicesModel> _serviceModel = new List<ServicesModel>();

        [BindProperty(SupportsGet = true)]
        public bool hasError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool hasResponse { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string errorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public DbOperations dbOperation { get; set; }

        public MyBookingsModel(IDataService db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task OnGetAsync()
        {
            //get all  
            var userId = int.Parse(Request.Cookies["userId"]);
            _BookingsModel = await _db.BookingsGetMine(userId);
            _serviceModel = await _db.ServicesGetAll();
        }

        public async Task<IActionResult> OnPost(BookingsModel model, DbOperations dbOperations, int deleteId)
        {
            //if deleting set operation to Delete
            dbOperations = deleteId > 0 ? DbOperations.Delete : dbOperations;
            var userId = int.Parse(Request.Cookies["userId"]);
            model.UserId = userId;

            try
            {
                switch (dbOperations)
                {
                    case DbOperations.Create:
                        await _db.BookingsCreate(model);
                        break;
                    case DbOperations.Update:
                        var checkIfFree = await _db.BookingsCheckIfFree(serviceId: model.ServiceId, bookingDate: model.BookingDate);
                        if (checkIfFree)
                        {
                            await _db.BookingsUpdate(model);
                        }
                        else
                        {
                            throw new ArgumentException($"Selected Date is unavailable");
                        }

                        break;
                    case DbOperations.Delete:
                        await _db.BookingsDelete(deleteId);
                        break;
                }
                return RedirectToPage(new
                {
                    dbOperation = deleteId > 0 ? DbOperations.Delete : dbOperations,
                    hasResponse = true
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return RedirectToPage(new
                {
                    hasError = true,
                    errorMessage = e.Message,
                    hasResponse = true
                });
            }
        }
    }
}
