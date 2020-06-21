using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject helpMenu;
    //[SerializeField] private GameObject next;
    //[SerializeField] private GameObject back;

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

    public void Help()
    {
        menu.SetActive(false);
        helpMenu.SetActive(true);
    }

    /*public void HelpNext(GameObject current, GameObject next)
    {
        next.SetActive(true);
        back.SetActive(true);
    }

    public void HelpBack(GameObject current, GameObject back)
    {
        next.SetActive(true);
        back.SetActive(true);
    }*/

    public void MainMenu()
    {
        helpMenu.SetActive(false);
        menu.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
