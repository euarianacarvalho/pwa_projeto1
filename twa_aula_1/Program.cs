using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace twa_aula_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var StringConexao = "server=localhost:3306;uid=root;passord=;database=Teste";
            var conexao = new MySqlConnection(StringConexao);

            conexao.Open();

            var sql = "INSERT INTO Atleta (id, nome, altura, peso) VALUES (@id, @nome, @altura, @peso)";
            var cmd = new MySqlCommand(sql, conexao);



            conexao.Close();

        }
    }
}
