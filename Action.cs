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
        public DamageType Damage  = DamageType.constant;

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
                    if (attacker.inventory.Items.Count == 0)
                    {
                        Console.WriteLine("No more health potion");
                        break;
                    }
                    Item item = attacker.inventory.Items[0];
                    UseItem(item, attacker);
                    break;
                default:
                    Console.WriteLine("No Action Picked");
                    break;
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

        public void UseItem(Item item, Character target)
        {
            item.Execute(target);
        }

        public void Execute(Character attacker, Character target)
        {
            _damage = CalculateDamage();
            Console.WriteLine($"{attacker.Name} used {_name} on {target.Name}");
            target.TakeDamage( _damage );
            Console.WriteLine($"{_name} dealt {_damage} to {target.Name}");
            Console.WriteLine($"{target.Name} is now at {target.HP}/{target.MaxHP} HP.\n");
        }
    }
}
