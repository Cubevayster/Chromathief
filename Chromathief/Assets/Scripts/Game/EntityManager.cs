using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EntityManager : Singleton<EntityManager>
{
    [SerializeField] List<AIBrain> allAI = new List<AIBrain>();
    public List<AIBrain> GetAIs() => allAI;

    void CheckOneOrMoreIsInAlert()
    {
        bool _oneInAlert = false;
        foreach (AIBrain _brain in allAI)
            _oneInAlert |= _brain.GetAlertType() == ALERT_TYPES.Warning;

        HUD.Instance?.SetAlert(_oneInAlert ? ALERT_TYPES.Warning : ALERT_TYPES.None);
    }

    bool AIAlreadyRegistered(AIBrain _ai) => allAI.Contains(_ai);

    public void RegisterGuard(AIBrain _ai)
    {
        if (AIAlreadyRegistered(_ai)) return;
        allAI.Add(_ai);
        _ai.OnAlertTypeChanged += (ALERT_TYPES _alert) => CheckOneOrMoreIsInAlert();
    }

    public void UnregisterGuard(AIBrain _ai)
    {
        if (!AIAlreadyRegistered(_ai)) return;
        allAI.Remove(_ai);
        _ai.OnAlertTypeChanged -= (ALERT_TYPES _alert) => CheckOneOrMoreIsInAlert();
    }
}
