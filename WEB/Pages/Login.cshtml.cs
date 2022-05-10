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
    public class LoginModel : PageModel
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

        public LoginModel(IDataService db)
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
                var data = await _db.UsersLogin(email: model.Email, password: model.Password);

                if (data == null)
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
                    //set cookie
                    var cookieOptions = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30)
                    };
                    Response.Cookies.Append("userId", data.Id.ToString(), cookieOptions);
                    Response.Cookies.Append("userName", data.Names, cookieOptions);
                    Response.Cookies.Append("userRole", "Admin", cookieOptions);

                    return RedirectToPage("/Index");
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
