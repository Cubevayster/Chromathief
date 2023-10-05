using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoor : ColorEntity
{


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out ColorEntity ce))
        {
            if (red && blue && yellow) { ce.ChangeRed(true); ce.ChangeBlue(true); ce.ChangeYellow(true); }
            else if (!red && !blue && !yellow) { ce.ChangeRed(false); ce.ChangeBlue(false); ce.ChangeYellow(false); }
            else
            {
                if (red) { ce.ToggleRed(); }
                if (blue) { ce.ToggleBlue(); }
                if (yellow) { ce.ToggleYellow(); }
            }
        }
    }
}
