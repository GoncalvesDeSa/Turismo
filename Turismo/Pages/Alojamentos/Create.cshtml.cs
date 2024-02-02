using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Turismo.Pages.Alojamentos;

namespace Turismo.Pages.Alojamentos
{
    public class CreateModel : PageModel
    {
        public AlojamentoInfo alojamentoInfo = new AlojamentoInfo();
        public string erro = "";
        public string sucesso = "";
        public void OnGet()
        {
        }

        public void OnPost()
        {
            alojamentoInfo.Nome = Request.Form["Nome"];
            alojamentoInfo.Morada = Request.Form["Morada"];
            alojamentoInfo.CP = Request.Form["CP"];
            alojamentoInfo.TipoAlojamento = Request.Form["TipoAlojamento"];



            //if (alojamentoInfo.Nome == null || alojamentoInfo.Morada == null || alojamentoInfo.CP == null ||
            //    alojamentoInfo.TipoAlojamento == null)

            if (alojamentoInfo.Nome.Length == 0 || alojamentoInfo.Morada.Length == 0 || alojamentoInfo.CP.Length == 0 ||
            alojamentoInfo.TipoAlojamento.Length == 0)
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
	                string sql = "INSERT INTO Alojamento (Nome, Morada, CP, TipoAlojiD) VALUES (@Nome, @Morada, @CP, @TipoAlojamento)";

	                using (SqlCommand command = new SqlCommand(sql, connection))
	                {
                        command.Parameters.AddWithValue("@Nome", alojamentoInfo.Nome);
                        command.Parameters.AddWithValue("@Morada", alojamentoInfo.Morada);
                        command.Parameters.AddWithValue("@CP", alojamentoInfo.CP);
                        command.Parameters.AddWithValue("@TipoAlojamento", alojamentoInfo.TipoAlojamento);
                        command.ExecuteNonQuery();
	                }

				}
            }
            catch (Exception ex)
            {
	            erro = ex.Message;
                return;
            }



            alojamentoInfo.Nome = ""; alojamentoInfo.Morada = ""; alojamentoInfo.CP = ""; alojamentoInfo.TipoAlojamento = "";
            sucesso = "Alojamento inserido com sucesso";

            Response.Redirect("/Alojamentos/Index");
		}	
    }
}
