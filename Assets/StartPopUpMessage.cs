using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class StartPopUpMessage 
{
    public static void Message(string message, Color color)
    {
        GameObject go = GameObject.Instantiate(GameManager.instance.popUpPrefab,GameManager.instance.mainCanvas);
        TMP_Text text = go.GetComponent<TMP_Text>();
        text.text = message;
        text.color = color;
    }


}
