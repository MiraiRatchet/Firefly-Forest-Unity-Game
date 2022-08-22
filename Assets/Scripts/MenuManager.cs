using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private AudioManager audioManager;
    public LevelLoader lvlLoader;

    public GameObject StartPanel;
    public GameObject LevelPanel;

    public GameObject settings;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        lvlLoader = FindObjectOfType<LevelLoader>();
        settings = GameObject.FindGameObjectWithTag("Set");

    }
    public void ExitGame()
    {
        audioManager.Play("ClickButton");
        Debug.Log("Exit");
        Application.Quit();
    }

    public void StartGame()
    {
        audioManager.Play("MouseClick");
        StartPanel.SetActive(false);
        LevelPanel.SetActive(true);
    }


    public void StartEasyGame()
    {
        settings.GetComponent<SettingsScript>().level = 9;
        audioManager.Play("MouseClick");
        audioManager.Stop("MusicMainTheme");
        lvlLoader.LoadNextLevel();
    }

    public void StartNormalGame()
    {
        settings.GetComponent<SettingsScript>().level = 12;
        audioManager.Play("MouseClick");
        audioManager.Stop("MusicMainTheme");
        lvlLoader.LoadNextLevel();
    }

    public void StartHardGame()
    {
        settings.GetComponent<SettingsScript>().level = 15;
        audioManager.Play("MouseClick");
        audioManager.Stop("MusicMainTheme");
        lvlLoader.LoadNextLevel();
    }

}
