using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ThiefAndPolice.Persons;

namespace ThiefAndPolice
{
    //Objekten/Klassen The City som är grunden på hur staden ska se ut och vad som kommer vara i den RITNINGEN
    //Klassen är public så man kan ropa på den i andra delar av programmet
    public class TheCity
    {
        //Vi ska skriva fält istället för propertys. Fält är när man inte har med {get; set;} men det är också egenskaper
        //Fält: används ofta när det ska vara privat eller inkapsling
        //Properties:  används ofta när det ska vara public och öppet för alla
        //Ex: När du skapar ett fält som private Person[,] grid;, reserverar du bara plats i minnet för att hålla en referens till en  matris, men du vet inte storleken på matrisen ännu. För att skapa själva matrisen måste du veta bredden och höjden, vilket kommer från konstruktorn.

        //Detta är en matris på själva rutnätet som ska vara staden och den kallas grid för det betyder rutnät på engelska
        private Person[,] grid; //private: betyder att grid bara är tillgänglig inom klassen TheCity och inte kan användas från andra delar av programmet direkt.
                                //Person[,]: Detta är en matris som representerar ett rutnät av personer (t.ex. poliser, tjuvar, medborgare). Rutnätet håller reda på var personerna befinner sig i staden.
                                //Person[,] betyder att detta är ett 2D-rutnät där varje cell kan hålla ett objekt av typen Person

        //Dessa är stadens bredd och höjd men jag ger inget värde i det för att jag kanske vill ändra storleken sen då kan det bli bökigt om jag skriver direkt här
        private int width;
        private int height;

        //Det här ska va listan som innehåller alla objekt i staden, asså alla P, T och C
        private List<Person> persons;

        //Det här kommer vara listan som håller alla nyheter. och jag ska skriva meningar så den får vara string
        // ex: "Polis grep en tjuv" eller "Tjuv rånade en medborgare"
        private List<string> newsFeed;


        //Alla variabler/egenskaper/fält neråt kommer inte finnas med i konstruktorn för att de är redan förutbestämda maxCount kommer alltid vara 3 och räknarna kommer alltid börja på 0
        //Värden som sätts i konstruktorn är vanligtvis sådana som kan variera beroende på hur och när objektet skapas.
        //Exempel: Stadens bredd och höjd (width och height) sätts i konstruktorn eftersom de kan vara olika varje gång du skapar ett nytt objekt av klassen TheCity
        //Men nyheten ska alltid visa de senaste 3 oavsett exempelvis.
        private const int maxNewsCount = 3; //Begränsa antal nyheter som visas till 3
                                            //const = constant (alltså hela tiden)
                                            //Const: är en variabel som typ string men skillnaden är att man kan inte ändra const värde efter att man skapat den.
                                            //Const är ens speciell variabel för man måste ändå skriva ut vilken datatyp som kommer vara const, i detta fall int.
                                            //D e typ för att säga t datorn vilken typ av variabel const ska hålla. Lite komplicerat men tänk bara att det är så leta nt efter logik :P

        //De här två ska finnas under nyheter på stadens statistik
        //Lägg allt till 0 för det ska ju ändras beroende på vilka som möts i staden
        private int citizensRobbedCount = 0; // Räknare för antal rånade medborgare
        private int thievesArrestedCount = 0; // Räknare för antal gripna tjuvar


        //Konstruktorn av TheCity den tar in bredd och höjd
        public TheCity(int width, int height)
        {
            this.width = width; //Det står this för att du har en rad över konstruktorn där du har en variabel med samma namn denna rad ---> private int width; (rad 22)
                                //När du har en rad(fält) med samma namn över konstruktorn måste du lägga this MEN
                                //bara om du låter konstruktorn ta in det som parameter. Om du tittar ner så har grid, persons och newsfeed inte this framför
                                //Varför? för dessa variabler matas inte in i kontruktorn.
                                //Varför matas inte grid, persons och newFeed i konstruktorn?: 
                                //Eftersom de skapas automatiskt inuti klassen TheCity. När staden skapas:
                                //1. grid(stadens karta) skapas utifrån stadens storlek(width och height) som redan skickas in till konstruktorn.
                                //2. persons(listan över personer) startar som en tom lista och fylls på senare under spelets gång.
                                //3. newsFeed(nyhetslistan) börjar också som en tom lista och fylls med händelser när spelet körs.
            this.height = height;

            grid = new Person[width, height]; //skapar en matris av typen Person, med storleken width x height. Ojekten P,T och C kan nu finnas i matrisen
            persons = new List<Person>();
            newsFeed = new List<string>();
        }






        //Metod 1
        //Används för att ska en person men också bestämma personens position i x och y axeln.
        private void AddPerson(Person person)
        {
            persons.Add(person); //persons: maskinen vet att det är en list för du har deklarerat det som en fält i TheCity klassen över konstruktorn       
                                 //Add: betyder att man ska lägga till 
                                 //persons.Add(person):  anropar listans Add-metod och lägger till objektet person i listan.
            grid[person.X, person.Y] = person; //grid är rutnätet som redan definerats tidigare i TheCity klassen (rad 21)
                                               //person.X: Detta representerar personens X-koordinat (den horisontella positionen) i staden.
                                               //person.Y: Detta representerar personens Y-koordinat(den vertikala positionen) i staden. Varje person har alltså en X-och en Y-position som avgör var i staden(i rutnätet) de befinner sig.
                                               //grid[person.X, person.Y]: Denna del av koden refererar till en specifik position i stadens grid(rutnät), baserat på personens X-och Y - koordinater.
                                               //= person: Detta innebär att du placerar själva person - objektet i den specifika cellen på kartan, som motsvarar deras X - och Y - koordinater.

        }








        //Metod 2
        public void AddNews(string news) //Jag döper meningen jag ska skriva till news i en string variabel så string news kan tex vara "polisen beslagtar tjuvens föremål"
        {
            newsFeed.Add(news); //newsFeed är en list som är private och finns längst upp i TheCity klassen därför används Add
                                //Add lägger till i listan och det som läggs till här är meningen som ska skrivas ut när personerna i staden interagerar. Gör en interaktion metod
            if (newsFeed.Count > maxNewsCount) //newsFeed.Count går igenom alla nyheter i listan
                                               //MaxNewsCount bestämdes redan t max 3 i The City klassen längst upp
                                               //kontrollerar om antalet nyheter i listan har överskridit detta maxvärde.
            {
                newsFeed.RemoveAt(0); // Ta bort den äldsta nyheten när max antal nyheter nås
                                      // det står inte 1 för att man börjar i datavärlden från 0
            }
        }










    }












}
