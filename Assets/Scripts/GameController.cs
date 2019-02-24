using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public static void TogglePanels(GameObject panel)
    {
        if (panel.activeInHierarchy)
            panel.SetActive(false);
        else
            panel.SetActive(true);
    }

    public static void TogglePause()
    {
        PlayerController playerInputs = GameObject.Find("Player").GetComponent<PlayerController>();

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if(playerInputs != null)
            playerInputs.enabled = !playerInputs.enabled;
    }
}
