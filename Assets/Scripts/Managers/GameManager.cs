using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;

    [SerializeField] private float propsSpeed;
    [SerializeField] private float propsDecelarationSpeed;
    [SerializeField] private float rotatingDecelarationSpeed;

    public enum GameState
    {
        MENU,
        GAME
    }
    
    private List<PropsSpeed> managedObjects = new List<PropsSpeed>();
    private List<RotatingObject> managedRotatingObjects = new List<RotatingObject>();
    public int generators;
    public bool gameHasStarted = false;    //Set this to true when the level is Generated
    private bool stillNeedToDecelerateMovingObjects = true;
    
    private GameState gameState = GameState.MENU;
    public PlayerController player;
    public Vector3 winPosition;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (gameManagerInstance == null)
            gameManagerInstance = this;
        else if(gameManagerInstance != this)
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.GAME_MUSIC);
            gameState = GameState.GAME;
        }
        else
        {
            SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.MENU_MUSIC);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.GAME:
                if (player == null)
                    break;
                
                // Victory
                if (generators <= 0 && gameHasStarted)
                {
                    FindObjectOfType<UIManager>().ShowVictory();
                    player.StartDancing();
                    StopAllMovingObjects();
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

    public void RegisterMovingObject(PropsSpeed managedObject)
    {
        managedObject.zSpeed = propsSpeed;
        managedObjects.Add(managedObject);
    }

    public void UnregisterMovingObject(PropsSpeed managedObject)
    {
        if (managedObjects.Contains(managedObject))
        {
            managedObjects.RemoveAt(managedObjects.FindIndex((x) => x == managedObject));
        }
    }
    
    public void RegisterRotatingObject(RotatingObject managedObject)
    {
        managedRotatingObjects.Add(managedObject);
    }
    
    public void UnregisterRotatingObject(RotatingObject managedObject)
    {
        if (managedRotatingObjects.Contains(managedObject))
        {
            managedRotatingObjects.RemoveAt(managedRotatingObjects.FindIndex((x) => x == managedObject));
        }
    }

    private void StopAllMovingObjects()
    {
        if (stillNeedToDecelerateMovingObjects)
        {
            stillNeedToDecelerateMovingObjects = false;
            for (int i = 0; i < managedObjects.Count; i++)
            {
                managedObjects[i].zSpeed -= propsDecelarationSpeed;

                if (managedObjects[i].zSpeed <= 0.0f)
                    managedObjects[i].zSpeed = 0.0f;

                if (managedObjects[i].zSpeed > 0.0f)
                    stillNeedToDecelerateMovingObjects = true;
            }
            
            for (int i = 0; i < managedRotatingObjects.Count; i++)
            {
                managedRotatingObjects[i].rotationSpeed.y -= rotatingDecelarationSpeed;

                if (managedRotatingObjects[i].rotationSpeed.y <= 0.0f)
                    managedRotatingObjects[i].rotationSpeed.y = 0.0f;

                if (managedRotatingObjects[i].rotationSpeed.magnitude > 0.0f)
                    stillNeedToDecelerateMovingObjects = true;
            }
        }
    }
    
    public void UpdateGameState(GameState newState)
    {
        switch (newState)
        {
            case GameState.MENU:
                SoundManager.soundManagerInstance.StopAllSounds();
                SceneManager.LoadScene("Menu");
                SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.MENU_MUSIC);
                gameState = newState;
                break;
            case GameState.GAME:
                gameHasStarted = false;
                stillNeedToDecelerateMovingObjects = true;
                generators = 0;
                managedObjects.Clear();
                managedRotatingObjects.Clear();
                Time.timeScale = 1;
                SceneManager.LoadScene("SampleScene");
                SoundManager.soundManagerInstance.PlayMusic(SoundManager.MusicList.GAME_MUSIC);
                gameState = newState;
                break;
                
        }
    }
}
