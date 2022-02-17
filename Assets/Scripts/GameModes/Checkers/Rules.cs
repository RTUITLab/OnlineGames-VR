using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.AllocateViewID(GetComponent<PhotonView>());
    }

}
