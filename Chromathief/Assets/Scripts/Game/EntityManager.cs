using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    private static EntityManager _entityManagerInstance;
    List<AIController> guards = new List<AIController>();

    public static EntityManager EntityManagerInstance {
        get {
            return _entityManagerInstance;
        }
    }
    
    public void RegisterGuard(AIController aiController)
    {
        this.guards.Add(aiController);
    }

    public List<AIController> GetGuards()
    {
        return this.guards;
    }

    public void UnRegisterGuard(AIController aiController)
    {
        this.guards.Remove(aiController);
    }

    public void NotifyGuardOfNoise(Vector3 noisePosition)
    {
        foreach (AIController guard in this.guards)
        {
            guard.MoveTowardsIfHeard(noisePosition);
        }
       
    }

    public void Awake()
    {
        if (_entityManagerInstance == null)
        {
            _entityManagerInstance = new EntityManager();
        }
    }



}
