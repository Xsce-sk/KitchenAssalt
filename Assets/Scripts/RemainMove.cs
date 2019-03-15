using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainMove : MonoBehaviour
{
    
    void Start()
    {
        for(int i=0; i<6; ++i)
        {
        	int rand = Random.Range(0,4);
        	switch (rand)
        	{
        		case 0:
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        			break;
        		case 1:
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.down * 100);
        			break;
        		case 2:
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100);
        			break;
        		case 3:
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
        			transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100);
        			break;
        	}
        }
    }

}
