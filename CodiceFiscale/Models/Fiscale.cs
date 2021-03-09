using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodiceFiscale.Models
{
    class Fiscale
    {

        char[] vocali = { 'a', 'e', 'i', 'o', 'u' };
        char[] consonanti = { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'x', 'y', 'w', 'z' };
        char[] mesi = { 'a','b','c','d','e','h','l','m','p', 'r','s','t'};

        //Vettori paralleli per il calcolo del controllo del codice fiscale (l'ultima cifra)
        int[] valoripari = {    0,  1,  2,  3,  4,  5,  6,  7,  8,  9,  0,  1,  2,  3,  4,  5,  6,  7,  8,  9,  10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
        int[] valoridispari = { 1,  0,  5,  7,  9,  13, 15, 17, 19, 21, 1,  0,  5,  7,  9,  13, 15, 17, 19, 21, 2,  4,  18, 20, 11, 3,  6,  8,  12, 14, 16, 10, 22, 25, 24, 23};
        char[] parallelo = {   '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z' };

        char[] Alfabeto = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };


        string _nome;
        public string Nome { get => _nome;}

        string _cognome;
        public string Cognome { get => _cognome;}

        string _sesso;
        public string Sesso { get => _sesso;}

        string _luogoDiNascita;
        public string LuogoDiNascita { get => _luogoDiNascita;}

        string _dataDiNascita;
        public string DataDiNascita { get => _dataDiNascita;}

        public Fiscale( string n, string c, string s, string l, string d) 
        {
            _nome = n;
            _cognome = c;
            _sesso = s;
            _luogoDiNascita = l;
            _dataDiNascita = d;
        }
        
        private string CalcoloCognome()
        {
            _cognome = _cognome.ToLower();
            char[] c = new char[_cognome.Length];
            c = _cognome.ToCharArray();

            int countconsonanti = 0;
            string codicecognome = "";
            int rompi = 0;

            if (_cognome.Length >= 3)
            {
                for (int i = 0; i < _cognome.Length; i++) //Per prima cosa, si contano quante consananti il cognome ha
                {
                    for (int j = 0; j < vocali.Length; j++)
                    {
                        if (c[i] != vocali[j])
                        {
                            countconsonanti++;
                        }
                    }
                }
                if (countconsonanti >= 3) //iniziano i vari calcoli per creare il codice del cognome
                {
                    codicecognome = CicliCognomeNome( codicecognome, rompi, c, _cognome.Length, 3, consonanti);
                }
                else
                {
                    codicecognome = CicliCognomeNome(codicecognome, rompi, c, _cognome.Length,countconsonanti, consonanti);
                    codicecognome = CicliCognomeNome(codicecognome, rompi, c, vocali.Length, 3, vocali);
                }
            }
            else
            {
                if (_cognome.Length == 1)
                    codicecognome = _cognome + "X" + "X";
                else
                    codicecognome = _cognome + "X";
            }
            return codicecognome = codicecognome.ToUpper();
        }

        private string CicliCognomeNome(string codicecognome, int rompi, char[] c, int lunghezza, int countrompi, char[] vettore) 
        {
            for (int i = 0; i < lunghezza && rompi < countrompi; i++)
            {
                for (int j = 0; j < vettore.Length; j++)
                {
                    if (c[i] == vettore[j])
                    {
                        codicecognome += c[i];
                        rompi++;
                    }
                }
            }
            return codicecognome;
        }

        private string CalcoloNome()
        {
            _nome = _nome.ToLower();
            char[] c = new char[_nome.Length];
            c = _nome.ToCharArray();

            int countconsonanti = 0;
            int rompi = 0;
            string codicenome = "";

            for (int i = 0; i < _nome.Length; i++)
            {
                for (int j = 0; j < consonanti.Length; j++)
                {
                    if (c[i] == consonanti[j])
                    {
                        countconsonanti++;
                    }
                }
            }
            if (_nome.Length>=3)
            {

                if (countconsonanti >= 3)
                {
                    if (countconsonanti >= 4)
                    {
                        for (int i = 0; i < _nome.Length && rompi < 4; i++)
                        {
                            for (int j = 0; j < consonanti.Length; j++)
                            {
                                if (c[i] == consonanti[j])
                                {
                                    if (rompi == 0)
                                    {
                                        codicenome += c[i];
                                    }
                                    else if (rompi == 2)
                                    {
                                        codicenome += c[i];
                                    }
                                    else if (rompi == 3)
                                    {
                                        codicenome += c[i];
                                    }
                                    rompi++;
                                }
                            }
                        }
                    }
                    else
                    {
                        codicenome = CicliCognomeNome(codicenome, rompi, c, _nome.Length, countconsonanti, consonanti);
                    }
                }
                else 
                {
                    codicenome = CicliCognomeNome(codicenome, rompi, c, _nome.Length, countconsonanti, consonanti);
                    codicenome = CicliCognomeNome(codicenome, rompi, c, vocali.Length, 1, vocali);
                }
            }
            else
            {
                if (_nome.Length == 1)
                    codicenome = _nome + "X" + "X";
                else
                    codicenome = _nome + "X";
            }
            return codicenome;
        }

        private string CalcoloDataDiNascita()
        {
            string[] s = _dataDiNascita.Split('/');
            int numeromese = Convert.ToInt32(s[1]); //mese
            char[] anno = new char[s[2].Length]; //mese
            anno = s[2].ToCharArray(); //anno
            string ritorna=""; //stringa da ritornare

            ritorna += anno[anno.Length-2]+"" + ""+ anno[anno.Length-1]+ "" + mesi[numeromese - 1];
            return ritorna;
        }

        private string CalcoloGiornoeSesso()
        {
            _sesso = _sesso.ToLower();
            string[] s = _dataDiNascita.Split('/');
            int numerogiorno = Convert.ToInt32(s[0]); //giorno
            string ritorna = "";
            if (_sesso == "f" || _sesso == "femmina" || _sesso == "donna" || _sesso == "signora") //controlli
            {
                    numerogiorno = numerogiorno + 40;
                    ritorna += numerogiorno;
            }
            else if (_sesso == "m" || _sesso == "maschio" || _sesso == "uomo" || _sesso == "signore")
            {
                   ritorna += s[0];
            }
            return ritorna;
        }
        private string CalcoloComune()
        {
            string ritorna = "";
            string path = @"Models/CodiciComuni.csv";
            StreamReader sr = new StreamReader(path);
            string[] colonna=new string[8220];
            int counter = 0;
            
            while ((colonna[counter] = sr.ReadLine()) != null)//in modo che ogni riga ha un numero e il numero e' il rispettivo studente
                counter++;
            
            sr.Close();

            _luogoDiNascita = _luogoDiNascita.ToUpper();
            char[] c = new char[_luogoDiNascita.Length];
            c = _luogoDiNascita.ToCharArray();
            int indice=0, fine=0;

            switch (c[0])
            {
                case 'A':
                    indice = 0;
                    fine = 445; 
                    break;

                case 'B':
                    indice = 446;
                    fine = 1042;
                    break;

                case 'C':
                    indice = 1043;
                    fine = 2561;
                    break;

                case 'D':
                    indice = 2562;
                    fine = 2669;
                    break;

                case 'E':
                    indice = 2670;
                    fine = 2711;
                    break;

                case 'F':
                    indice = 2712;
                    fine = 3024;
                    break;

                case 'G':
                    indice = 3025;
                    fine = 3384;
                    break;

                case 'H':
                    indice = 3385;
                    fine = 3385;
                    break;

                case 'I':
                    indice = 3386;
                    fine = 3460;
                    break;

                case 'J':
                    indice = 3461;
                    fine = 3471;
                    break;

                case 'L':
                    indice = 3472;
                    fine = 3785;
                    break;

                case 'M':
                    indice = 3786;
                    fine = 4640;
                    break;

                case 'N':
                    indice = 4641;
                    fine = 4770;
                    break;

                case 'O':
                    indice = 4771;
                    fine = 4947;
                    break;

                case 'P':
                    indice = 4948;
                    fine = 5672;
                    break;

                case 'Q':
                    indice = 5673;
                    fine = 5705;
                    break;

                case 'R':
                    indice = 5706;
                    fine = 6107;
                    break;

                case 'S':
                    indice = 6108;
                    fine = 7225;
                    break;

                case 'T':
                    indice = 7226;
                    fine = 7614;
                    break;

                case 'U':
                    indice = 7615;
                    fine = 7650;
                    break;

                case 'V':
                    indice = 7651;
                    fine = 8163;
                    break;

                case 'Z':
                    indice = 8164;
                    fine = 8217;
                    break;
            }

            for (int i = indice; i <= fine; i++)
            {
                string [] riga = colonna[i].Split(',');
                if (riga[1] == _luogoDiNascita)
                {
                    ritorna += riga[0];
                }
            }

            return ritorna;
        }

        private string CarattereDiControllo()
        {
            string ritorna = "";
            char[] codicipari = new char[10];
            char[] codicidispari = new char[10];
            int countpari = 0, countdispari = 0, sommapari = 0, sommadispari = 0;
            string codicialfanumerici = $"{CalcoloCognome().ToUpper()}{CalcoloNome().ToUpper()}{CalcoloDataDiNascita().ToUpper()}{CalcoloGiornoeSesso().ToUpper()}{CalcoloComune().ToUpper()}";
            char[] c = codicialfanumerici.ToCharArray();
            for (int i = 0; i < codicialfanumerici.Length; i++)
            {
                if ((i % 2) != 0)
                {
                    codicipari[countpari] = c[i];
                    countpari++;
                }
                else
                {
                    codicidispari[countdispari] = c[i];
                    countdispari++;
                }
            }
            for (int i = 0; i < countdispari; i++)
            {
                for (int j = 0; j < valoridispari.Length; j++)
                {
                    if (codicidispari[i] == parallelo[j])
                        sommadispari += valoridispari[j];
                }
            }
            for (int i = 0; i < countpari; i++)
            {
                for (int j = 0; j < valoripari.Length; j++)
                {
                    if (codicipari[i] == parallelo[j])
                        sommapari += valoripari[j];
                }
            }
            int sommatot = (sommapari + sommadispari);
            sommatot = sommatot % 26;
            ritorna+= Alfabeto[sommatot];
            return ritorna;
        }

        public override string ToString()
        {
            if (_nome.Length + _cognome.Length >= 14)
            {
                return $"{_nome} {_cognome}:" +
                $"{CalcoloCognome().ToUpper()}" +
                $"{CalcoloNome().ToUpper()}" +
                $"{CalcoloDataDiNascita().ToUpper()}" +
                $"{CalcoloGiornoeSesso().ToUpper()}" +
                $"{CalcoloComune().ToUpper()}" +
                $"{CarattereDiControllo().ToUpper()}";
            }
            else
            {
                return $"{_nome} {_cognome}:\t" +
                $"{CalcoloCognome().ToUpper()}" +
                $"{CalcoloNome().ToUpper()}" +
                $"{CalcoloDataDiNascita().ToUpper()}" +
                $"{CalcoloGiornoeSesso().ToUpper()}" +
                $"{CalcoloComune().ToUpper()}" +
                $"{CarattereDiControllo().ToUpper()}";
            }
        }
    }
}
