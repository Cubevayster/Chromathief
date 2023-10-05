using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 limitsXY = new Vector2(1,1);
    [SerializeField] Transform target;
    [SerializeField] float catchSpeed;
    [SerializeField] Vector2 distanceRange;
    private Vector3 basePosition;

    private void Start()
    {
        basePosition = this.transform.position;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseZ = Input.GetAxis("Mouse Y");

        Vector2 mousePos = Input.mousePosition;
        mousePos.x = (mousePos.x - (Screen.width / 2.0f)) / (Screen.width / 2.0f);
        mousePos.y = (mousePos.y - (Screen.height / 2.0f)) / (Screen.height / 2.0f);

        Vector3 pos = target.position + basePosition + new Vector3(mousePos.x * limitsXY.x, 0, mousePos.y * limitsXY.y);

        float dist = (pos - transform.position).magnitude;
        if (dist > distanceRange.x)
        {
            float c = Mathf.Min(1, dist / distanceRange.y);
            c = Mathf.Pow(c, 2);
            this.transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * catchSpeed * c);
        }
    }
}
