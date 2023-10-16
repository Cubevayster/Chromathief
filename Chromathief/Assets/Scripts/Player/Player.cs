using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayingEntity
{
    public override void Clear() { base.Clear(); HUD.Instance?.SetPrimaryColors(red, blue, yellow);  }
    public override void All() { base.All(); HUD.Instance?.SetPrimaryColors(red, blue, yellow); }
    public override void ChangeRed(bool r) { base.ChangeRed(r); HUD.Instance?.SetPrimaryColors(red, blue, yellow); }
    public override void ChangeBlue(bool b) { base.ChangeBlue(b); HUD.Instance?.SetPrimaryColors(red, blue, yellow); }
    public override void ChangeYellow(bool y) { base.ChangeYellow(y); HUD.Instance?.SetPrimaryColors(red, blue, yellow); }

    protected override void UpdateColor(bool playEffect = false, bool _red = false, bool _blue = false, bool _yellow = false)
    {
        base.UpdateColor(playEffect,_red,_blue,_yellow); 
        HUD.Instance?.SetCurrentColor(Color);
    }
}
