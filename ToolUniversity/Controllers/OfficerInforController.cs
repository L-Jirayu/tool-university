using Microsoft.AspNetCore.Mvc;
using ToolUniversity.Models.db;
using Microsoft.EntityFrameworkCore;

namespace ToolUniversity.Controllers
{
    public class OfficerInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public OfficerInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var allOffi = from offi in _dbContext.OfficerInfors
                          where offi.OfficerId > 0
                          select offi;

            if (allOffi == null)
            {
                return NotFound();
            }
            return View(allOffi);
        }

        //Create
        public IActionResult Create(OfficerInfor offi)
        {
            if (ModelState.IsValid)
            {
                _dbContext.OfficerInfors.Add(offi);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(offi);
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var offi = await _dbContext.OfficerInfors.FirstOrDefaultAsync(i => i.OfficerId == id);

            if (offi == null)
            {
                return NotFound();
            }

            return View(offi);
        }

        //Update
        public IActionResult Update()
        {
            IEnumerable<OfficerInfor> allOffi = _dbContext.OfficerInfors;
            return View(allOffi);
        }
        public IActionResult ShowUpd(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.OfficerInfors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult UpdDB(OfficerInfor obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.OfficerInfors.Update(obj);
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
            var obj = _dbContext.OfficerInfors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _dbContext.OfficerInfors.Remove(obj);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
