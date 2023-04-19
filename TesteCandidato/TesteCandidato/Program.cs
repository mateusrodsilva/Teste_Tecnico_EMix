using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Text;

namespace TesteCandidato
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            Bem vindo ao teste de Back-end da e.Mix!

            Abaixo está desenvolvido de uma forma bem simples e com alguns erros uma consulta de CEP.

            O que esperamos de você neste teste é que faça um novo projeto WEB da forma mais correta, segura e performática na sua avaliação com base no código abaixo.

            Entre os códigos você pode notar que existem observações "To Do" que também devem ser realizadas para que o teste esteja correto.
            Exemplo: "TODO: Criar banco de dados - LocalDB com o nome CEP"

            Observação: Você poderá utilizar qualquer tecnologia ou framework da sua preferência.

            */

            //TODO: Fazer um projeto WEB

            //TODO: Perguntar se o usuário quer consultar se logradouro existe na base
            Console.WriteLine("Olá! Seja bem vindo ao sistema de consultar CEP!");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Gostaria de consultar se o logradouro existe em nossa base de dados?\n Se quiser, digite SIM, senão SAIR");

            var respostaIniciarSistema = Console.ReadLine();

            if (respostaIniciarSistema.ToUpper() == "SAIR")
            {
                Console.WriteLine("Obrigado! Volte sempre!");
                return;
            }

            //TODO: Criar banco de dados - LocalDB com o nome CEP
            //TODO: Adicionar tabela conforme script abaixo
            //USE [CEP]
            //GO

            //SET ANSI_NULLS ON
            //GO

            //SET QUOTED_IDENTIFIER ON
            //GO

            //CREATE TABLE[dbo].[CEP] (
            //    [Id]          INT IDENTITY(1, 1) NOT NULL,
            //    [cep]         CHAR(9)       NULL,
            //    [logradouro] NVARCHAR(500) NULL,
            //    [complemento] NVARCHAR(500) NULL,
            //    [bairro] NVARCHAR(500) NULL,
            //    [localidade] NVARCHAR(500) NULL,
            //    [uf] CHAR(2)       NULL,
            //    [unidade] BIGINT NULL,
            //    [ibge]        INT NULL,
            //    [gia]         NVARCHAR(500) NULL
            //);

            Console.WriteLine("Digite um cep:");

            string cep = Console.ReadLine();
            cep = cep.Replace("-", string.Empty); //Para retirar hifen
            string result = string.Empty;

            do
            {
                if(result.Contains("erro") || cep.Length > 8){
                    Console.WriteLine("CEP Inválido - Verifique se digitou os números corretamente e o número de caracteres deve ser no máximo 8.");
                    System.Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("Digite o cep novamente:");

                    cep = Console.ReadLine();
                    cep = cep.Replace("-", string.Empty);
                }
                //TODO: Implementar forma de fazer o usuário poder errar várias vezes o CEP informado
                //TODO: Melhorar validação do CEP.
                //if (cep.Length > 8)
                //{
                //    Console.WriteLine("CEP Inválido");

                //    cep = Console.ReadLine();
                //    cep = cep.Replace("-", string.Empty);
                //}

                //Exemplo CEP 13050020
                string viaCEPUrl = $"https://viacep.com.br/ws/{cep}/json/";

                //TODO: Resolver dados com caracter especial no retorno do JSON 
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                result = client.DownloadString(viaCEPUrl);
            } while (result.Contains("\"erro\": true"));

            //TODO: Tratar CEP Inválido.

            JObject jsonRetorno = JsonConvert.DeserializeObject<JObject>(result);

            //Dados de conexão Banco de Dados
            string stringConexao = "Data Source=LAPTOP-LQPG37D5\\SQLEXPRESS;Initial Catalog=CEP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
            SqlConnection connection = new SqlConnection(stringConexao);         

            //TO DO Implementar Dapper
            string query = $"SELECT cep FROM CEP WHERE cep = {cep}";
            bool cepCadastrado = false;
            SqlCommand sqlCommand = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(ds, "Create DataView");
                adapter.Dispose();

                dv = ds.Tables[0].DefaultView;

                if(dv.Count > 0){
                    cepCadastrado = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }     

            if(cepCadastrado == true){
                Console.WriteLine("Já temos esse CEP cadastrado em nossa base!");
            }else{
                string desejaCadastrarCEP = string.Empty;
                do{
                    Console.WriteLine("Não temos esse CEP cadastrado em nossa base! \nDeseja cadastrar esse CEP? Se quiser digite SIM, se não NÃO");

                    desejaCadastrarCEP = Console.ReadLine();
                }while(desejaCadastrarCEP == string.Empty || desejaCadastrarCEP.ToUpper() != "SAIR")

                if(desejaCadastrarCEP.ToUpper() == "SAIR"){
                    Console.WriteLine("Encerrando...");
                    Console.ReadLine();
                    return;
                }
                
            }

            //TODO: Validar CEP existente
            query = "INSERT INTO [dbo].[CEP] ([cep], [logradouro], [complemento], [bairro], [localidade], [uf], [unidade], [ibge], [gia]) VALUES (";
            query = query + "'" + jsonRetorno["cep"] + "'";
            query = query + ",'" + jsonRetorno["logradouro"] + "'";
            query = query + ",'" + jsonRetorno["complemento"] + "'";
            query = query + ",'" + jsonRetorno["bairro"] + "'";
            query = query + ",'" + jsonRetorno["localidade"] + "'";
            query = query + ",'" + jsonRetorno["uf"] + "'";
            query = query + ",'" + jsonRetorno["unidade"] + "'";
            query = query + ",'" + jsonRetorno["ibge"] + "'";
            query = query + ",'" + jsonRetorno["gia"] + "'" + ")";

            

            sqlCommand.CommandType = CommandType.Text;

            try
            {
                connection.Open();

                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            sqlCommand.Dispose();
            connection.Close();
            connection.Dispose();

            ////TODO: Retornar os dados do CEP infomado no início para o usuário
            System.Console.WriteLine($"O CEP digitado remete ao endereço: \nCEP: {jsonRetorno["cep"]} \nLogradouro: {jsonRetorno["logradouro"]}\n" +
                $"Complemento: {jsonRetorno["complemento"]}\n" +
                $"Bairro: {jsonRetorno["bairro"]}\n" +
                $"Localidade: {jsonRetorno["localidade"]}\n" +
                $"UF: {jsonRetorno["uf"]} \n");

            
            string resposta = string.Empty;
            Console.WriteLine("Deseja visualizar todos os CEPs cadastrados em nossa base de dados de alguma UF? Se sim, informar UF, se não, informar sair.");
            do{
                if(resposta.ToUpper() > 2){
                    Console.WriteLine("UF inválida");
                    Console.WriteLine("Digite a UF novamente:");
                }else if()
                resposta = Console.ReadLine();
            }while(resposta.Length > 2)

            
            

            if (resposta == "sair")
            {
                Console.WriteLine("Encerrando...");
                Console.ReadLine();
                return;
            }

            if (resposta.Length > 2)
            {
                Console.WriteLine("UF inválida");
                resposta = Console.ReadLine();
            }

            if (resposta == "sair")
            {
                return;
            }


            //To Do Implementar Dapper
            connection = new SqlConnection(stringConexao);
            sqlCommand = new SqlCommand("Select * from CEP", connection);

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataView dv;
            sqlCommand.CommandType = CommandType.Text;

            try
            {
                connection.Open();
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(ds, "Create DataView");
                adapter.Dispose();

                dv = ds.Tables[0].DefaultView;

                for (int i = 0; i < dv.Count; i++)
                {
                    if (dv[i]["uf"].ToString() == resposta)
                    {
                        if (i == 0)
                        {
                            Console.Write(dv[i]["cep"]);
                        }
                        else
                        {
                            Console.Write(";" + dv[i]["cep"]);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Não temos CEPs dessa UF Cadastrados em nossa base! Para cadastrar, reinicie o sistema.");
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

            sqlCommand.Dispose();
            connection.Close();
            connection.Dispose();

            Console.ReadLine();
        }
    }
}
