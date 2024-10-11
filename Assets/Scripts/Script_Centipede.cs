using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Script_Ennemie_Centipede : MonoBehaviour
{
    [SerializeField] private bool isUp;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int currentElevation = 20;
    [SerializeField] private float speed = 5;
    [SerializeField] private float offset = 1;
    [SerializeField] private int life = 20;
    [SerializeField] private GameObject blocPREFAB;
    public Vector2 direction = new Vector2(-1, 0);

    Script_Player_Inputs playerInputs;
    Script_GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInputs = Script_Player_Inputs.instance;
        gameManager = Script_GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void TakeDamage()
    {
        playerInputs.AddRumble(new Vector2(0.6f, 0.6f), 0.2f);
        gameManager.currentHealth--;
        life -= 1;
        if(life == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(blocPREFAB, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "OutOfBound" || collision.transform.tag == "Bloc")
        {
            if(!isUp && currentElevation - 1 < 0)
            {
                isUp = true;
                offset = -offset;            
            }
            else if (isUp && currentElevation + 1 > 6)
            {
                isUp = false;
                offset = -offset;
            }
            direction *= -1;
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, -offset, 0);
            currentElevation -= offset > 0 ? 1 : -1;
        }
    }
}
