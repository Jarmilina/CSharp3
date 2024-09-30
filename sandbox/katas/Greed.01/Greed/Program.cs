using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

var greedGame = new GreedGame();

var dices = greedGame.ReturnDices();

foreach (var d in dices)
{
    Console.WriteLine(d);
}

var results = greedGame.CountNumbers(dices);

var score = greedGame.CountScore(results);

Console.WriteLine(score);
