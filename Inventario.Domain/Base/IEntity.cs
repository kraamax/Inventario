using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.Base
{
    public interface IEntity<out T>
    {
        T Id { get; }
    }
}
