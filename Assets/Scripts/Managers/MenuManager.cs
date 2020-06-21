using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void StartGame()
    {
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.GAME);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
