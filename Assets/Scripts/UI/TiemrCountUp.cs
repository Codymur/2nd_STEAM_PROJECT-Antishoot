using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TiemrCountUp : MonoBehaviour
{
	public TMP_Text timerText;

	private float milisecondsCount;
	private float secondsCount;
    private int minuteCount;


	public GameObject TimerObject;

	public PlayerHealth HealthScript;

	public Text PlayerTimer;

	void Update()
	{
        if (HealthScript.HealthBar.fillAmount <= 0f)
        {
			PlayerTimer.text = timerText.text;
        }
	}

	//call this on update
	public void UpdateTimerUI()
	{
		//set timer UI
		milisecondsCount += Time.deltaTime * 90;
		timerText.text = minuteCount + ":" + (int)secondsCount + ":" + (int)milisecondsCount;
        if (milisecondsCount >= 90)
        {
			secondsCount++;
			milisecondsCount = 0;
		}
		else if (secondsCount >= 60)
		{
			minuteCount++;
			secondsCount = 0;
		}
	}


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MainPlayerTag" && HealthScript.HealthBar.fillAmount > 0f)
        {
			UpdateTimerUI();
        }
    }
}
