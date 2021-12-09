using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode3DTicTacToe : GameMode
{
    public static GameMode3DTicTacToe Instance;

    private int[,,] field = new int[3, 3, 3];
    private List<int[,]> flatFields = new List<int[,]>();

    [SerializeField] private GameObject partPlacesParent;
    private PartPlace[] partPlaces;

    private int step = 0;
    [SerializeField] private GameObject[] crosses;
    [SerializeField] private GameObject[] circles;

    private void Start()
    {
        Debug.Log(gameObject.name + " is active.");

        Instance = this;

        partPlaces = partPlacesParent.GetComponentsInChildren<PartPlace>();

        NextStep(2);
    }

    public int GetPartId(PartPlace partPlace)
    {
        for (int i = 0; i < partPlaces.Length; i++)
        {
            if (partPlace.transform.position == partPlaces[i].transform.position)
                return i;
        }
        return 0;
    }

    public PartPlace GetPartPlace(int id)
    {
        return partPlaces[id];
    }

    public void NextStep(int playerId)
    {
        if (playerId == 1) // Cross, next Circle.
            circles[step / 2].SetActive(true);
        else
            crosses[step / 2].SetActive(true);

        step++;
    }

    private void ConvertToFlatFields()
    {
        flatFields = new List<int[,]>();

        // 9 standard fields.
        for (int i = 0; i < 3; i++)
        {
            flatFields.Add(new int[3, 3]);
            flatFields.Add(new int[3, 3]);
            flatFields.Add(new int[3, 3]);
            for (int a = 0; a < 3; a++)
            {
                for (int b = 0; b < 3; b++)
                {
                    flatFields[3 * i][a, b] = field[a, b, i];
                    flatFields[3 * i + 1][a, b] = field[a, i, b];
                    flatFields[3 * i + 2][a, b] = field[i, a, b];
                }
            }
        }

        // 6 diagonal fields.
        for (int i = 0; i < 6; i++)
        {
            flatFields.Add(new int[3, 3]);
        }
        for (int a = 0; a < 3; a++)
        {
            for (int b = 0; b < 3; b++)
            {
                flatFields[9][a, b] = field[a, a, b];
                flatFields[10][a, b] = field[a, 2 - a, b];
                flatFields[11][a, b] = field[a, b, a];
                flatFields[12][a, b] = field[a, b, 2 - a];
                flatFields[13][a, b] = field[a, b, a];
                flatFields[14][a, b] = field[a, b, 2 - a];
            }
        }
    }

    public void Place(int playerId, int x, int y, int z)
    {
        field[x, y, z] = playerId;

        int winner = FindWinner();
        if (winner == 0)
        {
            NextStep(playerId);
        } else
        {
            Debug.Log("WINNER: " + winner);
            // TODO Player 1 or Player 2 won.
        }
    }

    public int FindWinner()
    {
        ConvertToFlatFields();

        bool player1 = IsWinner(1);
        bool player2 = IsWinner(2);

        if (player1)
            return 1;
        if (player2)
            return 2;
        else
            return 0;
    }

    private bool IsWinner(int playerId)
    {
        foreach (var flatField in flatFields)
        {
            for (int i = 0; i < 3; i++)
            {
                // Check all rows.
                if (flatField[i, 0] == playerId &&
                    flatField[i, 1] == playerId &&
                    flatField[i, 2] == playerId)
                    return true;

                // Check all columns.
                if (flatField[0, i] == playerId &&
                    flatField[1, i] == playerId &&
                    flatField[2, i] == playerId)
                    return true;
            }

            // Check 1st diagonal.
            if (flatField[0, 0] == playerId &&
                flatField[1, 1] == playerId &&
                flatField[2, 2] == playerId)
                return true;

            // Check 2nd diagonal.
            if (flatField[0, 2] == playerId &&
                flatField[1, 1] == playerId &&
                flatField[2, 0] == playerId)
                return true;
        }

        return false;
    }
}
