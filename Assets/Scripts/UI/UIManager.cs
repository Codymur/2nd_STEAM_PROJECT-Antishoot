using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Animator transition;

    public GameObject CreditsUI;
    int i = 1;

    public void Play()
    {
        StartCoroutine(LoadLevel(2));
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {
        StartCoroutine(LoadLevel(1));
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadLevel(0));
    }
    public void Credits()
    {
        i = i * -1;

        if (i == -1)
        {
            CreditsUI.SetActive(true);
        }
        else if(i == 1)
        {
            CreditsUI.SetActive(false);
        }
        
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
