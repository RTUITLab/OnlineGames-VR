using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartPlace : MonoBehaviour
{
    [SerializeField] private GameMode3DTicTacToe gameMode;

    private int x;
    private int y;
    private int z;

    private void Start()
    {
        x = Mathf.RoundToInt(transform.localPosition.x);
        y = Mathf.RoundToInt(transform.localPosition.y);
        z = Mathf.RoundToInt(transform.localPosition.z);
    }

    public void Place(int playerId)
    {
        gameMode.Place(playerId, x, y, z);
    }
}
