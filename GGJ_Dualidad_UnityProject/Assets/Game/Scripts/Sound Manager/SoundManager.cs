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

    public AudioClip[] AudioClips;

    public AudioMixerGroup output;

    [Range(0f, 1f)]
    public float MinVolume = 1f;

    [Range(0f, 1f)]
    public float MaxVolume = 1f;

    [Range(0f, 3f)]
    public float MinPitch = 1f;

    [Range(0f, 3f)]
    public float MaxPitch = 1f;

    [Range(-1f, 1f)]
    public float StereoPan = 0f;

    [Range(0f, 5f)]
    public float DelayTime = 0f;

    [Range(0f, 5f)]
    public float RandomizeDelay = 0f;

    public bool AvoidRepeat = false;

    public bool Delay = false;

    public bool RandomizeLoop = false;

    public bool Mute = false;

    public bool PlayOnAwake = false;

    [HideInInspector]
    public bool PlayCalled = false;


    public void Play()
    {
        PlayCalled = true;

        float randomvolume = Random.Range(MinVolume, MaxVolume);
        float randompitch = Random.Range(MinPitch, MaxPitch);

        audioSource.volume = randomvolume;
        audioSource.pitch = randompitch;
        audioSource.panStereo = StereoPan;
        audioSource.loop = Loop;
        audioSource.mute = Mute;
        audioSource.outputAudioMixerGroup = Output;


        if(Delay == false && AvoidRepeat == false){
        audioSource.clip = AudioClip[Random.Range(0, AudioClips.lenth)];
        audioSource.Play();
        }

        if (AvoidRepeat == true && Delay == false) {
        int r = Random.Range(1, audioClips.Lenth);
        audioSource.clip = AudioClips[r];
        audioSource.Play();
        AudioClips[r] = AudioClips[0];
        AudioClips[0] = audioSource.clip;
        }

        if(Delay == true && AvoidRepeat == true){
        float delay = Random.Range(DelayTime - RandomizeDelay, DelayTime + RandomizeDelay);
        int r = Random.Range(1, audioClips.Lenth);
        audioSource.clip = AudioClips[r];
        audioSource.PlayDelayed(delay);
        AudioClips[r] = AudioClips[0];
        AudioClips[0] = audioSource.clip;
        }

        if(Delay == true && AvoidRepeat == false)
        {
        float delay = Random.Range(DelayTime - RandomizeDelay, DelayTime + RandomizeDelay);
        audioSource.clip = AudioClip[Random.Range(0, AudioClips.lenth)];
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
    public static SoundManager instance = null;

    [SerializeField]
    SoundEvent[] SoundEvents;

    private void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        for (int i = 0; i < SoundEvents.Length; i++)
        {
            if (SoundEvents[i].audioSource == null)
            {
                GameObject gameObject = new GameObject("SoundEvent_" + i + "_" + SoundEvents[i].Name);
                gameObject.transform.SetParent(this.transform);
                SoundEvents[i].audioSource = gameObject.AddComponent<AudioSource>();
            }
            if (SoundEvent[i].PlayOnAwake)
            {
                SoundEvents[i].Play();
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
        for (int i = 0; i < SoundEvents.Length; i++)
        {
            if (SoundEvents[i].Name == _name)
            {
                SoundEvents[i].Play();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not foundin Sound events" + _name);
    }


    public void StopSound(string _name)
    {
        for (int i = 0; i < SoundEvents.Length; i++)
        {
            if (SoundEvents[i].Name == _name)
            {
                SoundEvents[i].Stop();
                return;
            }
        }
        Debug.LogWarning("SoundManager: Sound not foundin Sound events," + _name);
    }



    IEnumerator RepeatPlaySound()
    {
        for (int i = 0; i < SoundEvents.Length; i++)
        {
            if (SoundEvents[i].PlayCalled == true && SoundEvents[i].RandomizeLoop == true && SoundEvents[i].AvoidRepeat == true)
            {
                if (SoundEvents[i].Delay == false)
                {
                    yield return new WaitForSeconds(SoundEvents[i].audioSource.clip.lenght);
                }

                if (SoundEvent[i].Delay == true)
                {
                    float delay = Random.Range(SoundEvents[i].DelayTime - SoundEvents[i].RandomizeDelay, SoundEvents[i].DelayTime + SoundEvents[i].RandomizeDelay);
                    yield return new WaitForSeconds(SoundEvents[i].AudioSource.clips.length + delay);
                }
                if (!SoundEvents[i].audioSource.isPlaying)
                {
                PlaySound(SoundEvents[i].Name);
                }
            }
        }

    }
}
