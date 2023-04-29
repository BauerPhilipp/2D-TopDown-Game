using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        Debug.Log(damage + " damage");
        Debug.Log(currentHealth + " hp");
        
        DetectDeath();
    }

    private void DetectDeath()
    {
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
    }


}
