using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;
using TrainingFPT.Models;

namespace TrainingFPT.Controllers
{
    public class CategoryController : BaseController<CategoryController>
    {
        private readonly TraningDBContext _dbContext;
        public CategoryController(TraningDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index(string SearchString)
        {
            var session = HttpContext.Session.GetString("SessionRoleId");
            if(session == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.RoleId = int.Parse(session);
            CategoryViewModel categoryModel = new CategoryViewModel();
            categoryModel.CategoryDetailList = new List<CategoryDetail>();
            var data = from m in _dbContext.Categories
                       select m;

            data = data.Where(m => m.DeletedAt == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.Name.Contains(SearchString) || (!string.IsNullOrEmpty(m.Description) ? m.Description.Contains(SearchString) : true));
            }
            data.ToList();

            foreach (var item in data)
            {
                categoryModel.CategoryDetailList.Add(new CategoryDetail
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    PosterNameImage = item.PosterImage,
                    Status = item.Status,
                    CreatedAt = item.CreatedAt,
                    UpdatedAt = item.UpdatedAt
                });
            }
            ViewData["CurrentFilter"] = SearchString;
            return View(categoryModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            CategoryDetail model = new CategoryDetail();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CategoryDetail category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if(category.PosterImage != null)
                    {
                        Regex rgx = new Regex(@"(.*?)\.(jfif|jpg|tiff|jpeg|nef|ai|bmp|png|gif|JPG|PNG|BMP|GIF|TIFF|NEF|AI|JPEG)$");
                        if (!rgx.IsMatch(category.PosterImage.FileName))
                        {
                            ViewBag.Error = "Định dạng ảnh không hơp lệ";
                            return View(category);
                        }
                        fileName = UploadFile(category.PosterImage);
                    }
                    var categoryData = new CategoriesDBContext
                    {
                        Name = category.Name,
                        Description = category.Description,
                        PosterImage = fileName,
                        Status = category.Status,
                        ParentId = 0,
                        CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };
                    _dbContext.Categories.Add(categoryData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch
                {
                    TempData["saveStatus"] = false;
                }
                return RedirectToAction(nameof(CategoryController.Index), "Category");
            }
            return View(category);
        }

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            CategoryDetail category = new CategoryDetail();
            var data = _dbContext.Categories.Where(m => m.Id == id).FirstOrDefault();
            if (data != null)
            {
                category.Id = data.Id;
                category.Name = data.Name;
                category.Description = data.Description;
                category.PosterNameImage = data.PosterImage;
                category.Status = data.Status;
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Update(CategoryDetail category)
        {
            try
            {
                var data = _dbContext.Categories.Where(m => m.Id == category.Id).FirstOrDefault();
                string fileName = "";
                if (category.PosterImage != null)
                {
                    Regex rgx = new Regex(@"(.*?)\.(jfif|jpg|tiff|jpeg|nef|ai|bmp|png|gif|JPG|PNG|BMP|GIF|TIFF|NEF|AI|JPEG)$");
                    if (!rgx.IsMatch(category.PosterImage.FileName))
                    {
                        ViewBag.Error = "Định dạng ảnh không hơp lệ";
                        return View(category);
                    }
                    fileName = UploadFile(category.PosterImage);
                }

                if (data != null)
                {
                    data.Name = category.Name;
                    data.Description = category.Description;
                    data.Status = category.Status;
                    data.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        data.PosterImage = fileName;
                    }
                    _dbContext.SaveChanges(true);
                    TempData["UpdateStatus"] = true;
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
            }
            return RedirectToAction(nameof(CategoryController.Index), "Category");
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Categories.Where(m => m.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.DeletedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.SaveChanges(true);
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = false;
                }
            }
            catch
            {
                TempData["DeleteStatus"] = false;
            }
            return RedirectToAction(nameof(CategoryController.Index), "Category");
        }
        private string UploadFile(IFormFile file)
        {
            string filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    string pathUploadImage = "wwwroot\\uploads\\images";
                    string fileName = file.FileName;
                    fileName = Path.GetFileName(fileName);
                    string uniqueStr = Guid.NewGuid().ToString();
                    fileName = uniqueStr + "-" + fileName;
                    if (!Directory.Exists(pathUploadImage))
                    {
                        Directory.CreateDirectory(pathUploadImage);
                    }
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), pathUploadImage, fileName);
                    var stream = new FileStream(uploadPath, FileMode.Create);
                    file.CopyToAsync(stream);
                    filePath = fileName;
                }
            }
            catch (Exception ex)
            {

            }
            return filePath;
        }
    }
}
