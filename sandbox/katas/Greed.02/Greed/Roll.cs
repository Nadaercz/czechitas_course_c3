using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Greed;

public class Roll()
{
    private int _dice;
    private int _scope;

    // public Roll(int dice)
    // {
    //     _dice = dice;
    // }

    public int Scope
    {
        get { return _scope; }
        set { _scope = value; }
    }

    public Dictionary<int, int> Numbers { get; set; }

    public void RollTheDice()
    {
        Numbers = new();

        var random = new Random();

        for (int i = 1; i <= _dice; i++)
        {
            int number = random.Next(1, 7);
            Numbers.TryAdd(number, 0);
            Numbers[number]++;
        }
    }

    public void NextChance()
    {
        Numbers.Clear();
        RollTheDice();
    }

    public int GetScope()
    {
        _scope = 0;
        Console.WriteLine("Checking your scope....");
        foreach (var kvp in Numbers)
        {
            CheckScope(kvp, _scope);
        }
        Console.WriteLine($"Your scope is {_scope}");
        return _scope;
    }

    private void CheckScope(KeyValuePair<int, int> kvp, int scope)
    {
        switch (kvp.Key)
        {
            case 1:
                if (kvp.Value == 3)
                    scope += 1000;
                else if (kvp.Value == 1)
                    scope += 100;
                break;
            case 2:
                if (kvp.Value == 3)
                    scope += 200;
                break;
            case 3:
                if (kvp.Value == 3)
                    scope += 300;
                break;
            case 4:
                if (kvp.Value == 3)
                    scope += 400;
                else
                    scope += 0;
                break;
            case 5:
                if (kvp.Value == 3)
                    scope += 500;
                break;
            case 6:
                if (kvp.Value == 3)
                    scope += 600;
                break;
            default:
                Console.WriteLine($"We haven't case for number {kvp.Key}");
                break;
        }

    }
}
