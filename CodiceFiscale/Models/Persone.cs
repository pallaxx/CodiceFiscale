using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodiceFiscale.Models
{
    class Persone
    {
        List<Fiscale> ListaCodiciFiscali = new List<Fiscale>();
        public string[,] riga = new string[5,5];
        public Persone() 
        {
            string path = @"Models/CodiciPersone.csv";
            StreamReader sr = new StreamReader(path);
            string[] colonna = new string[5];
            int counter = 0;

            while ((colonna[counter] = sr.ReadLine()) != null)//in modo che ogni riga ha un numero e il numero e' il rispettivo studente
                counter++;

            sr.Close();

            for (int i = 0; i < counter; i++)
            {
                string[] righe = colonna[i].Split(',');
                InserisciPersona(righe[0], righe[1], righe[2], righe[3], righe[4]);
                for (int j = 0; j < righe.Length; j++)
                {
                    riga[i, j] =righe[j].ToLower();
                }
            }
        }

        public void InserisciPersona(string n, string c, string s, string comune, string data)
        {
            ListaCodiciFiscali.Add(new Fiscale(n, c, s, comune, data));
        }

        public override string ToString()
        {
            Console.WriteLine("Lista Codici Fiscali:\n");

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ListaCodiciFiscali.Count; i++)
                sb.AppendLine(ListaCodiciFiscali[i].ToString());

            return sb.ToString();
        }

        public void RicercaTotale(string valore)
        {
            List<Fiscale> ListaCodiciFiscaliTrovati = new List<Fiscale>();
            string[] arr = new string[5];
            valore = valore.ToLower();
            string eil = "";
            for (int i = 0; i < ListaCodiciFiscali.Count; i++)
            {
                if (ListaCodiciFiscali[i].Nome.ToLower() == valore)
                {
                    eil = "Nome";
                    ListaCodiciFiscaliTrovati.Add(new Fiscale(riga[i, 0], riga[i, 1], riga[i, 2], riga[i, 3], riga[i, 4]));
                }
                else if (ListaCodiciFiscali[i].Cognome.ToLower() == valore)
                {
                    eil = "Cognome";
                    ListaCodiciFiscaliTrovati.Add(new Fiscale(riga[i, 0], riga[i, 1], riga[i, 2], riga[i, 3], riga[i, 4]));
                }
                else if (ListaCodiciFiscali[i].Sesso.ToLower() == valore)
                {
                    eil = "Sesso";
                    ListaCodiciFiscaliTrovati.Add(new Fiscale(riga[i, 0], riga[i, 1], riga[i, 2], riga[i, 3], riga[i, 4]));
                }
                else if (ListaCodiciFiscali[i].LuogoDiNascita.ToLower() == valore)
                {
                    eil = "Luogo di nascita";
                    ListaCodiciFiscaliTrovati.Add(new Fiscale(riga[i, 0], riga[i, 1], riga[i, 2], riga[i, 3], riga[i, 4]));
                }
                else if (ListaCodiciFiscali[i].DataDiNascita == valore)
                {
                    eil = "Data di nascita";
                    ListaCodiciFiscaliTrovati.Add(new Fiscale(riga[i, 0], riga[i, 1], riga[i, 2], riga[i, 3], riga[i, 4]));
                }
            }

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ListaCodiciFiscaliTrovati.Count; i++)
                Console.WriteLine("e' il {0} di {1}", eil, ListaCodiciFiscaliTrovati[i].ToString());

            if (eil == "")
                throw new Exception("Il valore inserito non e' stato trovato!");
        }

        public void SalvataggioDatiinJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"\t\tLISTA Codici Fiscali\t{DateTime.Today:dd/MM/yyyy}");
            for (int i = 0; i < ListaCodiciFiscali.Count; i++)
                sb.AppendLine(ListaCodiciFiscali[i].ToString());

            File.AppendAllText("ListaCodiciFiscali.Json", sb.ToString());
        }
    }
}
