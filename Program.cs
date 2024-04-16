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
            Player player1 = new();
            List<Item> items = [];

            items.Add(new Cure_potion("Health potion", 3, 150));
            items.Add(new Cure_potion("Full health potion", 3));
            items.Add(new Resist_potion("Resist fire potion", 3));
            items.Add(new Resist_potion("Resist shock potion", 3));
            items.Add(new Resist_potion("Resist freeze potion", 3));
            items.Add(new Resist_potion("antidote", 3));
            items.Add(new Trap("Shock trap", 3));
            items.Add(new Trap("Detonating Blast Trap", 3));
            items.Add(new Trap("Blast Trap", 3));

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
                        player1.seppuko();
                        Console.WriteLine(player1.Vida);
                        if (player1.Vida <= 0)
                        {
                            Console.WriteLine("Você morreu");
                            return;
                        }
                        break;
                    case ConsoleKey.E:
                        //verifica se é o ultimo ID
                        if (indiceAtual == items.Count - 1)
                        {
                            //player.
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
            public int Vida { get; set; } = 500;
            public int Vida_Max = 500;
            public Inventory[] Inventories { get; private set;}
            public Player()
            {
                Inventories = new Inventory[1]; // Inicialize a propriedade Inventory com um novo inventário
                Inventories[0] = new Inventory();
                Vida = 500;
                Vida_Max = 500;
            }

            public void seppuko()
            {
                Vida -= int.Parse(Console.ReadLine());
            }

            public void Use(Item item)
            {
                if (item.Qty > 0)
                {
                    item.Qty--;

                }
            }
        }
        public class Inventory
        {
            public Item[] inventory = new Item[5];
            private int Occupied = 0;
            public void AddItem(Item item)
            {
                if (Occupied >= 5)
                {
                    Console.WriteLine("Inventário cheio, não é possível adicionar mais itens.");
                }
                else
                {
                    int loop = 0;
                    while (loop >= 4)
                    {
                        if (inventory[loop].Qty == 0)
                        {
                            break;
                        }
                        loop++;
                    }
                    Occupied++;
                    inventory[loop] = item;
                    Console.WriteLine($"Item {item.Name} adicionado ao inventário.");
                }
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
            }
            public virtual void Use()
            {

            }
        }
        public class Cure_potion : Item
        {
            public int Strenght { get; set; }
            public Cure_potion(string name, int max, int strenght = -1)
            {
                Strenght = strenght;
                Name = name;
                Max_qty = max;
            }
        }
        public class Resist_potion : Item
        {
            public Resist_potion(string name, int max)
            {
                Name = name;
                Max_qty = max;
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
        public class Trap : Item
        {
            public Trap(string name, int max)
            {
                Name = name;
                Max_qty = max;
            }

            public void Use()
            {
                switch (Name)
                {

                }
            }
        }
    }
}

//git revert