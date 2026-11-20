using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LeaveManager.Web.Models.LeaveTypes;
using LeaveManager.Web.Services;

namespace LeaveManager.Web.Controllers
{
    public class LeaveTypesController(ILeaveTypeService leaveTypeService) : Controller
    {
        private const string LeaveTypeNameExistsErrorMessage = "This leave type alread exists in the db";

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            List<LeaveTypeReadOnlyViewModel> leaveTypeModels = await leaveTypeService.GetAllAsync();
            return View(leaveTypeModels);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var leaveTypeViewModel = await leaveTypeService.GetAsync<LeaveTypeReadOnlyViewModel>(id.Value);
            if (leaveTypeViewModel is null)
            {
                return NotFound();
            }

            return View(leaveTypeViewModel);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateViewModel leaveTypeCreate)
        {
            if (await leaveTypeService.NameExistsAsync(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), LeaveTypeNameExistsErrorMessage);
            }

            if (ModelState.IsValid)
            {
                await leaveTypeService.Create(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }

            return View(leaveTypeCreate);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var leaveTypeViewModel = await leaveTypeService.GetAsync<LeaveTypeEditViewModel>(id.Value);
            if (leaveTypeViewModel is null)
            {
                return NotFound();
            }

            return View(leaveTypeViewModel);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditViewModel leaveTypeEditViewModel)
        {
            if (id != leaveTypeEditViewModel.Id)
            {
                return NotFound();
            }

            if (await leaveTypeService.NameForEditExistsAsync(leaveTypeEditViewModel))
            {
                ModelState.AddModelError(nameof(leaveTypeEditViewModel.Name), LeaveTypeNameExistsErrorMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await leaveTypeService.Update(leaveTypeEditViewModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!leaveTypeService.Exists(leaveTypeEditViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEditViewModel);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var leaveTypeViewModel = await leaveTypeService.GetAsync<LeaveTypeReadOnlyViewModel>(id.Value);
            if (leaveTypeViewModel is null)
            {
                return NotFound();
            }

            return View(leaveTypeViewModel);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await leaveTypeService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
