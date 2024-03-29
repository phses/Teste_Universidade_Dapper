﻿

using Universidade.Domain.Entities;

namespace Universidade.Domain.Interfaces.Repository
{
    public interface IDepartamentoRepository : IBaseRepository<Departamento>
    {
        Departamento FindWithEndereco(int id);
        IEnumerable<Departamento> FindAllWithEndereco();
    }
}
