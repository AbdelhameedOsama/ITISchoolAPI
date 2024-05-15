using Day_3.Models;

namespace Day_3.Repositories
{
	public interface IUserRepo
	{
		public Account Authenticate(string email, string password);
	}
	public class UserRepo : IUserRepo
	{
		ITI_DBContext db;
		public UserRepo(ITI_DBContext db)
		{
			this.db = db;
		}
		public Account Authenticate(string email, string password)
		{
			Account account = db.Accounts.Find(email);
			if (account != null)
			{
				if (account.Password == password)
				{
					return account;
				}
			}
			return null;
		}


	}
}
