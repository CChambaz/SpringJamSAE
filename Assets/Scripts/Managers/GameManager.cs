using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    
    public enum GameState
    {
        MENU,
        GAME
    }
    
    public int generators;
    private bool gameHasStarted = false;    //Set this to true when the level is Generated

    private GameState gameState = GameState.MENU;
    public PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        if (gameManagerInstance == null)
            gameManagerInstance = this;
        else
            Destroy(this);
        
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene().name == "SampleScene")
            gameState = GameState.GAME;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        switch (gameState)
        {
            case GameState.GAME:
                if (player == null)
                    break;
                
                // Victory
                if (generators <= 0 && gameHasStarted)
                {
                    FindObjectOfType<UIManager>().ShowVictory();
                }   
                
                // Defeat
                if (player.isDead)
                {
                    FindObjectOfType<UIManager>().ShowDefeat();
                }
                break;
            default:
                break;
        }
    }

    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                SoundManager.soundManagerInstance.StopAllSounds();
                SceneManager.LoadScene("Menu");
                gameState = newState;
                break;
            case GameState.GAME:
                SceneManager.LoadScene("SampleScene");
                gameState = newState;
                break;
                
        }
    }
}
