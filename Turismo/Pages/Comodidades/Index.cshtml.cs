using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Turismo.Pages.Comodidades
{
    public class IndexModel : PageModel
    {
        public List<ComodidadeInfo> ListaComodidade = new List<ComodidadeInfo>();



        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT TOP (1000) [Comodidadeid],[nome] FROM [Turismo].[dbo].[Comodidade]";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ComodidadeInfo comodidade = new ComodidadeInfo();
                                    comodidade.Id = reader.GetString(0);
									comodidade.Nome = reader.GetString(1);
									ListaComodidade.Add(comodidade);
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

    public class ComodidadeInfo
    {

	    public string Id;
        public string Nome;
    }



}
