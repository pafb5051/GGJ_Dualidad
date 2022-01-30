using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    SoundManager soundManager;

    public string[] _soundnames;

    public string[] _Name
    {
        get { return _soundnames; }
        set { _soundnames = value; }
    }

    private void Start()
    {
        soundManager = soundManager.instance;
    }

    public void PlaySoundEvent(string _soundnames)
    {
        soundManager.PlaySound(_soundnames);
    }

    public void StopSoundEvent(string _soundnames)
    {
        soundManager.StopSound(_soundnames);
    }
    }
}
