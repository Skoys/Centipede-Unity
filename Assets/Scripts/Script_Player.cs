using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float speed = 1.0f;
    public bool bulletAvailable = true;

    [Header("Debug")]
    [SerializeField] private Script_Player_Inputs inputs;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movements = Vector2.zero;
    [SerializeField] private bool actionButton = false;

    private void Start()
    {
        inputs = GetComponent<Script_Player_Inputs>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
        Actions();
    }

    private void GetInputs()
    {
        movements = inputs.movement;
        actionButton = inputs.actionPressed;
    }

    private void Actions()
    {
        rb.velocity = movements * speed;
        if(actionButton && bulletAvailable)
        {
            Script_Player_Bullet bullet = Instantiate(bulletPrefab).GetComponent<Script_Player_Bullet>();
            bullet.transform.position = transform.position;
            bullet.player = this;
            bulletAvailable = false;
        }
    }
}
