using System.ComponentModel.DataAnnotations;

namespace LeaveManager.Web.Models.LeaveTypes;

public class LeaveTypeCreateViewModel
{
    [Required]
    [Length(4, 150, ErrorMessage = "Between 3 and 151 value")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(1, 90)]
    [Display(Name ="Naximum Allocation of Days")]
    public int NumberOfDays { get; set; }
}
