using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Gamemode1;
using System.Collections;

public class CheckersBoard : MonoBehaviour
{
    public int BlackLeft;
    public int WhiteLeft;

    public Piece[,] Board;
    public Piece SelectedPawn;

    private Game CheckersGame;
    private bool noAI = false;

    public string gameMode = "single"; // "single" / "multi"

    [SerializeField] private GameModeCheckersOnline manager;

    [SerializeField] private Material whitePieceMaterial;
    [SerializeField] private Material blackPieceMaterial;
    [SerializeField] private Material chosenPieceMaterial;

    public void Awake()
    {
        UIData.GameMode = gameMode;
        BlackLeft = 12;
        WhiteLeft = 12;
        Board = new Piece[8, 8];
        CheckersGame = new Game(this);
        manager.SetGame(CheckersGame);
        BoardSetup();
        DisplayBoard();
        StartCoroutine(SlowLoop());
    }


    private IEnumerator SlowLoop()
    {
        while (true)
        {
            CheckersGame.UpdateValidMoves();

            // 5 times per second.
            yield return new WaitForSeconds(0.2f);
        }
    }

    private string ConvertPiece(Piece piece)
    {
        if (piece.PieceGameObject != null)
        {
            return piece.Color == PieceColor.White ? "<color=red>O</color>" : "<color=black>O</color>";
        }
        else
        {
            return "<color=gray>O</color>";
        }
    }

    public void DisplayBoard()
    {
        string result = "";

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                result += " " + ConvertPiece(Board[i, 7 - j]);
            }
            result += '\n';
        }

        print($"{result}");
    }

    private void BoardSetup()
    {
        Piece[] pieces = transform.parent.GetComponentsInChildren<Piece>();

        foreach (var piece in pieces)
        {
            Board[piece.Row, piece.Col] = piece;
        }
    }

    public void TookPiece(int row, int col)
    {
        if (CheckersGame.Turn == CheckersGame.Player || noAI)
        {
            CheckersGame.Select(row, col);
        }
    }

    public void Update()
    {
        DisplayBoard();

        if (CheckersGame.Player != CheckersGame.Turn && !noAI)
        {
            if (UIData.GameMode == "single")
            {
                CheckersGame.UpdateValidMoves();
                CheckersGame.AI.FindBestMove(Board);
                KeyValuePair<KeyValuePair<int, int>, List<Piece>> move = CheckersGame.AI.BestMove;
                Piece piece = CheckersGame.AI.BestPiece;
                CheckersGame.Select(piece.Row, piece.Col);
                CheckersGame.Select(move.Key.Key, move.Key.Value);
            }
            else if (UIData.GameMode == "multi")
            {
                //Client client = FindObjectOfType<Client>();
                //if (client.EnemyMove.x >= 0)
                //{
                //    Debug.Log("Enemy Move " + client.EnemyMove);
                //    CheckersGame.Select((int)client.EnemyMove.x, (int)client.EnemyMove.y);
                //    client.EnemyMove = new Vector2(-1.0f, -1.0f);
                //}
            }
        }
    }

    public Piece GetPiece(int row, int col)
    {
        return Board[row, col];
    }

    public void Move(Piece piece, int row, int col)
    {
        // TODO sent this networking command

        Piece temp = Board[piece.Row, piece.Col];
        Board[piece.Row, piece.Col] = Board[row, col];
        Board[row, col] = temp;

        piece.Move(row, col);

        if ((row == 7 && piece.Color == PieceColor.White) ||
            (row == 0 && piece.Color == PieceColor.Black))
        {
            MakeKing(row, col);
        }
    }

    private void MakeKing(int x, int y)
    {
        if (Board[x, y].PieceGameObject == null)
        {
            return;
        }

        Board[x, y].MakeKing();
    }

    public void DrawValidMoves()
    {
        if (CheckersGame.Turn != CheckersGame.Player && UIData.GameMode == "single")
            return;
        DontDrawValidMoves();
        if (CheckersGame.ValidMoves == null)
            return;
        foreach (KeyValuePair<KeyValuePair<int, int>, List<Piece>> move in CheckersGame.ValidMoves)
        {
            int row = move.Key.Key;
            int col = move.Key.Value;
            GeneratePosibleMove(row, col);
        }
    }

    private void GeneratePosibleMove(int row, int col)
    {
        Debug.Log($"GeneratePosibleMove [{row},{col}]");
        Board[row, col].GetComponent<Renderer>().material = chosenPieceMaterial;
    }

    public void DontDrawValidMoves()
    {
        foreach (var piece in Board)
        {
            piece.GetComponent<Renderer>().material = piece.StartMaterial;
        }
    }

    public void RemovePieces(List<Piece> skipped)
    {
        if (skipped == null)
            return;

        foreach (Piece skip in skipped)
        {
            Destroy(skip.PieceGameObject); // TODO move it to the back
            Board[skip.Row, skip.Col] = null;
            if (skip.Color == PieceColor.Black)
            {
                // TODO move it to the back to the white player
                BlackLeft--;
            }
            else
            {
                WhiteLeft--;
            }
        }
    }

    public void CheckWinner()
    {
        if (WhiteLeft <= 0)
        {
            UIData.Winner = "Black";
        }
        else if (BlackLeft <= 0)
        {
            UIData.Winner = "White";
        }
        else
            return;
        CheckersGame.End = true;
        
        // TODO 
    }

    public void MovePiece(int row, int col, GameObject gameObject)
    {
        gameObject.transform.position = Board[row, col].transform.position + Vector3.up * 1.219256f;
    }

    public void LoseGame()
    {
        if (CheckersGame.Turn == PieceColor.Black)
            BlackLeft = 0;
        else
            WhiteLeft = 0;
        CheckWinner();
    }
}