using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Turismo.Pages.Alojamentos
{
    public class IndexModel : PageModel
    {
        public List<AlojamentoInfo> ListaAlojamento = new List<AlojamentoInfo>();



        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-1HH2R8G\\AULAS;Initial Catalog=Turismo;Integrated Security=True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql =
						"SELECT TOP (1000) [Alojamentoid],[nome],[morada],[nomeTipo],[CP],[localidade] FROM [Turismo].[dbo].[Alojamento] join TipoAlojamento on TipoAlojamento.TipoAlojiD = Alojamento.TipoAlojiD join CP on CP.id = Alojamento.CP";
                        //string sql = "SELECT * FROM Alojamento";
                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    AlojamentoInfo alojamento = new AlojamentoInfo();
                                    alojamento.Id = ""+ reader.GetInt32(0);
                                    alojamento.Nome = reader.GetString(1);
                                    alojamento.Morada = reader.GetString(2);
                                    alojamento.TipoAlojamento = "" + reader.GetString(3);
                                    alojamento.CP = "" + reader.GetInt32(4);
                                    alojamento.Localidade = "" + reader.GetString(5);
									ListaAlojamento.Add(alojamento);
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

    public class AlojamentoInfo
    {

	    public string Id;
        public string Nome;
        public string Morada;
        public string CP;
        public string TipoAlojamento;
        public string Localidade;

        //public int Id { get; set; }
        //public string Nome { get; set; }
        //public string Morada { get; set; }
        //public string CP { get; set; }
        //public string TipoAlojamento { get; set; }
        //public string Email { get; set; }
        //public string Site { get; set; }
        //public string Tipo { get; set; }
        //public string Categoria { get; set; }
        //public string Descricao { get; set; }
        //public string Imagem { get; set; }
    }



}
