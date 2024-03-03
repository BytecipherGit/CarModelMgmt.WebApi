using CarModelMgmt.Core.Entities;
using CarModelMgmt.Core.Interfaces;
using CarModelMgmt.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CarModelMgmt.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICarModelRepository _carService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AdminController(ICarModelRepository carService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> DashboardAsync(CarModelDTO dto)
        {
           //ViewData["searchTerm"] = searchTerm;
            var cardtl = await _carService.GetCarDetal();
            return View(cardtl);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarModelDTO dto)
        {
            try
            {

                if (dto.ModelImage != null )
                {

                    var projectRootDirectory = Directory.GetCurrentDirectory();
                    var targetDirectory = Path.Combine(projectRootDirectory, "wwwroot/Public/uploads");

                    if (!Directory.Exists(targetDirectory))
                    {
                        Directory.CreateDirectory(targetDirectory);
                    }




                    // Save file to wwwroot/uploads folder
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.ModelImage.FileName);
                    //var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

                    //using (var stream = new FileStream(filePath, FileMode.Create))
                    //{
                    //    dto.ModelImage.CopyTo(stream);
                    //}

                    // Save URL to the database
                    //var imageUrl = $"/uploads/{fileName}";
                    // dto.ModelImageUrl = imageUrl;
                    var filePath = Path.Combine(targetDirectory, fileName);
                    var imageUrl = Path.Combine("Public/Docs", fileName);

                    // Update the user's profileImagePath with the file path
                    //var urlPath = imageUrl.Replace(Path.DirectorySeparatorChar, '/');

                    var baseUrl = $"{HttpContext.Request.Scheme}:/{HttpContext.Request.Host}";

                    // Create the complete URL
                    var imageUrl1 = baseUrl + "\\" + imageUrl;
                    var mainimageUrl = imageUrl1.Replace("\\", "/");
                    //var filePath1 = Path.Combine(baseUrl, imageUrl);
                    string? FileUrl = mainimageUrl.ToString();
                    dto.ModelImageUrl = FileUrl;

                }




                int id = await _carService.SaveCarDetal(dto);
                return RedirectToAction("Dashboard");
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> CommissionDetail()
        {
            //ViewData["searchTerm"] = searchTerm;
            var cardtl = await _carService.GetCommissionDetal();
            return View(cardtl);
        }
        [HttpGet]
        public IActionResult CommissionDetailClassWise()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CommissionDetailClassWise(string selectedClass)
        {
            // Your logic to retrieve commission details based on the selected class
            var commissionDetails = await _carService.GetCommissionDetalClassWise(selectedClass);

            // Pass the commission details to the view
            return View(commissionDetails);
        }

       
    }
}
