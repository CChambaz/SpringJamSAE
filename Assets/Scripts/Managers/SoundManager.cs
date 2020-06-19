using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    List<AudioSource> emitters = new List<AudioSource>();

    public enum SoundList
    {
        WALK,
        JUMP,
        SWITCH,
        APPEAR,
        BOMB_MESH,
        BOMB_EXPLOSION,
        CAR_SOUND,
        TRAIN,
        BOAT,
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

    List<AudioClip> listWalkSounds = new List<AudioClip>();
    List<AudioClip> listCarSounds = new List<AudioClip>();

    [Header("VolumeSounds")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Sounds")]
    [SerializeField] AudioClip[] jumpClip;
    [SerializeField] AudioClip switchClip;
    [SerializeField] AudioClip appearClip;
    [SerializeField] AudioClip bombMeshClip;
    [SerializeField] AudioClip bombExplosionClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip menuSelection;
    [SerializeField] AudioClip menuValidation;

    [SerializeField] AudioClip trainClip;
    [SerializeField] AudioClip boatClip;

    [Header("WalkClips")]
    [SerializeField] AudioClip walkClip1;
    [SerializeField] AudioClip walkClip2;
    [SerializeField] AudioClip walkClip3;

    [Header("CarClips")]
    [SerializeField] AudioClip carSoundClip1;
    [SerializeField] AudioClip carSoundClip2;
    [SerializeField] AudioClip carSoundClip3;

    [Header("Musics")]
    [SerializeField] AudioClip menuMusicClip;
    [SerializeField] AudioClip gameMusicClip;
    [SerializeField] AudioClip winMusicClip; // Not looped
    [SerializeField] AudioClip loseMusicClip; // Not looped

    [Header("Emmiters")]
    [SerializeField] GameObject emitterPrefab;
    [SerializeField] int emitterNumber;
    [SerializeField] AudioSource musicEmitter;

    public AudioClip SwitchClip
    {
        get
        {
            return switchClip;
        }

        set
        {
            switchClip = value;
        }
    }


    // Use this for initialization
    void Start ()
    {
        DontDestroyOnLoad(gameObject);

        Random.InitState(Random.Range(0, int.MaxValue));

        for (int i = 0; i <= emitterNumber;i++)
        {
            GameObject audioObject = Instantiate(emitterPrefab, emitterPrefab.transform.position, emitterPrefab.transform.rotation);
            emitters.Add(audioObject.GetComponent<AudioSource>());
            DontDestroyOnLoad(audioObject);
        }

        listWalkSounds = new List<AudioClip>{walkClip1,
                                    walkClip2,
                                    walkClip3
        };

        listCarSounds = new List<AudioClip>{carSoundClip1,
                                    carSoundClip2,
                                    carSoundClip3
        };
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

    public void PlaySound(SoundList sound, float timeToLoop = 0.0f)
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
                case SoundList.WALK:
                    int random = Random.Range(0, listWalkSounds.Count);
                    emitterAvailable.clip = listWalkSounds[random];
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Footsteps")[0];
                    break;

                case SoundList.JUMP:
                    emitterAvailable.clip = jumpClip[Random.Range(0, jumpClip.Length - 1)];
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Jump")[0];
                    break;

                case SoundList.SWITCH:
                    emitterAvailable.clip = switchClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Switch")[0];
                    break;

                case SoundList.APPEAR:
                    emitterAvailable.clip = appearClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Appear")[0];
                    break;

                case SoundList.BOMB_MESH:
                    emitterAvailable.clip = bombMeshClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Mesh")[0];
                    break;

                case SoundList.BOMB_EXPLOSION:
                    emitterAvailable.clip = bombExplosionClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Explosion")[0];
                    break;

                case SoundList.CAR_SOUND:
                    int random2 = Random.Range(0, listCarSounds.Count);
                    emitterAvailable.clip = listCarSounds[random2];
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Car")[0];
                    break;

                case SoundList.TRAIN:
                    emitterAvailable.clip = trainClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Train")[0];
                    break;

                case SoundList.BOAT:
                    emitterAvailable.clip = boatClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Boat")[0];
                    break;

                case SoundList.DEATH:
                    emitterAvailable.clip = deathClip;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Death")[0];
                    break; 

                case SoundList.MENU_SELECTION:
                    emitterAvailable.clip = menuSelection;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Menu")[0];
                    break;

                case SoundList.MENU_VALIDATION:
                    emitterAvailable.clip = menuValidation;
                    emitterAvailable.outputAudioMixerGroup = audioMixer.FindMatchingGroups("Menu")[0];
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
                LoopedSound newLoopSound = new LoopedSound
                {
                    audioSource = emitterAvailable,
                    timeUntilStop = Utilities.StartTimer(timeToLoop)
                };
                loopedSoundList.Add(newLoopSound);  
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
