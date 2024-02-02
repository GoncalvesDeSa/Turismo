using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turismo.Pages.Comodidades;

namespace Turismo.Pages.ComodAloj
{
    public class CreateModel : PageModel
    {
        public ComodAloj ComodAlojInfo = new ComodAloj();
        public string erro = "";
        public string sucesso = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
	        ComodAlojInfo.Comodid = Request.Form["ComodId"];
			ComodAlojInfo.Alojid = Request.Form["AlojId"];
 


            if (ComodAlojInfo.Comodid.Length == 0 || ComodAlojInfo.Alojid.Length == 0 )
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
	                string sql = "INSERT INTO Comodidade_Alojamento(Comodidadeid, Alojamentoid) VALUES (@ComodId, @AlojId)";

	                using (SqlCommand command = new SqlCommand(sql, connection))
	                {
		                command.Parameters.AddWithValue("@ComodId", ComodAlojInfo.Comodid);
						command.Parameters.AddWithValue("@AlojId", ComodAlojInfo.Alojid);

                        command.ExecuteNonQuery();
	                }

				}
            }
            catch (Exception ex)
            {
	            erro = ex.Message;
                return;
            }



	        ComodAlojInfo.Comodid = ""; ComodAlojInfo.Alojid = "";
			sucesso = "Comodidade inserida com sucesso no alojamento";

            Response.Redirect("/ComodAloj/Index");
		}	
    }
}
