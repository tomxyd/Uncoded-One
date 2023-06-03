using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    public class Inventory
    {
        public List<Item> Items;

        private int _limit = 10;
        private int _maxWeight = 10;
        private float _maxVolume = 10;

        private int? _currentWeight = 0;
        private float? _currentVolume = 0;
        private int _size = 0;

        public int Limit => _limit;
        public int MaxWeight => _maxWeight;
        public float MaxVolume => _maxVolume;

        public int Size => _size;


        public Inventory()
        {
            Items = new List<Item>();
        }

        public void Add(Item item)
        {
            if (CanAdd(item))
            {
                Console.WriteLine($"{item.Name} added");
            }
            else
                Console.WriteLine("item not added");
        }

        private bool CanAdd(Item item)
        {
            
            if(item.Weight + _currentWeight <= _maxWeight && item.Volume + _currentVolume <= _maxVolume && _size + 1 <= _limit)
            {
                Items.Add(item);
                _size++;
                _currentVolume += item.Volume;
                _currentWeight += item.Weight;
                return true;
            }else return false;
        }

    }

    public abstract class Item
    {
        protected string? _name;
        protected int? _weight;
        protected float? _volume;
        public string Name { get { return _name;} }
        public float? Volume => _volume;
        public int? Weight => _weight;

        public Item() { }
        public Item(string name, int weight = 0, float volume = 0)
        {
            _name = name;
            _weight = weight;
            _volume = volume;
        }

        public abstract void Execute(Character character);
    }

    public class HealthPotion: Item
    {
        private int _healAmount;
        public HealthPotion()
        {
            _name = "HealthPotion";
            _weight = 1;
            _volume = .5f;
        }

        public HealthPotion(int amount)
        {
            _name = "HealthPotion";
            _weight = 1;
            _volume = .5f;
            _healAmount = amount;
        }

        public override void Execute(Character character)
        {

            _healAmount = Math.Min(character.MaxHP - character.HP, _healAmount);
            character.HP += _healAmount;
            Console.WriteLine($"{character.Name} used a health potion. {character.Name} gained {_healAmount} HP");
            character.inventory.Items.RemoveAt(0);
        }

    }


}
