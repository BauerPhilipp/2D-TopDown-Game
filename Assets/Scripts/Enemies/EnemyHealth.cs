using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int startingHealth = 3;
    private int currentHealth;
    private Knockback knockback;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;

        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
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
