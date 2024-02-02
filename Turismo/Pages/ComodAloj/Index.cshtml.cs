using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Turismo.Pages.ComodAloj
{
    public class IndexModel : PageModel
    {
        public List<ComodAloj> ListaComodAloj = new List<ComodAloj>();



        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT TOP (1000) [Comodidadeid] ,[Alojamentoid] FROM [Turismo].[dbo].[Comodidade_Alojamento]";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ComodAloj comaloj = new ComodAloj();
                                    comaloj.Comodid = reader.GetString(0);
									comaloj.Alojid = "" + reader.GetInt32(1);
									ListaComodAloj.Add(comaloj);
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

    public class ComodAloj
    {
	    public string Comodid;
		public string Alojid;
        
    }



}
