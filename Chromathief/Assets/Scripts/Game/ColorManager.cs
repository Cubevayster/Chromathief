using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField]
    Color[] colors = new Color[8];


    public static Color GetColor(bool red, bool blue, bool yellow)
    {
        return GetColor(ColorOf(red, blue, yellow));
    }
    public static Color GetColor(GameColor gameColor)
    {
        return instance.colors[(int)gameColor];
    }
    public static GameColor ColorOf(bool red, bool blue, bool yellow)
    {
        if(!red && !blue && !yellow) { return GameColor.White; }
        if(!red && !blue && yellow) { return GameColor.Yellow; }
        if(!red && blue && !yellow) { return GameColor.Blue; }
        if(red && !blue && !yellow) { return GameColor.Red; }
        if(red && !blue && yellow) { return GameColor.Orange; }
        if(red && blue && !yellow) { return GameColor.Purple; }
        if(!red && blue && yellow) { return GameColor.Green; }
        /*if(red && blue && yellow) {*/ return GameColor.Black;
    }

    public static ColorManager instance;
    public enum GameColor
    {
        White, Red, Blue, Yellow, Purple, Orange, Green, Black
    }

}
