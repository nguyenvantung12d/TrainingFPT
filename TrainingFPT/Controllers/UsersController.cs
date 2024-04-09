using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingFPT.Controllers.Base;
using TrainingFPT.DBContext;

namespace TrainingFPT.Controllers
{
    public class UsersController : BaseController<UsersController>
    {
        private readonly TraningDBContext _dbContext;
        public UsersController(TraningDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index(string SearchString)
        {
            List<UsersDBContext> userModel = new List<UsersDBContext>();
            var data = from m in _dbContext.Users
                       select m;
            data = data.Where(m => m.RoleId == 2);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.Username.Contains(SearchString) || m.Phone.Contains(SearchString));
            }
            userModel = data.ToList();
            ViewData["CurrentFilter"] = SearchString;
            return View(userModel);
        }
        [HttpGet]
        public IActionResult Add()
        {
            UsersDBContext usersDBContext = new UsersDBContext();
            return View(usersDBContext);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(UsersDBContext user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {

                    TempData["saveStatus"] = false;

                }
                return RedirectToAction(nameof(UsersController.Index), "Users");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            UsersDBContext model = new UsersDBContext();
            var data = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                model = data;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Update(UsersDBContext user)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == user.Id).FirstOrDefault();
                if (data != null)
                {
                    // gan lai du lieu trong db bang du lieu tu form model gui len
                    data.RoleId = user.RoleId;
                    data.Username = user.Username;
                    data.ExtraCode = user.ExtraCode;
                    data.Email = user.Email;
                    data.Status = user.Status;
                    data.Phone = user.Phone;
                    data.Gender = user.Gender;
                    data.Address = user.Address;
                    data.Birthday = user.Birthday;
                    data.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            return RedirectToAction(nameof(UsersController.Index), "Users");
        }
        [HttpGet]
        public IActionResult Delete(int id = 0)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.DeletedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Remove(data);
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
            return RedirectToAction(nameof(UsersController.Index), "Users");
        }

        [HttpGet]
        public IActionResult TrainerIndex(string SearchString)
        {

            List<UsersDBContext> userModel = new List<UsersDBContext>();
            var data = from m in _dbContext.Users
                       select m;
            data = data.Where(m => m.RoleId == 3);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.Username.Contains(SearchString) || m.Phone.Contains(SearchString));
            }
            userModel = data.ToList();
            ViewData["CurrentFilter"] = SearchString;
            return View(userModel);
        }
        [HttpGet]
        public IActionResult TrainerAdd()
        {

            UsersDBContext user = new UsersDBContext();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TrainerAdd(UsersDBContext user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {

                    TempData["saveStatus"] = false;

                }
                return RedirectToAction(nameof(UsersController.TrainerIndex), "Users");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult TrainerUpdate(int id)
        {
            UsersDBContext model = new UsersDBContext();
            var data = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                model = data;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult TrainerUpdate(UsersDBContext user)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == user.Id).FirstOrDefault();
                if (data != null)
                {
                    // gan lai du lieu trong db bang du lieu tu form model gui len
                    data.RoleId = user.RoleId;
                    data.Username = user.Username;
                    data.ExtraCode = user.ExtraCode;
                    data.Email = user.Email;
                    data.Status = user.Status;
                    data.Phone = user.Phone;
                    data.Gender = user.Gender;
                    data.Address = user.Address;
                    data.Birthday = user.Birthday;
                    data.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            return RedirectToAction(nameof(UsersController.TrainerIndex), "Users");
        }
        [HttpGet]
        public IActionResult TrainerDelete(int id = 0)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.DeletedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Remove(data);
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
            return RedirectToAction(nameof(UsersController.TrainerIndex), "Users");
        }
        [HttpGet]
        public IActionResult TraineeIndex(string SearchString)
        {
            var roleId = HttpContext.Session.GetString("SessionRoleId");
            if(roleId  == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.RoleId = int.Parse(roleId);
            List<UsersDBContext> userModel = new List<UsersDBContext>();
            var data = from m in _dbContext.Users
                       select m;
            data = data.Where(m => m.RoleId == 4);
            if (!string.IsNullOrEmpty(SearchString))
            {
                data = data.Where(m => m.Username.Contains(SearchString) || m.Phone.Contains(SearchString));
            }
            userModel = data.ToList();
            ViewData["CurrentFilter"] = SearchString;
            return View(userModel);
        }
        [HttpGet]
        public IActionResult TraineeAdd()
        {

            UsersDBContext user = new UsersDBContext();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TraineeAdd(UsersDBContext user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    user.CreatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Add(user);
                    _dbContext.SaveChanges(true);
                    TempData["saveStatus"] = true;
                }
                catch (Exception ex)
                {

                    TempData["saveStatus"] = false;

                }
                return RedirectToAction(nameof(UsersController.TraineeIndex), "Users");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult TraineeUpdate(int id)
        {
            UsersDBContext model = new UsersDBContext();
            var data = _dbContext.Users.Where(x => x.Id == id).FirstOrDefault();
            if (data != null)
            {
                model = data;
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult TraineeUpdate(UsersDBContext user)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == user.Id).FirstOrDefault();
                if (data != null)
                {
                    // gan lai du lieu trong db bang du lieu tu form model gui len
                    data.RoleId = user.RoleId;
                    data.Username = user.Username;
                    data.ExtraCode = user.ExtraCode;
                    data.Email = user.Email;
                    data.Status = user.Status;
                    data.Phone = user.Phone;
                    data.Gender = user.Gender;
                    data.Address = user.Address;
                    data.Birthday = user.Birthday;
                    data.UpdatedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
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
            return RedirectToAction(nameof(UsersController.TraineeIndex), "Users");
        }
        [HttpGet]
        public IActionResult TraineeDelete(int id = 0)
        {
            try
            {
                var data = _dbContext.Users.Where(m => m.Id == id).FirstOrDefault();
                if (data != null)
                {
                    data.DeletedAt = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    _dbContext.Users.Remove(data);
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
            return RedirectToAction(nameof(UsersController.TraineeIndex), "Users");
        }
    }
}
