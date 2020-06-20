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
    [SerializeField] private Text scoreText;
    [SerializeField] private Text hurricanText;

    [Header("Pause UI")] 
    [SerializeField] private CanvasGroup pauseCanvas;
    
    [Header("Defeat UI")]
    [SerializeField] private CanvasGroup defeatCanvas;
    
    [Header("Victory UI")]
    [SerializeField] private CanvasGroup victoryCanvas;
    
    private UIState state;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas.gameObject.SetActive(false);
        defeatCanvas.gameObject.SetActive(false);
        victoryCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            player = FindObjectOfType<PlayerController>();

        if(Input.GetButtonDown("Cancel"))
            SwitchState();
        
        scoreText.text = player.score.ToString();
        hurricanText.text = player.hurricanUsage.ToString();
    }

    public void ShowVictory()
    {
        victoryCanvas.gameObject.SetActive(true);
    }

    public void ShowDefeat()
    {
        defeatCanvas.gameObject.SetActive(true);
    }
    
    public void LoadMenu()
    {
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.MENU);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.GAME);
    }
    
    public void SwitchState()
    {
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
