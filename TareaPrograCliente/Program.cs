using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Configuration;
using TareaPrograCliente.Comunicacion;

namespace TareaPrograCliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            string servidor = ConfigurationManager.AppSettings["servidor"];

            Console.ForegroundColor = ConsoleColor.Green;            
            Console.WriteLine("Conectando a servidor {0} en puerto {1}", servidor, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
            if (clienteSocket.Conectar())
            {
                //CLIENTE
                Console.WriteLine("Conectado con el servidor...");
                int cont = 0;

                while (cont == 0)
                {
                    Console.WriteLine("Recibiendo Mensaje");
                    string respuesta = clienteSocket.Leer();
                    if (respuesta == null)
                    {
                        Console.WriteLine("Has sido desconectado");
                        clienteSocket.Desconectar();
                    }
                    Console.WriteLine("R: {0}", respuesta);

                    string mensaje = Console.ReadLine().Trim();
                    if (mensaje == null | mensaje == "chao")
                    {
                        clienteSocket.Desconectar();
                        cont = 1;
                    }
                    clienteSocket.Escribir(mensaje);
                    Console.WriteLine("Enviando Datos...");
                }
            }
            else
            {
                Console.WriteLine("Error de comunicacion...");
            }
            Console.ReadKey();
        }
    }
}

 