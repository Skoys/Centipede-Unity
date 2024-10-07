using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Script_Player_Bullet : MonoBehaviour
{
    public Script_Player player;

    [SerializeField] float speed = 1;
    [SerializeField] Vector2 direction = new Vector2(0,1);
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = direction * speed;
    }

    private void Die()
    {
        player.bulletAvailable = true;
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag == "OutOfBound")
        {
            Die();
        }
        if (other.transform.tag == "Ennemie")
        {
            if (other.GetComponent<Script_Ennemie_Centipede>() != null)
            {
                other.GetComponent<Script_Ennemie_Centipede>().Die();
                Die();
            }
        }
    }
}
