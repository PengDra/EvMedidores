using EvMedidores.DAL;
using EvMedidores.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lector.Comunicacion
{
    internal class HebraServidor
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        //Preguntar por la implementacion de esta linea........
        private static IMedidoresDAL listaMedidores = (IMedidoresDAL)ListaMedidoresDAL.GetInstancia();
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket servidor = new ServerSocket(puerto);
            Console.WriteLine("S: Lenvantando servidor en puerto {0}", puerto);
            if (servidor.Iniciar())
            {
                while (true)
                {
                    //Aqui podria empezar a abrir nuevas hebras con un for each por cada usuario
                    //deberia de crear un nuevo archivo en la carpeta comunicacion que contenga la ejecucion de cada una de las hebras por cliente que se conecte
                    Console.WriteLine("S: Espetando Cliente..");
                    Socket cliente = servidor.ObtenerCliente();
                    Console.WriteLine("S: cliente recibido");
                    ClienteCom clienteCom = new ClienteCom(cliente);
                    clienteCom.Escribir("Ingrese Id del Medidor");
                    string id = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese nombre:");
                    string fecha = clienteCom.Leer();
                    clienteCom.Escribir("Ingrese consumo:");
                    string consumo = clienteCom.Leer();
                    //Llenar año mes y dia con variables de tipo int en el objeto DateTime
                    //var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
                    //Console.WriteLine(date1.ToString());

                    Lectura lectura = new Lectura() 
                    {   
                        Idmedidor = id,
                        Fecha = fecha,
                        Consumo = consumo
                    };

                    //Antes de agregar cliente al txt deberia de consultar a la clase Medidor si el id de este existe dentro de la lista estatica
                    
                    

                    lecturasDAL.AgregarLectura(lectura);                  
                    clienteCom.Desconectar();
                }
            }
            else
            {
                Console.WriteLine("no se pudo levantar server en {0}", puerto);
            }


        }

    }
}
