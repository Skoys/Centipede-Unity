using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Block : MonoBehaviour
{
    [SerializeField] private int life = 5;

    public void TakeDamage()
    {
        life -= 1;
        if (life == 0)
        {
            Destroy(gameObject);
        }
    }

    
}
