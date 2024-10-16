using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class Script_Ennemie_Block : MonoBehaviour
{
    Script_Player_Inputs playerInputs;
    [SerializeField] private int life = 5;
    [SerializeField] private int points = 50;
    [SerializeField] private GameObject bonusPREFAB;

    private Script_UI ui;

    private void Start()
    {
        ui  = Script_UI.instance;
        playerInputs = Script_Player_Inputs.instance;
        GetComponent<VisualEffect>().Play();
    }

    public void TakeDamage()
    {
        playerInputs.AddRumble(new Vector2(0.3f, 0.05f), 0.2f);
        life -= 1;
        if (life <= 0)
        {
            if(Random.Range(0,3) == 1)
            {
                Instantiate(bonusPREFAB,transform.position, Quaternion.identity);
            }
            ui.UpdateScore(points);
            Destroy(gameObject);
        }
    }
}
