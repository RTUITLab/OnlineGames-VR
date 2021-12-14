using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSetup : MonoBehaviour
{
    [HideInInspector] public static GameModeSetup Instance;
    [SerializeField] private Text playersCountOutput;
    [SerializeField] private GameObject[] gameModes;
    private PhotonView photonView;

    private void Start()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();
    }

    private int GetPlayerCount()
    {
        return FindObjectsOfType<OnlinePlayer>().Length;
    }

    public void UpdatePlayersCount()
    {
        playersCountOutput.text = $"Игроков в комнате {GetPlayerCount()} из {2}";
    }

    public void ChooseGameMode(int gameMode)
    {
        photonView.RPC("chooseGameMode", RpcTarget.AllViaServer, gameMode);
    }

    [PunRPC]
    public void chooseGameMode(int gameMode)
    {
        gameModes[gameMode].SetActive(true);

        Destroy(transform.parent.gameObject);
    }
}
