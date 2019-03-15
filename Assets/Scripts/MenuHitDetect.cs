using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuHitDetect : MonoBehaviour, IDamageable
{
    public bool quitButton = false;
    public bool startButton = false;

    public Image blackImage;
    public Animator anim;

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
            StartCoroutine(Fading());
        else if (quitButton)
            Application.Quit();
    }

    public void Stun(float duration)
    {
        //Do the dew...Mountian Dew
    }

    IEnumerator Fading()
    {
        anim.SetBool("Fading", true);
        yield return new WaitUntil(() => blackImage.color.a == 1);
        GameController.LoadSceneByIndex(sceneIndex);
    }
}
