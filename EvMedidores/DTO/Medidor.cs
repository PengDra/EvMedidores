using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvMedidores.DAL
{
    public class Medidor
    {
        private string idmedidor;

        public string Id { get => idmedidor; set => idmedidor = value; }

        public override string ToString()
        {
            return idmedidor;
        }
    }
}
