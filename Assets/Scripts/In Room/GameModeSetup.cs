using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSetup : MonoBehaviour
{
    [HideInInspector] public GameModeSetup Instance;
    [SerializeField] private Text playersCountOutput;
    [SerializeField] private GameObject[] gameModes;
    private PhotonView photonView;

    private void Start()
    {
        Instance = this;
        photonView = GetComponent<PhotonView>();
    }

    public void UpdatePlayersCount()
    {
        playersCountOutput.text = $"Игроков в комнате {PhotonNetwork.PlayerList.Length} из {2}";
    }

    public void ChooseGameMode(int gameMode)
    {
        photonView.RPC("chooseGameMode", RpcTarget.AllViaServer, gameMode);
    }

    [PunRPC]
    public void chooseGameMode(int gameMode)
    {
        Instantiate(gameModes[gameMode]);

        Destroy(transform.parent.gameObject);
    }
}
