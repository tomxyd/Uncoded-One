using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uncoded_One
{
    internal class Game
    {
        public Party heroParty = new Party();
        public Party monsterParty = new Party();
        public Party monster2Party = new Party();
        public Party bossParty = new Party();

        public List<Party> monsterParties = new List<Party>();


        public void Battle(Party heroes, Party monsters)
        {
            int round = 0;
            bool isGameOver = false;

            while(!isGameOver)
            {
                foreach(Character hero in heroes.characters)
                {
                    Character? defeatedHero = null;
                    defeatedHero = CheckCharacterStatus(hero);
                    isGameOver = true;
                    if (defeatedHero != null)
                    {
                        heroes.characters.Remove(hero);
                        defeatedHero = null;
                    }

                    if(heroes.characters.Count > 0)
                    {
                        Console.WriteLine($"It is {hero.Name} turn");
                        hero.TakeTurn(heroes.characters, monsters.characters);
                    }else
                    {
                        isGameOver = true;
                        break;
                    }

                }

                for (int i = 0; i < monsters.characters.Count; i++)
                {
                    Character monster = monsters.characters[i];
                    Character? defeatedCharacter = null;
                    defeatedCharacter = CheckCharacterStatus(monster);
                    if (defeatedCharacter != null)
                    {
                        monsters.characters.Remove(monster);
                        defeatedCharacter = null;
                    }

                    if (monsters.characters.Count <= 0)
                    {
                        isGameOver = true;
                        break;
                    }
                    else
                    {

                        if (monsters.characters.Count == 1)
                        {
                            monster = monsters.characters[0];
                        }
                        Console.WriteLine($"It is {monster.Name} turn");
                        monster.TakeTurn(monsters.characters, heroes.characters);
                    }

                }

                round++;
            }
        }
        public Character? CheckCharacterStatus(Character character)
        {
            if (character.defeated)
            {
                Console.WriteLine($"{character.Name} has been defeated");
                return character;
                //characters.characters.Remove(hero);
            }
            else
                return null;
        }
        

        public void DisplayUI(Party heroes, Party monsters)
        {
            foreach(Character hero in heroes.characters)
            {
                Console.WriteLine("=======================BATTLE=========================");
                Console.WriteLine($"{hero.Name}   {hero.HP}/{hero.MaxHP}");
                Console.WriteLine("------------------VS----------------------------------");
                for (int i = 0; i < monsters.characters.Count; i++)
                {
                    Character monster = monsters.characters[i];
                    Console.WriteLine($"\t\t\t{monster.Name} {monster.HP}/{monster.MaxHP}");                   
                }
                Console.WriteLine("======================================================");
            }
        }

        public void StartGame()
        {
            for(int i = 0; i < monsterParties.Count; i++)
            {
                Console.WriteLine($"Loading Battle {i + 1} \n");
                if (monsterParties.Count > 0 && heroParty.characters.Count > 0)
                {
                    DisplayUI(heroParty, monsterParties[i]);
                    Battle(heroParty, monsterParties[i]);
                    EndGame();
                }
            }
        }

        public void EndGame()
        {
            if(heroParty.characters.Count > 0)
            {
                Console.WriteLine($"The heroes won this battle");
            }else
            {
                Console.WriteLine("The Uncoded one won this battle");           
            }
        }

    }
}
