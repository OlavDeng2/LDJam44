using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Panel[] allPanels;
    public Panel mainMenuPanel;
    public GameObject loadingScreen;

    public void Start()
    {
        allPanels = FindObjectsOfType<Panel>();
        SwitchPanel(mainMenuPanel);
        loadingScreen.SetActive(false);
    }
    public void SwitchPanel(Panel nextPanel)
    {
        foreach(Panel panel in allPanels)
        {
            panel.gameObject.SetActive(false);
        }

        nextPanel.gameObject.SetActive(true);
    }

    public void LoadLevel(int scene)
    {
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
