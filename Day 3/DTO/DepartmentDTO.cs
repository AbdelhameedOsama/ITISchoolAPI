namespace Day3.DTO
{
	public class DepartmentDTO
	{
		public int Dept_Id { get; set; }
		public string Dept_Name { get; set; }
		public string Dept_Desc { get; set; }
		public string Dept_Location { get; set; }
		public int? Dept_Manager { get; set; }
		public int NumberofStudents { get; set; }	
		public List<StudentsDTO> Students { get; set; }
	}
}
