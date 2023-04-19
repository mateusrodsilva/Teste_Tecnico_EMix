public class CepRepository{

    const string connectionString = "Data Source=LAPTOP-LQPG37D5\\SQLEXPRESS;Initial Catalog=CEP;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;"; 

    public async bool ConsultaCEPCadastrado(string cep){
        await using (var db = new SqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = $"Select cep From CEP WHERE cep = {cep}";
            var cepCadastrado = await db.QueryAsync<Cep>(query);

            if(cepCadastrado.cep == cep){
                return true;
            }else{
                return false;
            }
        }
    }

    public async List<Cep> ListaTodosCepsPorUf(string uf){
        await using (var db = new SqlConnection(connectionString))
        {
            await db.OpenAsync();
            var query = $"Select * From CEP WHERE cep = {cep}";
            var cepsUF = await db.QueryAsync<Cep>(query).ToList();

            return cepsUF;
        }
    }

    public async bool CadastraCEP(Cep novoCep){
         await using (var db = new SqlConnection(connectionString))
            {
                try
                {
                    await db.OpenAsync();
                    var query = @"Insert Into Cep Values(@cep,@logradouro,@complemento,@localidade,@uf,@unidade,@ibge,@gia)";
                    await db.ExecuteAsync(query, novoCep);

                    Console.WriteLine($"CEP {novoCep.cep} incluido com sucesso");
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
    }
}