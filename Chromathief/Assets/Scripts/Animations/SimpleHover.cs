using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHover : MonoBehaviour
{
    [SerializeField] float orginalScale = 1.2f;
    [SerializeField] float hoveredScale = 1.5f;
    public GameObject button;

    public void Hover()
    {
        button.transform.localScale = new Vector3(hoveredScale, hoveredScale, hoveredScale);
    }


    public void Leave()
    {
        button.transform.localScale = new Vector3(orginalScale, orginalScale, orginalScale);
    }


}
