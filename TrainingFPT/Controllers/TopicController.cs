using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;
using TrainingFPT.Migrations;

namespace TrainingFPT.Controllers
{
    public class TopicController : BaseController<TopicController>
    {
        private readonly TraningDBContext _dbContext;
        private readonly ILogger<TopicController> _logger;
        public TopicController(TraningDBContext dbContext, ILogger<TopicController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;   
        }
        public IActionResult Index(string SearchString)
        {
            List<TopicsDBContext> model = new List<TopicsDBContext>();
            var data = _dbContext.Topics
               .Where(m => m.DeletedAt == null)
               .ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.NameTopic.Contains(SearchString) || (!string.IsNullOrEmpty(m.Description) ? m.Description.Contains(SearchString) : true)).ToList();
            }
            foreach (var item in data)
            {
                var course = _dbContext.Courses.FirstOrDefault(x => x.Id == item.CouresId);
                if(course != null)
                {
                    item.CourseName = course.NameCourse;
                }
                model.Add(item);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Add()
        {
            TopicsDBContext topic = new TopicsDBContext();
            PopulateCategoryDropdown();
            return View(topic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(104857600)]
        public IActionResult Add(TopicsDBContext topic)
        {
            try
            {
                PopulateCategoryDropdown();
                if (ModelState.IsValid)
                {
                    try
                    {
                        if(topic.FileVideo == null && topic.FileAudio == null && topic.FileDocument == null)
                        {
                            ViewBag.Error = "Vui lòng thêm chọn thêm file video hoặc FileAudio hoặc file Document";
                            return View(topic);
                        }
                        string VideoFileName = string.Empty;
                        string AudioFileName = string.Empty;
                        string DocumentFileName = string.Empty;
                        if(topic.FileVideo != null)
                        {
                            Regex regexVideo = new Regex(@".+\.(mp4|avi|mkv|mov)$");
                            if (!regexVideo.IsMatch(topic.FileVideo.FileName))
                            {
                                ViewBag.Error = "Định dạng video không hợp lệ";
                                return View(topic);
                            }
                            VideoFileName = UploadVideos(topic.FileVideo);
                        }
                        if(topic.FileAudio != null)
                        {
                            Regex regexAudio = new Regex(@".+\.(mp3|wav|ogg|flac)$");
                            if (!regexAudio.IsMatch(topic.FileAudio.FileName))
                            {
                                ViewBag.Error = "Định dạng audio không hợp lệ";
                                return View(topic);
                            }
                            AudioFileName = UploadAudio(topic.FileAudio);
                        }
                        if (topic.FileDocument != null)
                        {
                            Regex regexDocument = new Regex(@".+\.(txt|doc|docx|pdf)$");
                            if (!regexDocument.IsMatch(topic.FileDocument.FileName))
                            {
                                ViewBag.Error = "Định dạng tài liệu không hợp lệ";
                                return View(topic);
                            }
                            DocumentFileName = UploadDocuments(topic.FileDocument);
                        }
                        topic.Video = VideoFileName;
                        topic.Audio = AudioFileName;
                        topic.DocumentTopic = DocumentFileName;
                        _dbContext.Topics.Add(topic);
                        _dbContext.SaveChanges();
                        TempData["saveStatus"] = true;
                    }
                    catch (Exception ex)
                    {                       
                        TempData["saveStatus"] = false;
                    }
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["saveStatus"] = false;
                }

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogError($"ModelState Error: {error.ErrorMessage}");
                    }
                }
                return View(topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request.");
                TempData["saveStatus"] = false;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult Update(int id = 0)
        {
            var data = _dbContext.Topics.FirstOrDefault(m => m.Id == id);
            if (data != null)
            {
                PopulateCategoryDropdown();
                return View(data);
            }
            else
            {
                TempData["UpdateStatus"] = false;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(104857600)]
        public async Task<IActionResult> Update(TopicsDBContext topic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PopulateCategoryDropdown();
                    var data = _dbContext.Topics.FirstOrDefault(m => m.Id == topic.Id);
                    if (data != null)
                    {
                        data.NameTopic = topic.NameTopic;
                        data.Description = topic.Description;
                        data.Status = topic.Status;
                        data.CouresId = topic.CouresId;

                        // Update the file fields if a new file is provided
                        if (topic.FileVideo != null)
                        {
                            Regex regexVideo = new Regex(@".+\.(mp4|avi|mkv|mov)$");
                            if (!regexVideo.IsMatch(topic.FileVideo.FileName))
                            {
                                ViewBag.Error = "Định dạng video không hợp lệ";
                                return View(topic);
                            }
                            data.Video = UploadVideos(topic.FileVideo);
                        }

                        if (topic.FileAudio != null)
                        {
                            Regex regexAudio = new Regex(@".+\.(mp3|wav|ogg|flac)$");
                            if (!regexAudio.IsMatch(topic.FileAudio.FileName))
                            {
                                ViewBag.Error = "Định dạng audio không hợp lệ";
                                return View(topic);
                            }
                            data.Audio = UploadAudio(topic.FileAudio);
                        }

                        if (topic.FileDocument != null)
                        {
                            Regex regexDocument = new Regex(@".+\.(txt|doc|docx|pdf)$");
                            if (!regexDocument.IsMatch(topic.FileDocument.FileName))
                            {
                                ViewBag.Error = "Định dạng tài liệu không hợp lệ";
                                return View(topic);
                            }
                            data.DocumentTopic = UploadDocuments(topic.FileDocument);
                        }

                        data.UpdatedAt = DateTime.Now;
                        _dbContext.SaveChanges();
                        TempData["UpdateStatus"] = true;
                    }
                    else
                    {
                        TempData["UpdateStatus"] = false;
                    }

                    return RedirectToAction(nameof(TopicController.Index), "Topic");
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                }
                return View(topic);
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
                return RedirectToAction(nameof(Index));
            }
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Topics.FirstOrDefault(m => m.Id == id);

                if (data != null)
                {
                    data.DeletedAt = DateTime.Now;
                    _dbContext.SaveChanges();
                    TempData["DeleteStatus"] = true;
                }
                else
                {
                    TempData["DeleteStatus"] = false;
                }
            }
            catch (Exception ex)
            {
                TempData["DeleteStatus"] = false;
            }

            return RedirectToAction(nameof(Index), new { SearchString = "" });
        }
        private void PopulateCategoryDropdown()
        {
            try
            {
                var courses = _dbContext.Courses
                    .Where(m => m.DeletedAt == null)
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.NameCourse })
                    .ToList();

                if (courses != null)
                {
                    ViewBag.Stores = courses;
                }
                else
                {
                    ViewBag.Stores = new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Stores = new List<SelectListItem>();
            }
        }
        private void PopulateCategoryDropdown1()
        {
            try
            {
                var users = _dbContext.Users
                    .Where(m => m.DeletedAt == null && m.RoleId == 3)
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Username })
                    .ToList();

                if (users != null)
                {
                    ViewBag.Stores1 = users;
                }
                else
                {
                    ViewBag.Stores1 = new List<SelectListItem>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Stores1 = new List<SelectListItem>();
            }
        }
        private string UploadVideos(IFormFile file)
        {
            string filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    string pathUploadImage = "wwwroot\\uploads\\videos";
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
        private string UploadDocuments(IFormFile file)
        {
            string filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    string pathUploadImage = "wwwroot\\uploads\\documents";
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
        private string UploadAudio(IFormFile file)
        {
            string filePath = string.Empty;
            try
            {
                if (file != null)
                {
                    string pathUploadImage = "wwwroot\\uploads\\audio";
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
