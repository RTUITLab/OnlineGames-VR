using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Gamemode1
{
    public class GameModeCheckers : GameMode
    {
        public static GameModeCheckers Instance;
        private Game CheckersGame;

        private Throwable[] figures;

        public void Awake()
        {
            Instance = this;
            figures = GetComponentsInChildren<Throwable>();

            StartCoroutine(SelectTest());
        }

        public void SetGame(Game checkersGame)
        {
            CheckersGame = checkersGame;
        }

        private IEnumerator SelectTest()
        {
            yield return new WaitForSeconds(2);
            CheckersGame.Select(2, 2);
            yield return new WaitForSeconds(2);
            CheckersGame.Select(2, 4);
            yield return new WaitForSeconds(2);
            CheckersGame.Select(2, 6);
            yield return new WaitForSeconds(2);
        }

        public void PlayerWon(int playerId)
        {
            GameCanvas.Instance.SetText($"ИГРОК {playerId} ПОБЕДИЛ!");
        }

        private void Update()
        {
            foreach (var p in CheckersGame.Board.Board)
            {
                p.PieceGameObject = null;
            }

            foreach (var f in figures)
            {
                Piece closest = GetClosestPiece(f.transform);
                if (closest != null)
                {
                    closest.PieceGameObject = f.gameObject;
                    closest.Color = f.gameObject.name.Contains("Black") ? PieceColor.Black : PieceColor.White;
                }
            }
        }

        private Piece GetClosestPiece(Transform t)
        {
            Piece tMin = null;
            float minDist = Mathf.Infinity;
            foreach (var p in CheckersGame.Board.Board)
            {
                float dist = Vector3.Distance(p.transform.position, t.position);
                if (dist < minDist)
                {
                    tMin = p;
                    minDist = dist;
                }
            }

            if (minDist > 0.2f)
                return null;
            else
                return tMin;
        }
    }
}