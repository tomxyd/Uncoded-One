using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    public enum ActionType
    {
        DoNothing,
        Attack,
        EquipGear,
        AttackWithGear,
        UseItem
    }

    public enum DamageType
    {
        constant,
        random
    }

    public class Action
    {
        private string _name;

        public Random Rand { get; private set; }

        private int _damage;

        public Action(string name) => this._name = name;

        public Action(string name, ActionType type)
        {
            _name = name;
            Type = type;
        }

        public Action(string name, int damage, ActionType type, DamageType damageType) : this(name)
        {
            _damage = damage;
            Type = type;
            Damage = damageType;
        }

        public string Name { get { return _name; } }
        public ActionType Type { get; set; } = ActionType.DoNothing;
        public DamageType Damage = DamageType.constant;

        public void PerformAction(ActionType type, Character attacker, Character target)
        {
            switch (type)
            {
                case ActionType.DoNothing:
                    Console.WriteLine($"{attacker.Name} did NOTHING");
                    break;
                case ActionType.Attack:
                    Execute(attacker, target);
                    break;
                case ActionType.UseItem:
                    if (attacker.inventory.items.Count == 0)
                    {
                        Console.WriteLine("No more health potion");
                        break;
                    }
                    Item item = attacker.inventory.items[0];
                    UseItem(item, attacker);
                    break;
                case ActionType.EquipGear:
                    Gear gear = null;
                    ChooseGear(attacker, out gear);
                    EquipGear(gear, attacker);
                    break;
                case ActionType.AttackWithGear:
                    ExecuteWithGear(attacker, target);
                    break;
                default:
                    Console.WriteLine("No Action Picked");
                    break;
            }
        }

        public void ExecuteWithGear(Character attacker, Character target)
        {
            _damage = CalculateDamageWithGear(attacker);
            _name = (attacker.gear == null ? _name : attacker.gear.Name);
            Console.WriteLine($"{attacker.Name} used {_name} on {target.Name}");
            target.TakeDamage(_damage);
            Console.WriteLine($"{_name} dealt {_damage} to {target.Name}");
            target.HP = Math.Max(0, target.HP);
            Console.WriteLine($"{target.Name} is now at {target.HP}/{target.MaxHP} HP.\n");
        }
        public void ChooseGear(Character character, out Gear gear)
        {
            gear = null;
            if (character.PlayerType == PlayerType.Human)
            {
                List<int> numGearAllowed = new List<int> { 0 };
                for (int i = 0; i < character.inventory.items.Count; i++)
                {
                    if (character.inventory.items[i].Type == ItemType.Gear)
                    {
                        Console.WriteLine($"{i} - {character.inventory.items[i].Name}");
                        numGearAllowed.Add(i);
                    }
                }

                Console.WriteLine("Please choose gear to equip");
                int userChoice = int.Parse(Console.ReadLine());
                for (int i = 0; i < numGearAllowed.Count; i++)
                {
                    if (userChoice == numGearAllowed[i])
                    {
                        gear = (Gear?)character.inventory.items[userChoice];
                    }
                }
                if(gear == null)
                {
                    Console.WriteLine("Out of Range");
                    ChooseGear(character, out gear);
                }
            }else
            {
                for (int i = 0; i < character.inventory.items.Count; i++)
                {
                    if (character.inventory.items[i].Type == ItemType.Gear)
                    {
                        gear = (Gear?)character.inventory.items[i];
                        break;
                    }
                }
            }
        }

        public int CalculateDamage()
        {
            switch (Damage)
            {
                case DamageType.constant:
                    return _damage;
                    break;
                case DamageType.random:
                    int seed = DateTime.Now.GetHashCode();
                    Rand = new Random(seed);
                    return Rand.Next(1,_damage);
                    break;
                default:
                    return _damage;
                    break;
            }
        }
        public int CalculateDamageWithGear(Character character)
        {
            switch (Damage)
            {
                case DamageType.constant:
                    return character.gear.Damage;
                    break;
                case DamageType.random:
                    int seed = DateTime.Now.GetHashCode();
                    Rand = new Random(seed);
                    return Rand.Next(1, character.gear.Damage);
                    break;
                default:
                    return character.gear.Damage;
                    break;
            }
        }

        public void UseItem(Item item, Character target)
        {
            item.Execute(target);
        }

        public void EquipGear(Gear item, Character target)
        {
            if(target.gear != null)
            {
                target.inventory.Add(target.gear);
                Console.WriteLine($"{target.Name} unequpped {target.gear.Name}");
            }

            target.gear = item;
            target.inventory.items.Remove(item);
            Console.WriteLine($"{target.Name} equpped {item.Name}");

        }

        public void Execute(Character attacker, Character target)
        {
            _damage = CalculateDamage();
            Console.WriteLine($"{attacker.Name} used {_name} on {target.Name}");
            target.TakeDamage( _damage );
            Console.WriteLine($"{_name} dealt {_damage} to {target.Name}");
            target.HP = Math.Max(0, target.HP);
            Console.WriteLine($"{target.Name} is now at {target.HP}/{target.MaxHP} HP.\n");
        }



    }
}
