using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public State currentState;
    private float speed;
    public bool attacking;
    public bool animPlaying;
    private Animator anim;
    public bool attacking2;
    private Vector3 rollDirection;
    public BoxCollider swordCollider;
    private float attackCd;

    public static bool rolling;
    public static PlayerMotor instance;
    public enum State
    {
        Idle,
        Crouching,
        Running,
        Walking
    }

    private void Awake()
    {
        attackCd = 0;
        rollDirection = Vector3.forward;
        swordCollider.enabled = false;
        instance = this;
        currentState = State.Idle;
        anim = GetComponent<Animator>();
    }


    private void Update()
    {

        InputsHandler();
        AnimationController();
        LookAtMouse();

        attackCd += Time.deltaTime;

        if(rolling)
        {
            transform.position += 5f * Time.deltaTime * (transform.rotation * rollDirection);
        }
    }
    private float rotationX = 0f;
    private float rotationY = 0f;
    private void LookAtMouse()
    {
        rotationY += Input.GetAxis("Mouse X") * 2;
        rotationY += Input.GetAxis("Mouse Y") * -2;

        transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);

        //Camera cam = Camera.main;
        //Vector3 mousePos = Input.mousePosition;
        //Vector3 playerPos = transform.position;
        //mousePos.z = cam.nearClipPlane;
        //Vector3 mousePosWorld = cam.ScreenToWorldPoint(mousePos);
        //mousePosWorld.y = playerPos.y;

        //Vector3 direction = mousePosWorld - playerPos;
        //if (Vector3.Dot(cam.transform.forward, direction) > 0)
        //{
        //    direction = -direction;
        //}

        //float distance = Vector3.Distance(playerPos, mousePosWorld);
        //float speed = Mathf.Lerp(1f, 5f, Mathf.InverseLerp(0f, 5f, distance));
        //Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        //speed -= 2.1f;
        //speed *= 100;
        //speed -= 10f;
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed / 10);
    }
    private void InputsHandler()
    {
        bool gotHit = GetHitController.animPlaying;

        if (gotHit)
            return;


        if (Input.GetMouseButtonDown(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Slash1") && !attacking)
        {
            swordCollider.enabled = true;
            attacking = true;
        } else if (Input.GetMouseButtonDown(0) && attackCd > 1.2f && !attacking2)
        {
            attackCd = 0;
            attacking2 = true;
        }
        else if(Input.GetMouseButton(1) && !animPlaying && PlayerAttributes.instance.stamina >= 30)
        {
            swordCollider.enabled = true;

            if (rollDirection == Vector3.forward)
                anim.Play("RollForward");
            else if (rollDirection == Vector3.left)
                anim.Play("RollLeft");
            else if (rollDirection == Vector3.right)
                anim.Play("RollRight");
            else
                anim.Play("RollForward");
            animPlaying = true;
            PlayerAttributes.instance.stamina -= 30;
        } else if(Input.GetMouseButton(1) && !animPlaying && PlayerAttributes.instance.stamina < 30)
        {
            // SES OYNAT
        }

        if (animPlaying)
            return;

        transform.position += 2f * speed * Time.deltaTime * GetDirection();
        if (Input.GetKey(KeyCode.LeftControl))
        {
            speed = 0.4f;
            currentState = State.Crouching;
        }
        else if(Input.GetKey(KeyCode.LeftShift) && WalkingCondition())
        {
            speed = 2f;
            currentState = State.Running;
        } else if(WalkingCondition())
        {
            speed = 1f;
            currentState = State.Walking;
        } else if(!animPlaying)
        {
            currentState = State.Idle;
        }
    }

    private void AnimationController()
    {
        bool gotHit = GetHitController.animPlaying;

        if (gotHit)
            return;

        if (currentState == State.Crouching && attacking)
        {
            attacking = false;
            anim.Play("CrouchSlash");
            animPlaying = true;
        }
        else if (currentState == State.Crouching && !animPlaying)
            anim.Play("CrouchIdle");
        else if (attacking && !animPlaying)
        {
            attacking = false;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slash0"))
                anim.Play("Slash1");
            else
                anim.Play("Slash0");

            animPlaying = true;
        }
        else if (currentState == State.Running && !animPlaying)
            anim.Play("RunSword");
        else if (currentState == State.Walking && !animPlaying)
            anim.Play("WalkSword");
        else if (currentState == State.Idle && !animPlaying)
            anim.Play("Idle");
    }

    private bool WalkingCondition()
    {
        return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
    }

    private Vector3 GetDirection()
    {
        string input;
        if (Input.GetKey(KeyCode.W))
            input = "w";
        else if (Input.GetKey(KeyCode.S))
            input = "s";
        else if (Input.GetKey(KeyCode.A))
            input = "a";
        else if (Input.GetKey(KeyCode.D))
            input = "d";
        else
            input = "";

        Vector3 direction = input switch
        {
            "w" => Vector3.forward,
            "a" => Vector3.left,
            "d" => Vector3.right,
            "s" => Vector3.back,
            _ => new Vector3(0, 0, 0)
        };

        rollDirection = direction;

        if (rollDirection == Vector3.back || rollDirection == new Vector3(0, 0, 0))
            rollDirection = Vector3.forward;

        return transform.rotation * direction;
    }
}
