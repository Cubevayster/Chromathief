using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float walkAcceleration;
    [SerializeField] float walkBrakeAcceleration;
    [SerializeField] AnimationCurve walkBrakeAccelerationCurve;
    [SerializeField] AnimationCurve walkAccelerationCurve;
    [SerializeField] float reverseWalkAcceleration;
    [SerializeField] AnimationCurve reverseWalkAccelerationCurve;

    [Space(5)]

    [SerializeField] float runSpeed;
    [SerializeField] float runAngle;
    [SerializeField] float runAcceleration;
    [SerializeField] AnimationCurve runAccelerationCurve;
    [SerializeField] float reverseRunAcceleration;
    [SerializeField] AnimationCurve reverseRunAccelerationCurve;

    [Space(5)]
    [SerializeField] float rotationSpeed;
    [SerializeField] Animator playerAnimator;
    bool isWalking; public bool IsWalking { get { return isWalking; } }
    bool isRunning; public bool IsRunning { get { return isRunning; } }
    Vector2 runDirection;

    Vector2 movementInput;
    Vector2 currentSpeed;
    Vector2 WalkSpeedRatio { get { return new Vector2(Mathf.Abs(currentSpeed.x) / walkSpeed, Mathf.Abs(currentSpeed.y )/ walkSpeed); } }
    Vector2 RunSpeedRatio { get { return new Vector2(Mathf.Abs(currentSpeed.x) / runSpeed, Mathf.Abs(currentSpeed.y) / runSpeed); } }

    Vector2 NormalizedWalkSpeedRatio { 
        get {
            return WalkSpeedRatio;
            /*Vector2 wa = WalkSpeedRatio; wa.Normalize(); wa = new Vector2(wa.x > 0 ? 1f / wa.x : 0,wa.y > 0 ? 1f / wa.y : 0);
            return new Vector2(float.IsNaN(wa.x) ? 0 : wa.x, float.IsNaN(wa.y) ? 0 : wa.y);  */
        } }

    Vector2 Acceleration(Vector2 input,Vector2 ratio, float forwardCoef,AnimationCurve forwardCurve,float reverseCoef, AnimationCurve reverseCurve)
    {
        Vector2 wa = ratio;
        Vector2 rv = new Vector2(input.x * currentSpeed.normalized.x, input.y * currentSpeed.normalized.y);

        if (rv.x < 0) { wa.x = reverseCoef * reverseCurve.Evaluate(-rv.x); }
        else { wa.x = forwardCoef * forwardCurve.Evaluate(wa.x); }
        if (rv.y < 0) { wa.y = reverseCoef * reverseCurve.Evaluate(-rv.y); }
        else { wa.y = forwardCoef * forwardCurve.Evaluate(wa.y); }

        return new Vector2(wa.x, wa.y);
    }
    Vector2 WalkAcceleration(Vector2 input)  { return Acceleration(input, WalkSpeedRatio, walkAcceleration, walkAccelerationCurve, reverseWalkAcceleration, reverseWalkAccelerationCurve);  }
    Vector2 RunAcceleration(Vector2 input)  { return Acceleration(input, RunSpeedRatio, runAcceleration, runAccelerationCurve, reverseRunAcceleration, reverseRunAccelerationCurve);  }


    private void FixedUpdate()
    {
        if(isRunning)
        {
            Run();
        }
        else
        {
            Walk();
        }

        RunInput();
        WalkInput();
        ApplySpeed();
        SetAnimatorParameters();
    }

    void Look(Vector2 direction)
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x,0, direction.y), transform.up); //new Vector3(currentSpeed.x, 0, currentSpeed.y)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    void Run()
    {
        float angle = Vector2.SignedAngle(runDirection, movementInput);
        if(angle > runAngle) { angle = runAngle; }
        if(angle < -runAngle) { angle = -runAngle; }

        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
        Vector2 targetDirection = new Vector2(
            (runDirection.x * cos) - (runDirection.y * sin), 
            (runDirection.x * sin) + (runDirection.y * cos));

        Vector2 wa = RunAcceleration(targetDirection);

        currentSpeed += new Vector2(targetDirection.x * wa.x, targetDirection.y * wa.y);

        Look(runDirection);
    }

    void Walk()
    {
        Vector2 wa = WalkAcceleration(movementInput);

        currentSpeed += new Vector2(movementInput.x * wa.x, movementInput.y * wa.y);

        if (Mathf.Abs(movementInput.x) < 0.1f) { currentSpeed.x /= 1 + (walkBrakeAcceleration * walkBrakeAccelerationCurve.Evaluate(WalkSpeedRatio.x)); }
        if (Mathf.Abs(movementInput.y) < 0.1f) { currentSpeed.y /= 1 + (walkBrakeAcceleration * walkBrakeAccelerationCurve.Evaluate(WalkSpeedRatio.y)); }

        isWalking = movementInput.magnitude > 0.1f;

        if (Mathf.Abs(movementInput.x) > 0.1f || Mathf.Abs(movementInput.y) > 0.1f)
        {
            Look(movementInput);
        }
    }

    void ApplySpeed()
    {
        transform.Translate(currentSpeed.x, 0, currentSpeed.y, Space.World);
    }

    void RunInput()
    {
        bool previousState = isRunning;
        isRunning = Input.GetKey(KeyCode.LeftShift) && movementInput.magnitude > 0.1f;
        if(isRunning && isRunning != previousState)
        {
            runDirection = currentSpeed.normalized;
        }
    }

    void WalkInput()
    {
        movementInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movementInput.Normalize();
    }

    void SetAnimatorParameters()
    {
        playerAnimator.SetBool("isWalking", isWalking);
        playerAnimator.SetBool("isRunning", isRunning);
    }
}
