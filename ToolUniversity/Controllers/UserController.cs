using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolUniversity.Models.db;

namespace ToolUniversity.Controllers
{
    public class UserController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public UserController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var allUser = from user in _dbContext.Users
                          where user.UserId > 0
                          select user;

            if (allUser == null)
            {
                return NotFound();
            }
            return View(allUser);
        }

        //Create
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(new User());
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }


        //Update
        public IActionResult Update()
        {
            IEnumerable<User> allUser = _dbContext.Users;
            return View(allUser);
        }
        public IActionResult ShowUpd(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult UpdDB(User obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Users.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //Delete
        public IActionResult DelDB(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            var feeTickets = _dbContext.FeeInfors.Where(t => t.UserId == id).ToList();
            _dbContext.FeeInfors.RemoveRange(feeTickets);

            var returnTickets = _dbContext.ReturnInfors.Where(t => t.UserId == id).ToList();
            _dbContext.ReturnInfors.RemoveRange(returnTickets);

            var brokenTickets = _dbContext.BrokenInfors.Where(t => t.UserId == id).ToList();
            _dbContext.BrokenInfors.RemoveRange(brokenTickets);

            var lendTickets = _dbContext.LendInfors.Where(t => t.UserId == id).ToList();
            _dbContext.LendInfors.RemoveRange(lendTickets);


            _dbContext.Users.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
