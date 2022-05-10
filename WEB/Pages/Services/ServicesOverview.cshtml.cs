using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.interfaces;
using Domain.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages.Services
{
    public class ServicesOverviewModel : PageModel
    {
        private readonly IDataService _db;

        public ServicesModel ServicesModels = new ServicesModel();
        public List<BookingsModel> _BookingsModel = new List<BookingsModel>();

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public int userId { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool hasError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool hasResponse { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string errorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public DbOperations dbOperation { get; set; }

        public ServicesOverviewModel(IDataService db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            ServicesModels = await _db.ServicesGet(id);
            _BookingsModel = await _db.BookingsGetAll();
            userId = int.Parse(Request.Cookies["userId"]);
        }

        public async Task<IActionResult> OnPost(BookingReviewsModel model, BookingsModel bmodel, DbOperations dbOperations, int deleteId, String src)
        {
            //if deleting set operation to Delete
            dbOperations = deleteId > 0 ? DbOperations.Delete : dbOperations;
            userId = int.Parse(Request.Cookies["userId"]);

            try
            {
                switch (src)
                {
                    case "Rating":
                        model.UserId = userId;

                        switch (dbOperations)
                        {
                            case DbOperations.Create:
                                var id = await _db.BookingReviewsCreate(model);
                                break;
                            case DbOperations.Update:
                                await _db.BookingReviewsUpdate(model);
                                break;
                            case DbOperations.Delete:
                                await _db.BookingReviewsDelete(deleteId);
                                break;
                        }
                        break;
                    case "Booking":
                        bmodel.UserId = userId;

                        switch (dbOperations)
                        {
                            case DbOperations.Create:
                                var checkIfFree = await _db.BookingsCheckIfFree(serviceId: bmodel.ServiceId, bookingDate: bmodel.BookingDate);
                                if (checkIfFree)
                                {
                                    await _db.BookingsCreate(bmodel);
                                }
                                else
                                {
                                    throw new ArgumentException($"Selected Date is unavailable");
                                }
                                break;
                            case DbOperations.Update:
                                await _db.BookingsUpdate(bmodel);
                                break;
                            case DbOperations.Delete:
                                await _db.BookingsDelete(deleteId);
                                break;
                        }
                        break;
                }

                return RedirectToPage(new
                {
                    dbOperation = deleteId > 0 ? DbOperations.Delete : dbOperations,
                    hasResponse = true,
                    id = model.ServiceId,
                });
            }
            catch (Exception e)
            {
                return RedirectToPage(new
                {
                    hasError = true,
                    errorMessage = e.Message,
                    hasResponse = true,
                    id = model.ServiceId,
                });
            }
        }
    }
}
