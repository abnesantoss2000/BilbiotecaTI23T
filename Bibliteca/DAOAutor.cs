using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Importando os comanado de ocnexão com o bnaco

namespace Bibliteca
{
    class DAOAutor
    {
        public MySqlConnection conexao;//Criando a variável que representa o banco
        public string dados;
        public string comando;
        public int[] codigo;
        public string[] nome;
        public string[] genero;
        public string[] endereco;
        public int i;
        public int contar;
        public string msg;
        public DAOAutor()
        {
            //Conxão com o banco de dados
            this.conexao = new MySqlConnection("server=localhost;DataBase=registro;Uid=root;Password=;Convert Zero DateTime=True");
            try
            {
                this.conexao.Open();//Abrindo a conexão
                Console.WriteLine("Conectado com sucesso!");
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Algo deu errado!\n\n {erro}");
                this.conexao.Close();
            }

        }//Inserir o dado no banco

        public void Inserir(string nome, string genero, string endereco)
        {
            try
            {
                this.dados = $"('','{nome}','{genero}','{endereco}')";
                this.comando = $"Insert into autor(codigo, nome, genero, endereco) values{this.dados}";
                //Inserir o cmando no banco
                MySqlCommand sql = new MySqlCommand(this.comando, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Executo o comando
                Console.WriteLine($"Inserido com sucesso! \n\n{resultado}");
            }
            catch(Exception erro)
            {
                Console.WriteLine($"Algo deu errado\n\n {erro}");
            }//fim do catch
        }//fim do método

        //Preencher Vetor --> Coletar os dados do banco o preenher o vetor

        public void PreencherVetor()
        {
            string query = "select * from autor";//Buscnado todos os dados da tabela autor
            //Instanciar os vetores
            this.codigo = new int[100];
            this.nome = new string[100];
            this.genero = new string[100];
            this.endereco = new string[100];

            //Preenchar os vetores com valores padrões
            for(i = 0; i < 100; i++)
            {
                this.codigo[i] = 0;
                this.nome[i] = "";
                this.genero[i] = "";
                this.endereco[i] = "";
            }//fim do for

            //Executar o comando do SQL
            MySqlCommand coletar = new MySqlCommand(query, this.conexao);

            //Leitura do dado no bnaco
            MySqlDataReader leitura = coletar.ExecuteReader();//Percoorre o banco e traz os dados

            //Zerar o contador
            i = 0;
            while (leitura.Read())
            {
                this.codigo[i] = Convert.ToInt32(leitura["codigo"]);
                this.nome[i] = leitura["nome"] + "";
                this.genero[i] = leitura["genero"] + "";
                this.endereco[i] = leitura["endereco"] + "";
                i++;
                this.contar++;//Informer o processo de busca
            }//fim do while

            leitura.Close();//Encerrando o processo de busca
        }//fim do método

        public string ConsultarTudo()   
        {
            PreencherVetor();//Preencher todos os dados do betor
            this.msg = "";
            for(i = 0; i < this.contar; i++)
            {
                this.msg += $"\nCódigo; {this.codigo[i]} " +
                            $"\nNome: {this.nome[i]}" +
                            $"\nGênero: {this.genero[i]}" +
                            $"\nEndereço: {this.endereco[i]}\n\n";

            }
            return this.msg;
        }//fim so método

        public string ConsultarPorCodigo(int codigo)
        {
            PreencherVetor();//Preencher todos os dados do betor
            this.msg = "";
            for (i = 0; i < this.contar; i++)
            {
                this.msg += $"\nCódigo; {this.codigo[i]} " +
                            $"\nNome: {this.nome[i]}" +
                            $"\nGênero: {this.genero[i]}" +
                            $"\nEndereço: {this.endereco[i]}\n\n";
                return this.msg;
            }//fim do if
            return "Código informado não existe!";
        }//fim so método

        public string Atualizar(int codigo, string campo, string novoDado)
        {
            try
            {
                string query = $"updare autor set {campo} = '{novoDado}' where codigo = '{codigo}'";
                //Executar o camando
                MySqlCommand sql = new MySqlCommand(query, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();///Comando de isnerção no banco
                return$"Arualizado com sucesso!\n\n {resultado}";
            }
            catch(Exception erro)
            {
                return $"Algo deu errado!\n\n {erro}";
            }//fim do catch
        }//fim do atualizar

        public string Deletar(int codigo)
        {
            try
            {
                String query = $"delete from autor where codigo = '{codigo}'";
                //Executar o camando
                MySqlCommand sql = new MySqlCommand(query, this.conexao);
                string resultado = "" + sql.ExecuteNonQuery();//Comando de isnerção no banco
                return $"Deletado com sucsso\n\n {resultado}";
            }
            catch (Exception erro)
            {
                return $"Algo deu errado!\n\n {erro}";
            }//fim do catch
        }












    }//fim da classe
}//fim do projeto
