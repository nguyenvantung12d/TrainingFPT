using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;
using TrainingFPT.Migrations;

namespace TrainingFPT.Controllers
{
    public class CourseController : BaseController<CourseController>
    {
        private readonly TraningDBContext _dbContext;
        public CourseController(TraningDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string SearchString)
        {
            var session = HttpContext.Session.GetString("SessionRoleId");
            if (session == null)
            {
                return Redirect("/Login/Index");
            }
            ViewBag.RoleId = int.Parse(session);
            List<CourseDBContext> model = new List<CourseDBContext>();
            var data = _dbContext.Courses.Where(m => m.DeletedAt == null);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.NameCourse.Contains(SearchString) || (!string.IsNullOrEmpty(m.Description) ? m.Description.Contains(SearchString) : true));
            }
            foreach(var item in data.ToList())
            {
                var categories = _dbContext.Categories.FirstOrDefault(x => x.Id == item.CategoryId);
                if(categories != null)
                {
                    item.CategoryName = categories.Name;
                }
                model.Add(item);
            }
            ViewData["CurrentFilter"] = SearchString;
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            CourseDBContext model = new CourseDBContext();
            var categoryList = _dbContext.Categories
                .Where(m => m.DeletedAt == null)
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
            ViewBag.Stores = categoryList;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CourseDBContext course)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = string.Empty;
                    if(course.FileImage != null)
                    {
                        fileName = UploadFile(course.FileImage);
                    }
                    course.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    course.Image = fileName;
                    _dbContext.Courses.Add(course);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {
                    TempData["saveStatus"] = false;
                }
                return RedirectToAction(nameof(CourseController.Index), "Course");
            }

            var categoryList = _dbContext.Categories
              .Where(m => m.DeletedAt == null)
              .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
            ViewBag.Stores = categoryList;
            Console.WriteLine(ModelState.IsValid);
            return View(course);
        }

        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            CourseDBContext course = new CourseDBContext();
            var data = _dbContext.Courses.Where(m => m.Id == id).FirstOrDefault();
            if (data != null)
            {
                course.Id = data.Id;
                course.NameCourse = data.NameCourse;
                course.Description = data.Description;
                course.Status = data.Status;
                course.Image = data.Image;
                course.StarCourse = data.StarCourse;
            }
            var categoryList = _dbContext.Categories.Where(m => m.DeletedAt == null).Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
            ViewBag.Stores = categoryList;
            return View(course);
        }
        [HttpPost]
        public IActionResult Update(CourseDBContext course)
        {
            try
            {
                var data = _dbContext.Courses.Where(m => m.Id == course.Id).FirstOrDefault();
                if (data != null)
                {
                    data.NameCourse = course.NameCourse;
                    data.Description = course.Description;
                    data.Status = course.Status;
                    data.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    if (course.FileImage != null)
                    {
                        string uniqueIconAvatar = UploadFile(course.FileImage);
                        data.Image = uniqueIconAvatar;
                    }
                    _dbContext.SaveChanges(true);
                    TempData["UpdateStatus"] = true;
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
            }
            catch
            {
                TempData["UpdateStatus"] = false;
            }
            return RedirectToAction(nameof(CategoryController.Index), "Course");
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Courses.Where(m => m.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.DeletedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.SaveChanges(true);
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteStatus"] = false;
                //return Ok(ex.Message);
            }
            return RedirectToAction(nameof(CategoryController.Index), "Course");
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
