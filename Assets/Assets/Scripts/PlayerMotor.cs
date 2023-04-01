using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private bool running;
    private int combo;
    private Animator anim;

    private float cdTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        cdTimer += Time.deltaTime;

        if(Input.anyKeyDown)
            KeyInputHandler();

        if (cdTimer < 1f)
            return;

        if(Input.GetMouseButtonDown(0))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slash0"))
                anim.Play("Slash1");
            else
                anim.Play("Slash0");

            cdTimer = 0;
        } 
        else if(Input.GetMouseButtonDown(1))
        {
            
        }
    }

    private void KeyInputHandler()
    {
        string input = Input.inputString;

        switch(input)
        {
            case "w":
                break;
            case " ":
                break;
            case "s":
                break;
            case "d":
                break;
            case "a":
                break;
        }
    }
}
