using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float hitboxSize = 0.33f;


    [Header("Projectile")]
    [SerializeField] private int projectileLevel = 0;
    [SerializeField] private GameObject bulletPrefab;
    private float currentProjectileTime = 0.0f;
    [SerializeField] private float projectileTime = 0.05f;
    [SerializeField] private GameObject projectilesFolder;

    [Header("Debug")]
    [SerializeField] private Script_Player_Inputs inputs;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 movements = Vector2.zero;
    [SerializeField] private bool actionButton = false;
    [SerializeField] private bool slowButton = false;

    private void Start()
    {
        inputs = Script_Player_Inputs.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();
        Actions();
        CollisionDetection();

        if(projectileLevel > 50) { projectileLevel = 50; }
    }

    private void GetInputs()
    {
        movements = inputs.movement;
        actionButton = inputs.actionPressed;
        slowButton = inputs.slowPressed;
    }

    private void Actions()
    {
        rb.velocity = movements * (slowButton ? speed*0.33f : speed);
        if (actionButton)
        {
            if(currentProjectileTime <=0)
            {
                SpawnProjectile();
                currentProjectileTime = projectileTime;
            }
        }
        currentProjectileTime -= Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        if(projectileLevel < 10)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0,0.5f), Quaternion.identity);
        }
        else if (projectileLevel < 20)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.10f, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.10f, 0.5f), Quaternion.identity);
        }
        else if (projectileLevel < 30)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(0.15f, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.15f, 0.5f), Quaternion.identity);
        }
        else if (projectileLevel < 40)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0.10f, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.10f, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(0.10f, 0.5f), Quaternion.Euler(0,0,-10));
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.10f, 0.5f), Quaternion.Euler(0, 0, 10));
        }
        else if (projectileLevel <= 50)
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f), Quaternion.identity);
            Instantiate(bulletPrefab, transform.position + new Vector3(0.2f, 0.5f), Quaternion.Euler(0, 0, -5));
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.2f, 0.5f), Quaternion.Euler(0, 0, 5));
            Instantiate(bulletPrefab, transform.position + new Vector3(0.2f, 0.5f), Quaternion.Euler(0, 0, -10));
            Instantiate(bulletPrefab, transform.position + new Vector3(-0.2f, 0.5f), Quaternion.Euler(0, 0, 10));
        }

    }

    private void CollisionDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), hitboxSize);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.tag == "Ennemie")
            {
                inputs.AddRumble(new Vector2(1, 1), 1);
                projectileLevel = projectileLevel / 2;
                foreach (Transform child in projectilesFolder.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Bonus")
        {
            Script_Bonus bonus = collision.transform.GetComponent<Script_Bonus>();
            if (bonus != null)
            {
                projectileLevel += bonus.value;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxSize);
    }
}
