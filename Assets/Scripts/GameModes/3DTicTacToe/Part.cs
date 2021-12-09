using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Valve.VR.InteractionSystem;

namespace Gamemode0
{
    public class Part : MonoBehaviour
    {
        public int PlayerId;
        private List<PartPlace> insidePlaces = new List<PartPlace>();
        private PhotonView photonView;

        private void Start()
        {
            photonView = GetComponent<PhotonView>();
            GetComponent<InteractableHoverEvents>().onDetachedFromHand.AddListener(OnDetachFromHand);

            PhotonNetwork.AllocateViewID(photonView);
        }

        public void OnDetachFromHand()
        {
            Debug.Log("OnDetachFromHand, " + insidePlaces.Count);
            if (insidePlaces.Count == 0)
                return;
            Debug.Log("OnDetachFromHand execute");

            // Find nearest place.
            PartPlace nearestPlace = insidePlaces[0];
            foreach (var place in insidePlaces)
            {
                if (Vector3.Distance(transform.position, place.transform.position) <
                    Vector3.Distance(transform.position, nearestPlace.transform.position))
                {
                    nearestPlace = place;
                }
            }

            Debug.Log(nearestPlace.transform.localPosition);

            // Place inside of it.
            Place(GameMode3DTicTacToe.Instance.GetPartId(nearestPlace));
        }

        private void Place(int partPlaceId)
        {
            Debug.Log("Part");
            photonView.RPC("PlacePart", RpcTarget.All, partPlaceId);
        }

        [PunRPC]
        private void PlacePart(int partPlaceId)
        {
            Debug.Log("PlacePart");
            GameMode3DTicTacToe.Instance.GetPartPlace(partPlaceId).Place(PlayerId);

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            PartPlace place = other.GetComponent<PartPlace>();
            if (place)
            {
                insidePlaces.Add(place);
            }
            Debug.Log("OnTriggerEnter " + insidePlaces.Count);
        }

        private void OnTriggerExit(Collider other)
        {
            PartPlace place = other.GetComponent<PartPlace>();
            if (place)
            {
                insidePlaces.Remove(place);
            }
            Debug.Log("OnTriggerExit " + insidePlaces.Count);
        }
    }
}