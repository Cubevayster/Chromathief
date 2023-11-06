using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReceiver : MonoBehaviour
{
    public Action<Vector3, float> OnSoundReceived = null;

    private void Start() => InitializeReceiver();

    private void OnDestroy()
    {
        OnSoundReceived = null;
    }

    void InitializeReceiver()
    {
        SoundManager.Instance.RegisterSoundReceiver(this);
    }
}
