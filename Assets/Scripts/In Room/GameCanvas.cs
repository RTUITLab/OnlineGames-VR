using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour 
{
    [HideInInspector] public GameCanvas Instance;
    [SerializeField] private Text headerText; 

    public void SetText(string headerText)
    {
        this.headerText.text = headerText;
    }
}
 