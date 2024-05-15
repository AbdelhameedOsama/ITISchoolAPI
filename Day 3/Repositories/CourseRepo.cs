using Day_3.DTO;
using Day_3.Models;
using Day3.DTO;
using Microsoft.EntityFrameworkCore;

namespace Day_3.Repositories
{
    public interface ICourseRepo
    {
		public List<StudentsDTO> StudentsInCourse(int id);
		public List<StdCrsDTO> StdCrs(int id);



	}
	public class CourseRepo : ICourseRepo
	{
        ITI_DBContext db;
		IStudentRepo StudentRepo;
        public CourseRepo(ITI_DBContext db ,IStudentRepo st)
        {
			this.db = db;
			StudentRepo = st;
		}
        public List<StudentsDTO> StudentsInCourse(int id)
        {
            var stdCrss= db.Stud_Courses.Where(c => c.Crs_Id==id).ToList();
            List<Student> stds = new List<Student>();
            foreach (var item in stdCrss)
            {
				stds.Add(db.Students.Find(item.St_Id));
			}
            return StudentRepo.studentsDTOs(stds);
		}
		public List<StdCrsDTO> StdCrs(int id)
		{
			List<Stud_Course> stdc = db.Stud_Courses.Where(sc => sc.Crs_Id == id).ToList();
			return stdc.Select(sc => new StdCrsDTO
			{
				StName = db.Students.Find(sc.St_Id).St_Fname+" "+db.Students.Find(sc.St_Id).St_Lname,
				Degree = sc.Grade
			}).ToList();
		}
	}
}
