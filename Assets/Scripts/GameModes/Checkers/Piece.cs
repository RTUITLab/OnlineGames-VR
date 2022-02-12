using System.Collections.Generic;
using UnityEngine;

public enum PieceColor
{
    White,
    Black
}

public class Piece : MonoBehaviour
{
    public int Row, Col;
    private float posX, posZ;

    public Material StartMaterial;
    public PieceColor Color;

    public bool King;

    public GameObject PieceGameObject;
    public Dictionary<KeyValuePair<int, int>, List<Piece>> ValidMoves;

    private void Start()
    {
        StartMaterial = GetComponent<Renderer>().material;
    }

    public override string ToString()
    {
        return $"{Color.ToString()} [{Row}, {Col}]";
    }

    public void Move(int row, int col)
    {
        Row = row;
        Col = col;
        MovePiece();
    }

    public void calcPosition()
    {
        posX = Row;
        posZ = Col;
    }

    public void MovePiece()
    {
        // TODO rewrite

        calcPosition();
        //PieceGameObject.transform.position = ((Vector3.right * posX) + (Vector3.forward * posZ) + boardOffset + pieceOffset);
        //float yRotation = Camera.main.transform.eulerAngles.y;
        //PieceGameObject.transform.eulerAngles = new Vector3(PieceGameObject.transform.eulerAngles.x, -yRotation, PieceGameObject.transform.eulerAngles.z);
    }

    public void MakeKing()
    {
        King = true;
        SpriteRenderer[] kingIcons = PieceGameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (var icon in kingIcons)
        {
            icon.color = new Color(1, 1, 1, 1);
        }
    }
}