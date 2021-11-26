using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSetup : MonoBehaviour
{
    public GameModeSetup Instance;
    [SerializeField] private Text playersCountOutput;
    [SerializeField] private GameObject[] gameModes;
    [SerializeField] private GameObject[] disableOnGameStart;


    private void Start()
    {
        Instance = this;
    }

    public void UpdatePlayersCount()
    {
        playersCountOutput.text = $"Игроков в комнате {PhotonNetwork.PlayerList.Length} из {2}";
    }

    public void ChooseGameMode(int gameMode)
    {
        gameModes[gameMode].SetActive(true);

        foreach (var d in disableOnGameStart)
            d.SetActive(false);
    }
}
