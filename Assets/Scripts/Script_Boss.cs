using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Boss : MonoBehaviour
{
    private int currentHealth = 0;
    [SerializeField] private int maxHealth = 250;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
    }
}
