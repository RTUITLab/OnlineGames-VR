using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Gamemode1
{
    public class GameModeCheckersOnline : GameMode
    {
        public static GameModeCheckersOnline Instance;
        public Game CheckersGame;

        private Throwable[] figures;

        public void Awake()
        {
            Instance = this;
            figures = GetComponentsInChildren<Throwable>();
        }

        public void SetGame(Game checkersGame)
        {
            CheckersGame = checkersGame;
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
                if (!f.gameObject.name.Contains("Figure"))
                    continue;

                Piece closest = GetClosestPiece(f.transform);
                if (closest != null)
                {
                    closest.PieceGameObject = f.gameObject;
                    closest.Color = f.gameObject.name.Contains("Black") ? PieceColor.Black : PieceColor.White;
                    f.gameObject.GetComponent<Figure>().Row = closest.Row;
                    f.gameObject.GetComponent<Figure>().Col = closest.Col;
                    closest.King = f.gameObject.GetComponent<Figure>().King;
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