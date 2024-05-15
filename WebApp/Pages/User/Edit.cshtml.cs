using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.model;

namespace WebApp.Pages.User
{
	public class EditModel : PageModel
	{
		public Users user = new Users();
		public string successMessage = string.Empty;
		public string errorMessage = string.Empty;

		private readonly IConfiguration configuration;
		public EditModel(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public void OnGet()
		{
			string id = Request.Query["id"];
			try
			{
				if (int.TryParse(id, out int userId))
				{
					DAL dal = new DAL();
					user = dal.GetUser(userId, configuration);
				}
				else
				{
					//
				}
			}
			catch (Exception ex)
			{
				errorMessage = ex.Message;
			}
		}

	}
}
