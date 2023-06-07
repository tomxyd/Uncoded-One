using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    public class AttackModifier
    {
        public string Name { get; set; }
        public int Value { get; set; }

        public AttackModifier()
        {
            Name = string.Empty;
        }

        public AttackModifier(string name)
        {
            this.Name = name;
        }
        public AttackModifier(string name, int value)
        {
            this.Name=name;
            this.Value = value;
        }

        public virtual AttackData ModifyAttack(AttackData data)
        {
            data.damage -= Value;
            return data;
        }
    }

    public class Decoder : AttackModifier
    {
        public Decoder(string name, int value)
        {
            this.Name = name;
            this.Value = value;
        }
        public override AttackData ModifyAttack(AttackData data)
        {
            if (data.damageType == DamageType.random)
            {
                data.damage -= 2;
            }

            return data;
        }
    }
    public enum PlayerType
    {
        Computer,
        Human
    }
    public class Character
    {

        protected string _name = "default";
        protected IPlayer _player;
        protected PlayerType _playerType = PlayerType.Human;

        private int _maxHitPoints;
        private int _hitPoints;
        private List<Action> _actions = new List<Action>();

        public string Name { get => _name; set { _name = value; } }
        public int HP { get { return _hitPoints; } set { _hitPoints = value; } }
        public PlayerType PlayerType { get { return _playerType; } set { _playerType = value; } }
        public int MaxHP { get { return _maxHitPoints;  } set { _maxHitPoints = value; } }
        public AttackModifier DefensiveAttackModifier { get; set; }

        public Inventory inventory = new();
        public Gear gear = null;

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

        public Character(string name, IPlayer player, PlayerType type)
        {
            _name = name;
            _player = player;
            _playerType = type;
        }

        public Character(string name, IPlayer player, PlayerType type, AttackModifier modifier)
        {
            _name = name;
            _player = player;
            _playerType = type;
            DefensiveAttackModifier = modifier;
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


            int? userInput = 0;
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
                case 5:
                    index = 4;
                    return ActionType.AttackWithGear;
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
            int chance = 0;
            Random rand = new Random();
            bool equippedGear = false;
            if (character.gear != null)
            {
               equippedGear = true;
            }
            else
                equippedGear = false;
            chance = rand.Next(1, 101);

            if(character.inventory.items.Count > 0 )
            {
                if(character.HP <= character.MaxHP / 2)
                {
                    if(chance <= 25)
                    {
                        index = 2;
                        return ActionType.UseItem;
                    }else
                    {
                        if (equippedGear)
                        {
                            index = 4;
                            return ActionType.AttackWithGear;
                        }
                        else
                        {
                            index = 1;
                            return ActionType.Attack;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < character.inventory.items.Count; i++)
                    {
                        if (character.inventory.items[i].Type == ItemType.Gear && !equippedGear)
                        {
                            if (chance <= 50)
                            {
                                index = 3;
                                equippedGear = true;
                                return ActionType.EquipGear;
                            }
                        }
                        else
                            break;
                    }
                }
            }
            else
            {
                if(equippedGear)
                {
                    index = 4;
                    return ActionType.AttackWithGear;
                }else
                {
                    index = 1;
                    return ActionType.Attack;
                }
            }

            if (equippedGear)
            {
                index = 4;
                return ActionType.AttackWithGear;
            }
            else
            {
                index = 1;
                return ActionType.Attack;
            }
        }
    }

}
