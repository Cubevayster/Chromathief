using UnityEngine;

public class CustomManager : Singleton<CustomManager>
{
    public string TimeText;
    public string ScoreText = "1000$";

    protected override void Awake()
    {
        base.Awake(); 

    }


}
