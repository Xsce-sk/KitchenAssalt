using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LifeUI : MonoBehaviour
{
    public PlayerScript player;
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        tmp.text = "HP: " + player.CurrentHealth();
    }
}
