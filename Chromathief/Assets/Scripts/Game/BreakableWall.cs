using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            if(other.gameObject.TryGetComponent(out PlayerControler pc))
            {
                if (pc.IsRunning)
                {
                    Instantiate(destroyParticle.gameObject, transform.position, transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
