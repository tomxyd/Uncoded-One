using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    public class Character
    {

        protected string? _name;
        protected IPlayer _player;

        private int _maxHitPoints;
        private int _hitPoints;
        private List<Action> _actions = new List<Action>();

        public string Name { get { return _name; } set { _name = value; } }
        public int HP { get { return _hitPoints; } set { _hitPoints = value; } }

        public int MaxHP { get { return _maxHitPoints;  } set { _maxHitPoints = value; } }

        public Inventory inventory = new();
        public Item gear = null;

        public bool defeated = false;
        public Character()
        {
            Init();
        }
        public Character(string name)
        {
            _name = name;
        }
        public Character(string name, IPlayer player)
        {
            _name = name;
            _player = player;
        }

        public void Init()
        {
            _hitPoints = _maxHitPoints;
            Action defaultAction = new Action("NOTHING", ActionType.DoNothing);
            _actions.Add(defaultAction);
        }

        public void AddAction(Action action)
        {
            _actions.Add(action);
        }

        public void TakeTurn(List<Character> allies, List<Character> enemies)
        {
            int index;
            ActionType actionType;
            actionType = _player.ChooseAction(this,out index);
            Action action = _actions[index];
            action.PerformAction(action.Type, this, enemies[0]);
            Thread.Sleep(500);
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            if( _hitPoints <= 0 )
            {
                defeated = true;
            }
        }
    }

    public interface IPlayer
    {
        ActionType ChooseAction(Character character, out int index);
    }

    public class HumanPlayer: IPlayer
    {
        public ActionType ChooseAction(Character character, out int index)
        {
            Console.WriteLine("Please pick an action");
            Console.WriteLine("1 - Do Nothing");
            Console.WriteLine("2 - Standard Attack (PUNCH)");
            Console.WriteLine("3 - Use Health Potion");
            Console.WriteLine("4 - Equip Gear");
            if(character.gear != null)
            {
                Console.WriteLine("5 - Use Gear");
            }


            int userInput = 0;
            userInput = int.Parse(Console.ReadLine());
            switch(userInput)
            {
                case 1:
                    index = 0;
                    return ActionType.DoNothing;
                    break;
                case 2:
                    index = 1;
                    return ActionType.Attack;
                    break;
                case 3:
                    index = 2;
                    return ActionType.UseItem;
                    break;
                case 4:
                    index = 3;
                    return ActionType.EquipGear;
                    break;
                default:
                    index = 0;
                    return ActionType.DoNothing;
                    break;
            }
        }
    }

    public class ComputerPlayer : IPlayer
    {
        public ActionType ChooseAction(Character character, out int index)
        {
            index = 0;
            if (character.inventory.items.Count > 0 && character.HP <= character.MaxHP / 2)
            {
                Random rand = new Random();
                int chance = rand.Next(4);
                index = 1;
                if (chance <= 1)
                {
                    index = 2;
                    return ActionType.UseItem;
                }else
                {
                    index = 1;
                    return ActionType.Attack;
                }
            }
            else
            {
                index = 1;
                return ActionType.Attack;
            }
        }
    }
}
