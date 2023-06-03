// See https://aka.ms/new-console-template for more information

using Uncoded_One;

Game g = new Game();

string userName = " ";

Console.WriteLine("Please enter a name for your character:");
userName = Console.ReadLine();

// ITEM CREATION
HealthPotion potion1 = new HealthPotion(10);
HealthPotion potion2 = new HealthPotion(10);

// SETTING UP PLAYER CHARACTER
Character hero = new Character(userName, new HumanPlayer());
hero.MaxHP = 25;
hero.Init();
hero.inventory.Add(potion1);
hero.inventory.Add(potion2);

// ACTIONS INITIATION
Uncoded_One.Action punch = new Uncoded_One.Action("punch", 1, ActionType.Attack, DamageType.constant);
Uncoded_One.Action health = new Uncoded_One.Action("health", ActionType.UseItem);

hero.AddAction(punch);
hero.AddAction(health);

Character monster = new Character("SKELETON", new ComputerPlayer());
Character monster1 = new Character("SKELETON1", new ComputerPlayer());
Character monster2 = new Character("SKELETON2", new ComputerPlayer());
Character uncodedOne = new Character("The Uncoded One", new ComputerPlayer());

monster.MaxHP = 5;
monster.Init();

monster1.MaxHP = 5;
monster1.Init();

monster2.MaxHP = 5;
monster2.Init();

uncodedOne.MaxHP = 15;
uncodedOne.Init();

Uncoded_One.Action crunch = new Uncoded_One.Action("crunch", 2, ActionType.Attack, DamageType.random);
Uncoded_One.Action unraveling = new Uncoded_One.Action("unraveling", 3, ActionType.Attack, DamageType.random);

monster.AddAction(crunch);
monster1.AddAction(crunch);
monster2.AddAction(crunch);
uncodedOne.AddAction(unraveling);


g.heroParty.AddCharcter(hero);

g.monsterParty.AddCharcter(monster);

g.monster2Party.AddCharcter(monster1);

g.monster2Party.AddCharcter(monster2);

g.bossParty.AddCharcter(uncodedOne);

g.monsterParties.Add(g.monsterParty);
g.monsterParties.Add(g.monster2Party);
g.monsterParties.Add(g.bossParty);


g.StartGame();
