using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public CameraController PauseCameraController;

    public GameObject GameUI;
    public GameObject PausedUI;

    public float Pausei = 1f;

    
    public GameObject SettingsUI;

    public GameObject AudioSettingsUI;
    public GameObject GamePlayUI;
    public GameObject VideoSettingsUI;

    public PlayerHealth PlayerHealthScript;



    public Animator transition;


    public int PressTab = 1;
    public Image TimerIcon;
    public Image TimerSquareIcon;
    public TMP_Text TimerText;

    public Image ScoreIcon;
    public TMP_Text ScoreText;


    private void Start()
    {
        PauseCameraController.enabled = true;
        GameUI.SetActive(true);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StartCoroutine(LoadLevel(0));
        }

        if (Pausei == -1)
        {
            PauseCameraController.enabled = false;
        }

        if (Pausei == 1)
        {
            PauseCameraController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PressTab = PressTab * -1;
        }

        if (PressTab == -1)
        {
            TimerIcon.color = new Color(TimerIcon.color.r, TimerIcon.color.g, TimerIcon.color.b, 1f);
            TimerSquareIcon.color = new Color(TimerSquareIcon.color.r, TimerSquareIcon.color.g, TimerSquareIcon.color.b, 1f);
            ScoreIcon.color = new Color(ScoreIcon.color.r, ScoreIcon.color.g, ScoreIcon.color.b, 1f);
            TimerText.color = new Color(TimerText.color.r, TimerText.color.g, TimerText.color.b, 1f);
            ScoreText.color = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 1f);
        }

        if (PressTab == 1)
        {
            TimerIcon.color = new Color(TimerIcon.color.r, TimerIcon.color.g, TimerIcon.color.b, 0f);
            TimerSquareIcon.color = new Color(TimerSquareIcon.color.r, TimerSquareIcon.color.g, TimerSquareIcon.color.b, 0f);
            ScoreIcon.color = new Color(ScoreIcon.color.r, ScoreIcon.color.g, ScoreIcon.color.b, 0f);
            TimerText.color = new Color(TimerText.color.r, TimerText.color.g, TimerText.color.b, 0f);
            ScoreText.color = new Color(ScoreText.color.r, ScoreText.color.g, ScoreText.color.b, 0f);
        }
    }

    public void ContinueButton()
    {
        GameUI.SetActive(true);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Pausei = 1;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = 1;
        StartCoroutine(LoadLevel(0));
    }

    public void SettingsMenu()
    {
        PauseCameraController.enabled = true;
        GameUI.SetActive(false);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(true);
        AudioSettingsUI.SetActive(false);
        GamePlayUI.SetActive(false);
        VideoSettingsUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = -1;
    }


    public void BackButton()
    {
        GameUI.SetActive(false);
        PausedUI.SetActive(true);
        SettingsUI.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = -1;
    }

    public void VideoButton()
    {
        GameUI.SetActive(false);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(true);
        AudioSettingsUI.SetActive(false);
        GamePlayUI.SetActive(false);
        VideoSettingsUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = -1;
    }

    public void AudioButton()
    {
        GameUI.SetActive(false);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(true);
        AudioSettingsUI.SetActive(true);
        GamePlayUI.SetActive(false);
        VideoSettingsUI.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = -1;
    }

    public void GamePlayButton()
    {
        GameUI.SetActive(false);
        PausedUI.SetActive(false);
        SettingsUI.SetActive(true);
        AudioSettingsUI.SetActive(false);
        GamePlayUI.SetActive(true);
        VideoSettingsUI.SetActive(false);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = -1;
    }

    public void RetryButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Pausei = 1;
        StartCoroutine(LoadLevel(2));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelIndex);
    }
}
