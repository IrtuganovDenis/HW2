using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace ConsoleApp11
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество грузов");
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Введите количество секторов");
            int v = int.Parse(Console.ReadLine());

            List<Item> a = new List<Item>();

            string[] c = new string[v];
            for (int i = 0; i < v; i++)
            {
                Console.WriteLine("Введите название сектора");
                c[i] = Console.ReadLine();
            }
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите название груза");
                string s = Console.ReadLine();

                Console.WriteLine("Введите вес груза");
                double x = double.Parse(Console.ReadLine());
                Item it = new Item(s, x);

                Console.WriteLine("Введите название сектора для груза");
                it.sector = Console.ReadLine();
                a.Add(it);

            }
            Warehouse wh = new Warehouse(a, c);

            Console.WriteLine("Введите название нового груза");
            string new_item_name = Console.ReadLine();

            Console.WriteLine("Введите вес нового груза");
            double new_item_weight = double.Parse(Console.ReadLine());
            Item new_item = new Item(new_item_name, new_item_weight);

            Console.WriteLine("Введите название сектора для нового груза");
            new_item.sector = Console.ReadLine();
            wh.AddNewItem(new_item);

            Console.WriteLine("Введите название груза, который надо удалить");
            string del_name = Console.ReadLine();
            wh.ShipItems(del_name);

            Console.WriteLine("Введите названия груза для получения его сектора");
            string return_name = Console.ReadLine();
            Item return_item = wh[return_name];
            Console.WriteLine(return_item.sector);

            Console.WriteLine("Введите название сектора для получения грузов в нем");
            string sector_name = Console.ReadLine();
            List<Item> items = wh.GetAllBySector(sector_name);
            foreach (Item i in items)
            {
                Console.WriteLine(i.name_get);
            }

            double total_weight = wh.GetTotalWeight();
            Console.WriteLine("Общий вес всех грузов: " + total_weight);
            Console.WriteLine("Вся информация о секторах и грузах в них");
            wh.GetInfo();
        }
    }
    class Item
    {
        private string Name;
        private double Weight;
        private string Sector;

        public Item(string name, double weight)
        {
            Name = name;
            Weight = weight;
        }

        public string name_get
        {
            get
            {
                return Name;
            }
        }
        public string sector
        {
            get
            {
                return Sector;
            }
            set
            {
                Sector = value;
            }
        }

        public double weight
        {
            get
            {
                return Weight;
            }
        }

        public string Getinfo()
        {
            return Name + Sector;
        }


    }

    class Warehouse
    {
        private List<Item> _items;
        private string[] _sectors;
        private int[] count;

        public Warehouse(List<Item> i, string[] c)
        {
            _items = i;
            _sectors = c;
            count = new int[(_sectors.Length)];
        }

        // int[] count = new int[(_sectors.Length)];

        //Определяем сектор груза и добавляем его на склад
        public bool AddNewItem(Item item)
        {
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] < 10)
                {
                    item.sector = _sectors[i];
                    _items.Add(item);
                    return true;
                }

            }
            return false;
        }

        //Удаляем груз с определенным названием со склада 
        public void ShipItems(string name)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].name_get == name)
                {
                    _items.RemoveAt(i);
                }
            }
        }

        //Получаем груз с определенным названием
        public Item this[string name]
        {
            get
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].name_get == name)
                    {
                        return _items[i];
                    }
                }
                return _items[0];
            }

        }

        //Получаем список всех грузов находящихся в заданном секторе
        public List<Item> GetAllBySector(string sector_name)
        {
            List<Item> a = new List<Item>();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].sector == sector_name)
                {
                    a.Add(_items[i]);
                }
            }
            return a;
        }

        //Получаем вес всех грузов на складе
        public double GetTotalWeight()
        {
            double sum = 0;
            foreach (Item i in _items)
            {
                sum += i.weight;
            }
            return sum;
        }

        //Получаем список грузов с каждого сектора
        public void GetInfo()
        {
            foreach (string i in _sectors)
            {
                Console.WriteLine(i + ":");
                foreach (Item j in _items)
                {
                    if(j.sector == i)
                    {
                        Console.WriteLine(j.name_get + " ");
                    }
                    
                }
            }
        }
    }
}

/*
Введите количество секторов
3
Введите название сектора
A
Введите название сектора
B
Введите название сектора
C
Введите название груза
asd
Введите вес груза
12
Введите название сектора для груза
A
Введите название груза
zxc
Введите вес груза
34
Введите название сектора для груза
B
Введите название нового груза
qwe
Введите вес нового груза
56
Введите название груза, который надо удалить
asd
Введите названия груза для получения его сAектора
zxc
zxc
B
Введите название сектора для получения грузов в нем
A
qwe
90
Вся информация о секторах и грузах в них
A:
zxc
qwe
B:
zxc
qwe
C:
zxc
qwe
*/
