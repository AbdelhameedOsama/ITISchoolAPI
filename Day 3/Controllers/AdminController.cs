using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Day_3.Repositories;
using Day_3.Models;
using Microsoft.AspNetCore.Authorization;

namespace Day_3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize(Roles ="Admin")]
	public class AdminController : ControllerBase
	{
		IStudentRepo studentRepo;
		IDepartmentRepo departmentRepo;
		ICourseRepo courseRepo;
		UnitOfWork unitOfWork;
		public AdminController(IStudentRepo studentRepo,IDepartmentRepo departmentRepo,UnitOfWork unitOfWork ,ICourseRepo courseRepo)
		{
			this.studentRepo = studentRepo;
			this.departmentRepo = departmentRepo;
			this.unitOfWork = unitOfWork;
			this.courseRepo = courseRepo;
		}

		[HttpGet("AllStudents")]
		public IActionResult AllStudents()
		{
			List<Student> stds= unitOfWork.StudentRepo.getAll();
			return Ok(studentRepo.studentsDTOs(stds));
		}
		[HttpGet("AllDepartments")]
		public IActionResult AllDepartments()
		{
			List<Department> depts= unitOfWork.DepartmentRepo.getAll();
			return Ok(departmentRepo.GetDepartmentDTOs(depts));
		}

		[HttpGet("Students's grades In Course")]
		public IActionResult StudentInCourse(int CourseId)
		{
			return Ok(courseRepo.StdCrs(CourseId));
		}
	
	}
}
