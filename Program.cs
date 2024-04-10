using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace Objetos
{
    public class Objeto()
    {
        static System.Timers.Timer timer;
        /*
        ideias:
        quando eu clicar Q/E ele vai diminuir/aumentar o Id da poção
        se aquele Id tiver 0 poções, ele repete o processo.
        Ele repete esse processo pelo numero do maior Id,
        se ele nunca encontrar um resultado ele fala "sem poções"
        caso o unico resultado seja o inicial "sem mais poções"
        */
        public static void Main(string[] args)
        {
            Player player = new();
            List<Potion> pocoes = [];
            pocoes.Add(new Cure_potion("Health potion", 150));
            pocoes.Add(new Cure_potion("Full health potion"));
            pocoes.Add(new Resist_potion("Resist fire potion"));
            pocoes.Add(new Resist_potion("Resist shock potion"));
            pocoes.Add(new Resist_potion("Resist freeze potion"));
            pocoes.Add(new Resist_potion("antidote"));
            int indiceAtual = 0;
            Console.WriteLine("Oque deseja fazer?\n[1] sepuko \n[Q-E] poção\n[I] inventário\n[spacebar] usar item");
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.WriteLine("Você usou a " + pocoes[indiceAtual].Name);
                        pocoes[indiceAtual].Use();
                        break;
                    case ConsoleKey.D1:
                        Console.WriteLine("Quanto de vida deseja retirar?");
                        player.seppuko();
                        Console.WriteLine(Player.Vida);
                        if (Player.Vida <= 0)
                        {
                            Console.WriteLine("Você morreu");
                            return;
                        }
                        break;
                    case ConsoleKey.E:
                        //verifica se é o ultimo ID
                        if (indiceAtual == pocoes.Count - 1)
                        {
                            //volta do topo
                            indiceAtual = -1;
                        }
                        indiceAtual++;
                        int tentativas = pocoes.Count;
                        Console.WriteLine($"poção atual: {pocoes[indiceAtual].Name}, ID: {pocoes[indiceAtual].Id}");
                        break;
                    case ConsoleKey.Q:
                        //verifica se é o ultimo ID
                        if (indiceAtual == 0)
                        {
                            //volta do topo
                            indiceAtual = pocoes.Count;
                        }
                        indiceAtual--;
                        Console.WriteLine($"poção atual: {pocoes[indiceAtual].Name}, ID: {pocoes[indiceAtual].Id}");
                        break;

                    case ConsoleKey.I:

                        break;
                }
            }
        }
        public class Player
        {
            public static int Vida { get; set; } = 500;
            public static int Vida_Max = 500;
            public Player()
            {
                Vida = 500;
                Vida_Max = 500;
            }
            public void seppuko()
            {
                Vida -= int.Parse(Console.ReadLine());
            }
        }
        public class Potion
        {
            private static int LastId { get; set; } = 0;
            protected int Max_qty { get; set; }
            public int Id { get; private set; }
            public virtual string Name { get; set; }
            public int Qty { get; private set; }
            public Potion(int qty = 1)
            {
                LastId++;
                Id = LastId;
                Qty = qty;
            }
            public virtual void Use()
            {

            }
        }
        public class Cure_potion : Potion
        {
            public int Strenght { get; set; }
            public Cure_potion(string name, int strenght = -1)
            {
                Strenght = strenght;
                Name = name;
            }
            public void Use()
            {
                if (Strenght == -1)
                {
                    Player.Vida = Player.Vida_Max;
                    Console.WriteLine("Você usou a poção de cura total");
                }
                else
                {
                    Player.Vida += Strenght;
                }
            }
        }
        public class Resist_potion : Potion
        {
            public Resist_potion(string name)
            {
                Name = name;
            }
            public void Use()
            {
                switch (Name)
                {
                    case "Resist fire potion":
                        int fireTimer = 90000;
                        break;
                    case "antidote":
                        int venomTimer = 90000;
                        break;
                    case "Resist shock potion":
                        int shockTimer = 90000;
                        break;
                    case "Resiste freeze potion":
                        int freezeTimer = 90000;
                        break;
                }
            }
        }
    }
}