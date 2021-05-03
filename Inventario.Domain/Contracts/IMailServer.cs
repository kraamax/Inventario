using System;
using System.Collections.Generic;
using System.Text;

namespace Inventario.Domain.Contracts
{
    public interface IMailServer
    {
        string Send(string message, string email);
    }
}
