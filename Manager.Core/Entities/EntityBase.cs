using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core.Entities;

public abstract class EntityBase
{
    public long Id { get; set; }

    // Lista de erros de validação de Domínio da Entidade
    internal List<string> _errors;
    public IReadOnlyCollection<string> Errors => _errors;
    public abstract bool Validate();
}

