using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToolUniversity.Models.db;
using ToolUniversity.ViewModels;

namespace ToolUniversity.Controllers
{
    public class LendInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public LendInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var lends = await _dbContext.LendInfors
                .Include(l => l.Tool)
                .Include(l => l.User)
                .Select(l => new LendInforViewModel
                {
                    LendId = l.LendId,
                    ToolId = l.ToolId,
                    ToolName = l.Tool.ToolName,
                    UserId = l.UserId,
                    Name = l.User.Name,
                    DateLend = l.DateLend
                })
                .ToListAsync();

            return View(lends);
        }

        public IActionResult Create()
        {
            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName");
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LendInforViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", model.ToolId);
                ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", model.UserId);
                return View(model);
            }

            var entity = new LendInfor
            {
                ToolId = model.ToolId,
                UserId = model.UserId,
                DateLend = model.DateLend
            };

            _dbContext.LendInfors.Add(entity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var lend = await _dbContext.LendInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.LendId == id);

            if (lend == null) return NotFound();

            var viewModel = new LendInforViewModel
            {
                LendId = lend.LendId,
                ToolId = lend.ToolId,
                ToolName = lend.Tool.ToolName,
                UserId = lend.UserId,
                Name = lend.User.Name,
                DateLend = lend.DateLend
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lendInfor = await _dbContext.LendInfors.FindAsync(id);
            if (lendInfor == null)
            {
                return NotFound();
            }

            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", lendInfor.ToolId);
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", lendInfor.UserId);

            var viewModel = new LendInforViewModel
            {
                LendId = lendInfor.LendId,
                ToolId = lendInfor.ToolId,
                UserId = lendInfor.UserId,
                DateLend = lendInfor.DateLend
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LendInforViewModel vm)
        {
            if (id != vm.LendId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var lend = await _dbContext.LendInfors.FindAsync(id);
                    if (lend == null) return NotFound();

                    lend.ToolId = vm.ToolId;
                    lend.UserId = vm.UserId;
                    lend.DateLend = vm.DateLend;

                    _dbContext.Update(lend);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LendInforExists(vm.LendId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ToolId"] = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", vm.ToolId);
            ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "Name", vm.UserId);

            return View(vm);
        }

        private bool LendInforExists(int id)
        {
            return _dbContext.LendInfors.Any(e => e.LendId == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lend = await _dbContext.LendInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.LendId == id);

            if (lend == null) return NotFound();

            var vm = new LendInforViewModel
            {
                LendId = lend.LendId,
                ToolName = lend.Tool.ToolName,
                Name = lend.User.Name,
                DateLend = lend.DateLend
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lend = await _dbContext.LendInfors.FindAsync(id);
            if (lend == null) return NotFound();

            _dbContext.LendInfors.Remove(lend);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
