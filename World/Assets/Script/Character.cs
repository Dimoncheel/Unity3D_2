using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private float CharacterSpeed = 2.0f;
    private CharacterController characterController;
    private Animator _animator;


    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isRun = Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
        float factor = CharacterSpeed;//*Time.deltaTime
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            factor *= 2;
        }
        float dx = Input.GetAxis("Horizontal");  // <-, ->, A, D
        float dy = Input.GetAxis("Vertical");

        Vector3 moveDirection =  
            (dx * this.transform.right + dy * this.transform.forward);

        if (moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized; 
        }
        moveDirection *= factor;

        MoveState moveState;
        float speed = moveDirection.magnitude;
        if (speed <= 0.1f)   
        {
            moveState = MoveState.IDle;
            
        }
        else  
        {
            if (Mathf.Abs(dx) > Mathf.Abs(dy))
            {
                if (isRun)
                {
                    moveState = MoveState.RunSideways;
                }
                else
                {
                    moveState = MoveState.WalkSideways;
                }
            }
            else
            {
                if (isRun)
                {
                    moveState = MoveState.RunForward;
                }
                else
                {
                    moveState = MoveState.WalkForward;
                }
            }
           
        }
     
      //  characterController.SimpleMove(moveDirection);



        groundedPlayer = characterController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {

            playerVelocity.y +=
                Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
            moveState = MoveState.JumpForward;
            if (isRun)
            {
                moveState = MoveState.JumpForward;
                playerVelocity.y *= 2;
            }
        }
        _animator.SetInteger("MoveState", (int)moveState);
        playerVelocity.y += gravityValue * Time.deltaTime;
        moveDirection.y = playerVelocity.y;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void Step(int steps)
    {
        
    }
    private enum MoveState
    {
        IDle=0,
        WalkForward,
        RunForward,
        WalkSideways,
        RunSideways,
        JumpForward
    }
}
