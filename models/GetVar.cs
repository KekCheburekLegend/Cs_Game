using GameBattle.controllers;
using QuakeConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBattle.models
{
    public static class GetVar
    {
        public static void AddMyVarGame(PythonInterpreter interpreter, Player player1, Player player2)
        {
            if (interpreter == null) return;

            interpreter.AddVariable("player1", player1);
            interpreter.AddVariable("player2", player2);
        }
    }
}
