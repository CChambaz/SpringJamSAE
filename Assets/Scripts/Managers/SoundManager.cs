using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManagerInstance;
    
    List<AudioSource> emitters = new List<AudioSource>();

    public enum AudioMixerGroup
    {
        PLAYER,
        ENVIRONMENT,
        CONSTANT_NOISE
    }
    
    public enum SoundList
    {
        RUN,
        FIRE,
        WOOD_IMPACT,
        STEEL_IMPACT,
        TRASH_IMPACT,
        GENE_DAMAGE,
        GENE_DESTROY,
        POWER_UP,
        CONVEYORBELT,
        DEATH,
        MENU_SELECTION,
        MENU_VALIDATION,
        WIN_MUSIC,
        LOSE_MUSIC
    }

    public enum MusicList
    {
        NONE,
        MENU_MUSIC,
        GAME_MUSIC
    }

    public struct LoopedSound
    {
        public AudioSource audioSource;
        public float timeUntilStop;
    }
    List<LoopedSound> loopedSoundList = new List<LoopedSound>();

    MusicList currentMusicPlaying = MusicList.NONE;

    [Header("VolumeSounds")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Sounds")]
    [SerializeField] AudioClip[] runClips;
    [SerializeField] AudioClip fireClip;
    [SerializeField] AudioClip[] woodImpactClip;
    [SerializeField] AudioClip[] steelImpactClip;
    [SerializeField] AudioClip[] trashImpactClip;
    [SerializeField] AudioClip conveyorBeltclip;
    [SerializeField] AudioClip geneDamageClip;
    [SerializeField] AudioClip geneDestroyClip;
    [SerializeField] AudioClip powerUpClip;
    [SerializeField] AudioClip[] deathClips;
    [SerializeField] AudioClip menuSelection;
    [SerializeField] AudioClip menuValidation;

    [Header("Musics")]
    [SerializeField] AudioClip menuMusicClip;
    [SerializeField] AudioClip gameMusicClip;
    [SerializeField] AudioClip winMusicClip; // Not looped
    [SerializeField] AudioClip loseMusicClip; // Not looped

    [Header("Emmiters")]
    [SerializeField] GameObject emitterPrefab;
    [SerializeField] int emitterNumber;
    [SerializeField] AudioSource musicEmitter;

    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        if (soundManagerInstance == null)
            soundManagerInstance = this;
        else
            Destroy(this);
        
        Random.InitState(Random.Range(0, int.MaxValue));

        for (int i = 0; i <= emitterNumber;i++)
        {
            GameObject audioObject = Instantiate(emitterPrefab, emitterPrefab.transform.position, emitterPrefab.transform.rotation);
            emitters.Add(audioObject.GetComponent<AudioSource>());
            DontDestroyOnLoad(audioObject);
        }
    }

    private void Update()
    {
        foreach(LoopedSound loopedSound in loopedSoundList)
        {
            if(Utilities.CheckTimer(loopedSound.timeUntilStop))
            {
                loopedSound.audioSource.Stop();
                loopedSoundList.Remove(loopedSound);
                break;
            }
        }
    }

    public void StopAllSounds()
    {
        foreach (AudioSource audioSource in emitters)
        {
            audioSource.Stop();
        }
    }
    public void PlaySound(SoundList sound, AudioMixerGroup group, float timeToLoop = 0.0f, bool infiniteLoop = false)
    {
        AudioSource emitterAvailable = null;

        foreach(AudioSource emitter in emitters)
        {
            if(!emitter.isPlaying)
            {
                emitterAvailable = emitter;
            }
        }

        if (emitterAvailable != null)
        {
            emitterAvailable.loop = false;
            switch (sound)
            {
                case SoundList.RUN:
                    emitterAvailable.clip = runClips[Random.Range(0, runClips.Length - 1)];
                    break;

                case SoundList.FIRE:
                    emitterAvailable.clip = fireClip;
                    break;

                case SoundList.WOOD_IMPACT:
                    emitterAvailable.clip = woodImpactClip[Random.Range(0, woodImpactClip.Length - 1)];
                    break;

                case SoundList.STEEL_IMPACT:
                    emitterAvailable.clip = steelImpactClip[Random.Range(0, steelImpactClip.Length - 1)];
                    break;

                case SoundList.TRASH_IMPACT:
                    emitterAvailable.clip = trashImpactClip[Random.Range(0, trashImpactClip.Length - 1)];
                    break;
                
                case SoundList.CONVEYORBELT:
                    emitterAvailable.clip = conveyorBeltclip;
                    break;
                
                case SoundList.GENE_DAMAGE:
                    emitterAvailable.clip = geneDamageClip;
                    break;
                
                case SoundList.GENE_DESTROY:
                    emitterAvailable.clip = geneDestroyClip;
                    break;
                
                case SoundList.POWER_UP:
                    emitterAvailable.clip = powerUpClip;
                    break;
                
                case SoundList.DEATH:
                    emitterAvailable.clip = deathClips[Random.Range(0, deathClips.Length - 1)];
                    break; 

                case SoundList.MENU_SELECTION:
                    emitterAvailable.clip = menuSelection;
                    break;

                case SoundList.MENU_VALIDATION:
                    emitterAvailable.clip = menuValidation;
                    break;
                case SoundList.WIN_MUSIC:
                    musicEmitter.clip = winMusicClip;
                    musicEmitter.Play();
                    break;
                case SoundList.LOSE_MUSIC:
                    musicEmitter.clip = loseMusicClip;
                    musicEmitter.Play();
                    break;
            }

            if(timeToLoop > 0.0f)
            {
                emitterAvailable.loop = true;

                if(!infiniteLoop)
                {
                    LoopedSound newLoopSound = new LoopedSound
                    {
                        audioSource = emitterAvailable,
                        timeUntilStop = Utilities.StartTimer(timeToLoop)
                    };
                    loopedSoundList.Add(newLoopSound);
                }
            }

            switch (group)
            {
                case AudioMixerGroup.PLAYER:
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Player")[0];
                    break;
                case AudioMixerGroup.ENVIRONMENT:
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Environment")[0];
                    break;
                case AudioMixerGroup.CONSTANT_NOISE:
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("ConstantNoise")[0];
                    break;
            }
            
            emitterAvailable.Play();
        }
        else
        {
            Debug.Log("no emitter available");
        }        
    }

    public void PlayMusic(MusicList music)
    {
        if (currentMusicPlaying != music)
        {
            musicEmitter.loop = true;

            switch (music)
            {
                case MusicList.MENU_MUSIC:
                    musicEmitter.clip = menuMusicClip;
                    musicEmitter.Play();
                    break;
                case MusicList.GAME_MUSIC:
                    musicEmitter.clip = gameMusicClip;
                    musicEmitter.Play();
                    break;
                case MusicList.NONE:
                    musicEmitter.Stop();
                    break;
            }
            currentMusicPlaying = music;
        }
    }
}
