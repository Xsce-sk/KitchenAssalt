using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollideReset : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameController.LoadSceneByIndex(0);
        }
    }
}
