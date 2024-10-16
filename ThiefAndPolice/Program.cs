using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using static ThiefAndPolice.Persons;

namespace ThiefAndPolice
{
    class Program
    {
        static void Main(string[] args)
        {
            //Här skapas en instans av klassen TheCity och tilldelas variabeln city.
            //Enkalre:  du refererar till klassen TheCity, refererar till klassens fält och konstruktor
            TheCity city = new TheCity(100, 20); //ÄNDRA INTE STORLEKEN från 20 uppåt. För då gör konsolen bara skumma grejjer. Den "växer"

            city.SetPersonsInCity(); //Metod 2
                                     //I den nya staden som du skapade ovan ska du lägga till en metod från TheCityklassen. Gissa 3 gånger vilken :) Börjar med S och slutar med etPErsonsInCity :) 

            // Huvudloop för simuleringen
            while (true)
            {
                city.UpdatePositions(); //Metod 5
                                        //Flytta alla personer
              
                city.PrintCity();        //Metod 6
                                         //Skriv ut staden och nyhetsflödet

                Thread.Sleep(500); // Vänta en halv sekund innan nästa uppdatering
            }

        }
    }

}