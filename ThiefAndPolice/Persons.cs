using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ThiefAndPolice
{
    internal class Persons
    {

        //Här skapas ett objekt alltså en klass som heter Person
        //Det är mallen (typ ritningen) till P, T och C
        //abstract betyder:  att du inte kan skapa ett objekt av den klassen. Istället används den som en mall för andra klasser att ärva från.
        //Person är abstrakt:  för att det inte finns någon anledning att skapa en "vanlig" person. Vi vill bara skapa specifika typer av personer, som Police, Thief och Citizen
        //Fördelar:  När en abstrakt klass har en abstrakt metod (en metod utan implementation) tvingas alla klasser som ärver den abstrakta klassen att implementera den metoden.
        //Struktur:  Detta säkerställer att alla underklasser följer en viss struktur och innehåller viktiga metoder, även om de har olika implementationer.
        //Exempel:  I Person-klassen är Display en abstrakt metod. Alla typer av personer (Police, Thief, Citizen) måste implementera Display på sitt eget sätt. Polisen visar ett P, tjuven ett T och medborgaren ett C.
        //Fördelar: Fördelar: Tydlig struktur, återanvändning av kod och säkerhet i att underklasser följer vissa regler.
        //Nackdelar: Du kan inte skapa objekt direkt av en abstrakt klass, men det är inte poängen. Poängen är att skapa specifika varianter (som Police, Thief, Citizen) med gemensamma egenskaper och metoder.
        public abstract class Person
        {
            //Skapar egenskaper som oxå kallas properties
            public int X { get; set; } //Vart på x axel som personen ska befinna sig på. Det ska vara en siffra för det är int
            public int Y { get; set; } //Vart på y axeln som personen ska befinna sig på. Det ska vara en siffra för det är int
            public string Name { get; set; } //Är string alltså text och ska lagra Police, Citizen eller Thief.
                                             //Name används för att ge varje objekt av Police, Citizen, och Thief ett namn, t.ex. "Police0", "Thief1", "Citizen2"

            private int directionX;
            private int directionY;
            //Varför private?: När du gör directionX och directionY private gör du inkapsling som innebär att dessa variabler endast kan nås och ändras inom klassen Person. Andra klasser har ingen direkt åtkomst till de.
            //EXempelvis kommer du ändra directionY i  metod 2 Move längre ner
            //Fördelar m private: Förhindra att andra klasser oavsiktligt ändrar riktningen och skapar buggar.
            //Fördelar m private: Om directionX och directionY är private, kan du lätt ändra hur riktningen fungerar i framtiden utan att behöva ändra någon annan kod som använder Person eller dess underklasser.
            //Sammanfattning: I korthet handlar det om inkapsling, som är ett grundläggande koncept inom objektorienterad programmering (OOP). Genom att använda private säkerställer du att riktningen hanteras korrekt och på ett kontrollerat sätt inom Person-klassen.


            //Här gör du en konstruktor som är en speciell metod i en klass som används för att skapa ett objekt (eller en instans) av den klassen. Properties skapar egenskaperna och konstruktorn själva objektet
            //Den är public så att subklasserna ska nå alla egenskaperna i den
            public Person(int x, int y, string name)//Konstruktorn tar in personens position i x axeln, y axeln och namnet personen har tex police4.
            {
                X = x; //Du ger X-egenskapen(propertyn) värdet som skickas in som argument (x). Du ger alltså propertyn det värdet som skrivs in som x
                Y = y;
                Name = name;
                RandomDirection(); //Anropa metoden för att sätta en slumpmässig rörelseriktning när personen skapas.
                                   //Varför anropas metoden i konstruktorn?: för att se till att varje ny Person får en slumpmässig rörelseriktning direkt när den skapas.
                                   //Exempel: om jag skapar Person person = new Police(10, 5, "Police4"); så kommer police4 att gå på ett visst sätt direkt.
            }






            //Metod 1 i Person klassen ska bestämma vilken riktning P, T och C ska gå, den slumpar riktningarna
            private void RandomDirection() //private: gör att SetRandomDirection() endast kan anropas inom klassen där den är definierad (Person).
                                           //void:  Metoden returnerar inget värde. Den gör bara ett arbete (sätter en slumpmässig riktning för directionX och directionY).
            {
                int[] directions = { -1, 0, 1 };//int[]: Skapar en array (en lista med flera värden) som innehåller heltal (int).
                                                //directions: Namnet på arrayen. Arrayen används för att hålla alla möjliga värden som directionX och directionY kan få
                                                //{ -1, 0, 1 }: innehåller tre värden: -1, 0 och 1. 
                                                //-1: personen rör sig upp eller vänster.
                                                //1: personen rör sig ner eller höger
                                                //0: personen rör sig ingenting, om 0 kommer så kommer den inte kunna röra sig snett

                directionX = directions[Random.Shared.Next(0, 3)]; //directionX =: Tilldelar variabeln directionX ett värde från directions-arrayen.
                                                                   //Random används för att generera ett slumpmässigt tal. 
                                                                   //0: får man 0 så får man värdet -1 för den är först i arrayen
                                                                   //1: får man 1 så får man värder 0 för den är andra i arrayen
                                                                   //2: får man 2 så får man värdet 1 för den är tredje i arrayen
                directionY = directions[Random.Shared.Next(0, 3)];

                if (directionX == 0 && directionY == 0) //Här säger den att om x axel och y axel båda blir 0 alltså inte rör på sig så måste man anropa metoden igen
                                                        //Glöm inte denna jävel för annars ser det konstigt ut i konsolen. 
                {
                    RandomDirection();
                }
            }





            //Metod 2 i Person klassen används för att få personerna röra på sig hela tiden i den riktningen som bestäms i metoden innan:
            //Sammanfattning:
            //1. Den här metoden flyttar personen genom att uppdatera dess X och Y positioner baserat på directionX och directionY.
            //2. Modulo(%) används för att hantera så kallad "wrapping", vilket betyder att om personen går utanför ena sidan av rutnätet, kommer den tillbaka på motsatt sida(som en cirkel).
            //3. Kontrollerna för negativa värden(if (X< 0) och if (Y< 0)) ser till att om personen går utanför vänster- eller överkanten, kommer de tillbaka på den motsatta kanten.
            //Den här metoden tar in 2 int variabler, ena heter maxY och andra maxX)
            public void Move(int maxX, int maxY) //Metoden är public, vilket betyder att den kan anropas från alla andra klasser såsom TheCity exempelvis
                                                 //Den tar in 2 int variabler
                                                 //maxX är bredden (antalet kolumner), och maxY är höjden (antalet rader) i rutnätet. 
            {
                //Den här raden ser till att X uppdateras genom att lägga till en rörelse (directionX) och håller värdet inom en viss gräns (maxX). Om X blir större än gränsen börjar det om från noll.
                X = (X + directionX + maxX) % maxX; //Meningen gör så att personen rör på sig hela tiden beroende på vilken riktning directionX har bestämts till i den första metoden
                                                    //X =: Detta tilldelar ett nytt värde till variabeln X, den finns i properties i Person klassen
                                                    //X: Nuvarande X-position för personen.
                                                    //directionX: Detta värde (kan vara -1, 0 eller 1) bestämmer om personen rör sig åt vänster, står still eller rör sig åt höger. Den bestämdes i första metoden.
                                                    //+ maxX: Detta läggs till för att undvika negativa värden när vi använder modulo (förklaras strax).
                                                    //%: Modulo betyder att vi delar två tal och tar resten av divisionen.
                                                    //Exempel: 10 % 3 betyder att vi delar 10 med 3. Kvoten är 3, och resten blir 1 (eftersom 3 går in i 10 tre gånger, med 1 över). Så 10 % 3 = 1.
                                                    //Varför används den här?: När vi använder modulo på X, ser vi till att personen "wrappar" runt spelplanen. d.v.s kmr tbx.
                                                    //Om maxX = 10 (rutnätets bredd är 10): Om personen är på X = 9 och directionX = 1(rör sig höger), så får vi:
                                                    //(9 + 1 + 10) % 10 = 20 % 10 = 0.Personen flyttas till den vänstra kanten(X = 0).

                Y = (Y + directionY + maxY) % maxY;

                //if satsen ska se till att personerna kommer tillbaka till spelplanen när de går ut, alltså när det blir negativt
                if (X < 0) //Denna rad kontrollerar om X-positionen har blivit negativ. Detta kan inträffa om modulo-beräkningen inte korrekt hanterar negativa tal, eller om personen flyttar sig för långt utanför vänsterkanten.
                {
                    X = maxX - 1; //Om X är negativt (utanför rutnätet), sätts X-positionen till den högsta tillåtna positionen, som är maxX - 1. Detta gör att personen flyttas till den högra kanten av rutnätet.
                                  //maxX-1 är den sista. om maxX=10 är maxX-1 = 9. Du kan tro 10 är sista men glöm inte att man börjar från 0
                }
                if (Y < 0)
                {
                    Y = maxY - 1;
                }
            }




            //Metod 3
            //Display är en metod som finns för att varje subklass ska döpa sina personer. När Display() anropas på ett polisobjekt, kommer "P" att visas på skärmen.
            //Metoden får alltså varje underklass visa en symbol (t.ex. "P", "T", eller "C") när Display() anropas.
            //Abstrakt betyder att metoden inte har någon kod i klassen där den skapas. Istället måste alla klasser som ärver från den abstrakta klassen skriva sin egen kod för metoden.
            //Du bestämmer att metoden ska finnas, men du säger inte vad den gör i den abstrakta klassen.Varje klass som ärver måste skapa sin egen version av metoden.
            //Varför?: När du har en abstrakt klass som t.ex. Person, kan du använda abstrakta metoder för att definiera en metod som alla underklasser måste implementera på sitt eget sätt.
            public abstract void Display();
        }


















        //första subklassen som skapas och ärver från Person klassen. : är en symbol som skrivs när man ska ärva
        public class Police : Person
        {
            public List<string> Inventory { get; private set; } //Man kan göra en property i en subklass som inte finns i basklassen precis som jag gör här. 
                                                                //public: Detta betyder att egenskapen Inventory är tillgänglig överallt i programmet. Man kan läsa värdet av Inventory från andra klasser och metoder.
                                                                //List<string>: dethär är datatypen för Inventory. Det är en lista av strängar för vi ska t.ex ha "nycklar" å så
                                                                //Inventory: Detta är namnet på egenskapen.

            //Det är Police subklassens konstruktor, alltså en speciell metod.
            public Police(int x, int y, string name) : base(x, y, name) //: base(x, y, name): Detta anropar konstruktorn för basklassen Person. Med andra ord skickas dessa värden (x, y, name) till Person-klassen, som sköter de grundläggande egenskaperna för en person, såsom X- och Y-positionen och namnet. 
                                                                        //Det betyder att du inte behöver skriva om x y och name i polisens konstruktor oxå, du kopierar bara klassen Persons konstruktor.

            {
                Inventory = new List<string>(); //Du måste dock skriva inventory i konstukrorn för den fanns inte i klassen Persons konstruktor.
                                                //I Police-konstruktorn kan du fokusera på de egenskaper som är specifika för polisen (som Inventory), utan att behöva tänka på att sätta grundläggande egenskaper som redan finns i basklassen (X, Y, och Name).
            }

            //Ropa på metoden Display som finns i Person klassen
            public override void Display()//Den ropar på abstrakta metoden siplay och måste välja, i detta fall, en symbol
                                          //Varför override?: override används för att skriva om (eller "ersätta") en metod som finns i en basklass (i detta fall den abstrakta klassen Person).
            {
                Console.Write("P");//Den väljer symbolen P
            }

            // Metod för att ta en tjuvs stulna föremål
            //Metoden finns i Police klassen för det är något en polis ska göra
            public void ArrestThief(Thief thief) //ArrestThief(Thief thief): namnet på metoden
                                                 //det betyder att den ska ta in en tjuv d e en parameter av typen Thief (en tjuv).
            {
                if (thief.Inventory.Count > 0) //Count används när man använder list och inventory är list därför står count där. Den går igenom som tex getlength går igenom matris
                                               //Raden kontrollerar om tjuvens (thief.) Inventory innehåller några föremål.
                                               // > 0 betyder ju att det ska vara mer än 0 och det är det ju inte om tjuven har något föremål i sin inventory
                {
                    Inventory.AddRange(thief.Inventory); //AddRange är också nått man använder när man skriver List. Add används när man lägger till på sin lista, i detta fall polisens inventory
                                                         //AddRange och inte bara Add för Add är en sak och Range flera saker samtidigt. Ifall att tjuven har tagit flera föremål.
                                                         //Den här raden säger att vi ska lägga tjuvens inventory i polisens inventory
                    thief.Inventory.Clear();             //Den här raden säger att vi ska ta bort allt som finns i tjuvens inventory
                                                         //Det som händer då är att tjuvens inventory läggs till på polisens och tjuvens invnetory tas sedan bort.
                    Console.WriteLine($"Polis {Name} tar tjuven {thief.Name} och beslagtar alla föremål.");
                }
            }
        }





















        //Andra subklassen som ärver från person och som är en tjuv
        public class Thief : Person
        {
            public List<string> Inventory { get; private set; } //Ge tjuven en egen egenskap(property) som bara gäller i tjuvklassen

            //Tjuvens egna konstuktor
            public Thief(int x, int y, string name) : base(x, y, name) //Copy paste alla properties från PErson klassen genom base: 
            {
                Inventory = new List<string>(); //Tjuvens inventory skriv i tjuvens konstuktor
            }

            //Den abstracta metoden som fanns i abstracta personklassen
            public override void Display()
            {
                Console.Write("T"); //metoden har i uppgift att ge personen en symbol i konsolen, i detta fall T för tjuv
            }

            // Metod för att råna en medborgare och returnera det stulna föremålet
            //Metoden finns bara här för det är bara tjuvar som rånar och den tar in en medborgare alltså citizen
            //Lägg märke till att det inte är void utan string. Det betyder den ska returnera en string 
            public string RobCitizen(Citizen citizen)
            {
                if (citizen.Inventory.Count > 0) //Det här är likt polisens inventory men den säger om meborgarens inventory är mindre än 0 alltså tom då ska raden under i if-satsen gälla
                {
                    int itemIndex = Random.Shared.Next(citizen.Inventory.Count); //Raden ska välja ett slumpmässigt föremål
                                                                                 //int itemIndex: Denna variabel deklareras och kommer att lagra ett slumpmässigt index som används för att välja ett objekt från medborgarens inventory
                                                                                 //Random.Shared.Next() är en metod som genererar ett slumpmässigt tal. I detta fall används det för att välja ett slumpmässigt index från medborgarens lista av föremål.
                                                                                 //citizen.Inventory.Count läser igenom alla föremål i medborgarens inventory, så vi ser till att den väljer slumpmässigt från medborgarens inventory
                    string stolenItem = citizen.Inventory[itemIndex]; //Raden ska lagra slumpmässiga föremålet i variabeln stolenItem
                                                                      //string stolenItem: Här deklareras en variabel stolenItem som representerar föremålet som tjuven kommer att stjäla.
                                                                      //citizen.Inventory[itemIndex]: Detta hämtar det föremål från medborgarens inventory som finns på den slumpmässiga positionen itemIndex.


                    Inventory.Add(stolenItem); // Lägg till i tjuvens inventory 
                                               //Det är Add och inte AddRange för Add lägger till en sak men Range flera samtidigt
                    citizen.Inventory.RemoveAt(itemIndex); // Ta bort föremålet från medborgaren. RemoveAt används i samband med Lister precis som AddRange
                    return stolenItem; // Returnera det stulna föremålet för att visa i nyheterna
                }
                return string.Empty; // Returnera en tom sträng om inget föremål kunde stjälas. Inventory får inga fler föremål.
                                     //String.Empty är samma sak som att skriva "" (en tom sträng med citattecken), men det här är lite proffsigare.
            }
        }






















        //Tredje sublklassen som är medborgare och som också ärver från basklassen Person
        public class Citizen : Person
        {
            public List<string> Inventory { get; private set; } //Medborgare får också sin egen inventory

            public Citizen(int x, int y, string name) : base(x, y, name) //Medborgaren tar också emot alla propertys från basklassen
            {
                // Varje medborgare har fyra föremål. Det är skillnaden från Police och Thiefs klassen, där var deras inventory inte bestämd.
                Inventory = new List<string> { "Nycklar", "Mobiltelefon", "Pengar", "Klocka" }; //Här är sakerna som finns i medborgarnas inventory
            }

            //Den här klassen använder oxå abstract metoden som  finns i PersonKlassen.
            //Detta för att bestämma en symbol, i detta fall "C" för att markera i konsolen.
            public override void Display()
            {
                Console.Write("C");
            }
        }


    }
}
