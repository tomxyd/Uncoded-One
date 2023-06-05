// See https://aka.ms/new-console-template for more information

using Uncoded_One;

Game g = new Game();

string? userName = " ";

Console.WriteLine("Please enter a name for your character:");
userName = Console.ReadLine();

// ITEM CREATION
HealthPotion potion1 = new HealthPotion(10);
HealthPotion potion2 = new HealthPotion(10);
HealthPotion standardPotion = new HealthPotion(15);
Gear sword = new Gear("slash", 2);
Gear dagger = new Gear("dagger", 1);

// SETTING UP PLAYER CHARACTER
Character hero = new Character(userName, new HumanPlayer(), PlayerType.Human);
hero.MaxHP = 25;
hero.Init();
hero.inventory.Add(potion1);
hero.inventory.Add(potion2);
hero.gear = sword;
hero.inventory.Add(dagger);

// ACTIONS INITIATION
Uncoded_One.Action punch = new Uncoded_One.Action("punch", 1, ActionType.Attack, DamageType.constant);
Uncoded_One.Action health = new Uncoded_One.Action("health", ActionType.UseItem);
Uncoded_One.Action gear = new Uncoded_One.Action("gear", ActionType.EquipGear);
Uncoded_One.Action attackWithGear = new Uncoded_One.Action("GearAttack", ActionType.AttackWithGear);

hero.AddAction(punch);
hero.AddAction(health);
hero.AddAction(gear);
hero.AddAction(attackWithGear);

Character monster = new Character("SKELETON", new ComputerPlayer(), PlayerType.Computer);
Character monster1 = new Character("SKELETON1", new ComputerPlayer(), PlayerType.Computer);
Character monster2 = new Character("SKELETON2", new ComputerPlayer(), PlayerType.Computer);
Character uncodedOne = new Character("The Uncoded One", new ComputerPlayer(), PlayerType.Computer);

monster.MaxHP = 5;
monster.Init();
monster.gear = dagger;

monster1.MaxHP = 5;
monster1.Init();

monster2.MaxHP = 5;
monster2.Init();

uncodedOne.MaxHP = 15;
uncodedOne.Init();
//uncodedOne.inventory.Add(standardPotion);



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

for (int i = 0; i < g.monsterParties.Count; i++)
{
    foreach(Character character in g.monsterParties[i].characters)
    {
        character.inventory.Add(standardPotion);
        character.inventory.Add(dagger);
        character.inventory.Add(dagger);

        character.AddAction(health);
        character.AddAction(gear);
        character.AddAction(attackWithGear);
    }
}


g.StartGame();
