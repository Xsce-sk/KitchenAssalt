using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static UnityEvent pauseEvent = new UnityEvent();
    public Image blackImage;
    public Animator anim;
    private int sceneIndex;

    private void Awake()
    {
        GameObject fadeImage = GameObject.Find("FadeImage");
        blackImage = fadeImage.GetComponent<Image>();
        anim = fadeImage.GetComponent<Animator>();
    }

    public void LoadSceneByIndexPublic(int index)
    {
        sceneIndex = index;
        StartCoroutine(Fading());
    }

    public static void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void QuitGamePublic()
    {
        QuitGame();
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public void TogglePanelsPublic(GameObject panel)
    {
        TogglePanels(panel);
    }

    public static void TogglePanels(GameObject panel)
    {
        if (panel.activeInHierarchy)
            panel.SetActive(false);
        else
            panel.SetActive(true);
    }

    public void TogglePausePublic()
    {
        TogglePause();
    }

    public static void TogglePause()
    {
        //PlayerController playerInputs = GameObject.Find("Player").GetComponent<PlayerController>();
        pauseEvent.Invoke();
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        //if(playerInputs != null)
            //playerInputs.enabled = !playerInputs.enabled;
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fading", true);
        yield return new WaitUntil(() => blackImage.color.a == 1);
        LoadSceneByIndex(sceneIndex);
    }
}
