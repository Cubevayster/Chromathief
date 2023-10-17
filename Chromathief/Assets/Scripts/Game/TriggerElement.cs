using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerElement : MonoBehaviour
{
    [SerializeField] GameObject sendTo;

    private void OnTriggerEnter(Collider other)
    {
        sendTo.SendMessage("Triggered", new TriggerData(this,other));
    }
    private void OnTriggerExit(Collider other)
    {
        sendTo.SendMessage("UnTriggered", new TriggerData(this, other));
    }
    public struct TriggerData
    {
        public TriggerElement triggerElement;
        public Collider collider;
        public TriggerData(TriggerElement te, Collider col) { triggerElement = te; collider = col; }
    }
}
