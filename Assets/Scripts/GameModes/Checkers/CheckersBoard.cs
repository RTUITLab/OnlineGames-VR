using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckersBoard : MonoBehaviour
{
    public int BlackLeft;
    public int WhiteLeft;

    public Piece[,] Board;
    public Piece SelectedPawn;

    private Game CheckersGame;

    private List<GameObject> possibleMoves;
    private bool noAI = false;

    [SerializeField] private Material whitePieceMaterial;
    [SerializeField] private Material blackPieceMaterial;
    [SerializeField] private Material chosenPieceMaterial;

    public void Awake()
    {
        UIData.GameMode = "multi";
        BlackLeft = 12;
        WhiteLeft = 12;
        Board = new Piece[8, 8];
        CheckersGame = new Game(this);
        possibleMoves = new List<GameObject>();
        BoardSetup();
    }

    private void BoardSetup()
    {
        Piece[] pieces = GetComponentsInChildren<Piece>();

        foreach (var piece in pieces)
        {
            Board[piece.Row, piece.Col] = piece;
        }
    }

    public void TookPiece(Vector2Int pos)
    {
        if (CheckersGame.Turn == CheckersGame.Player || noAI)
        {
            CheckersGame.Select(pos.x, pos.y);
        }
    }

    public void Update()
    {
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
        // TODO fix networking position here

        Piece temp = Board[piece.Row, piece.Col];
        Board[piece.Row, piece.Col] = Board[row, col];
        Board[row, col] = temp;

        piece.Move(row, col);

        if (row == 7 || row == 0)
        {
            MakeKing(row, col);
        }
    }

    private void MakeKing(int x, int y)
    {
        if (Board[x, y] == null)
        {
            return;
        }

        Board[x, y].King = true;
        
        // TODO change mesh
    }

    public void ChangeMaterial(Piece piece, bool selected, bool possibleMove)
    {
        // TODO check this method
        return;

        if (piece == null)
            return;

        if (CheckersGame.Turn != CheckersGame.Player)
        {
            selected = false;
            possibleMove = false;
        }

        if (!selected && piece.Color == PieceColor.White)
        {
            Material[] currentMaterial = piece.PieceGameObject.GetComponent<Renderer>().materials;
            currentMaterial[0] = whitePieceMaterial;
            piece.PieceGameObject.GetComponent<Renderer>().materials = currentMaterial;
        }
        else if (!selected && piece.Color == PieceColor.Black)
        {
            Material[] currentMaterial = piece.PieceGameObject.GetComponent<Renderer>().materials;
            currentMaterial[0] = blackPieceMaterial;
            piece.PieceGameObject.GetComponent<Renderer>().materials = currentMaterial;
        }
        else
        {
            piece.PieceGameObject.GetComponent<Renderer>().material = chosenPieceMaterial;
        }

        if (possibleMove)
        {
            Material[] currentMaterial = piece.PieceGameObject.GetComponent<Renderer>().materials;
            //currentMaterial[1] = chosenPieceMaterial;
            piece.PieceGameObject.GetComponent<Renderer>().materials = currentMaterial;
        }
    }

    public void DrawValidMoves()
    {
        if (CheckersGame.Turn != CheckersGame.Player)
            return;
        DeleteValidMoves();
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
        // TODO rewrite

        //GameObject gameObject = Instantiate(InvisiblePiecePrefab);
        //gameObject.transform.SetParent(transform, true);
        //gameObject.transform.localScale = new Vector3(1, 1, 1);
        //MovePiece(row, col, gameObject);
        //possibleMoves.Add(gameObject);
    }

    public void DeleteValidMoves()
    {
        for (int i = possibleMoves.Count - 1; i >= 0; i--)
        {
            Destroy(possibleMoves[i]);
            possibleMoves.RemoveAt(i);
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

    public void ShowChosenPieces(bool show)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (Board[i, j] != null && Board[i, j].Color == CheckersGame.Turn)
                {
                    if (show && (Board[i, j].ValidMoves != null && Board[i, j].ValidMoves.Count != 0))
                    {
                        ChangeMaterial(Board[i, j], false, true);
                    }
                    else
                    {
                        ChangeMaterial(Board[i, j], false, false);
                    }
                }
            }
        }
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