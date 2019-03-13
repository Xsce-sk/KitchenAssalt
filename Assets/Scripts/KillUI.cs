using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillUI : MonoBehaviour
{
    public int killNum = 0;
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "Kills: " + killNum; 
    }

    public void IterateKillNum(int deaths)
    {
        killNum += deaths;
    }
}
