using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToolUniversity.Models.db;
using ToolUniversity.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ToolUniversity.Controllers
{
    public class BrokenInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public BrokenInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var returns = await _dbContext.BrokenInfors
                .Include(b => b.Tool)
                .Include(b => b.User)
                .Include(b => b.Officer)
                .Select(b => new BrokenInforViewModel
                {
                    BrokenId = b.BrokenId,
                    ToolId = b.ToolId,
                    ToolName = b.Tool.ToolName,
                    UserId = b.UserId,
                    Name = b.User.Name,
                    Reason = b.Reason,
                    OfficerId = b.OfficerId,
                    OfficerName = b.Officer.Name
                })
                .ToListAsync();

            return View(returns);
        }

        public IActionResult Create()
        {
            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName");
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name");
            ViewBag.OfficerId = new SelectList(_dbContext.OfficerInfors, "OfficerId", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrokenInforViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", model.ToolId);
                ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", model.UserId);
                ViewBag.OfficerId = new SelectList(_dbContext.OfficerInfors, "OfficerId", "Name", model.OfficerId);
                return View(model);
            }

            var entity = new BrokenInfor
            {
                ToolId = model.ToolId,
                UserId = model.UserId,
                Reason = model.Reason ?? string.Empty,
                OfficerId = model.OfficerId,
            };

            _dbContext.BrokenInfors.Add(entity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var brok = await _dbContext.BrokenInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .Include(o => o.Officer)
                .FirstOrDefaultAsync(m => m.BrokenId == id);

            if (brok == null) return NotFound();

            var viewModel = new BrokenInforViewModel
            {
                BrokenId = brok.BrokenId,
                ToolId = brok.ToolId,
                ToolName = brok.Tool.ToolName,
                UserId = brok.UserId,
                Name = brok.User.Name,
                Reason = brok.Reason,
                OfficerId = brok.OfficerId,
                OfficerName = brok.Officer.Name
            };

            return View(viewModel);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brokenInfor = await _dbContext.BrokenInfors.FindAsync(id);
            if (brokenInfor == null)
            {
                return NotFound();
            }

            ViewBag.ToolId = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", brokenInfor.ToolId);
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", brokenInfor.UserId);
            ViewBag.OfficerId = new SelectList(_dbContext.OfficerInfors, "OfficerId", "Name", brokenInfor.OfficerId);


            var viewModel = new BrokenInforViewModel
            {
                BrokenId = brokenInfor.BrokenId,
                ToolId = brokenInfor.ToolId,
                UserId = brokenInfor.UserId,
                OfficerId = brokenInfor.OfficerId,
                Reason = brokenInfor.Reason
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BrokenInforViewModel vm)
        {
            if (id != vm.BrokenId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var brok = await _dbContext.BrokenInfors.FindAsync(id);
                    if (brok == null) return NotFound();

                    brok.ToolId = vm.ToolId;
                    brok.UserId = vm.UserId;
                    brok.OfficerId = vm.OfficerId;
                    brok.Reason = vm.Reason ?? string.Empty;

                    _dbContext.Update(brok);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrokenInforExists(vm.BrokenId)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ToolId"] = new SelectList(_dbContext.ToolInfors, "ToolId", "ToolName", vm.ToolId);
            ViewData["UserId"] = new SelectList(_dbContext.Users, "UserId", "Name", vm.UserId);
            ViewData["UserId"] = new SelectList(_dbContext.OfficerInfors, "OfficerId", "Name", vm.OfficerId);

            return View(vm);
        }

        private bool BrokenInforExists(int id)
        {
            return _dbContext.ReturnInfors.Any(e => e.ReturnId == id);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var brok = await _dbContext.BrokenInfors
                .Include(t => t.Tool)
                .Include(u => u.User)
                .Include(o => o.Officer)
                .FirstOrDefaultAsync(m => m.BrokenId == id);

            if (brok == null) return NotFound();

            var vm = new BrokenInforViewModel
            {
                BrokenId = brok.BrokenId,
                ToolId = brok.ToolId,
                ToolName = brok.Tool.ToolName,
                UserId = brok.UserId,
                Name = brok.User.Name,
                OfficerId = brok.OfficerId,
                OfficerName = brok.Officer.Name,
                Reason = brok.Reason
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brok = await _dbContext.BrokenInfors.FindAsync(id);
            if (brok == null) return NotFound();

            _dbContext.BrokenInfors.Remove(brok);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
