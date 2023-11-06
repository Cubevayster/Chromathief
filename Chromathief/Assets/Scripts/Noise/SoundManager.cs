using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SOUND_NAME
{
    DEFAULT,
    BROKEN_WALL
};

[Serializable]
public class PairNameSound
{
    [SerializeField] SOUND_NAME name = SOUND_NAME.DEFAULT;
    [SerializeField] AudioClip sound = null;

    public SOUND_NAME Name() => name;
    public AudioClip Sound() => sound;
}

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]List<PairNameSound> allSounds = new List<PairNameSound>();
    [SerializeField]List<SoundReceiver> allSoundReceivers = new List<SoundReceiver>();

    public List<PairNameSound> GetAllSounds() => allSounds;
    public List<SoundReceiver> GetNoiseRecevers() => allSoundReceivers;

    bool SoundReceiverAlreadyRegistered(SoundReceiver _noiseReceiver) => allSoundReceivers.Contains(_noiseReceiver);

    public void RegisterSoundReceiver(SoundReceiver _noiseReceiver)
    {
        if (SoundReceiverAlreadyRegistered(_noiseReceiver)) return;
        allSoundReceivers.Add(_noiseReceiver);
    }

    public void UnregisterSoundReceiver(SoundReceiver _noiseReceiver)
    {
        if (!SoundReceiverAlreadyRegistered(_noiseReceiver)) return;
        allSoundReceivers.Remove(_noiseReceiver);
    }

    AudioClip GetSoundByName(SOUND_NAME _name)
    {
        foreach (PairNameSound _soundData in allSounds)
            if (_soundData.Name() == _name) return _soundData.Sound();
        return null;
    }

    public void PlaySound(SOUND_NAME _soundName, Vector3 _soundPos, float _soundRange = -1)
    {
        AudioClip _sound = GetSoundByName(_soundName);
        //if (!_sound) return;
        //AudioSource.PlayClipAtPoint(_sound, _soundPos);
        foreach (SoundReceiver _possibleReceiver in allSoundReceivers)
            _possibleReceiver.OnSoundReceived.Invoke(_soundPos, _soundRange);
    }
}
