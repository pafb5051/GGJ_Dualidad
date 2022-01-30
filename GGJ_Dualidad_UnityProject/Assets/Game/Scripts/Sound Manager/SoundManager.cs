using System.Collections.Concurrent;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using Ramdom = UnityEngine.Random;


[System.Serializable]
public class SoundEvent
{
    public string name;

    public AudioSource audioSource;

    public AudioClip[] audioClips;

    public AudioMixerGroup output;

    [Range(0f, 1f)]
    public float minVolume = 1f;

    [Range(0f, 1f)]
    public float maxVolume = 1f;

    [Range(0f, 3f)]
    public float minPitch = 1f;

    [Range(0f, 3f)]
    public float maxPitch = 1f;

    [Range(-1f, 1f)]
    public float stereoPan = 0f;

    [Range(0f, 5f)]
    public float delayTime = 0f;

    [Range(0f, 5f)]
    public float randomizeDelay = 0f;

    public bool avoidRepeat = false;

    public bool delay = false;

    public bool randomizeLoop = false;

    public bool mute = false;

    public bool playOnAwake = false;

    [HideInInspector]
    public bool playCalled = false;

    public bool loop;


    public void Play()
    {
        playCalled = true;

        float randomvolume = Random.Range(minVolume, maxVolume);
        float randompitch = Random.Range(minPitch, maxPitch);

        audioSource.volume = randomvolume;
        audioSource.pitch = randompitch;
        audioSource.panStereo = stereoPan;
        audioSource.loop = loop;
        audioSource.mute = mute;
        audioSource.outputAudioMixerGroup = output;


        if(delay == false && avoidRepeat == false){
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
        }

        if (avoidRepeat == true && delay == false) {
            int r = Random.Range(1, audioClips.Length);
            audioSource.clip = audioClips[r];
            audioSource.Play();
            audioClips[r] = audioClips[0];
            audioClips[0] = audioSource.clip;
        }

        if(delay == true && avoidRepeat == true){
            float delay = Random.Range(delayTime - randomizeDelay, delayTime + randomizeDelay);
            int r = Random.Range(1, audioClips.Length);
            audioSource.clip = audioClips[r];
            audioSource.PlayDelayed(delay);
            audioClips[r] = audioClips[0];
            audioClips[0] = audioSource.clip;
        }

        if(delay == true && avoidRepeat == false)
        {
        float delay = Random.Range(delayTime - randomizeDelay, delayTime + randomizeDelay);
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.PlayDelayed(delay);
        }
}

        public void Stop()
        {
          if (audioSource.isPlaying)
             {
            audioSource.Stop();
             }
        }

}


public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance = null;
    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    [SerializeField]
    SoundEvent[] soundEvents;

    private void Awake()
    {
        if (_instance != null)
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int i = 0; i < soundEvents.Length; i++)
        {
            if (soundEvents[i].audioSource == null)
            {
                GameObject gameObject = new GameObject("SoundEvent_" + i + "_" + soundEvents[i].name);
                gameObject.transform.SetParent(this.transform);
                soundEvents[i].audioSource = gameObject.AddComponent<AudioSource>();
            }
            if (soundEvents[i].playOnAwake)
            {
                soundEvents[i].Play();
                return;
            }
        }
    }

    private void Update()
    {
        InitializeCoroutine();
    }


    void InitializeCoroutine()
    {
        StartCoroutine(RepeatPlaySound());
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < soundEvents.Length; i++)
        {
            if (soundEvents[i].name == _name)
            {
                soundEvents[i].Play();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not foundin Sound events" + _name);
    }


    public void StopSound(string _name)
    {
        for (int i = 0; i < soundEvents.Length; i++)
        {
            if (soundEvents[i].name == _name)
            {
                soundEvents[i].Stop();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not foundin Sound events," + _name);
    }



    IEnumerator RepeatPlaySound()
    {
        for (int i = 0; i < soundEvents.Length; i++)
        {
            if (soundEvents[i].playCalled == true && soundEvents[i].randomizeLoop == true && soundEvents[i].avoidRepeat == true)
            {
                if (soundEvents[i].delay == false)
                {
                    yield return new WaitForSeconds(soundEvents[i].audioSource.clip.length);
                }

                if (soundEvents[i].delay == true)
                {
                    float delay = Random.Range(soundEvents[i].delayTime - soundEvents[i].randomizeDelay, soundEvents[i].delayTime + soundEvents[i].randomizeDelay);
                    yield return new WaitForSeconds(soundEvents[i].audioSource.clip.length + delay);
                }
                if (!soundEvents[i].audioSource.isPlaying)
                {
                    PlaySound(soundEvents[i].name);
                }
            }
        }

    }
}
