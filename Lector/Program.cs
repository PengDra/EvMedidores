using EvMedidores.DAL;
using EvMedidores.DTO;
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
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        static bool Menu()
        {
            bool continuar = true;
            Console.WriteLine("Sistema de Lectura de Medidores");
            Console.WriteLine(" 1. Mostrar Lecturas \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1":
                    Mostrar();
                    break;
                case "0":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void Mostrar()
        {
            List<Lectura> lecturas = null;
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            foreach (Lectura lect in lecturas)
            {
                Console.WriteLine(lect);
            }

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Inicializando...");
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar));
            t.IsBackground = true;
            t.Start();
            bool a = true;

            while (Menu()) ;
    
            
        }
    }
}
