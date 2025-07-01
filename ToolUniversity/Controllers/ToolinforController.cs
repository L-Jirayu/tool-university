using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToolUniversity.Models.db;

namespace ToolUniversity.Controllers
{
    public class ToolinforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public ToolinforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var allTool = from tool in _dbContext.ToolInfors
                          where tool.ToolId > 0
                          select tool;

            if (allTool == null)
            {
                return NotFound();
            }
            return View(allTool);
        }

        //Create
        public IActionResult Create(ToolInfor tool)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ToolInfors.Add(tool);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tool);
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var tool = await _dbContext.ToolInfors.FirstOrDefaultAsync(i => i.ToolId == id);

            if (tool == null)
            {
                return NotFound();
            }

            return View(tool);
        }

        //Update
        public IActionResult Update()
        {
            IEnumerable<ToolInfor> allTool = _dbContext.ToolInfors;
            return View(allTool);
        }
        public IActionResult ShowUpd(int? id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var obj = _dbContext.ToolInfors.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        public IActionResult UpdDB(ToolInfor obj)
        {
            if (ModelState.IsValid)
            {
                _dbContext.ToolInfors.Update(obj);
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

            var tool = _dbContext.ToolInfors.Find(id);
            if (tool == null)
            {
                return NotFound();
            }

            var brokenTickets = _dbContext.BrokenInfors
                .Where(b => b.ToolId == id).ToList();
            _dbContext.BrokenInfors.RemoveRange(brokenTickets);

            var lendTickets = _dbContext.LendInfors
                .Where(l => l.ToolId == id).ToList();
            _dbContext.LendInfors.RemoveRange(lendTickets);

            var returnTickets = _dbContext.ReturnInfors
                .Where(r => r.ToolId == id).ToList();

            var returnIds = returnTickets.Select(r => r.ReturnId).ToList();
            var feeTickets = _dbContext.FeeInfors
                .Where(f => returnIds.Contains(f.ReturnId)).ToList();
            _dbContext.FeeInfors.RemoveRange(feeTickets);

            _dbContext.ReturnInfors.RemoveRange(returnTickets);

            _dbContext.ToolInfors.Remove(tool);

            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
