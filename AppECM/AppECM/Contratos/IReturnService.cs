using System;

namespace AppECM.Contratos
{
    public interface IReturnService
    {
        bool Error { get; set; }
        string Message { get; set; }
    }
}
