using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    public int health = 3;
    public int numOfHearts = 3;


    public GameObject player;
    public Transform respawn;
    /*
    public Player player;
    public IAEnemigo enemy;
     public int tipo;
    */

   

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void Update()
    {
        if(health <= 0)
        {
            player.transform.position = respawn.transform.position;
            health = 3;
            Debug.Log("0 hearts");
        }

        if (health > numOfHearts)
        {
            health = numOfHearts;
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }

        }
    }

    private void OnTriggerEnter2D (Collider2D Collision)
    {
       
        if (Collision.CompareTag("MapLimits"))
        {
            health--;
        }
    }
}