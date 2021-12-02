using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private Text gameHeader;

    public void UpdateHeader(string text)
    {
        // ХОД ИГРОКА ********

        gameHeader.text = text;
    }
}