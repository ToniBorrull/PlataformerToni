using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLimits : MonoBehaviour
{
    public Transform respawn;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player.transform.position = respawn.transform.position;
        }
    }
}
