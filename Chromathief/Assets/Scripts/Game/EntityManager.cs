using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    [SerializeField] List<AIBrain> allAI = new List<AIBrain>();
    public List<AIBrain> GetAIs() => allAI;

    bool AIAlreadyRegistered(AIBrain _ai) => allAI.Contains(_ai);

    public void RegisterGuard(AIBrain _ai)
    {
        if (AIAlreadyRegistered(_ai)) return;
        allAI.Add(_ai);
    }

    public void UnregisterGuard(AIBrain _ai)
    {
        if (!AIAlreadyRegistered(_ai)) return;
        allAI.Remove(_ai);
    }
}
