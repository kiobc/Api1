using System;
namespace curso
{
    class Program
    {


        static void Main(string[] args)
        {
            int anio = 2022;
            double dec = 5.5;
            string nombre = "Brayan";
            Console.WriteLine("Hola mundo"+" "+anio+" "+dec+" "+nombre);
    Console.WriteLine(nombre+"{0} {1} {2}", dec,anio,dec);
            Console.ReadLine();
        }
    }
}