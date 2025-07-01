using Microsoft.AspNetCore.Mvc;
using ToolUniversity.Models.db;
using Microsoft.EntityFrameworkCore;

namespace ToolUniversity.Controllers
{
    public class ManagerInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public ManagerInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var allMagn = from magn in _dbContext.ManagerInfors
                          where magn.ManagerId > 0
                          select magn;

            if (allMagn == null)
            {
                return NotFound();
            }
            return View(allMagn);
        }

        //Create
        public IActionResult Create(ManagerInfor magn)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ManagerInfors.Add(magn);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(magn);
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var magn = await _dbContext.ManagerInfors.FirstOrDefaultAsync(i => i.ManagerId == id);

            if (magn == null)
            {
                return NotFound();
            }

            return View(magn);
        }

        //Update
        public IActionResult Update()
        {
            IEnumerable<ManagerInfor> allMagn = _dbContext.ManagerInfors;
            return View(allMagn);
        }
        public IActionResult ShowUpd(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.ManagerInfors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult UpdDB(ManagerInfor obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ManagerInfors.Update(obj);
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
            var obj = _dbContext.ManagerInfors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.ManagerInfors.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
