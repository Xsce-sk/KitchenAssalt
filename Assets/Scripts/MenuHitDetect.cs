using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHitDetect : MonoBehaviour, IDamageable
{
    public bool quitButton = false;
    public bool startButton = false;

    public int sceneIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseHealth(int healthDelta)
    {
        if (startButton)
            GameController.LoadSceneByIndex(sceneIndex);
        else if (quitButton)
            Application.Quit();
    }

    public void Stun(float duration)
    {
        //Do the dew...Mountian Dew
    }
}
