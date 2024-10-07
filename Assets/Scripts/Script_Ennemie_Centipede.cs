using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Centipede : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5;
    [SerializeField] private float offset = 1;
    public Vector2 direction = new Vector2(-1, 0);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "OutOfBound")
        {
            direction *= -1;
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, -offset, 0);
        }
    }
}
