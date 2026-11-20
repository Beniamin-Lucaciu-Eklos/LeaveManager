using LeaveManager.Web.Models.LeaveTypes;

namespace LeaveManager.Web.Services;

public interface ILeaveTypeService
{
    Task Create(LeaveTypeCreateViewModel model);

    Task<List<LeaveTypeReadOnlyViewModel>> GetAllAsync();

    Task<TViewModel?> GetAsync<TViewModel>(int id) where TViewModel : class;

    Task RemoveAsync(int id);

    Task Update(LeaveTypeEditViewModel model);

    bool Exists(int id);

    Task<bool> NameExistsAsync(string name);

    Task<bool> NameForEditExistsAsync(LeaveTypeEditViewModel leaveTypeEdit);
}
