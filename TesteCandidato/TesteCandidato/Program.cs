using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Text;
using TesteCandidato.Context;
using TesteCandidato.Repositories;

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

            if (respostaIniciarSistema.ToUpper() != "SIM" && respostaIniciarSistema.ToUpper() != "SAIR")
            {
                do
                {
                    Console.WriteLine("Resposta inválida \nGostaria de consultar se o logradouro existe em nossa base de dados?\n Se quiser, digite SIM, senão SAIR");
                    respostaIniciarSistema = Console.ReadLine();
                } while (respostaIniciarSistema.ToUpper() != "SIM" && respostaIniciarSistema.ToUpper() != "SAIR");
            }

            if (respostaIniciarSistema.ToUpper() == "SAIR")
            {
                Console.WriteLine("Obrigado! Volte sempre!");
                Console.ReadLine();
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
            //    [Id]          INT  IDENTITY(1, 1) NOT NULL,
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
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Digite um cep (SEM O TRAÇO):");

            string cep = Console.ReadLine();
            string result = string.Empty;

            do
            {
                if (result.Contains("erro") || cep.Length > 8)
                {
                    Console.WriteLine("CEP Inválido - Verifique se digitou os números corretamente e o número máximo de caracteres deve ser 8 (sem o traço).");
                    System.Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine("Digite o cep novamente:");

                    cep = Console.ReadLine();
                }
                //TODO: Implementar forma de fazer o usuário poder errar várias vezes o CEP informado
                //TODO: Melhorar validação do CEP.

                //Exemplo CEP 13050020
                string viaCEPUrl = $"https://viacep.com.br/ws/{cep.Replace("-", string.Empty)}/json/";

                //TODO: Resolver dados com caracter especial no retorno do JSON 
                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;
                result = client.DownloadString(viaCEPUrl);
            } while (result.Contains("\"erro\": true"));

            //TODO: Tratar CEP Inválido.
            JObject jsonRetorno = JsonConvert.DeserializeObject<JObject>(result);


            var cepRepository = new CepRepository();
            bool cepCadastrado = cepRepository.ConsultaCEPCadastrado(jsonRetorno["cep"].ToString());

            if (cepCadastrado == true)
            {
                Console.WriteLine("Já temos esse CEP cadastrado em nossa base!");

                ////TODO: Retornar os dados do CEP infomado no início para o usuário
                Console.WriteLine($"O CEP digitado remete ao endereço: \nCEP: {jsonRetorno["cep"]} \nLogradouro: {jsonRetorno["logradouro"]}\n" +
                    $"Complemento: {jsonRetorno["complemento"]}\n" +
                    $"Bairro: {jsonRetorno["bairro"]}\n" +
                    $"Localidade: {jsonRetorno["localidade"]}\n" +
                    $"UF: {jsonRetorno["uf"]} \n");
            }
            else
            {
                string desejaCadastrarCEP = string.Empty;
                ////TODO: Retornar os dados do CEP infomado no início para o usuário
                Console.WriteLine($"O CEP digitado remete ao endereço: \nCEP: {jsonRetorno["cep"]} \nLogradouro: {jsonRetorno["logradouro"]}\n" +
                    $"Complemento: {jsonRetorno["complemento"]}\n" +
                    $"Bairro: {jsonRetorno["bairro"]}\n" +
                    $"Localidade: {jsonRetorno["localidade"]}\n" +
                    $"UF: {jsonRetorno["uf"]} \n");
                Console.WriteLine("Não temos esse CEP cadastrado em nossa base! \nDeseja cadastrar esse CEP? Se quiser digite SIM, se não SAIR");
                desejaCadastrarCEP = Console.ReadLine();

                if (desejaCadastrarCEP.ToUpper() != "SIM" && desejaCadastrarCEP.ToUpper() != "SAIR")
                {
                    do
                    {
                        Console.WriteLine("Resposta inválida");
                        Console.WriteLine("Não temos esse CEP cadastrado em nossa base! \nDeseja cadastrar esse CEP? Se quiser digite SIM, se não NÃO");

                        desejaCadastrarCEP = Console.ReadLine();
                    } while (desejaCadastrarCEP.ToUpper() != "SIM" && desejaCadastrarCEP.ToUpper() != "SAIR");
                }


                if (desejaCadastrarCEP.ToUpper() == "SAIR")
                {
                    Console.WriteLine("Encerrando...");
                    Console.ReadLine();
                    return;
                }

                //TODO: Validar CEP existente
                CEP novoCep = new CEP();


                novoCep.cep1 = jsonRetorno["cep"].ToString();
                novoCep.logradouro = jsonRetorno["logradouro"].ToString();
                novoCep.complemento = jsonRetorno["complemento"].ToString();
                novoCep.localidade = jsonRetorno["localidade"].ToString();
                novoCep.uf = jsonRetorno["uf"].ToString();
                novoCep.gia = jsonRetorno["gia"].ToString();
                novoCep.ibge = int.Parse(jsonRetorno["ibge"].ToString());


                cepRepository.CadastraCEP(novoCep);

            }

            

            var ufs = cepRepository.ListaTodosUFsCadastradas();

            string resposta = string.Empty;
            Console.WriteLine("Deseja visualizar todos os CEPs cadastrados em nossa base de dados de alguma UF? Se sim, digite SIM, se não, informar SAIR.");

            resposta = Console.ReadLine();


            if (resposta.ToUpper() != "SIM" && resposta.ToUpper() != "SAIR")
            {
                do
                {
                    Console.WriteLine("Resposta inválida");
                    Console.WriteLine("Deseja visualizar todos os CEPs cadastrados em nossa base de dados de alguma UF? Se sim, digite SIM, se não, informar SAIR.");
                } while (resposta.ToUpper() != "SIM" && resposta.ToUpper() != "SAIR");
            }

            if (resposta.ToUpper() == "SIM")
            {
                Console.WriteLine("Estas são as UFs que temos cadastradas em nossa base de dados, escola uma");
                foreach (var item in ufs)
                {
                    Console.WriteLine(item.ToString());
                }

                string ufEscolhida = Console.ReadLine();

                if (ufEscolhida.Length > 2 || !ufs.Contains(ufEscolhida.ToUpper()))
                {
                    do
                    {
                        Console.WriteLine("UF inválida");
                        Console.WriteLine("Digite a UF novamente:");
                        ufEscolhida = Console.ReadLine();
                    } while (ufEscolhida.Length > 2 || ufEscolhida == string.Empty || !ufs.Contains(ufEscolhida.ToLower()));
                }

                var cepsUF = cepRepository.ListaTodosCepsPorUf(ufEscolhida);

                foreach (var item in cepsUF)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else if (resposta.ToUpper() == "SAIR")
            {
                Console.WriteLine("Encerrando...");
                Console.ReadLine();
                return;
            }

            Console.ReadLine();
        }
    }
}
