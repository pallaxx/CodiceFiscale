using System;
using CodiceFiscale.Models;

namespace CodiceFiscale
{
    class Program
    {
        static void Main(string[] args)
        {
            Persone Codici = new Persone();
            //Ricerca
            Console.WriteLine("Scrivere il valore che si vuole cercare");
            try
            {
                Codici.RicercaTotale(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Fine Ricerca

            Console.WriteLine("\n-------------");

            Codici.SalvataggioDatiinJson();

            Console.WriteLine(Codici);
        }
    }
}
