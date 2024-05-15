using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.model;

namespace WebApp.Pages.User
{
    public class EditModel : PageModel
    {
        public Users user { get; private set; } = new Users();
        public string SuccessMessage { get; private set; } = string.Empty;
        public string ErrorMessage { get; private set; } = string.Empty;

        private readonly IConfiguration _configuration;

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string id = Request.Query["id"];
            try
            {
                if (int.TryParse(id, out int userId))
                {
                    DAL dal = new DAL();
                    user = dal.GetUser(userId, _configuration) ?? new Users(); // Ensure user is initialized
                }
                else
                {
                    ErrorMessage = "Invalid user ID format.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            if (int.TryParse(Request.Form["hiddenId"], out int userId))
            {
                user.ID = userId;
            }
            else
            {
                ErrorMessage = "Invalid user ID format.";
                return;
            }

            user.FirstName = Request.Form["FirstName"];
            user.LastName = Request.Form["LastName"];

            if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
            {
                ErrorMessage = "All fields are required!";
                return;
            }

            try
            {
                DAL dal = new DAL();
                int rowsAffected = dal.UpdateUser(user, _configuration);
                if (rowsAffected > 0)
                {
                    SuccessMessage = "User has been updated.";
                    Response.Redirect("/User/Index");
                }
                else
                {
                    ErrorMessage = "Failed to update user.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}



//using Microsoft.AspNetCore.Mvc.RazorPages;
//using WebApp.model;

//namespace WebApp.Pages.User
//{
//	public class EditModel : PageModel
//	{
//		public Users user = new Users();
//		public string successMessage = string.Empty;
//		public string errorMessage = string.Empty;

//		private readonly IConfiguration configuration;
//		public EditModel(IConfiguration configuration)
//		{
//			this.configuration = configuration;
//		}

//		public void OnGet()
//		{
//			string id = Request.Query["id"];
//			try
//			{
//				if (int.TryParse(id, out int userId))
//				{
//					DAL dal = new DAL();
//					user = dal.GetUser(userId, configuration);
//				}
//				else
//				{
//					//
//				}
//			}
//			catch (Exception ex)
//			{
//				errorMessage = ex.Message;
//			}
//		}

//		public void OnPost()
//		{
//			if (int.TryParse(Request.Form["hiddenId"], out int userId))
//			{
//				user.ID = userId;
//			}
//			else
//			{
//				errorMessage = "Invalid user ID format";
//				return;
//			}

//			user.FirstName = Request.Form["FirstName"];
//			user.LastName = Request.Form["LastName"];

//			if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.LastName))
//			{
//				errorMessage = "All fields are required!";
//				return;
//			}

//			try
//			{
//				DAL dal = new DAL();
//				int rowsAffected = dal.UpdateUser(user, configuration);
//				if (rowsAffected > 0)
//				{
//					successMessage = "User has been updated";
//					user.FirstName = "";
//					user.LastName = "";
//				}
//				else
//				{
//					errorMessage = "Failed to update user";
//				}
//			}
//			catch (Exception ex)
//			{
//				errorMessage = ex.Message;
//			}

//			Response.Redirect("/User/Index");
//		}
//	}
//}
