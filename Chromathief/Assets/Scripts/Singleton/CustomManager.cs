using UnityEngine;

public class CustomManager
{
    private static CustomManager instance;

    public string TimeText = "00:00:01";
    public string ScoreText = "1000$";

    private CustomManager() { }
    public static CustomManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CustomManager();
            }
            return instance;
        }
    }
}
