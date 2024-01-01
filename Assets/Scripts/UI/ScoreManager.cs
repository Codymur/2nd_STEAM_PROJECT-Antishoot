using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    public Image MinigunSlider;
    public float MinigunValue;
    public GameObject PressEForMinigun;

    public WeaponSwitching WeaponSwitchScript;


    public PlayerHealth HealthScript;

    public Text FinalTimer;

    private void Start()
    {
        MinigunSlider.fillAmount = MinigunValue;
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<TMP_Text>();
        // Initialize the score and update the text
        UpdateScore();
    }

    public void AddToScore(int points)
    {
        if (!WeaponSwitchScript.MinigunIsEnabled)
        {
            // Add points to the score
            score += points;

            // Update the score text
            UpdateScore();
        }
    }

    public void MinigunSliderFiller(float minigunSliderFillingAmount)
    {
        if (!WeaponSwitchScript.MinigunIsEnabled)
        {

            MinigunValue += minigunSliderFillingAmount;
            MinigunSlider.fillAmount = MinigunValue;
        }
    }

    private void UpdateScore()
    {
        // Update the TextMeshPro Text component with the current score as a string
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (MinigunSlider.fillAmount == 1)
        {
            MinigunSlider.color = Color.yellow;
            PressEForMinigun.SetActive(true);
        }
        else
        {
            MinigunSlider.color = Color.white;
            PressEForMinigun.SetActive(false);
        }

        if (HealthScript.HealthBar.fillAmount <= 0f)
        {
            FinalTimer.text = scoreText.text;
        }
    }
}
