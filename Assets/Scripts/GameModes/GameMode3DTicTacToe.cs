using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode3DTicTacToe : GameMode
{
    private const int size = 3;
    private int[,,] field = new int[size, size, size];

    private int step = 0;
    [SerializeField] private GameObject[] crosses;
    [SerializeField] private GameObject[] circles;

    private void Start()
    {
        Debug.Log(gameObject.name + " is active.");

        NextStep();
    }

    public void NextStep()
    {
        crosses[step].SetActive(true);
        circles[step].SetActive(true);
        step++;
    }

    private bool IsWinner(int playerId)
    {
        //for (int i = 0; i < size; i++)
        //{
        //    for (int j = 0; j < size; j++)
        //    {
        //        if (field[i, j, 0] == playerId &&
        //            field[i, j, 1] == playerId &&
        //            field[i, j, 2] == playerId)
        //        {
        //            return true;
        //        }

        //        if (field[i, 0, j] == playerId &&
        //            field[i, 1, j] == playerId &&
        //            field[i, 2, j] == playerId)
        //        {
        //            return true;
        //        }

        //        if (field[0, i, j] == playerId &&
        //            field[1, i, j] == playerId &&
        //            field[2, i, j] == playerId)
        //        {
        //            return true;
        //        }
        //    }
        //}
        return false;
    }
}
