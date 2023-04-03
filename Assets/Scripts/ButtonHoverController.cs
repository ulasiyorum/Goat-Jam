using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverController : MonoBehaviour
{
    public Image hoverImage;

    void Start()
    {
        
    }

    
    public void OnPointerEnter()
    {
        hoverImage.gameObject.SetActive(true);
    }

    public void OnPointerExit()
    {
        hoverImage.gameObject.SetActive(false);
    }
}
