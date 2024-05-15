using System.Data;
using System.Data.SqlClient;

namespace WebApp.model
{
	public class DAL
	{
		public List<Users> GetUsers(IConfiguration configuration)
		{
			List<Users> listUsers = new List<Users>();
			using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
			{
				SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers", con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				if (dt.Rows.Count > 0)
				{
					for (int i = 0; i < dt.Rows.Count; i++)
					{
						Users user = new Users();
						user.ID = (int)dt.Rows[i]["ID"];
						user.FirstName = dt.Rows[i]["FirstName"]?.ToString() ?? "";
						user.LastName = dt.Rows[i]["LastName"]?.ToString() ?? "";
						listUsers.Add(user);
					}
				}
			}

			return listUsers;
		}

		public int AddUser(Users user, IConfiguration configuration)
		{
			int i = 0;
			using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
			{
				SqlCommand cmd = new SqlCommand("INSERT INTO TblUsers(FirstName, LastName) VALUES('" + user.FirstName + "', '" + user.LastName + "')", con);
				con.Open();
				i = cmd.ExecuteNonQuery();
				con.Close();
			};

			return i;
		}

		public Users GetUser(int id, IConfiguration configuration)
		{
			Users user = new Users();
			using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
			{
				SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers WHERE ID = '" + id + "'", con);
				DataTable dt = new DataTable();
				da.Fill(dt);
				if (dt.Rows.Count > 0)
				{
					user.ID = (int)dt.Rows[0]["ID"];
					user.FirstName = dt.Rows[0]["FirstName"]?.ToString() ?? "";
					user.LastName = dt.Rows[0]["LastName"]?.ToString() ?? "";
				}
			}

			return user;
		}

		public int UpdateUser(Users user, IConfiguration configuration)
		{
			int rowsAffected = 0;
			using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS")))
			{
				string query = "UPDATE TblUsers SET FirstName = @FirstName, LastName = @LastName WHERE ID = @ID";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
					cmd.Parameters.AddWithValue("@LastName", user.LastName);
					cmd.Parameters.AddWithValue("@ID", user.ID);

					con.Open();
					rowsAffected = cmd.ExecuteNonQuery();
					con.Close();
				}
			}

			return rowsAffected;
		}



		//public Users GetUser(int id, IConfiguration configuration)
		//{
		//	Users user = new Users();
		//	using (SqlConnection con = new SqlConnection(configuration.GetConnectionString("DBCS").ToString()))
		//	{
		//		SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM TblUsers WHERE ID = '" + id + "'", con);
		//		DataTable dt = new DataTable();
		//		da.Fill(dt);
		//		if (dt.Rows.Count > 0)
		//		{
		//			user.ID = (int)dt.Rows[0]["ID"];
		//			user.FirstName = dt.Rows[0]["FirstName"]?.ToString() ?? "";
		//			user.LastName = dt.Rows[0]["LastName"]?.ToString() ?? "";
		//		}
		//	}

		//	return user;
		//}
	}
}
