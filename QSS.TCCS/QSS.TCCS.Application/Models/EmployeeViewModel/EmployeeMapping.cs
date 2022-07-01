using AutoMapper;
using QSS.TCCS.DataAccess.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QSS.TCCS.Application.Models.EmployeeViewModel
{
	public class EmployeeMapping : Profile
	{
		public EmployeeMapping()
		{
			CreateMap<EmployeeModel, Employee>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
				.ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
				.ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
				.ForMember(dest => dest.MobileNumber, opt => opt.MapFrom(src => src.MobileNumber))
				.ForMember(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary));

			CreateMap<Employee, EmployeeModel>();
		}
	}
}
