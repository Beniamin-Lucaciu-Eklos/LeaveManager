using System;
using AutoMapper;
using LeaveManager.Web.Data;
using LeaveManager.Web.Models.LeaveTypes;

namespace LeaveManager.Web.MappingProfiles;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>();
        // .ForMember(dest => dest.Days, options => options.MapFrom(src => src.NumberOfDays));

        CreateMap<LeaveTypeCreateViewModel, LeaveType>();

        CreateMap<LeaveType, LeaveTypeEditViewModel>()
            .ReverseMap();
    }

}
