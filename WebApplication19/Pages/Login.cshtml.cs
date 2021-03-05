using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication19.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Login { get; set; }

        [BindProperty]
        public string Haslo { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Login))
            {
                ModelState.AddModelError("Login", "Podaj login !");
                return Page();
            }


            if (string.IsNullOrWhiteSpace(Haslo))
            {
                ModelState.AddModelError("Haslo", "Podaj has³o !");
                return Page();
            }

            if (CzyDobryLoginHaslo(Login, Haslo))
            {
                return RedirectToPage("./welcome");
            }
            else
            {
                ModelState.AddModelError("Haslo", "Z³y login lub has³o !");
                return Page();
            }
        }

        private bool CzyDobryLoginHaslo(string login, string haslo)
        {
            try
            {
                SqlConnection cnUsers = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=UsersSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

                string sSQL = "SELECT * FROM Uzytkownicy WHERE Login='" + login + "' AND Haslo='" + haslo + "'";
                SqlDataAdapter daUsers = new SqlDataAdapter(sSQL, cnUsers);
                DataSet dsUsers = new DataSet();
                daUsers.Fill(dsUsers);

                return (dsUsers.Tables[0].Rows.Count > 0);
            }
            catch
            {
                return false;
            }
        }
    }
}
