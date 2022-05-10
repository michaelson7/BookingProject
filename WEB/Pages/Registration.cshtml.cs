using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.interfaces;
using Domain.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly IDataService _db;

        [BindProperty(SupportsGet = true)]
        public bool hasError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool hasResponse { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string errorMessage { get; set; }
        [BindProperty(SupportsGet = true)]

        public DbOperations dbOperations { get; set; }

        public RegistrationModel(IDataService db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
        }

        public async Task<IActionResult> OnPost(UsersModel model)
        {
            try
            {
                var data = await _db.UsersCreate(model);

                if (data == 0)
                {
                    return RedirectToPage(new
                    {
                        error = true,
                        hasResponse = true,
                        errorMessage = "Account does not exist"
                    });
                }
                else
                {
                    var userAccount = await _db.UsersGet(data);

                    if (userAccount != null)
                    {
                        //set cookie
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30)
                        };
                        Response.Cookies.Append("userId", userAccount.Id.ToString(), cookieOptions);
                        Response.Cookies.Append("userName", userAccount.Names, cookieOptions);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage(new
                        {
                            error = true,
                            hasResponse = true,
                            errorMessage = "Unable to sign in"
                        });
                    }
                }

            }
            catch (Exception e)
            {
                return RedirectToPage(new
                {
                    error = true,
                    errorMessage = e.Message,
                    hasResponse = true
                });
            }
        }
    }
}
