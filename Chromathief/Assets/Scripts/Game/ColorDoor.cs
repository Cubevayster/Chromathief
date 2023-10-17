using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoor : ColorEntity
{
    [SerializeField] TriggerElement leftEnter;
    [SerializeField] TriggerElement rightEnter;

    int lastTriggered;
    bool[] lastColor = new bool[3] { false, false, false };

    public void UnTriggered(TriggerElement.TriggerData triggerData) { UnTriggered(triggerData.triggerElement, triggerData.collider); }
    public void UnTriggered(TriggerElement te, Collider col)
    {
        if (col.TryGetComponent(out Player p))
        {
            if (lastTriggered == 1 && te == leftEnter)
            {
                Colorize(p, true);
                lastTriggered = 0;
            }
            else if (lastTriggered == -1 && te == rightEnter)
            {
                Colorize(p, true);
                lastTriggered = 0;
            } 
            else if (lastTriggered == -1 && te == leftEnter)
            {
                lastTriggered = 0;
            }
            else if (lastTriggered == 1 && te == rightEnter)
            {
                lastTriggered = 0;
            }
        }
        //Debug.Log(lastTriggered);
    }
    public void Triggered(TriggerElement.TriggerData triggerData) { Triggered(triggerData.triggerElement, triggerData.collider); }
    public void Triggered(TriggerElement te, Collider col)
    {
        if (col.TryGetComponent(out Player p))
        {
            if (lastTriggered == 0)
            {
                lastTriggered = te == leftEnter ? 1 : -1; //left>right = 1   rigth>left = -1
            }
            else if (lastTriggered == 1 && te == leftEnter)
            {
                //ignore (went back)
            }
            else if (lastTriggered == -1 && te == rightEnter)
            {
                //ignore (went back)
            }
            else
            {
                Colorize(p);
                lastTriggered = -lastTriggered;
            }
        }
        else if(col.TryGetComponent(out ColorEntity ce))
        {
            Colorize(ce);
        }
        //Debug.Log(lastTriggered);
    }

    void Colorize(ColorEntity ce,bool isUndo = false)
    {
        /*if (isUndo)
        {
            if (lastColor[0]) { ce.ToggleRed(); }
            if (lastColor[1]) { ce.ToggleBlue(); }
            if (lastColor[2]) { ce.ToggleYellow(); }
        }
        else
        {*/
            lastColor[0] = ce.Red; lastColor[1] = ce.Blue; lastColor[2] = ce.Yellow;
            if (red && blue && yellow) { ce.All(); }//ce.ChangeRed(true); ce.ChangeBlue(true); ce.ChangeYellow(true); }
            else if (!red && !blue && !yellow) { ce.Clear(); }//ce.ChangeRed(false); ce.ChangeBlue(false); ce.ChangeYellow(false); }
            else
            {
                if (red) { ce.ToggleRed(); }
                if (blue) { ce.ToggleBlue(); }
                if (yellow) { ce.ToggleYellow(); }
            }
        //}
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ColorEntity ce))
        {
            if (red && blue && yellow) { ce.All(); }//ce.ChangeRed(true); ce.ChangeBlue(true); ce.ChangeYellow(true); }
            else if (!red && !blue && !yellow) { ce.Clear(); }//ce.ChangeRed(false); ce.ChangeBlue(false); ce.ChangeYellow(false); }
            else
            {
                if (red) { ce.ToggleRed(); }
                if (blue) { ce.ToggleBlue(); }
                if (yellow) { ce.ToggleYellow(); }
            }
        }
    }*/
}
