using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicTransition : MonoBehaviour
{
    private static MusicTransition instance;
    public Animator MusicAnim;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Level 1")
        {
            StartCoroutine(FadeOutMusic());
        }
    }

    IEnumerator FadeOutMusic()
    {
        MusicAnim.SetTrigger("Gameplay");
        yield return null;
    }
}
