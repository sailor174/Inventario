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
            List<Item> items = [];

            items.Add(new Cure_potion("Health potion", 150));
            items.Add(new Cure_potion("Full health potion"));
            items.Add(new Resist_potion("Resist fire potion"));
            items.Add(new Resist_potion("Resist shock potion"));
            items.Add(new Resist_potion("Resist freeze potion"));
            items.Add(new Resist_potion("antidote"));

            int indiceAtual = 0;
            Console.WriteLine("Oque deseja fazer?\n[1] sepuko \n[Q-E] poção\n[I] inventário\n[spacebar] usar item");
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        Console.WriteLine("Você usou a " + items[indiceAtual].Name);
                        items[indiceAtual].Use();
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
                        if (indiceAtual == items.Count - 1)
                        {
                            //volta do topo
                            indiceAtual = -1;
                        }
                        indiceAtual++;
                        int tentativas = items.Count;
                        Console.WriteLine($"poção atual: {items[indiceAtual].Name}, ID: {items[indiceAtual].Id}");
                        break;
                    case ConsoleKey.Q:
                        //verifica se é o ultimo ID
                        if (indiceAtual == 0)
                        {
                            //volta do topo
                            indiceAtual = items.Count;
                        }
                        indiceAtual--;
                        Console.WriteLine($"poção atual: {items[indiceAtual].Name}, ID: {items[indiceAtual].Id}");
                        break;

                    case ConsoleKey.I:
                      //  Inventory.DisplayInventory();
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

        public class Item
        {
            public int Qty { get; set; }
            protected static int LastId { get; set; } = 0;
            protected int Max_qty { get; set; }
            public virtual string Name { get; set; }
            public int Id { get; set; }
            public Item(int qty = 1)
            {
                LastId++;
                Id = LastId;
                Qty = qty;
                if (Qty > Max_qty)
                {
                    Qty = Max_qty;
                    Console.WriteLine("Limite de poções atingido");
                }
              //  if (Item.)
            }
            public virtual void Use()
            {

            }
        }
        public class Cure_potion : Item
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
                    Qty--;
                }
                else
                {
                    Player.Vida += Strenght;
                }
            }
        }
        public class Resist_potion : Item
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
                    case "Resist freeze potion":
                        int freezeTimer = 90000;
                        break;
                }
            }
        }
        public class Inventory
        {
            private List<Item> items;

            public Inventory()
            {
                items = new List<Item>();
            }

            public bool AddItem(Item item)
            {
                if (items.Count >= 15)
                {
                    Console.WriteLine("Inventário cheio, não é possível adicionar mais itens.");
                    return false;
                }

                else
                {
                    items.Add(item);
                    Console.WriteLine($"Item {item.Name} adicionado ao inventário.");
                    return true;
                }
            }
        }
    }
}

