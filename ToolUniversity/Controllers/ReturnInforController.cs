using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToolUniversity.Models.db;
using ToolUniversity.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace ToolUniversity.Controllers
{
    public class ReturnInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public ReturnInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var returns = await _dbContext.ReturnInfors
                .Include(r => r.Tool)
                .Include(r => r.User)
                .Select(r => new ReturnInforViewModel
                {
                    ReturnId = r.ReturnId,
                    ToolId = r.ToolId,
                    ToolName = r.Tool.ToolName,
                    UserId = r.UserId,
                    Name = r.User.Name,
                    DateReturn = r.DateReturn
                })
                .ToListAsync();

            return View(returns);
        }

        public IActionResult Create()
        {
            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName");
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReturnInforViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", model.ToolId);
                ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", model.UserId);
                return View(model);
            }

            var entity = new ReturnInfor
            {
                ToolId = model.ToolId,
                UserId = model.UserId,
                DateReturn = model.DateReturn
            };

            _dbContext.ReturnInfors.Add(entity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var lend = await _dbContext.ReturnInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ReturnId == id);

            if (lend == null) return NotFound();

            var viewModel = new ReturnInforViewModel
            {
                ReturnId = lend.ReturnId,
                ToolId = lend.ToolId,
                ToolName = lend.Tool.ToolName,
                UserId = lend.UserId,
                Name = lend.User.Name,
                DateReturn = lend.DateReturn
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnInfor = await _dbContext.ReturnInfors.FindAsync(id);
            if (returnInfor == null)
            {
                return NotFound();
            }

            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", returnInfor.ToolId);
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", returnInfor.UserId);

            var viewModel = new ReturnInforViewModel
            {
                ReturnId = returnInfor.ReturnId,
                ToolId = returnInfor.ToolId,
                UserId = returnInfor.UserId,
                DateReturn = returnInfor.DateReturn
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReturnInforViewModel vm)
        {
            if (id != vm.ReturnId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var lend = await _dbContext.ReturnInfors.FindAsync(id);
                    if (lend == null) return NotFound();

                    lend.ToolId = vm.ToolId;
                    lend.UserId = vm.UserId;
                    lend.DateReturn = vm.DateReturn;

                    _dbContext.Update(lend);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnInforExists(vm.ReturnId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ToolId"] = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", vm.ToolId);
            ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "Name", vm.UserId);

            return View(vm);
        }

        private bool ReturnInforExists(int id)
        {
            return _dbContext.ReturnInfors.Any(e => e.ReturnId == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var lend = await _dbContext.ReturnInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ReturnId == id);

            if (lend == null) return NotFound();

            var vm = new ReturnInforViewModel
            {
                ReturnId = lend.ReturnId,
                ToolName = lend.Tool.ToolName,
                Name = lend.User.Name,
                DateReturn = lend.DateReturn
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retu = await _dbContext.ReturnInfors.FindAsync(id);
            if (retu == null) return NotFound();

            _dbContext.ReturnInfors.Remove(retu);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
