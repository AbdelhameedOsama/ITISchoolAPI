using Day_3.DTO;
using Day_3.Models;
using Day3.DTO;

namespace Day_3.Repositories
{
	public interface IStudentRepo
	{
		List<Student> search(string name);
		public List<StudentsDTO> studentsDTOs(List<Student> s);
		public List<StdDegreesDTO> StudentDegrees(int id);
		public List<StdForBlazor> GetStdForBlazors();
		public StdForBlazor StdForBlazorById(int id);
		public void Blazoradd(StdForBlazor s);
		public void Blazorupdate(StdForBlazor s);


	}
	public class StudentRepo : IStudentRepo
	{
		ITI_DBContext db;
		public StudentRepo(ITI_DBContext db)
		{
			this.db = db;
		}

		public List<Student> search(string name)
		{
			return db.Students.Where(s => s.St_Fname.StartsWith(name) || s.St_Lname.StartsWith(name)).ToList();
		}
		public List<StudentsDTO> studentsDTOs(List<Student> stds)
		{
			return stds.Select(s => new StudentsDTO
			{
				St_Id = s.St_Id,
				St_Fname = s.St_Fname,
				St_Lname = s.St_Lname,
				St_Address = s.St_Address,
				St_Age = s.St_Age,
				Dept_Id = s.Dept_Id,
				Dept_Name = s.Dept_Id == null ? null : db.Departments.Find(s.Dept_Id).Dept_Name,
				SupervisorID = s.St_super,
				Supervisor = s.St_super == null ? null : db.Students.Find(s.St_super).St_Fname + " " + db.Students.Find(s.St_super).St_Lname.Trim()
			}).ToList();
		}
		public List<StdDegreesDTO> StudentDegrees(int id)
		{
			return db.Stud_Courses.Where(sc => sc.St_Id == id).ToList().Select(sc => new StdDegreesDTO
			{
				crsName = db.Courses.Find(sc.Crs_Id).Crs_Name,
				degree = sc.Grade
			}).ToList();
		}


		//Blaazor Task
		public List<StdForBlazor> GetStdForBlazors()
		{
			return db.Students.Select(s => new StdForBlazor
			{
				St_Id = s.St_Id,
				St_Fname = s.St_Fname,
				St_Lname = s.St_Lname,
				St_Address = s.St_Address,
				St_Age = s.St_Age,
				Dept_Id = s.Dept_Id,
				St_super = s.St_super
			}).ToList();
		}
		public StdForBlazor StdForBlazorById(int id)
		{
			Student s = db.Students.Find(id);
			return new StdForBlazor
			{
				St_Id = s.St_Id,
				St_Fname = s.St_Fname,
				St_Lname = s.St_Lname,
				St_Address = s.St_Address,
				St_Age = s.St_Age,
				Dept_Id = s.Dept_Id,
				St_super = s.St_super
			};
		}

		public void Blazoradd(StdForBlazor s)
		{
			db.Students.Add(new Student
			{
				St_Id = s.St_Id,
				St_Fname = s.St_Fname,
				St_Lname = s.St_Lname,
				St_Address = s.St_Address,
				St_Age = s.St_Age,
				Dept_Id = s.Dept_Id,
				St_super = s.St_super
			});
		}

		public void Blazorupdate(StdForBlazor s)
		{
			Student student = db.Students.Find(s.St_Id);
			if (student != null) { 
			student.St_Fname = s.St_Fname;
			student.St_Lname = s.St_Lname;
			student.St_Address = s.St_Address;
			student.St_Age = s.St_Age;
			student.Dept_Id = s.Dept_Id;
			student.St_super = s.St_super;
			}
			else
			{
                Console.WriteLine("notFound");
            }
			db.Students.Update(student);

		}
		

	}
}
