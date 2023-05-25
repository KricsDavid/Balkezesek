using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Balkezesek
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Jatekos> jatekosok = Beolvas("balkezesek.csv");

           
            
        }

        static List<Jatekos> Beolvas(string fajlnev)
        {
            List<Jatekos> jatekosok = new List<Jatekos>();
            string[] sorok = File.ReadAllLines(fajlnev);

            foreach (string sor in sorok.Skip(1)) 
            {
                string[] adatok = sor.Split(';');
                string nev = adatok[0];
                DateTime elso = DateTime.Parse(adatok[1]);
                DateTime utolso = DateTime.Parse(adatok[2]);
                int suly = int.Parse(adatok[3]);
                int magassag = int.Parse(adatok[4]);

                jatekosok.Add(new Jatekos(nev, elso, utolso, suly, magassag));
            }

            return jatekosok;
        }

        static int KerekítettBeolvas(string prompt, int alsoHatar, int felsoHatar)
        {
            int evszam;
            bool ervenyes;
            do
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                ervenyes = int.TryParse(input, out evszam) && evszam >= alsoHatar && evszam <= felsoHatar;

                if (!ervenyes)
                {
                    Console.WriteLine("Hibás adat, kérek egy " + alsoHatar + " és " + felsoHatar + " közötti évszámot!");
                }
            } while (!ervenyes);

            return evszam;
        }

        static double AtlagSuly(List<Jatekos> jatekosok, int evszam)
        {
            var jatekosokEvszam = jatekosok.Where(j => j.Első.Year <= evszam && j.Utolsó.Year >= evszam);
            double atlagSuly = jatekosokEvszam.Average(j => j.Súly);
            return atlagSuly;
        }
    }

    class Jatekos
    {
        public string Név { get; }
        public DateTime Első { get; }
        public DateTime Utolsó { get; }
        public int Súly { get; }
        public int Magasság { get; }

        public Jatekos(string név, DateTime első, DateTime utolsó, int súly, int magasság)
        {
            Név = név;
            Első = első;
            Utolsó = utolsó;
            Súly = súly;
            Magasság = magasság;
        }
    }
}
