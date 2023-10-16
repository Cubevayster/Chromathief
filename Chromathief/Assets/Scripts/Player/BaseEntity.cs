using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    [SerializeField] GameObject detectedEffect;

    bool detected; public void SetDetected(bool b) { detected = b; }

    protected virtual void Update()
    {
        if (detectedEffect != null)
        {
            detectedEffect.SetActive(detected);
        }
    }

    protected virtual void Start() { }
}
