using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamemode1
{
    public class GameModeCheckers : GameMode
    {
        public static GameModeCheckers Instance;

        private void Start()
        {
            Instance = this;
        }

        public void PlayerWon(int playerId)
        {
            GameCanvas.Instance.SetText($"ИГРОК {playerId} ПОБЕДИЛ!");
        }
    }
}