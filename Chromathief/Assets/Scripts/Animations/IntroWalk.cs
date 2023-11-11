using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWalk : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;
    private bool finished = false;
    [SerializeField] private Transform target;
    [SerializeField] private AudioClip door;

    void Awake()
    {
        target = GameObject.Find("PlayerFinishPoint").transform;
    }

    void Update()
    {
        if (!finished)
        {
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.LookAt(target);

            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                finished = true; 
                Invoke("PlayDoorSound",0.0f); 
            }
        }

    }

    void PlayDoorSound()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.Stop();
        audio.clip = door;
        audio.Play();
        Invoke("LoadLevel1", audio.clip.length);
    }

    void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }


}
