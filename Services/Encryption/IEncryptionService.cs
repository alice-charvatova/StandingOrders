using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StandingOrders.API.Services.Encryption
{
    public interface IEncryptionService
    {
        string Encrypt(string dataToEncrypt);

        string Decrypt(string dataToDecrypt);
    }
}
