using System;
using System.Collections.Generic;
using System.Linq;
using TesteCandidato.Context;

namespace TesteCandidato.Repositories
{
    public class CepRepository
    {
        public bool ConsultaCEPCadastrado(string cep)
        {
            using (CEPEntities DB = new CEPEntities())
            {
                var cepCadastrado = DB.CEP.Select(x => x.cep1).FirstOrDefault(x => x == cep);

                if (cepCadastrado == cep)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public IEnumerable<CEP> ListaTodosCepsPorUf(string uf)
        {
            using (CEPEntities DB = new CEPEntities())
            {
                var cepsUF = DB.CEP.Where(x => x.uf == uf).ToList();
                return cepsUF;
            }
        }

        public bool CadastraCEP(CEP novoCep)
        {
            try
            {
                using (CEPEntities DB = new CEPEntities())
                {
                    DB.CEP.Add(novoCep);
                    DB.SaveChanges();

                    Console.WriteLine($"CEP {novoCep.cep1} incluido com sucesso");
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<string> ListaTodosUFsCadastradas()
        {

            using (CEPEntities DB = new CEPEntities())
            {
                var ufs = DB.CEP.AsQueryable().Select(x => x.uf).ToList();

                return ufs;
            }
        }
    }
}
