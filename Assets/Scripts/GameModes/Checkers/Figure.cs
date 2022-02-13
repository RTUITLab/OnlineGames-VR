using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamemode1
{
    public class Figure : MonoBehaviour
    {
        [HideInInspector] public int Row, Col;
        public bool King;
        private PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            PhotonNetwork.AllocateViewID(photonView);
        }

        public void OnPickUp()
        {
            GameModeCheckersOnline.Instance.CheckersGame.Select(Row, Col);
        }

        public void OnDetach()
        {
            GameModeCheckersOnline.Instance.CheckersGame.Deselect();
        }
    }
}
