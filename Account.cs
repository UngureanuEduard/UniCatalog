using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCatalog
{
    internal class Account
    {
        public String User { get; set; }
        public String Password { get; set; }

        public int UserType{get; set; }
    }
}
