using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Turismo.Pages.Alojamentos
{
	public class StoredProcedureModel : PageModel
	{


		public List<StoredInfo> ListaStored = new List<StoredInfo>();



		public void OnGet()
		{
			try
			{
				string connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;";
				
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					
					string sql = "exec SelectTodosAlojamentos2";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								StoredInfo stored = new StoredInfo();
								stored.Id = "" + reader.GetInt32(0); 
								stored.Nome = reader.GetString(1);
								stored.Morada = reader.GetString(2);
								stored.CP = "" + reader.GetInt32(3);
								stored.Comodidade = "" + reader.GetString(4);
								this.ListaStored.Add(stored);
							}
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
		}
	}


	public class StoredInfo
	{

		public string Id;
		public string Nome;
		public string Morada;
		public string CP;
		public string Comodidade;

	}


}