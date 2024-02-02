using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Turismo.Pages.Comodidades
{
    public class EditModel : PageModel
    {
	    public ComodidadeInfo comodidadeInfo = new ComodidadeInfo();
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
					string sql = "SELECT * FROM Comodidade WHERE Comodidadeid = @Id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Id", id);
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								comodidadeInfo.Id = reader.GetString(0);
								comodidadeInfo.Nome = reader.GetString(1);

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
			comodidadeInfo.Id = Request.Form["Id"];
			comodidadeInfo.Nome = Request.Form["Nome"];


			if (comodidadeInfo.Nome.Length == 0 )
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
					string sql = "UPDATE Comodidade SET Nome = @Nome WHERE Comodidadeid = @Id";

					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@Id", comodidadeInfo.Id);
						command.Parameters.AddWithValue("@Nome", comodidadeInfo.Nome);


						Console.WriteLine($"Id: {comodidadeInfo.Id}, Nome: {comodidadeInfo.Nome}");
						command.ExecuteNonQuery();
					}

				}
			}
			catch (Exception ex)
			{
				erro = ex.Message;
				return;
			}

			Response.Redirect("/Comodidades/Index");

		}
    }
}
