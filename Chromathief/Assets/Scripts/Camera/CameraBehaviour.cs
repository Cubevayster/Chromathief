using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private Vector2 limitsXY = new Vector2(1,1);
    private Vector3 basePosition;

    private void Start()
    {
        basePosition = this.transform.localPosition;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseZ = Input.GetAxis("Mouse Y");

        Vector2 mousePos = Input.mousePosition;
        mousePos.x = (mousePos.x - (Screen.width / 2.0f))/ (Screen.width / 2.0f);
        mousePos.y = (mousePos.y - (Screen.height / 2.0f)) / (Screen.height / 2.0f);

        this.transform.localPosition = new Vector3(mousePos.x * limitsXY.x, 0,
                                                    mousePos.y * limitsXY.y) + basePosition;
    }
}
