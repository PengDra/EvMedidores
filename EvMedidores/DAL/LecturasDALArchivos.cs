using EvMedidores.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvMedidores.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {

        //para implementar Singleton:
        //1. El contructor tiene que ser private
        private LecturasDALArchivos()
        {
        }
        //2. Debe poseer un atributo del mismo tipo de la clase y estatico
        private static LecturasDALArchivos instancia;
        //3. Tener un metodo GetInstance, que devuelve una referencia al atributo
        public static ILecturasDAL GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new LecturasDALArchivos();
            }
            return instancia;

        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/lecturas.txt";



        public void AgregarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(archivo, true))
                {
                    writer.WriteLine(lectura.Idmedidor + ";" + lectura.Fecha + ";" + lectura.Consumo);
                    writer.Flush();
                }
            }
            catch (Exception)
            {

            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lista = new List<Lectura>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Lectura lectura = new Lectura()
                            {
                                Idmedidor = arr[0],
                                Fecha = arr[1],
                                Consumo = arr[2]
                            };
                            lista.Add(lectura);
                        }
                    } while (texto != null);
                }
            }
            catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
