using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToolUniversity.Models.db;
using ToolUniversity.ViewModels;

namespace ToolUniversity.Controllers
{
    public class FeeInforController : Controller
    {
        private readonly ToolUniverContext _dbContext;

        public FeeInforController(ToolUniverContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var fees = await _dbContext.FeeInfors
                .Include(f => f.User)
                .Include(f => f.Lend)
                .Include(f => f.Return)
                .Select(f => new FeeInforViewModel
                {
                    FeeId = f.FeeId,
                    UserId = f.UserId,
                    UserName = f.User.Name,
                    LendId = f.LendId,
                    ReturnId = f.ReturnId,
                    Fee = f.Fee
                })
                .ToListAsync();

            return View(fees);
        }

        public IActionResult Create()
        {
            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name");
            ViewBag.LendId = new SelectList(_dbContext.LendInfors, "LendId", "LendId");
            ViewBag.ReturnId = new SelectList(_dbContext.ReturnInfors, "ReturnId", "ReturnId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FeeInforViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", model.UserId);
                ViewBag.LendId = new SelectList(_dbContext.LendInfors, "LendId", "LendId", model.LendId);
                ViewBag.ReturnId = new SelectList(_dbContext.ReturnInfors, "ReturnId", "ReturnId", model.ReturnId);
                return View(model);
            }

            var entity = new FeeInfor
            {
                UserId = model.UserId,
                LendId = model.LendId,
                ReturnId = model.ReturnId,
                Fee = model.Fee
            };

            _dbContext.FeeInfors.Add(entity);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var fee = await _dbContext.FeeInfors
                .Include(f => f.User)
                .Include(f => f.Lend)
                .Include(f => f.Return)
                .FirstOrDefaultAsync(f => f.FeeId == id);

            if (fee == null) return NotFound();

            var viewModel = new FeeInforViewModel
            {
                FeeId = fee.FeeId,
                UserId = fee.UserId,
                UserName = fee.User.Name,
                LendId = fee.LendId,
                ReturnId = fee.ReturnId,
                Fee = fee.Fee
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var fee = await _dbContext.FeeInfors.FindAsync(id);
            if (fee == null) return NotFound();

            ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", fee.UserId);
            ViewBag.LendId = new SelectList(_dbContext.LendInfors, "LendId", "LendId", fee.LendId);
            ViewBag.ReturnId = new SelectList(_dbContext.ReturnInfors, "ReturnId", "ReturnId", fee.ReturnId);

            var viewModel = new FeeInforViewModel
            {
                FeeId = fee.FeeId,
                UserId = fee.UserId,
                LendId = fee.LendId,
                ReturnId = fee.ReturnId,
                Fee = fee.Fee
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FeeInforViewModel model)
        {
            if (id != model.FeeId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.UserId = new SelectList(_dbContext.Users, "UserId", "Name", model.UserId);
                ViewBag.LendId = new SelectList(_dbContext.LendInfors, "LendId", "LendId", model.LendId);
                ViewBag.ReturnId = new SelectList(_dbContext.ReturnInfors, "ReturnId", "ReturnId", model.ReturnId);
                return View(model);
            }

            var fee = await _dbContext.FeeInfors.FindAsync(id);
            if (fee == null) return NotFound();

            fee.UserId = model.UserId;
            fee.LendId = model.LendId;
            fee.ReturnId = model.ReturnId;
            fee.Fee = model.Fee;

            _dbContext.Update(fee);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var fee = await _dbContext.FeeInfors
                .Include(f => f.User)
                .Include(f => f.Lend)
                .Include(f => f.Return)
                .FirstOrDefaultAsync(f => f.FeeId == id);

            if (fee == null) return NotFound();

            var vm = new FeeInforViewModel
            {
                FeeId = fee.FeeId,
                UserId = fee.UserId,
                UserName = fee.User.Name,
                LendId = fee.LendId,
                ReturnId = fee.ReturnId,
                Fee = fee.Fee
            };

            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fee = await _dbContext.FeeInfors.FindAsync(id);
            if (fee == null) return NotFound();

            _dbContext.FeeInfors.Remove(fee);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
