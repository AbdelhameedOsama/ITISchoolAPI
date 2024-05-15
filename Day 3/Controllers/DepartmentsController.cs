using Day_3.Models;
using Day3.DTO;
using Day_3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Day_3.Repositories;
using Microsoft.AspNetCore.Authorization;
using Day_3.DTO;

namespace Day2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmentsController : ControllerBase
	{
		UnitOfWork unitOfWork;
		IDepartmentRepo departmentRepo;
		public DepartmentsController(UnitOfWork unitOfWork , IDepartmentRepo departmentRepo)
		{
			this.unitOfWork = unitOfWork;
			this.departmentRepo = departmentRepo;
		}
		[HttpGet]
		[Route("GetDepartments")]
		[SwaggerOperation(Summary = "Get All Departments", Description = "Get All Departments")]

		[Authorize(Roles = "Admin")]
		public IActionResult GetAllDepartments()
		{
			List<Department> departments =unitOfWork.DepartmentRepo.getAll();
			List<DepartmentDTO> result =departmentRepo.GetDepartmentDTOs(departments);
			return Ok(result);
		}
		[HttpGet]
		[Route("GetDepartments/Blazor")]
		[SwaggerOperation(Summary = "Get All Departments for Blazor", Description = "Get All Departments for Blazor")]
		public IActionResult GetDepartmentsForBlazor() {
			 List<DeptForBlazor> result = departmentRepo.GetAllForDeptForBlazor();
			return Ok(result);
		}
	}
}
