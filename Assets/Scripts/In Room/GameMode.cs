using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Text gameHeader;

    private void Start()
    {
        Debug.Log(gameObject.name + " is active.");
    }

    public void UpdateHeader(string text)
    {
        // ХОД ИГРОКА ********

        gameHeader.text = text;
    }
}