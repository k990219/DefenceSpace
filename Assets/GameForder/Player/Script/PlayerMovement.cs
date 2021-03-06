﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private static PlayerMovement instance;
    public static PlayerMovement playerMovement
    {
        get
        {
            return !instance ?
                instance = FindObjectOfType(typeof(PlayerMovement)) as PlayerMovement : instance;
        }
    }
    Weapon weapon;

    new Camera camera;

    CharacterController controller;
    Animator ani;
    
    float moveX;
    float moveY;
    float rotX;
    float rotY;
    Vector3 movement;
    public float moveSpeed;
    float mouseSpeed = 150f;
    float sprintSpeed = 3.5f;
    float defaultSpeed = 2.0f;
    float airSpeed = 1.0f;

    Vector3 jumpVelocity;
    Vector3 JumpDirection;
    float gravity = -15.0f;
    float jump = 10.0f;
    float doubleJumpPower = 8f;
    float airPower = 1.5f;
    private bool isGround
    {
        get
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
            {
                state = State.Ground;
                return true;
            }
            return false;
        }

    }
    
    public int doubleTab = 0;
    float tabDelay = 0.5f;
    public float tabTime;
    bool isTab = false;

    public enum State { Ground, Jump, dJump,Air}
    public State state=State.Ground;

    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
        ani = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.playerScript.isDead)
            return;

        InputMovement();
        Jumping();

    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        camera.transform.localRotation = Quaternion.Euler(rotY, 0, 0);
        transform.rotation = Quaternion.Euler(0, rotX, 0);

        //   transform.Translate(movement * Time.deltaTime * moveSpeed);
        controller.Move(movement * Time.deltaTime * moveSpeed);
    }

    void InputMovement()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        movement = new Vector3(moveX, 0, moveY);
        movement = transform.TransformDirection(movement * Time.deltaTime * 60.0f);

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (PlayerManager.playerScript.playerBoost <= PlayerManager.playerScript.sprintBoost)
                return;
            ani.SetBool("Sprint", true);
            PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.sprintBoost * Time.deltaTime;
            PlayerManager.playerScript.boostUse = true;
            WeaponManager.weaponScript.handWeapon.ChaingeSprint();
            moveSpeed = sprintSpeed;
        }

        else
        {
            ani.SetBool("Sprint", false);
            moveSpeed = defaultSpeed;
            PlayerManager.playerScript.boostUse = false;
        }


        rotX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;

        rotY -= Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;
        rotY = Mathf.Clamp(rotY, -60.0f, 60.0f);


    }



    void Jumping()
    {
        if (isGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                state = State.Jump;
                jumpVelocity.y = jump;
            }
        }

        else if (state == State.Jump)
        {
            DoubleJump();
        }

        else if (state == State.dJump)
        {
            controller.Move(JumpDirection * Time.deltaTime);
        }

        else if (state == State.Air)
        {
            if (Input.GetButton("Jump"))
            {
                if (PlayerManager.playerScript.playerBoost <= PlayerManager.playerScript.airBoost)
                    return;

                moveSpeed = airSpeed;
                PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.airBoost * Time.deltaTime;
                PlayerManager.playerScript.boostUse = true;
                if (transform.position.y > GameManager.gameManager.maxMoveY)
                    return;
                jumpVelocity.y = airPower;
            }
            else
                PlayerManager.playerScript.boostUse = false;
            DoubleJump();

        }
        jumpVelocity.y += gravity * Time.deltaTime;
        controller.Move(jumpVelocity * Time.deltaTime);
        
    }

    void DoubleJump()
    {
        if (PlayerManager.playerScript.playerBoost <= PlayerManager.playerScript.dJumpBoost)
            return;

        if (tabTime > tabDelay || isGround)
        {
            TabReset();
        }

        if (isTab) tabTime += Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
        {
            state = State.Air;
            isTab = true;
            if (tabTime < tabDelay)
            {
                doubleTab++;
            }
        }
        

        if (doubleTab>=2)
        {
            if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.A))
            {
                state = State.dJump;

                JumpDirection = -transform.right * doubleJumpPower;
                PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.dJumpBoost;
            }
            else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.D))
            {
                state = State.dJump;

                JumpDirection = transform.right * doubleJumpPower;
                PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.dJumpBoost;
            }
            else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.W))
            {
                state = State.dJump;

                JumpDirection = transform.forward * doubleJumpPower;
                PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.dJumpBoost;
            }
            else if (Input.GetButtonDown("Jump") && Input.GetKey(KeyCode.S))
            {
                state = State.dJump;

                JumpDirection = -transform.forward * doubleJumpPower;
                PlayerManager.playerScript.playerBoost -= PlayerManager.playerScript.dJumpBoost;
            }
            
        }


    }

    void TabReset()
    {
        tabTime = 0;
        doubleTab = 0;
        isTab = false;
    }


}
