using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;


    [SerializeField] public Sprite emptyHeart;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Image[] hearts;


    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;


     for(int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            
            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
    }
}
