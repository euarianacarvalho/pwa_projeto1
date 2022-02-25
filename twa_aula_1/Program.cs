using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using twa_aula_1.Models;
using Dapper;

namespace twa_aula_1
{
    class Program
    {
        static void Main(string[] args)
        { 
            var StringConexao = "server=localhost;uid=root;password=aluno;database=teste";
            var conexao = new MySqlConnection(StringConexao);

            conexao.Open();

            //Inserir (conexao);

            //Selecionar(conexao);

            //InserirComDapper(conexao);

            SelecionarComDapper(conexao);

            conexao.Close();

        }

        private static void SelecionarComDapper(MySqlConnection conexao)
        {
            Console.Write("Informe a parte do nome do Atleta a ser selecionado: ");
            var parteNome = Console.ReadLine();

            
            var sql = "select * from atleta where nome like @parteNome order by nome";
            //var sql = "select * from atleta order by nome";
            
            var lista = conexao.Query<Atleta>(sql, new { ParteNome = "%" + parteNome + "%"});

            Imprimir(lista);
        }

        private static void InserirComDapper(MySqlConnection conexao)
        {
            var sql = "INSERT INTO atleta (id, nome, altura, peso) VALUES (@id, @nome, @altura, @peso)";

            var obj = LeAtleta();

            conexao.Execute(sql, obj);
        }

        private static void Selecionar(MySqlConnection conexao)
        {
            var sql = "select * from atleta order by nome";

            var cmd = new MySqlCommand(sql, conexao);

            var da = new MySqlDataAdapter(cmd);

            var regs = new DataTable();

            da.Fill(regs);

            var lista = new List<Atleta>();

            foreach (DataRow reg in regs.Rows)
            {
                lista.Add(
                    new Atleta
                    {
                        Id = reg["id"].ToString(),
                        Nome = reg["nome"].ToString(),
                        Altura = Convert.ToDouble(reg["altura"]),
                        Peso = Convert.ToDouble(reg["peso"])
                    }
                    );
            }
            Imprimir(lista);
        }

        private static void Imprimir(IEnumerable<Atleta> lista)
        {
            foreach(var obj in lista)
            {
                Console.WriteLine($"{obj.Nome}, altura: {obj.Altura:N2}, peso: {obj.Peso:N2}");
            }
        }

        private static void Inserir (MySqlConnection conexao)
        {

            var sql = "INSERT INTO atleta (id, nome, altura, peso) VALUES (@id, @nome, @altura, @peso)";

            var obj = LeAtleta();

            var cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", obj.Id);
            cmd.Parameters.AddWithValue("@nome", obj.Nome);
            cmd.Parameters.AddWithValue("@altura", obj.Altura);
            cmd.Parameters.AddWithValue("@peso", obj.Peso);

            cmd.ExecuteNonQuery();
        }

        private static Atleta LeAtleta()
        {
            var obj = new Atleta();

            obj.Id = Guid.NewGuid().ToString();
            Console.Write("Atleta: ");
            Console.Write(" - Nome: ");
            obj.Nome = Console.ReadLine();
            Console.Write(" - Altura: ");
            obj.Altura = Convert.ToDouble(Console.ReadLine());
            Console.Write(" - Peso: ");
            obj.Peso = Convert.ToDouble(Console.ReadLine());

            return obj;
        }
    }
}
