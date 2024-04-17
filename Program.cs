using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;
using System.Timers;

namespace Objetos
{
    public class Objeto()
    {
        //static System.Timers.Timer timer;
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
            items.Add(new Trap("Detonating blast trap", 3));
            items.Add(new Trap("Blast trap", 3));

            int indiceAtual = 0;
            Console.WriteLine("Oque deseja fazer?\n[1] sepuko \n[Q-E] poção\n[I] inventário\n[spacebar] usar item");
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key)
                {
                    case ConsoleKey.Spacebar:
                        player1.Use();
                        //Console.WriteLine("Você usou a " + player1.inventory.inventory[indiceAtual].Name);
                        //items[indiceAtual].Use();
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
                        player1.Next();
                        break;
                    case ConsoleKey.Q:
                        player1.Back();
                        break;

                    case ConsoleKey.R:
                        bool Loop = true; 
                        while (Loop)
                        {
                            ConsoleKeyInfo keyInf = Console.ReadKey(true);
                            items[indiceAtual].Qty++;
                            Console.WriteLine("você tem" + items[indiceAtual].Qty + " de " + items[indiceAtual].Name);
                            switch (keyInf.Key)
                            {
                                case ConsoleKey.E:
                                    //verifica se é o ultimo ID
                                    if (indiceAtual == items.Count - 1)
                                    {

                                        //volta do topo
                                        indiceAtual = -1;
                                    }
                                    indiceAtual++;
                                    int tentativas = items.Count;
                                    Console.WriteLine($"poção atual: {items[indiceAtual].Name}, ID: {items[indiceAtual].Id}\nquantidade: {items[indiceAtual].Qty}");
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
                                case ConsoleKey.R:
                                    Loop = false;
                                    break;
                            }
                        }
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
            private int SelectedItem;

            public Inventory inventory;
            public Player()
            {
                inventory = new Inventory();
                Vida = 500;
                Vida_Max = 500;
                SelectedItem = 1;
            }

            public void Next()
            {
                Console.WriteLine("verificação externa");
                if(inventory.inventory.Length != 0)
                {
                    Console.WriteLine("verificação interna");
                
                if (SelectedItem == 5)
                {
                    SelectedItem = 0;
                }
                int tentativas = 5;
                while (inventory.inventory[SelectedItem].Qty == 0 && tentativas > 0)
                {
                    SelectedItem++;
                    tentativas--;
                    if (tentativas == 0 && inventory.inventory[SelectedItem].Qty == 0)
                    {
                        Console.WriteLine("Sem nenhum item no inventario");
                    }
                }
                if (inventory.inventory[SelectedItem].Qty > 0)
                {
                    Console.WriteLine("Item Selecionado: " + inventory.inventory[SelectedItem].Name);
                }
                }
            }

            public void Back()
            {
                if(inventory.inventory.Length == 0)
                {
                    return;
                }
                SelectedItem--;
                if (SelectedItem == 5)
                {
                    SelectedItem = 0;
                }
                int tentativas = 5;
                while (inventory.inventory[SelectedItem].Qty == 0 && tentativas > 0)
                {
                    SelectedItem--;
                    tentativas--;
                    if (tentativas == 0 && inventory.inventory[SelectedItem].Qty == 0)
                    {
                        Console.WriteLine("Sem nenhum item no inventario");
                    }
                }
                if (inventory.inventory[SelectedItem].Qty > 0)
                {
                    Console.WriteLine("Item Selecionado: " + inventory.inventory[SelectedItem].Name);
                }
            }

            public void seppuko()
            {
                int temp;
                int.TryParse(Console.ReadLine(), out temp);
                Vida -= temp;
            }

            public void Use()
            {
                if (inventory.inventory.Length >= 1)
                {
                    if (inventory.inventory[SelectedItem].Qty <= 0)
                    {
                        Console.WriteLine("Você não tem mais " + inventory.inventory[SelectedItem].Name);
                    }
                    else
                    {
                        inventory.inventory[SelectedItem].Qty--;
                        switch (inventory.inventory[SelectedItem].Name)
                        {
                            case "Resist fire potion":
                                Console.WriteLine("Você usou a poção de resistencia a fogo");
                                break;
                            case "antidote":
                                Console.WriteLine("Você usou o antidoto");
                                break;
                            case "Resist shock potion":
                                Console.WriteLine("Você usou a poção de resistencia a choque");
                                break;
                            case "Resist freeze potion":
                                Console.WriteLine("Você usou a poção de resistencia a congelamento");
                                break;
                            case "Health potion":
                                Vida += 150;
                                break;
                            case "Full health potion":
                                Vida = Vida_Max;
                                break;
                            case "Shock trap":
                                Console.WriteLine("você colocou a armadilha de choque");
                                break;
                            case "Detonating blast trap":
                                Console.WriteLine("você colocou a armadilha explosiva de detonação");
                                break;
                            case "Blast trap":
                                Console.WriteLine("você colocou a armadilha explosiva");
                                break;
                        }
                    }
                }
            }
        }
        public class Inventory
        {
            public Item[] inventory;
            public void AddItem(Item item)
            {
                int slot = 0;
                while (slot >= 4)
                {
                    if (inventory[slot].Qty == 0 || inventory[slot].Name == item.Name)
                    {
                        break;
                    }
                    slot++;
                }
                if (inventory[slot].Name == item.Name)
                {
                    inventory[slot].Qty += 1;
                    Console.WriteLine($"Item {item.Name} adicionado a pilha de {item.Name}.");
                    return;
                }
                inventory[slot] = item;
                Console.WriteLine($"Item {item.Name} adicionado ao inventário.");
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
        }
        public class Trap : Item
        {
            public Trap(string name, int max)
            {
                Name = name;
                Max_qty = max;
            }
            public override void Use()
            {
                Console.WriteLine("você usou a " + Name);
            }
        }
    }
}