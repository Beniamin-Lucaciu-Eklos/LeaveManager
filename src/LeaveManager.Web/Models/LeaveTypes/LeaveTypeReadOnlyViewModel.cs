using System;
using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Web.Models.LeaveTypes;

public class LeaveTypeReadOnlyViewModel : BaseLeaveTypeViewModel
{
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Naximum Allocation of Days")]
    public int NumberOfDays { get; set; }
}
