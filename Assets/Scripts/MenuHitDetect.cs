using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHitDetect : MonoBehaviour, IDamageable
{
    public bool quitButton = false;
    public bool startButton = false;

    public GameController gc;

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
            gc.LoadSceneByIndexPublic(sceneIndex);
        else if (quitButton && !gc.anim.GetBool("Fading"))
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
            
    }

    public void Stun(float duration)
    {
        //Do the dew...Mountian Dew
    }
}
