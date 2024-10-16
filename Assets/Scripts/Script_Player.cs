using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float hitboxSize = 0.33f;
    [SerializeField] private int life = 3;


    [Header("Projectile")]
    [SerializeField, Range(0,50)] private int projectileLevel = 0;
    private float nextLevelDown = 0;
    private float timeLevelDown = 2;
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

    private Script_UI ui;

    private void Start()
    {
        inputs = Script_Player_Inputs.instance;
        ui = Script_UI.instance;
        rb = GetComponent<Rigidbody2D>();

        ui.UpdatePower(projectileLevel);
    }

    void Update()
    {
        GetInputs();
        Actions();
        CollisionDetection();
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

        if(nextLevelDown < Time.time)
        {
            nextLevelDown = Time.time + 2;
            projectileLevel--;
            if(projectileLevel < 0) { projectileLevel = 0; }
            ui.UpdatePower(projectileLevel);
        }
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
                if (life == 0) { Script_GameOver.instance.GameOver(); }
                projectileLevel = projectileLevel / 2;
                foreach (Transform child in projectilesFolder.transform)
                {
                    Destroy(child.gameObject);
                }
                life--;
                ui.UpdateLife(life);
                ui.UpdatePower(projectileLevel);
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
                nextLevelDown = Time.time + 2;
                projectileLevel += bonus.value;
                if (projectileLevel > 50) { projectileLevel = 50; }
                ui.UpdatePower(projectileLevel);
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
