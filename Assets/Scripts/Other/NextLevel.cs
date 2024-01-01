using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    IEnumerator ToLevels()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(ToLevels());
        }
    }
}
