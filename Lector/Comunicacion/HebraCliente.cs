using EvMedidores.DAL;
using EvMedidores.DTO;
using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lector.Comunicacion
{
    class HebraCliente
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        private static IMedidoresDAL medidoresDAL = ListaMedidoresDAL.GetInstancia();

        private ClienteCom clienteCom;
        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }
        public void Ejecutar()
        {
            Console.WriteLine("Cliente Conectado");
            string id;
            string dia;
            string mes;
            string anho;
            string consumo;
            decimal consumodecimal;
            string fecha;
            bool valido = false;
            bool comp1=false;         
            bool comp2 = false;
            bool comp3 = false;
            bool comp4 = false;
            bool comp5 = false;
            int valor = 0;
            int cont = 0;
            List<Medidor> lista = medidoresDAL.ObtenerListaMedidores();
            while (cont==0)
            {
                do
                {
                    do
                    {
                        clienteCom.Escribir("Ingrese id de medidor");
                        id = clienteCom.Leer();
                        Console.WriteLine("Recibiendo id de medidor");
                        comp1 = Int32.TryParse(id, out int idinta);
                        if (comp1 == false)
                        {
                            Console.WriteLine("Formato de id erroneo");
                        }

                    }
                    while (comp1 == false);
                    int idint = Int32.Parse(id);

                    do
                    {

                        clienteCom.Escribir("Ingrese dia de lectura");
                        dia = clienteCom.Leer();
                        Console.WriteLine("Recibiendo fecha de lectura");
                        comp2 = Int32.TryParse(dia, out valor);
                        try
                        {

                            comp2 = valor <= 31;
                            Console.WriteLine(comp2);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Formato de Dia erroneo o valor mayor a 31");
                        }


                    }
                    while (comp2 == false);
                    int diaint = Int32.Parse(dia);
                    do
                    {
                        clienteCom.Escribir("Ingrese mes de lectura");
                        mes = clienteCom.Leer();
                        Console.WriteLine("Recibiendo fecha de lectura");
                        comp3 = Int32.TryParse(mes, out valor);
                        try
                        {

                            comp3 = valor <= 12;
                            Console.WriteLine(comp2);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Formato de mes erroneo o valor mayor a 12");
                        }
                    }
                    while (comp3 == false);
                    int mesint = Int32.Parse(mes);

                    do
                    {
                        clienteCom.Escribir("Ingrese año de lectura");
                        anho = clienteCom.Leer();
                        Console.WriteLine("Recibiendo fecha de lectura");
                        comp4 = Int32.TryParse(anho, out valor);
                        try
                        {

                            comp4 = valor >= 1995;
                            Console.WriteLine(comp4);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Formato de año erroneo o valor menor a 1995");
                        }

                    } while (comp4 == false);
                    int anhoint = Int32.Parse(anho);

                    do
                    {
                        clienteCom.Escribir("Ingrese consumo de lectura");
                        consumo = clienteCom.Leer();
                        Console.WriteLine("Recibiendo consumo de lectura");
                        comp5 = decimal.TryParse(consumo, out consumodecimal);
                        if (comp5 == false)
                        {
                            Console.WriteLine("Formato de Consumo erroneo");
                        }
                    } while (comp5 == false);



                    if (lista.Any(x => x.Id == id))
                    {
                        valido = true;
                        //Llenar año mes y dia con variables de tipo int en el objeto DateTime
                        //var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
                        //Console.WriteLine(date1.ToString());
                        fecha = new DateTime(anhoint, mesint, diaint).ToString();
                        consumodecimal = Decimal.Parse(consumo);
                        Lectura lectura = new Lectura()
                        {
                            Idmedidor = id,
                            Fecha = fecha,
                            Consumo = consumo
                        };


                        lock (lecturasDAL)
                        {
                            lecturasDAL.AgregarLectura(lectura);
                        }
                        Console.WriteLine("Registro creado satisfactoriamente, desconectando cliente");

                        clienteCom.Desconectar();
                        valido = true;
                        cont = 1;

                    }
                    else
                    {
                        Console.WriteLine("Id no valido, desconectando cliente");
                        clienteCom.Desconectar();
                        valido = true;                        
                        cont = 1;

                    }
                } while (valido == false);

            }
           
        }

    }
}
