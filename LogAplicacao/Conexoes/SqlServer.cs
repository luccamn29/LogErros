using System.Data.SqlClient;
using System.IO;

namespace LogAplicacao.Conexoes
{
    public class SqlServer
    {
        private readonly SqlConnection _conexao;

        public SqlServer()
        {
            string stringConexao = File.ReadAllText(@"C:\Users\lucca\Dropbox\Curso Rumo Exercícios\Acesso SQL Log Erros.txt");
            _conexao = new SqlConnection(stringConexao);
        }

        public void CatalogarErro(Entidades.Erro erro)
        {
            try
            {
                _conexao.Open();

                string query = @"INSERT INTO LogAplicacao
                                (DataHora
                                ,MensagemErro
                                ,RastreioErro
                                ,NomeMaquina
                                ,NomeAplicacao
                                ,Usuario)

                                VALUES
                                (@DataHora
                                ,@MensagemErro
                                ,@RastreioErro
                                ,@NomeMaquina
                                ,@NomeAplicacao
                                ,@Usuario);";

                using (var cmd = new SqlCommand(query, _conexao))
                {
                    cmd.Parameters.AddWithValue("@DataHora", erro.DataHora);
                    cmd.Parameters.AddWithValue("@MensagemErro", erro.MensagemErro);
                    cmd.Parameters.AddWithValue("@RastreioErro", erro.RastreioErro);
                    cmd.Parameters.AddWithValue("@NomeMaquina", erro.NomeMaquina);
                    cmd.Parameters.AddWithValue("@NomeAplicacao", erro.NomeAplicacao);
                    cmd.Parameters.AddWithValue("@Usuario", erro.Usuario);

                    cmd.ExecuteNonQuery();
                }
            }

            finally
            {
                _conexao.Close();
            }
        }
    }
}
