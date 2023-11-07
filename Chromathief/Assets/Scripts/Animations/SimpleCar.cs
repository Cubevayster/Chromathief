using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCar : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;
    private bool finished = false;

    [SerializeField] private Transform target;

    void Awake()
    {
    }

    void Update()
    {
        if(!finished)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                WheelRotate[] wheels = GetComponentsInChildren<WheelRotate>();
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].finished = true;
                }
               finished = true;
            }
        }
       
    }

}
