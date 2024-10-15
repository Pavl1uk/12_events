using System;
using System.Collections.Generic;
using System.Linq;

public class King
{
    public string Name { get; private set; }
    public King(string name)
    {
        this.Name = name;
    }
    public void OnAttack()
    {
        Console.WriteLine($"King {this.Name} is under attack!");
    }
}
public interface IUnit
{
    string Name { get; }
    void RespondToAttack();
}
public class Footman : IUnit
{
    public string Name { get; private set; }
    public Footman(string name)
    {
        this.Name = name;
    }
    public void RespondToAttack()
    {
        Console.WriteLine($"Footman {this.Name} is panicking!");
    }
}
public class RoyalGuard : IUnit
{
    public string Name { get; private set; }
    public RoyalGuard(string name)
    {
        this.Name = name;
    }
    public void RespondToAttack()
    {
        Console.WriteLine($"Royal Guard {this.Name} is defending!");
    }
}
public class KingGame
{
    private King king;
    private List<IUnit> units;
    public KingGame(King king, List<IUnit> units)
    {
        this.king = king;
        this.units = units;
    }
    public void AttackKing()
    {
        this.king.OnAttack();
        
        foreach (var unit in units)
        {
            unit.RespondToAttack();
        }
    }
    public void KillUnit(string name)
    {
        var unitToKill = this.units.FirstOrDefault(u => u.Name == name);
        if (unitToKill != null)
        {
            this.units.Remove(unitToKill);
        }
    }
}

public class Program
{
    public static void Main()
    {
        string kingName = Console.ReadLine();
        King king = new King(kingName);

        string[] royalGuardNames = Console.ReadLine().Split();
        List<IUnit> units = new List<IUnit>();

        foreach (var name in royalGuardNames)
        {
            units.Add(new RoyalGuard(name));
        }
        
        string[] footmanNames = Console.ReadLine().Split();
        foreach (var name in footmanNames)
        {
            units.Add(new Footman(name));
        }

        KingGame game = new KingGame(king, units);

        string command;
        while ((command = Console.ReadLine()) != "End")
        {
            if (command == "Attack King")
            {
                game.AttackKing();
            }
            else if (command.StartsWith("Kill "))
            {
                string unitName = command.Split()[1];
                game.KillUnit(unitName);
            }
        }
    }
}
