using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turismo.Pages.Comodidades;

namespace Turismo.Pages.Comodidades
{
    public class CreateModel : PageModel
    {
        public ComodidadeInfo comodidadeInfo = new ComodidadeInfo();
        public string erro = "";
        public string sucesso = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            comodidadeInfo.Id = Request.Form["Id"];
            comodidadeInfo.Nome = Request.Form["Nome"];


            if (comodidadeInfo.Id.Length == 0 || comodidadeInfo.Nome.Length == 0)
            {
				erro = "Preencha todos os campos";
				return;
			}

            //guardar na base de dados
            try
            {
	            String connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
	                connection.Open();
	                string sql = "INSERT INTO Comodidade(Comodidadeid, Nome) VALUES (@Id, @Nome)";

	                using (SqlCommand command = new SqlCommand(sql, connection))
	                {
                        command.Parameters.AddWithValue("@Id", comodidadeInfo.Id);
                        command.Parameters.AddWithValue("@Nome", comodidadeInfo.Nome);


                        command.ExecuteNonQuery();
	                }

				}
            }
            catch (Exception ex)
            {
	            erro = ex.Message;
                return;
            }



            comodidadeInfo.Id = ""; comodidadeInfo.Nome = "";
            sucesso = "Comodidade inserida com sucesso";

            Response.Redirect("/Comodidades/Index");
		}	
    }
}
