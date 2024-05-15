using Day_3.DTO;
using Day_3.Models;
using Day3.DTO;
using Microsoft.EntityFrameworkCore;

namespace Day_3.Repositories
{
	public interface IDepartmentRepo
	{
		int getDepartmentStudentsCount(int id);
		public List<StudentsDTO> DepStds(int id);
		public List<DepartmentDTO> GetDepartmentDTOs(List<Department> depts);
		public List<DeptForBlazor> GetAllForDeptForBlazor();


	}
	public class DepartmentRepo : IDepartmentRepo
	{
		ITI_DBContext db;
		public DepartmentRepo(ITI_DBContext db)
		{
			this.db = db;
		}
		public List<DepartmentDTO> GetDepartmentDTOs(List<Department> depts)
		{
			return depts.Select(d => new DepartmentDTO
			{
				Dept_Id = d.Dept_Id,
				Dept_Name = d.Dept_Name,
				Dept_Desc = d.Dept_Desc,
				Dept_Location = d.Dept_Location,
				Dept_Manager = d.Dept_Manager,
				NumberofStudents = getDepartmentStudentsCount(d.Dept_Id),
				Students = DepStds(d.Dept_Id)
			}).ToList();
		}
		public int getDepartmentStudentsCount(int id)
		{
			return db.Students.Count(s => s.Dept_Id == id);
		}
		public List<StudentsDTO> DepStds(int id)
		{
			return db.Students.Where(s => s.Dept_Id == id).Include(s => s.Dept).Select(s =>

				new StudentsDTO
				{
					St_Id = s.St_Id,
					St_Fname = s.St_Fname,
					St_Lname = s.St_Lname,
					St_Address = s.St_Address,
					St_Age = s.St_Age,
					Dept_Id = s.Dept_Id,
					Dept_Name = s.Dept.Dept_Name,
					SupervisorID = s.St_super,
					Supervisor = s.St_superNavigation.St_Fname + " " + s.St_superNavigation.St_Lname
				}
			).ToList();
		}
		public List<DeptForBlazor> GetAllForDeptForBlazor()
		{
			return db.Departments.Select(d => new DeptForBlazor
			{
				Dept_Id = d.Dept_Id,
				Dept_Name = d.Dept_Name,
				Dept_Desc = d.Dept_Desc,
				Dept_Location = d.Dept_Location,
				Dept_Manager = d.Dept_Manager
			}).ToList();
		}
	}
}
