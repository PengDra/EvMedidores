using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvMedidores.DAL
{
    public class ListaMedidoresDAL : IMedidoresDAL
    {
        //para implementar Singleton:
        //1. El contructor tiene que ser private
        private ListaMedidoresDAL() { }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static ListaMedidoresDAL instancia;
        //3.Tener un metodo GetInstance, que devuelve una referencia al atributo
        public static ListaMedidoresDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new ListaMedidoresDAL();
            }
            return instancia;

        }
        public List<Medidor> ObtenerListaMedidores() {

            List<Medidor> lista = new List<Medidor>();
            lista.Add(new Medidor() { Id="1" });
            lista.Add(new Medidor() { Id ="2" });
            lista.Add(new Medidor() { Id ="3" });

            return lista;
        }



    }
}
