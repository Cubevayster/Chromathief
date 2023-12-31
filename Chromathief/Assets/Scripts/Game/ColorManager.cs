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
        if (instance == null) { return staticColors[(int)gameColor]; }
        return instance.colors[(int)gameColor];
    }
    public static GameColor ColorOf(bool red, bool blue, bool yellow)
    {
        if (!red && !blue && !yellow) { return GameColor.White; }
        if (!red && !blue && yellow) { return GameColor.Yellow; }
        if (!red && blue && !yellow) { return GameColor.Blue; }
        if (red && !blue && !yellow) { return GameColor.Red; }
        if (red && !blue && yellow) { return GameColor.Orange; }
        if (red && blue && !yellow) { return GameColor.Purple; }
        if (!red && blue && yellow) { return GameColor.Green; }
        /*if(red && blue && yellow) {*/
        return GameColor.Black;
    }

    public static ColorManager instance;

    private void Awake()
    {
        instance = this;
    }

    public enum GameColor
    {
        White, Red, Blue, Yellow, Purple, Orange, Green, Black
    }
    public static Color[] staticColors = new Color[8] {
        Color.white,
        Color.red,
        Color.blue,
        Color.yellow,
        Color.magenta,
        new Color(1,0.4f,0),
        Color.green,
        Color.black

    };
}
