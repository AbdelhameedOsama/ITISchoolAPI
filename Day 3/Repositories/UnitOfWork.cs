using Day_3.Models;

namespace Day_3.Repositories
{
	public class UnitOfWork
	{
		ITI_DBContext db;
		GenericRepo<Student> studentRepo;
		GenericRepo<Department> departmentRepo;
		public UnitOfWork(ITI_DBContext db)
		{
			this.db = db;
		}
		public GenericRepo<Student> StudentRepo
		{
			get
			{
				if (studentRepo == null)
				{
					studentRepo = new GenericRepo<Student>(db);
				}
				return studentRepo;
			}
		}
		public GenericRepo<Department> DepartmentRepo
		{
			get
			{
				if (departmentRepo == null)
				{
					departmentRepo = new GenericRepo<Department>(db);
				}
				return departmentRepo;
			}
		}
		public void Save()
		{
			db.SaveChanges();
		}
	}
}
