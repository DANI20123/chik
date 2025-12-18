using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public UIManager manager;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Gold"))
        {
            manager.valueScore += 10;
        }
        if(collision.gameObject.CompareTag("White"))
        {
            manager.valueScore += 4;
        }if(collision.gameObject.CompareTag("Gray"))
        {
            manager.valueScore -= 6;
            if(manager.valueScore<=0)
            {
                manager.valueScore = 0;
                manager.Lose();
            }
        }
        manager.ShowScore();
        Destroy(collision.gameObject);
    }
}
