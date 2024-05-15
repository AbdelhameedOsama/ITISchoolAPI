using Day_3.Models;
using Day3.DTO;
using Day_3.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Day_3.DTO;

namespace Day_3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class StudentsController : ControllerBase
	{
		UnitOfWork unitOfWork;
		IStudentRepo studentRepo;
		public StudentsController(UnitOfWork _unitOfWork, IStudentRepo studentRepo)
		{
			unitOfWork = _unitOfWork;
			this.studentRepo = studentRepo;
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Get All Students", Description = "Get All Students")]
		public IActionResult GetAllStudents()
		{
			List<Student> students = unitOfWork.StudentRepo.getAll();
			List<StudentsDTO> result = new List<StudentsDTO>();
			foreach(Student student in students)
			{
				StudentsDTO studentDTO = new StudentsDTO();
				studentDTO.St_Id = student.St_Id;
				studentDTO.St_Fname = student.St_Fname;
				studentDTO.St_Lname = student.St_Lname;
				studentDTO.St_Address = student.St_Address;
				studentDTO.St_Age = student.St_Age;
				studentDTO.Dept_Id = student.Dept_Id;
				studentDTO.Dept_Name = student.Dept_Id==null?null: unitOfWork.DepartmentRepo.getById((int)student.Dept_Id).Dept_Name;
				studentDTO.SupervisorID = student.St_super;
				studentDTO.Supervisor = student.St_super == null ? null : unitOfWork.StudentRepo.getById((int)student.St_super).St_Fname + " " + unitOfWork.StudentRepo.getById((int)student.St_super).St_Lname;
					
				result.Add(studentDTO);
			}
			return Ok(result);
		}
		[HttpPost]
		[Consumes("application/json")]
		[Produces("application/json")]
		[SwaggerOperation(Summary = "Add Student", Description = "Add Student")]
		public IActionResult AddStudent(StudentsDTO student)
		{
			unitOfWork.StudentRepo.add(new Student
			{
				St_Fname = student.St_Fname,
				St_Lname = student.St_Lname,
				St_Address = student.St_Address,
				St_Age = student.St_Age,
				St_Id = student.St_Id,
				St_super = student.SupervisorID,
				Dept_Id = student.Dept_Id,

			});
				
			unitOfWork.Save();

			return Ok(student);
		}

		[HttpGet]
		[Route("StudentPaging")]
		[SwaggerOperation(Summary = "Get All Students with Paging", Description = "Get All Students with Paging")]
		public IActionResult GetStds(int page , int pageSize)
		{
			List<Student> stds =unitOfWork.StudentRepo.getAll();
			int count = stds.Count();
			int totalPages=count/pageSize;
			stds=stds.Skip((page-1)*pageSize).Take(pageSize).ToList();
			List<StudentsDTO> result = studentRepo.studentsDTOs(stds);
			return Ok(result) ;
		}
		[HttpGet]
		[Route("StudentSearch")]
		[SwaggerOperation(Summary = "Search for Students", Description = "Search for Students")]
		public IActionResult SearchStds(string name)
		{
			List<Student> stds = studentRepo.search(name);
			List<StudentsDTO> result =studentRepo.studentsDTOs(stds);
			return Ok(result);
		}
		[HttpGet]
		[Route("StudentDegrees")]
		[SwaggerOperation(Summary = "Get Student Degrees", Description = "Get Student Degrees")]
		public IActionResult StudentDegrees(int id)
		{
			List<StdDegreesDTO> result = studentRepo.StudentDegrees(id);
			return Ok(result);
		}
		//Blaazor Task
		[HttpGet]
		[Route("GetStdForBlazors")]
		[SwaggerOperation(Summary = "Get Students for Blazor", Description = "Get Students for Blazor")]
		public IActionResult GetStdForBlazors()
		{
			List<StdForBlazor> result = studentRepo.GetStdForBlazors();
			return Ok(result);
		}
		[HttpGet]
		[Route("StdForBlazorById")]
		[SwaggerOperation(Summary = "Get Student for Blazor by ID", Description = "Get Student for Blazor by ID")]
		public IActionResult StdForBlazorById(int id)
		{
			StdForBlazor result = studentRepo.StdForBlazorById(id);
			return Ok(result);
		}
		[HttpPost]
		[Route("editStudentForBlazor")]
		[SwaggerOperation(Summary = "Edit Student for Blazor", Description = "Edit Student for Blazor")]
		public IActionResult editStudentForBlazor(int id,StdForBlazor std)
		{
			Student student = unitOfWork.StudentRepo.getById(id);
			student.St_Fname = std.St_Fname;
			student.St_Lname = std.St_Lname;
			student.St_Address = std.St_Address;
			student.St_Age = std.St_Age;
			student.Dept_Id = std.Dept_Id;
			student.St_super = std.St_super;
			unitOfWork.Save();
			return Ok(std);
		}


	}
}
