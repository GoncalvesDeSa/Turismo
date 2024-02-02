using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Turismo.Pages.Alojamentos
{
    public class EditModel : PageModel
    {
	    public AlojamentoInfo alojamentoInfo = new AlojamentoInfo();
	    public string erro = "";
	    public string sucesso = "";

		public void OnGet()
        {
			String id = Request.Query["id"];

			try
			{
				String connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM Alojamento WHERE Alojamentoid = @Id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								alojamentoInfo.Id = "" + reader.GetInt32(0);
								alojamentoInfo.Nome = reader.GetString(1);
								alojamentoInfo.Morada = reader.GetString(2);
								alojamentoInfo.TipoAlojamento = "" + reader.GetInt32(3);
								alojamentoInfo.CP = "" + reader.GetInt32(4);
							}
						}
					}
				}

			}
			catch (Exception ex)
			{
				erro = ex.Message;
			}

        }

		public void OnPost()
		{
			alojamentoInfo.Id = Request.Form["Id"];
			alojamentoInfo.Nome = Request.Form["Nome"];
			alojamentoInfo.Morada = Request.Form["Morada"];
			alojamentoInfo.CP = Request.Form["CP"];
			alojamentoInfo.TipoAlojamento = Request.Form["TipoAlojamento"];

			if (alojamentoInfo.Nome.Length == 0 || alojamentoInfo.Morada.Length == 0 || alojamentoInfo.CP.Length == 0 ||
			    alojamentoInfo.TipoAlojamento.Length == 0)
			{
				erro = "Preencha todos os campos";
				return;
			}

			//guardar na base de dados
			try
			{
				string connectionString =  "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "UPDATE Alojamento SET Nome = @Nome, Morada = @Morada, CP = @CP, TipoAlojiD = @TipoAlojamento WHERE Alojamentoid = @Id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Id", alojamentoInfo.Id);
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

			Response.Redirect("/Alojamentos/Index");

		}
    }
}
