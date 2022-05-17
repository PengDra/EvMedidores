using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvMedidores.DTO
{
    public class Lectura
    {
        private string idmedidor;
        private string fecha;
        private string consumo;

        public string Idmedidor { get => idmedidor; set => idmedidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string Consumo { get => consumo; set => consumo = value; }

        public override string ToString()
        {
            return idmedidor + " " + fecha + " " + consumo;
        }
    }
}
