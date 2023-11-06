using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyParticle;
    [SerializeField] NavMeshSurface navMesh;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return; 

        other.gameObject.TryGetComponent(out PlayerControler pc);
        if (!pc || !pc.IsRunning) return;

        Destroy(gameObject);
        Instantiate(destroyParticle.gameObject, transform.position, transform.rotation);

        //navMesh.UpdateNavMesh(navMesh.navMeshData);
        SoundManager.Instance.PlaySound(SOUND_NAME.BROKEN_WALL, transform.position);
    }
}
