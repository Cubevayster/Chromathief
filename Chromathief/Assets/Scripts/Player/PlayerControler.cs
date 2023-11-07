using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] bool canMove = true;
    public bool CanMove { get => canMove; set { canMove = value; } }

    [SerializeField] float walkSpeed;

    [Space(5)]

    [SerializeField] float runSpeed;
    [SerializeField] float runAngle;
    [SerializeField] float runMinDuration;

    [Space(5)]
    [SerializeField] float rotationSpeed;
    [SerializeField] Animator playerAnimator;
    bool isWalking; public bool IsWalking { get { return isWalking; } }
    bool isRunning; public bool IsRunning { get { return isRunning; } }

    Vector2 runDirection; float runCurrentDuration;
    Vector2 movementInput;

    float TimeStanding = 0f;

    private void FixedUpdate()
    {
        WalkInput();
        RunInput();
        
        if(isRunning)
        {
            Run();
            runCurrentDuration -= Time.deltaTime;
        }
        else
        {
            Walk();
        }

        if(!isWalking && !isRunning)
        {
            TimeStanding += Time.deltaTime;
        }
        else
        {
            TimeStanding = 0f;
        }

        SetAnimatorParameters();
    }

    void Look(Vector2 direction)
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x,0, direction.y), transform.up); //new Vector3(currentSpeed.x, 0, currentSpeed.y)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    void Run()
    {
        if(!canMove) return;

        float angle = Vector2.SignedAngle(runDirection, movementInput);
        if(angle > runAngle) { angle = runAngle; }
        if(angle < -runAngle) { angle = -runAngle; }

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 targetDirection = new Vector2(
            (runDirection.x * cos) - (runDirection.y * sin), 
            (runDirection.x * sin) + (runDirection.y * cos));

        Rigidbody.velocity = new Vector3(targetDirection.x, 0, targetDirection.y) * runSpeed;

        Look(runDirection);
    }

    void Walk()
    {
        if (!canMove) return;

        Rigidbody.velocity = new Vector3(movementInput.x, 0, movementInput.y) * walkSpeed;

        isWalking = movementInput.magnitude > 0.1f;

        if (Mathf.Abs(movementInput.x) > 0.1f || Mathf.Abs(movementInput.y) > 0.1f)
        {
            Look(movementInput);
        }
    }

    void RunInput()
    {
        if (!canMove) return;

        bool previousState = isRunning;
        isRunning = runCurrentDuration > 0 || (Input.GetKey(KeyCode.LeftShift) && movementInput.magnitude > 0.1f);
        if(isRunning && isRunning != previousState) //Start running
        {
            runDirection = movementInput.normalized;
            runCurrentDuration = runMinDuration;
        }
        /*else if(!isRunning && isRunning != previousState) //Stop running
        {
            isSliding = true;
            slideStart = position2D;
        }*/
    }

    void WalkInput()
    {
        if (!canMove) return;
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput.Normalize();
    }

    void SetAnimatorParameters()
    {
        playerAnimator.SetBool("isWalking", isWalking);
        playerAnimator.SetBool("isRunning", isRunning);
        playerAnimator.SetFloat("TimeStanding", TimeStanding);
    }

    public void StopPlayerControler()
    {
        //Debug.Log("Stop player");
        canMove = false;
    }

    public Rigidbody Rigidbody { get { return GetComponent<Rigidbody>(); } }
    public Vector3 velocity { get { return Rigidbody.velocity; } }
    public Vector2 position2D { get { return new Vector2(transform.position.x, transform.position.z); } }
}
