using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject credits;
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
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        GameManager.gameManagerInstance.UpdateGameState(GameManager.GameState.GAME);
    }

    public void Help()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        menu.SetActive(false);
        credits.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void Credit()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        menu.SetActive(false);
        credits.SetActive(true);
        helpMenu.SetActive(false);
    }

    public void MainMenu()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_SELECTION, SoundManager.AudioMixerGroup.PLAYER);
        helpMenu.SetActive(false);
        credits.SetActive(false);
        menu.SetActive(true);
    }
    public void ExitGame()
    {
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.MENU_VALIDATION, SoundManager.AudioMixerGroup.PLAYER);
        Application.Quit();
    }
}
