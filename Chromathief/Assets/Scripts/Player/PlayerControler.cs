using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float walkAcceleration;
    [SerializeField] float walkBrakeAcceleration;
    [SerializeField] AnimationCurve walkAccelerationCurve;
    [SerializeField] float reverseWalkAcceleration;
    [SerializeField] AnimationCurve reverseWalkAccelerationCurve;
    [SerializeField] float runSpeed;
    [SerializeField] float runAcceleration;
    [SerializeField] AnimationCurve runAccelerationCurve;
    [SerializeField] float rotationSpeed;

    Vector2 currentSpeed;
    Vector2 WalkSpeedRatio { get { return new Vector2(Mathf.Abs(currentSpeed.x) / walkSpeed, Mathf.Abs(currentSpeed.y )/ walkSpeed); } }
    Vector2 RunSpeedRatio { get { return new Vector2(Mathf.Abs(currentSpeed.x) / runSpeed, Mathf.Abs(currentSpeed.y) / runSpeed); } }

    Vector2 NormalizedWalkSpeedRatio { 
        get {
            return WalkSpeedRatio;
            /*Vector2 wa = WalkSpeedRatio; wa.Normalize(); wa = new Vector2(wa.x > 0 ? 1f / wa.x : 0,wa.y > 0 ? 1f / wa.y : 0);
            return new Vector2(float.IsNaN(wa.x) ? 0 : wa.x, float.IsNaN(wa.y) ? 0 : wa.y);  */
        } }

    Vector2 WalkAcceleration(Vector2 input)
    {
        Vector2 wa = WalkSpeedRatio;
        Vector2 rv = new Vector2(input.x * currentSpeed.normalized.x, input.y * currentSpeed.normalized.y);

        if(rv.x < 0) { wa.x = reverseWalkAcceleration * reverseWalkAccelerationCurve.Evaluate(-rv.x) ; } 
        else { wa.x = walkAcceleration * walkAccelerationCurve.Evaluate(wa.x); }
        if(rv.y < 0) { wa.y = reverseWalkAcceleration * reverseWalkAccelerationCurve.Evaluate(-rv.y) ; } 
        else { wa.y = walkAcceleration * walkAccelerationCurve.Evaluate(wa.y); }
        
        return new Vector2(wa.x,wa.y);
    }
    Vector2 RunAcceleration { get { return new Vector2(runAcceleration * runAccelerationCurve.Evaluate(RunSpeedRatio.x), runAcceleration * runAccelerationCurve.Evaluate(RunSpeedRatio.y)); } }



    private void FixedUpdate()
    {
        Move();

        Look();

        WalkInput();
    }

    void Look()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(currentSpeed.x, 0, currentSpeed.y), transform.up); 
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, Time.deltaTime * rotationSpeed);
    }

    void Move()
    {
        transform.Translate(currentSpeed.x,0,currentSpeed.y,Space.World);
    }

    void WalkInput()
    {
        Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


        Vector2 wa = WalkAcceleration(dir);

        currentSpeed += new Vector2(dir.x * wa.x, dir.y * wa.y);

        if (dir.x < 0.1f) { currentSpeed.x /= walkBrakeAcceleration; }
        if (dir.y < 0.1f) { currentSpeed.y /= walkBrakeAcceleration; }
    }
}
