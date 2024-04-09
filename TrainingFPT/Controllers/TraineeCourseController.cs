using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;
using TrainingFPT.Models;

namespace TrainingFPT.Controllers
{
    public class TraineeCourseController : BaseController<TraineeCourseController>
    {
        private readonly TraningDBContext _dbContext;
        public TraineeCourseController(TraningDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string SearchString)
        {
            TraineeCourseModel traineecourseModel = new TraineeCourseModel();
            traineecourseModel.TraineeCourseDetailLists = new List<TraineeCourseDetail>();

            var data = _dbContext.TraineeCourses.
                Join(_dbContext.Courses, tr => tr.CourseId, c => c.Id, (tr, c) => new { tr, c })
                .Join(_dbContext.Users, trr => trr.tr.UserId, u => u.Id, (trr, u) => new { trr, u })
                .Select(m => new TraineeCourseDetail
                {
                    Id = m.trr.tr.Id,
                    Courseid = m.trr.tr.CourseId,
                    UserId = m.trr.tr.UserId,
                    Createdat = m.trr.tr.CreatedAt,
                    Updatedat = m.trr.tr.UpdatedAt,
                    CourseName = m.trr.c.NameCourse,
                    TraineeName = m.u.Username
                });
            traineecourseModel.TraineeCourseDetailLists = data.ToList();
            ViewData["CurrentFilter"] = SearchString;
            return View(traineecourseModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            TraineeCourseDetail traineecourse = new TraineeCourseDetail();
            ViewDefault();
            return View(traineecourse);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TraineeCourseDetail traineecourse)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var traineecourseData = new TraineeCourseDBContext()
                    {
                        CourseId = traineecourse.Courseid,
                        UserId = traineecourse.UserId,
                        CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
                    };

                    _dbContext.TraineeCourses.Add(traineecourseData);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {

                    TempData["saveStatus"] = false;
                }
                return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
            }
            ViewDefault();
            Console.WriteLine(ModelState.IsValid);
            foreach (var key in ModelState.Keys)
            {
                var error = ModelState[key].Errors.FirstOrDefault();
                if (error != null)
                {
                    Console.WriteLine($"Error in {key}: {error.ErrorMessage}");
                }
            }
            return View(traineecourse);
        }
        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            var existingData = _dbContext.TraineeCourses
                .Where(tc => tc.Id == id)
                .FirstOrDefault();

            if (existingData == null)
            {
                return NotFound();
            }

            var traineeCourseDetail = new TraineeCourseDetail
            {
                Id = existingData.Id,
                Courseid = existingData.CourseId,
                UserId = existingData.UserId,
            };
            ViewDefault();
            return View(traineeCourseDetail);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(TraineeCourseDetail updatedData)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingData = _dbContext.TraineeCourses
                        .Where(tc => tc.Id == updatedData.Id)
                        .FirstOrDefault();

                    if (existingData != null)
                    {
                        existingData.CourseId = updatedData.Courseid;
                        existingData.UserId = updatedData.UserId;
                        existingData.UpdatedAt = DateTime.Now;
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

                return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
            }
            ViewDefault();
            return View(updatedData);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var data = _dbContext.TraineeCourses
                    .Where(tc => tc.Id == id)
                    .FirstOrDefault();

                if (data != null)
                {
                    _dbContext.TraineeCourses.Remove(data);
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

            return RedirectToAction(nameof(TraineeCourseController.Index), "TraineeCourse");
        }
        public void ViewDefault()
        {
            var courseList = _dbContext.Courses
            .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.NameCourse }).ToList();
            ViewBag.Stores = courseList;

            var traineeList = _dbContext.Users
              .Where(m => m.DeletedAt == null && m.RoleId == 4)
              .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Username }).ToList();
            ViewBag.Stores1 = traineeList;
        }
    }
}
