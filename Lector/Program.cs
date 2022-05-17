using Lector.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando...");
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            //Falta probar servidor,Comprobar funcionamiento de la instancia de la linea 18 en HebraServidor
            //FALTA CONEXION MULTIPLE
            //FALTA VALIDAR MEDIDOR
            //FALTA CONSTRUIR CLIENTE
        }
    }
}
