using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamemode1
{
    public class GameModeCheckers : GameMode
    {
        public static GameModeCheckers Instance;

        private int[,] field = new int[8, 8];

        private int step = 0;

        private void Start()
        {
            Instance = this;

            NextStep(2);
        }

        public void NextStep(int playerId)
        {
            // TODO

            step++;
        }

        public void TryPlace(int playerId, int x, int y)
        {
            // TODO Check if can move
            // TODO Check if eat

            field[x, y] = playerId;

            int winner = FindWinner();
            if (winner == 0)
            {
                NextStep(playerId);
            }
            else
            {
                GameCanvas.Instance.SetText($"ИГРОК {winner} ПОБЕДИЛ!");
            }
        }

        public int FindWinner()
        {
            int player1 = Count(1);
            int player2 = Count(2);

            if (player1 == 0)
                return 2;
            if (player2 == 0)
                return 1;
            else
                return 0;
        }

        private int Count(int playerId)
        {
            int result = 0;

            foreach (var x in field)
            {
                if (playerId == x)
                    result++;
            }

            return result;
        }
    }
}