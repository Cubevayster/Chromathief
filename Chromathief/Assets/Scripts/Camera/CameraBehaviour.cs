using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 limitsXY = new Vector2(1,1);
    [SerializeField] AnimationCurve moveCurve;
    [SerializeField] Transform target;
    [SerializeField] float catchSpeed;
    [SerializeField] AnimationCurve catchCurve;
    [SerializeField] Vector2 distanceRange;
    [SerializeField] Vector2 zoomRange;
    [SerializeField] Vector2 zoomSpeeds;
    [SerializeField] new Camera camera;
    private Vector3 basePosition;

    private Vector3 offset;

    private void Start()
    {
        basePosition = this.transform.localPosition *(  this.transform.parent.localScale.magnitude / 2 ); 
        transform.parent = null;
    }

    void Move()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos.x = (mousePos.x - (Screen.width / 2.0f)) / (Screen.width / 2.0f);
        mousePos.y = (mousePos.y - (Screen.height / 2.0f)) / (Screen.height / 2.0f);
        mousePos.x = moveCurve.Evaluate(Mathf.Abs(mousePos.x)) * Mathf.Sign(mousePos.x);
        mousePos.y = moveCurve.Evaluate(Mathf.Abs(mousePos.y)) * Mathf.Sign(mousePos.y);

        offset = new Vector3(mousePos.x * limitsXY.x, 0, mousePos.y * limitsXY.y);
    }

    void FocusTarget()
    {
        Vector3 pos = target.position + basePosition + offset;

        float dist = (pos - transform.position).magnitude;
        if (dist > distanceRange.x)
        {
            float c = Mathf.Clamp((dist - distanceRange.x) / (distanceRange.y - distanceRange.x), 0, 1);
            c = catchCurve.Evaluate(c);
            transform.position = Vector3.MoveTowards(transform.position, pos, Time.fixedDeltaTime * catchSpeed * c);
        }
    }

    void Zoom()
    {
        if(target.TryGetComponent(out PlayerControler pc))
        {
            if(pc.IsRunning)
            {
                camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, zoomRange.y, zoomSpeeds.y);
            }
            else
            {
                camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, zoomRange.x, zoomSpeeds.x);
            }
        }
        else
        {
            camera.fieldOfView = zoomRange.x;
        }
    }

    private void FixedUpdate()
    {
        Move();
        FocusTarget();
        Zoom();
    }
}
