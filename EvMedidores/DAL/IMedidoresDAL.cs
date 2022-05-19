using EvMedidores.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvMedidores.DAL
{
    public interface IMedidoresDAL
    {
        List<Medidor> ObtenerListaMedidores();
    }
}
