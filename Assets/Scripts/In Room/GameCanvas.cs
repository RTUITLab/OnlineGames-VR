using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour 
{
    [HideInInspector] public static GameCanvas Instance;
    [SerializeField] private Text headerText;

    public void Start()
    {
        Instance = this;
    }

    public void SetText(string headerText)
    {
        this.headerText.text = headerText;
    }
}
 