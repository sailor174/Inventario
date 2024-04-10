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
            Console.WriteLine("Oque deseja fazer?\n[1] sepuko \n[Q-E] poção\n[I] inventário\n[]");
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.WriteLine("Você usou a " + );
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
                        indiceAtual = (indiceAtual + 1) % pocoes.Count;
                        int tentativas = pocoes.Count;
                        while (pocoes[indiceAtual].Qty >= 0 && tentativas >= 1)
                        {
                            indiceAtual = (indiceAtual + 1) % pocoes.Count;
                        }
                        if (tentativas == 0)
                        {
                            Console.WriteLine("sem mais poções");
                        }
                        Console.WriteLine($"Produto atual: {pocoes[indiceAtual].Name}, ID: {pocoes[indiceAtual].Id}");
                        break;

                    case ConsoleKey.Q:
                        indiceAtual = (indiceAtual - 1) % pocoes.Count;
                        Console.WriteLine($"Produto atual: {pocoes[indiceAtual].Name}, ID: {pocoes[indiceAtual].Id}");
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
            public string Name { get; set; }
            public int Qty { get; private set; }

            public Potion(int qty = 0)
            {
                LastId++;
                Id = LastId;
                Qty = qty;
            }
        }
        public class Cure_potion : Potion
        {
            public int Strenght { get; set; }
            public Cure_potion(string name, int strenght = -1)
            {
                int Strenght = strenght;
                string Name = name;
            }
            ///*
            public void Use()
            {
                if (Strenght == -1)
                {
                    Player.Vida = Player.Vida_Max;
                }
                else
                {
                    Player.Vida = Strenght;
                }
            }
            //*/
        }

        public class Resist_potion : Potion
        {
            public Resist_potion(string name)
            {
                Name = name;

                void Use()
                {
                    switch (Name)
                    {
                        /*
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
                            */
                    }
                }
            }
        }
    }
}