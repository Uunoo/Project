using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] public int health;
    [SerializeField] public int maxHealth = 10;

    [SerializeField] public SpriteRenderer playerSr;
    [SerializeField] public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            playerSr.enabled = false;
            playerMovement.enabled = false;
        }
    }
}

 