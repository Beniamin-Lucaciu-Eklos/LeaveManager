using System;
using AutoMapper;
using LeaveManager.Web.Data;
using LeaveManager.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManager.Web.Services;

public class LeaveTypeService(ApplicationDbContext context, IMapper mapper) : ILeaveTypeService
{
    public async Task<List<LeaveTypeReadOnlyViewModel>> GetAllAsync()
    {
        List<LeaveType> leaveTypes = await context.LeaveTypes.ToListAsync();

        var leaveTypeModels = mapper.Map<List<LeaveTypeReadOnlyViewModel>>(leaveTypes);
        return leaveTypeModels;
    }

    public async Task<TViewModel?> GetAsync<TViewModel>(int id) where TViewModel : class
    {
        var leaveType = await GetByIdAsync(id);
        if (leaveType is null)
            return null;

        var leaveTypeViewModel = mapper.Map<TViewModel>(leaveType);
        return leaveTypeViewModel;
    }

    public async Task Create(LeaveTypeCreateViewModel model)
    {
        var leaveType = mapper.Map<LeaveType>(model);
        context.Add(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task Update(LeaveTypeEditViewModel model)
    {
        var leaveType = mapper.Map<LeaveType>(model);
        context.Update(leaveType);
        await context.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var leaveType = await GetByIdAsync(id);
        if (leaveType is not null)
        {
            context.LeaveTypes.Remove(leaveType);
            await context.SaveChangesAsync();
        }
    }

    private Task<LeaveType?> GetByIdAsync(int id)
    {
        return context.LeaveTypes
        .FirstOrDefaultAsync(m => m.Id == id);
    }

    public bool Exists(int id)
    {
        return context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> NameExistsAsync(string name)
    {
        string nameLower = name.ToLower();
        return await context.LeaveTypes.AnyAsync(e => e.Name.ToLower() == nameLower);
    }

    public async Task<bool> NameForEditExistsAsync(LeaveTypeEditViewModel leaveTypeEdit)
    {
        string nameLower = leaveTypeEdit.Name.ToLower();
        return await context.LeaveTypes.AnyAsync(e => e.Name.ToLower() == nameLower && e.Id != leaveTypeEdit.Id);
    }
}
