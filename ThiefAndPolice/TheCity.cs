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
        //Ex: När du skapar ett fält som private Person[,] grid;, reserverar du bara plats i minnet för att hålla en referens till en  matris, men du vet inte storleken på matrisen ännu.
        //För att skapa själva matrisen måste du veta bredden och höjden, vilket kommer från konstruktorn.

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
            this.width = width; //Det står this för att du har en rad över konstruktorn där du har en variabel med samma namn denna rad ---> private int width; (rad 26)
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
            persons.Add(person); //persons: maskinen vet att det är en list för du har deklarerat det som en fält i TheCity klassen över konstruktorn (rad 30)        
                                 //Add: betyder att man ska lägga till 
                                 //persons.Add(person):  anropar listans Add-metod och lägger till objektet person i listan.
            grid[person.X, person.Y] = person; //grid är rutnätet som redan definerats tidigare i TheCity klassen (rad 21)
                                               //person.X: Detta representerar personens X-koordinat (den horisontella positionen) i staden.
                                               //person.Y: Detta representerar personens Y-koordinat(den vertikala positionen) i staden. Varje person har alltså en X-och en Y-position som avgör var i staden(i rutnätet) de befinner sig.
                                               //grid[person.X, person.Y]: Denna del av koden refererar till en specifik position i stadens grid(rutnät), baserat på personens X-och Y - koordinater.
                                               //= person: Detta innebär att du placerar själva person - objektet i den specifika cellen på kartan, som motsvarar deras X - och Y - koordinater.

        }










        //Metod 2
        //Den används för att slumpmässigt placera personer (poliser, tjuvar och medborgare) i staden.
        public void SetPersonsInCity()
        {
            for (int i = 0; i < 15; i++)//här loopar du och antalet gånger du loopar kommer vara antal poliser, tjuvar och medborgare som kommer finnas. I detta fall 15 st av varje
            {
                AddPerson(new Police(Random.Shared.Next(0, width), Random.Shared.Next(0, height), "Police" + i));
                //AddPerson(...):  är metod 1 i klassen TheCity som lägger till en ny person (i det här fallet en polis).
                //new Police(...): Du skapar en polis genom polis klassen som finns i klassen Persons
                //new Polic(...): anropar oxå konstruktorn för klassen Police, och skickar in de argument som behövs för att skapa en polis: en X-koordinat, en Y-koordinat och ett namn.
                //X värdet: vi börjar med att skriva ut x värdet som måste matas in som parameter, det bestämde vi i polisens konstruktor:
                //Random.Shared.Next(0, width): genererar ett slumpmässigt tal mellan 0 och stadens bredd (värdet av width). så från 0 till så bred som staden är.
                //Random.Shared.Next(0, height) fungerar på samma sätt som ovan, men det genererar ett slumpmässigt tal mellan 0 och stadens höjd (värdet av height).
                //"Police" är en sträng som står för namnet, och + i är en variabel som används för att skapa ett unikt nummer för varje polis, t.ex. "Police0", "Police1", och så vidare.
                //i:  detta är alltså loopen som går från 0-14 och ger en siffra till polisen.

                AddPerson(new Thief(Random.Shared.Next(0, width), Random.Shared.Next(0, height), "Thief" + i));
                AddPerson(new Citizen(Random.Shared.Next(0, width), Random.Shared.Next(0, height), "Citizen" + i));
            }
        }













        //Metod 3
        //Private för den ska inte användas i Main utan bara i denna klass. Den används i Main genom en annan metod.
        //Först i metoden Interaction som oxå är private men sen i metoden UpdatePosition som är public
        //VAD GÖR DEN?
        private void AddNews(string news) //Den tar in en string för vi kommer i Interaction metoden skriva  "Medborgaren blir rånad, Polisen beslgagtar tjuvens föremål" å liknande saker.
                                          //Jag döper meningen jag ska skriva till news i en string variabel så string news kan tex vara "polisen beslagtar tjuvens föremål"
        {
            newsFeed.Add(news); //newsFeed är en list som är private och finns längst upp i TheCity klassen därför används Add
                                //Add lägger till i listan och det som läggs till här är meningen som ska skrivas ut när personerna i staden interagerar. Finns i metoden Interactions
            if (newsFeed.Count > maxNewsCount) //newsFeed.Count går igenom alla nyheter i listan
                                               //MaxNewsCount bestämdes redan t max 3 i The City klassen längst upp
                                               //kontrollerar om antalet nyheter i listan har överskridit detta maxvärde.
            {
                newsFeed.RemoveAt(0); // Ta bort den äldsta nyheten när max antal nyheter nås
                                      // det står inte 1 för att man börjar i datavärlden från 0
            }
        }











        //Metod 4
        //Den ska säga vad som händer när de olika personerna möter varandra. PErsonerna är P, T och C
        //Private varför?: Den ska bara användas i den här klassen inte i main. I main genom en annan metod
        //Genom att göra metoden privat skyddar du den från att bli använd av andra klasser utanför TheCity. Detta upprätthåller klassens kontroll över hur interaktioner mellan personer hanteras.
        private void Interaction(Person p1, Person p2) //Tar in två variabler från Person klassen p1 och p2 (alltså person 1 å 2)
        {
            if (p1 is Police && p2 is Thief)//Här kontrollerar du om p1 är en instans av typen Police.
                                            //is:  används för att kontrollera om ett objekt är av en viss typ.
                                            //Innan skrev jag == och det blev fel varför?:
                                            //När du skriver p1 == Police, försöker du jämföra p1 (som är ett objekt, till exempel en person) med Police (som är en klass eller typ, inte en enskild polis). Det är som att jämföra ett riktigt föremål med en ritning eller mall för föremål.
                                            //p1: är en specifik person i din stad, som kan vara en polis, en tjuv eller en medborgare.
                                            //Police: är själva "ritningen" eller typen för alla poliser.Det är inte en specifik polis, utan snarare en mall som berättar hur alla poliser ska vara.
                                            //Varfr is: då säger du "Är den här personen (p1) en polis?", vilket är vad du vill veta. Det är som att fråga: "Följer den här personen ritningen för poliser?"
            {
                ((Police)p1).ArrestThief((Thief)p2); //(Police)p1: Detta är en typkonvertering (type casting) som säger att vi behandlar objektet p1 som en polis (av typen Police)
                                                     //Fördelar?: Det gör att vi kan använda metoden ArrestThief, som bara finns i Police-klassen exempelvis.
                                                     //ArrestThief: är en metod som finns i Police klassen den finns under fliken Persons.cs längs upp i vyn. D drf metoden där var public, så man kan nå den från denna klass.

                thievesArrestedCount++; // Ökar räknaren för gripna tjuvar.
                                        // Den här variabeln är deklarerad som fält i TheCity klassen den är private men det är okej för den här metoden är också i TheCity klassen (rad 50)

                AddNews($"Polis {p1.Name} tar tjuven {p2.Name} och beslagtar alla föremål."); //AddNews är en metod som finns längre ner i koden. Den basiclly gör så att 3 nyheter syns på konsolen bara
                                                                                              //$: d behöver inte ha med denna symbol du kan lögga + mellan istället, smaksak.
            }
            else if (p1 is Thief && p2 is Citizen)//Om båda dessa villkor är sanna (dvs. om p1 är en tjuv och p2 är en medborgare), körs koden i block.
                                                  //is:  används för att kontrollera om ett objekt är av en viss typ.
                                                  //Varfr is: då säger du "Är den här personen (p1) en tjuv?"Det är som att fråga: "Följer den här personen ritningen för tjuvar?"
            {
                string stolenItem = ((Thief)p1).RobCitizen((Citizen)p2); //Här säger du t programt att p1 e Thief (tjuv) "Behandla p1 som en tjuv".
                                                                         //stolenItem blir namnet på föremålet som tjuven stjäl
                                                                         //StolenItem finns i tjuvens metod RobCitizen
                                                                         //RobCitizen är en metod i Thief klassen som finns i Persons längst upp i vyn. Metoden väljer random förmål och lägger i tjuvens inventory.
                if (!string.IsNullOrEmpty(stolenItem)) //Här kontrolleras om något föremål faktiskt blev stulet.
                                                       //! betyder "inte", så villkoret !string.IsNullOrEmpty(stolenItem) betyder:
                                                       //"Om stolenItem inte är tomt eller null" (dvs. om något föremål blev stulet).
                {
                    citizensRobbedCount++; // Öka räknaren för rånade medborgare räknaren finns längre upp i koden i TheCity klassen
                    AddNews($"Tjuven {p1.Name} rånar medborgaren {p2.Name} och stjäl {stolenItem}."); //AddNews är en metod som finns längre ner i koden. Den basiclly gör så att 3 nyheter syns på konsolen bara
                }
            }
            else if (p2 is Police && p1 is Thief)//Varför möts polis å tjuv igen?
                                                 //Nu är den andra p2 så alla interaktioner finns med
                                                 //Om båda dessa villkor är sanna (dvs. om p1 är en tjuv och p2 är en polis), körs koden i block.
                                                 //is:  används för att kontrollera om ett objekt är av en viss typ.
                                                 //Varfr is: då säger du "Är den här personen (p1) en polis?", vilket är vad du vill veta. Det är som att fråga: "Följer den här personen ritningen för poliser?"
            {
                ((Police)p2).ArrestThief((Thief)p1);//.ArrestThief((Thief)p1): Här anropar vi polisens metod ArrestThief och skickar in p1 som en tjuv (efter att ha typ konverterat p1 till en Thief).
                                                    //Metoden ArrestThief arresterar tjuven och tar alla föremål från tjuven och lägger dem i polisens inventory.
                thievesArrestedCount++; // Öka räknaren för gripna tjuvar. Den här variabeln finns längre upp i koden i klassen TheCity
                AddNews($"Polis {p2.Name} tar tjuven {p1.Name} och beslagtar alla föremål."); //AddNews är en metod som finns längre ner i koden. Den basiclly gör så att 3 nyheter syns på konsolen bara
            }
            else if (p2 is Thief && p1 is Citizen) //Här säger du om p2 följer ritningen för tjuv och p1 för medborgare gör det som står i blocket nere
                                                   //Längre upp möttes redan tjuv och medborgare så varför igen?: 
                                                   //Det handlar om att programmet måste kunna hantera interaktionen oavsett vilken ordning personerna möts i:
                                                   //I det första fallet: p1 är tjuven och p2 är medborgaren.
                                                   //I det andra fallet: p2 är tjuven och p1 är medborgaren.
            {
                string stolenItem = ((Thief)p2).RobCitizen((Citizen)p1);//Här säger du t programt att p2 e Thief (tjuv) "Behandla p1 som en tjuv".
                                                                        //stolenItem blir namnet på föremålet som tjuven stjäl
                                                                        //StolenItem finns i tjuvens metod RobCitizen
                                                                        //RobCitizen är en metod i Thief klassen som finns i Persons längst upp i vyn. Metoden väljer random förmål och lägger i tjuvens inventory.
                if (!string.IsNullOrEmpty(stolenItem)) //Här kontrolleras om något föremål faktiskt blev stulet.
                                                       //! betyder "inte", så villkoret !string.IsNullOrEmpty(stolenItem) betyder:
                                                       //"Om stolenItem inte är tomt eller null" (dvs. om något föremål blev stulet).
                {
                    citizensRobbedCount++; // Öka räknaren för rånade medborgare
                    AddNews($"Tjuven {p2.Name} rånar medborgaren {p1.Name} och stjäl {stolenItem}."); //Använd metoden Addnew för att lägga till i senaste tre nyheter
                }
            }
        }













        //Metod 5
        //Den här metoden uppdaterar  varje persons position i staden och hanterar interaktioner mellan personer som möts på samma plats.
        //Varför?: varje gång personerna i staden flyttar sig, lämnar de sin gamla plats och får en ny position på kartan.
        //Varför?: Om vi inte tömmer grid, skulle personerna "stanna kvar" på sina gamla platser i rutnätet och finnas på sina nya platser, vilket skulle skapa problem
        public void UpdatePositions()
        {
            Array.Clear(grid, 0, grid.Length); //Array.Clear: är typ som Console.Clear men den tömmer matrisen istället
                                               //grid: är själva arrayen som du vill tömma eller rensa. 
                                               //0: betyder att du vill börja rensa från den allra första positionen i arrayen, så du börjar med att tömma från början av arrayen
                                               //grid.Length: är längden på arrayen alltså slutet eller hela 

            foreach (var person in persons) //Gå igenom varje person i persons
                                            //persons är i det här fallet en lista som innehåller alla personerna i staden, t.ex. poliser, tjuvar och c
                                            //Varje person ska göra det som står nere
            {
                person.Move(width, height); //person.Move(width, height);: Varje person flyttas genom att anropa deras Move-metod som finns i Persons klassen
                                            //Move(width, height) använder stadens width och height för att se till att personen flyttas inom gränserna för stadens rutnät
                                            //Personen flyttar sig baserat på en tidigare bestämd riktning, det bestämdes i klassen Person slumpmässigt (t.ex. höger, vänster, upp, ner).

                //Raden kommer säga vad som händer om t¨vå personer hamnar på samma ruta
                if (grid[person.X, person.Y] != null) //grid = är själva matrisen
                                                      //person.X och person.Y är personens aktuella koordinater i stadens rutnät 
                                                      //grid[person.X, person.Y] != null kontrollerar om det redan finns en annan person på samma position i kartan.
                                                      //Om det finns en person där (dvs. att platsen inte är tom, inte null), betyder det att två personer har hamnat på samma plats.
                                                      //Vad händr då? kolla blocket ner
                {
                    Interaction(person, grid[person.X, person.Y]); //ropar en metod som heter Interaction och skickar två personer till den:
                                                                   //person: Personen som just har flyttat till en ny position.
                                                                   //grid[person.X, person.Y]: Personen som redan befinner sig på den platsen.
                                                                   // betyder alltså: "Den person som befinner sig på platsen (X, Y) i rutnätet."
                                                                   //Det är alltså p2 som finns i metoden Interactions

                }
                //p1´s position uppdateras. Det var personen som flyttade till platsen. Första parametern i Interaction metoden.
                grid[person.X, person.Y] = person;
            }
        }














        //Metod 6
        //Måste vara public för jag ska ropa på den i main
        //Ska skriva ut staden men oxå nyheter och statistik
        public void PrintCity()
        {
            Console.Clear(); // Rensa konsolen innan utskrift.
                             // Det används för att ta bort allt som tidigare visades i konsolen så att staden kan ritas upp från början utan att gammal information syns.

            for (int y = 0; y < height; y++) //Den här loopen går igenom varje rad i rutnätet (som representerar staden), en rad i taget från överst till nederst.
            {
                for (int x = 0; x < width; x++) //Den här loopen går igenom varje kolumn på den aktuella raden (från vänster till höger).
                {
                    if (grid[x, y] != null) //grid[x, y] != null betyder: "Finns det någon person eller objekt på den här platsen i rutnätet?"
                                            //Om det finns någon där (dvs. om den INTE är tom !=), så körs koden inuti if-blocket.

                    {
                        grid[x, y].Display(); //MAtrisens x och y position ska skriva ut en symbol
                                              //ropar Display()-metod. Den metoden som ritar ut en symbol för personen, till exempel "P" och finns i PErson klassen. Den finns i varje subklass.
                                              //VArför är metoden abstract i PErson klassen?:
                                              //för att tvinga alla klasser som ärver från Person(t.ex.Police, Thief, och Citizen) att implementera sin egen version av Display().

                    }
                    else
                    {
                        Console.Write(" "); //Om det inte finns en person ska du skriva en tom rad i matrisen. Inte WriteLine för då hoppar den ner en rad.
                    }
                }
                Console.WriteLine();
            }
            //Viktigt att komma ihåg för att jag gjorde fel 7 miljoner ggr på dehär :)
            //VArför ska if satserna vara i loopen för kolumner och inte rader?
            //Kolumnloopen behövs för att gå igenom varje enskild cell i varje rad. Varje gång den körs, kontrollerar den om det finns en person på positionen grid[x, y]
            //Om koden fanns i radloopen istället, skulle den INTE gå igenom varje kolumn utan bara behandla en hel rad som en enhet, vilket inte är vad vi vill.
            //Lärdom: Lägg bara allt i kolumn loopar framöver för de är mer detaljerade :)


            Console.WriteLine(); //design mellanrum
            // Visa nyhetsflödet längst ner i konsolen
            Console.WriteLine("--- Senaste Nyheter i Ilaydas söta stad <3 ---");
            foreach (var news in newsFeed)
            {
                Console.WriteLine(news);
            }

            Console.WriteLine(); //design mellanrum
            // Visa statistiken längst ner
            Console.WriteLine("--- Statistik ---");
            Console.WriteLine($"Antal rånade medborgare: {citizensRobbedCount}"); //$ använd den istället för att lägga in + hejvilt 
            Console.WriteLine($"Antal gripna tjuvar: {thievesArrestedCount}");
        }
    }



}
