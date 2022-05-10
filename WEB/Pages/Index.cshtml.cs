using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.interfaces;
using Domain.models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IDataService _db;
        private readonly IWebHostEnvironment _env;

        public ImageHandler _imageHandler = new ImageHandler();
        public List<ServicesModel> ServicesModels = new List<ServicesModel>();


        [BindProperty(SupportsGet = true)]
        public bool hasError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool hasResponse { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string errorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public DbOperations dbOperation { get; set; }

        public IndexModel(IDataService db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }

        public async Task OnGetAsync()
        {
            //get all  
            ServicesModels = await _db.ServicesGetAll();
        }

    }
}
