using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.model;

namespace WebApp.Pages.User
{
	public class CreateModel : PageModel
	{
		public Users user = new Users();
		public string successMessage = string.Empty;
		public string errorMessage = string.Empty;

		private readonly IConfiguration configuration;

		public CreateModel(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void OnGet()
		{
		}

		public void OnPost()
		{
			user.FirstName = Request.Form["FirstName"];
			user.LastName = Request.Form["LastName"];

			if (user.FirstName.Length == 0 || user.LastName.Length == 0)
			{
				errorMessage = "All fields are required!";
				return;
			}

			try
			{
				DAL dal = new DAL();
				int i = dal.AddUser(user, configuration);
				if (i > 0)
				{
					successMessage = "User has been added";
				}
				else
				{
					errorMessage = "Failed to add user";
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				return;
			}

			user.FirstName = ""; user.LastName = "";
			Response.Redirect("/User/Index");
		}
	}
}


//using Microsoft.AspNetCore.Mvc.RazorPages;
//using WebApp.model;

//namespace WebApp.Pages.User
//{
//	public class CreateModel : PageModel
//	{
//		public Users user = new Users();
//		public string successMessage = string.Empty;
//		public string errorMessage = string.Empty;

//		private readonly IConfiguration configuration;

//		public CreateModel(IConfiguration configuration)
//		{
//			this.configuration = configuration;
//		}

//		public void OnGet()
//		{
//		}

//		public void OnPost()
//		{
//			user.FirstName = Request.Form["FirstName"];
//			user.LastName = Request.Form["LastName"];

//			if (user.FirstName.Length == 0 || user.LastName.Length == 0)
//			{
//				errorMessage = "All fields are required!";
//				return;
//			}

//			try
//			{
//				DAL dal = new DAL();
//				int i = dal.AddUser(user, configuration);
//			}
//			catch (Exception ex)
//			{
//				errorMessage = ex.Message;
//				return;
//			}

//			user.FirstName = ""; user.LastName = "";
//			successMessage = "User has been added";
//			Response.Redirect("/User/Index");
//		}
//	}
//}
