using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Boss : MonoBehaviour
{
    private int currentHealth = 0;
    [SerializeField] private int maxHealth = 250;
    [SerializeField] private Slider healthUI;

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
        healthUI.value = (float)currentHealth / maxHealth;
    }
}
