using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private State currentState;
    private int combo;
    private Animator anim;

    public enum State
    {
        Idle,
        Crouching,
        Running,
        Walking,
        Attacking
    }

    private float cdTimer;

    private void Awake()
    {
        currentState = State.Idle;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (currentState == State.Running)
        {
            transform.position += Vector3.forward * Time.deltaTime * 2f;
            anim.Play("RunSword");
        }
        else if (currentState == State.Walking)
        {
            transform.position += Vector3.forward * Time.deltaTime * 0.8f;
            anim.Play("WalkSword");
        }
        else if (currentState == State.Crouching)
            anim.Play("CrouchIdle");
        else if (currentState == State.Idle && currentState != State.Attacking)
            anim.Play("Idle");

        cdTimer += Time.deltaTime;

        if (Input.anyKeyDown)
            KeyInputHandler();
        else if (Input.GetKey(KeyCode.LeftControl))
            ControlsInputHandler();
        else
            currentState = State.Idle;

        if (cdTimer < 1f)
            return;

        AttackInputHandler();
    }

    private void AttackInputHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentState == State.Crouching)
                anim.Play("CrouchSlash");
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slash0"))
                anim.Play("Slash1");
            else
                anim.Play("Slash0");
            currentState = State.Attacking;
            cdTimer = 0;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Left Click");
        }
    }


    private void ControlsInputHandler()
    {
        currentState = State.Crouching;
    }

    private void KeyInputHandler()
    {
        string input = Input.inputString;

        switch(input)
        {
            case "w":
                if (Input.GetKey(KeyCode.LeftShift))
                    anim.Play("RunSword");
                else
                    anim.Play("WalkSword");
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
