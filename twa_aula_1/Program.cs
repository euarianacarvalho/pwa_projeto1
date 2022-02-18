using System;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace twa_aula_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var StringConexao = "server=localhost;uid=root;password=aluno;database=teste";
            var conexao = new MySqlConnection(StringConexao);

            conexao.Open();

            var sql = "INSERT INTO atleta (id, nome, altura, peso) VALUES (@id, @nome, @altura, @peso)";
            var cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", "1");
            cmd.Parameters.AddWithValue("@nome", "Ana");
            cmd.Parameters.AddWithValue("@altura", 1.7);
            cmd.Parameters.AddWithValue("@peso", 70);

            cmd.ExecuteNonQuery();

            conexao.Close();

        }
    }
}
