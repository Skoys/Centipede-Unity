using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Script_Boss : MonoBehaviour
{
    private int currentHealth = 0;
    private bool checkHealth = false;
    [SerializeField] private int maxHealth = 250;
    [SerializeField] private Slider healthUI;

    private int phase = 0;

    private Script_GameManager gameManager;
    private VisualEffect vfxHit;

    // Start is called before the first frame update
    void Start()
    {
        vfxHit = GetComponent<VisualEffect>();
        currentHealth = maxHealth;
        gameManager = Script_GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && checkHealth)
        {
            if (phase == 1)
            {
                IEnumerator coroutine = gameManager.NextGamePhase(true);
            }
        }
        
    }

    public void ChangeThePhase(int _phase)
    {
        phase = _phase;
        if(_phase == 1)
        {
            checkHealth = true;
        }
    }

    public void TakeDamage()
    {
        currentHealth -= 1;
        healthUI.value = (float)currentHealth / maxHealth;
        vfxHit.Play();
    }
}
