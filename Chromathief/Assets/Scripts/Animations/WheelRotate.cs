using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public bool finished = false;

    void Update()
    {
        if (!finished)
        {
            this.transform.Rotate(0, 0, -1);
        }
    }
}
