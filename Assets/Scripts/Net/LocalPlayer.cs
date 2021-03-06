using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class LocalPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject onlineBodyPref;     //Префаб онлайн тела.
    [SerializeField] private Transform[] bodyPoints;    //Точки тела, которые нужно синхронизировать с оналйн телом.
    private Transform transform;
    private OnlinePlayer onlinePlayer = null;

    private void Awake()
    {
        transform = gameObject.transform;
    }

    public override void OnJoinedRoom() //При входе, создаёт своё тело и запоминает его, что бы менять ему положение.
    {
        FindObjectOfType<MainMenuController>().StartGame();
        GameObject onlineBody = PhotonNetwork.Instantiate(onlineBodyPref.name, transform.position, Quaternion.identity);
        onlinePlayer = onlineBody.GetComponent<OnlinePlayer>();
        onlinePlayer.hideBody();

        FindObjectOfType<LocomotionConstant>().onlinePlayer = this.onlinePlayer;

        SendNickname();
    }

    public void SendChangeTurn()
    {
        onlinePlayer.ChangeTurn();
    }

    public void SendNickname()
    {
        string nickname = PlayerPrefs.GetString("Nickname");
        onlinePlayer.HideNickname();
        onlinePlayer.SendNickname(nickname);
    }

    private void Update()   //Меняет положение своего тела, которое отрпавляеться всем остальным.
    {
        if (PhotonNetwork.IsConnected && onlinePlayer != null)
        {
            for (int i = 0; i < bodyPoints.Length; ++i)
            {
                onlinePlayer.SetTransform(bodyPoints[i], i);
            }
        }
    }
}
