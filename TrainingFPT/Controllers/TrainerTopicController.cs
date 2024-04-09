using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;
using TrainingFPT.Models;

namespace TrainingFPT.Controllers
{
    public class TrainerTopicController : BaseController<TrainerTopicController>
    {
        private readonly TraningDBContext _dbContext;
        public TrainerTopicController(TraningDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Index(string searchString)
        {
            var trainerTopicModel = new TrainerTopicModel
            {
                TrainerTopicDetailLists = _dbContext.TrainerTopics
                    .Join(_dbContext.Topics, tt => tt.TopicId, t => t.Id, (tt, t) => new { tt, t })
                    .Join(_dbContext.Users, ttt => ttt.tt.UserId, u => u.Id, (ttt, u) => new TrainerTopicDetail
                    {
                        Id = ttt.tt.Id,
                        topic_id = ttt.tt.TopicId,
                        trainer_id = ttt.tt.UserId,
                        created_at = ttt.tt.CreatedAt,
                        updated_at = ttt.tt.UpdatedAt,
                        TopicName = ttt.t.NameTopic,
                        TrainerName = u.Username
                    }).ToList()
            };

            ViewData["CurrentFilter"] = searchString;
            return View(trainerTopicModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            var trainerTopic = new TrainerTopicDetail();
            PopulateDropdowns();
            return View(trainerTopic);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(TrainerTopicDetail trainerTopic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var trainerTopicData = new TrainerTopicDBContext
                    {
                        TopicId = trainerTopic.topic_id,
                        UserId = trainerTopic.trainer_id,
                        CreatedAt = DateTime.Now
                    };

                    _dbContext.TrainerTopics.Add(trainerTopicData);
                    _dbContext.SaveChanges();
                    TempData["saveStatus"] = true;
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["saveStatus"] = false;
                }
            }
            PopulateDropdowns();
            return View(trainerTopic);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var existingData = _dbContext.TrainerTopics
                .Where(tc => tc.Id == id)
                .FirstOrDefault();

            if (existingData == null)
            {
                return NotFound();
            }

            var trainerTopicDetail = new TrainerTopicDetail
            {
                Id = existingData.Id,
                topic_id = existingData.TopicId,
                trainer_id = existingData.UserId
            };

            PopulateDropdowns();
            return View(trainerTopicDetail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TrainerTopicDetail updatedData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingData = _dbContext.TrainerTopics
                        .FirstOrDefault(tc => tc.Id == updatedData.Id);

                    if (existingData != null)
                    {
                        existingData.TopicId = updatedData.topic_id;
                        existingData.UserId = updatedData.trainer_id;
                        existingData.UpdatedAt = DateTime.Now;
                        _dbContext.SaveChanges();

                        TempData["UpdateStatus"] = true;
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["UpdateStatus"] = false;
                        ModelState.AddModelError(string.Empty, "Record not found for update.");
                    }
                }
                else
                {
                    TempData["UpdateStatus"] = false;
                    ModelState.AddModelError(string.Empty, "Invalid model state. Please check the form inputs.");
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateStatus"] = false;
            }

            PopulateDropdowns();
            return View(updatedData);
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.TrainerTopics
                    .Where(tc => tc.Id == id)
                    .FirstOrDefault();

                if (data != null)
                {
                    _dbContext.TrainerTopics.Remove(data);
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

            return RedirectToAction(nameof(Index));
        }
        private void PopulateDropdowns()
        {
            ViewBag.Stores = _dbContext.Topics
                .Where(t => t.DeletedAt == null)
                .Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.NameTopic })
                .ToList();

            ViewBag.Stores1 = _dbContext.Users
                .Where(u => u.DeletedAt == null && u.RoleId == 3)
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Username })
                .ToList();
        }
    }
}
