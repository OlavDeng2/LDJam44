using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Panel[] allPanels;
    public Panel mainMenuPanel;

    public void Start()
    {
        allPanels = FindObjectsOfType<Panel>();
        SwitchPanel(mainMenuPanel);
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
        SceneManager.LoadScene(scene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
