using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroCar : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;
    private bool finished = false;

    [SerializeField] private Transform target;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject animCamera;
    [SerializeField] private AudioClip door;

    void Awake()
    {
    }

    void Update()
    {
        if(!finished)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            if (Vector3.Distance(transform.position, target.position) < 0.5f)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.Stop();
                WheelRotate[] wheels = GetComponentsInChildren<WheelRotate>();
                for (int i = 0; i < wheels.Length; i++)
                {
                    wheels[i].finished = true;
                }
               finished = true;
                audio.clip = door;
                audio.Play();
                InstantiatePlayer();
                audio.Stop();
            }
        }
       
    }

    private void InstantiatePlayer()
    {
        GameObject.Instantiate(player,new Vector3( transform.position.x , transform.position.y -1 , transform.position.z -5 ) , transform.rotation);
        player.transform.Rotate(0, 180, 0);
        animCamera.transform.position = new Vector3(animCamera.transform.position.x, animCamera.transform.position.y + (0.065f*animCamera.transform.parent.localScale.y), animCamera.transform.position.z- (0.05f * animCamera.transform.parent.localScale.z));

    }
}
