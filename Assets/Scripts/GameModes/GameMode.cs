using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log(gameObject.name + " is active.");
    }
}