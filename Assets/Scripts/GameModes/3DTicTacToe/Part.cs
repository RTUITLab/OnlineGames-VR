using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Valve.VR.InteractionSystem;

public class Part : MonoBehaviour
{
    public int PlayerId;
    private List<PartPlace> insidePlaces = new List<PartPlace>();
    private Throwable throwable;


    private void Start()
    {
        throwable = GetComponent<Throwable>();
        throwable.onDetachFromHand.AddListener(OnDetachFromHand);
    }

    public void OnDetachFromHand()
    {
        if (insidePlaces.Count == 0)
            return;

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

        // Place inside of it.
        nearestPlace.Place(PlayerId);
    }

    private void OnTriggerEnter(Collider other)
    {
        PartPlace place = other.GetComponent<PartPlace>();
        if (place)
        {
            insidePlaces.Add(place);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PartPlace place = other.GetComponent<PartPlace>();
        if (place)
        {
            insidePlaces.Remove(place);
        }
    }
}
