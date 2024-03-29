﻿using Dapper;
using System.Data.SqlClient;
using Universidade.Domain.Entities;
using Universidade.Domain.Interfaces.Repository;

namespace Universidade.Infrastructure.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly SqlConnection _connection;

        public EnderecoRepository(IDapperDbConnection dapperDbConnection)
        {
            _connection = dapperDbConnection.CreateDbConnection() as SqlConnection;
        }

        public async Task<Endereco> FindAsync(int id)
        {
            var sql = "SELECT [Id], [Ativo], [DataDeCriacao], [DataDeAlteracao], [Rua], [Cidade], [Estado], [Cep], [Complemento] FROM [Enderecos] WHERE [Id]=@id";
            return await _connection.QueryFirstOrDefaultAsync<Endereco>(sql, new { id });
        }

        public async Task<IEnumerable<Endereco>> ListAsync()
        {
            var sql = "SELECT [Id], [Ativo], [DataDeCriacao], [DataDeAlteracao], [Rua], [Cidade], [Estado], [Cep], [Complemento] FROM [Enderecos]";
            return await _connection.QueryAsync<Endereco>(sql);
        }

        public async Task<int> AddAsync(Endereco endereco)
        {
            var sql = "INSERT INTO [Enderecos] VALUES(@Ativo, @DataDeCriacao, @DataDeAlteracao, @Rua, @Cidade, @Estado, @Cep, @Complemento);SELECT CAST(scope_identity() AS INT)";
            return await _connection.ExecuteScalarAsync<int>(sql, endereco);
        }

        public async Task AtualizarAsync(Endereco endereco, int id)
        {
            endereco.DataDeAlteracao = DateTime.Now;
            var sql = "UPDATE [Enderecos] SET [Ativo] = @Ativo,[DataDeAlteracao] = @DataDeAlteracao,[Rua] = @Rua,[Cidade] = @Cidade,[Estado] = @Estado,[Cep] = @Cep,[Complemento] = @Complemento WHERE Id = @id";
            await _connection.ExecuteAsync(sql, new {id, endereco.Ativo, endereco.DataDeAlteracao, endereco.Rua, endereco.Cidade, endereco.Estado, endereco.Cep, endereco.Complemento });
        }

        public async Task DeleteAsync(int id)
        {
            var sql = "DELETE FROM [Enderecos] WHERE [Id]=@id";
            await _connection.ExecuteAsync(sql, new { id });
        }

    }
}
