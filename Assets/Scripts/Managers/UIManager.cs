using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    private enum UIState
    {
        GAME,
        END_GAME,
        PAUSE
    }

    [Header("Player UI")] 
    [SerializeField] private CanvasGroup uiCanvas;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text hurricanText;

    [Header("Pause UI")] 
    [SerializeField] private CanvasGroup pauseCanvas;
    
    [Header("Defeat UI & parameters")]
    [SerializeField] private CanvasGroup defeatCanvas;
    [SerializeField] private float timeBeforeStop;

    [Header("Victory UI")]
    [SerializeField] private CanvasGroup victoryCanvas;
    
    private UIState state;
    private PlayerController player;
    private float defeatTimer = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        defeatCanvas.gameObject.SetActive(false);
        victoryCanvas.gameObject.SetActive(false);

        Time.timeScale = 1;
        SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.GAME_MUSIC);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if(Input.GetButtonDown("Cancel") && !defeatCanvas.isActiveAndEnabled && !victoryCanvas.isActiveAndEnabled)
            SwitchState();
        
        scoreText.text = player.score.ToString();
        hurricanText.text = player.hurricanUsage.ToString();
    }

    public void ShowVictory()
    {
        SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.WIN_MUSIC);
        uiCanvas.gameObject.SetActive(false);
        victoryCanvas.gameObject.SetActive(true);
    }

    public void ShowDefeat()
    {
        if (defeatTimer >= timeBeforeStop && Time.timeScale != 0)
        {
            SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.DEFEAT_MUSIC);
            Time.timeScale = 0;
            uiCanvas.gameObject.SetActive(false);
            defeatCanvas.gameObject.SetActive(true);
        }
        else
        {
            defeatTimer += Time.deltaTime;
        }
    }
    
    public void LoadMenu()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        Time.timeScale = 1;
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.MENU);
    }

    public void Restart()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        Time.timeScale = 1;
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.GAME);
    }
    
    public void SwitchState()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        
        switch (state)
        {
            case UIState.GAME:
                Time.timeScale = 0;
                pauseCanvas.gameObject.SetActive(true);
                state = UIState.PAUSE;
                break;
            case UIState.PAUSE:
                Time.timeScale = 1;
                pauseCanvas.gameObject.SetActive(false);
                state = UIState.GAME;
                break;
            default:
                break;
        }
    }
}
