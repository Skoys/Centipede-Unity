using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Player : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float hitboxSize = 0.33f;


    [Header("Projectile")]
    [SerializeField] private GameObject bulletPrefab;
    public bool bulletAvailable = true;
    [SerializeField] private bool oneAtTime = true;
    [SerializeField] private float projectileSpeed = 10;
    private float currentProjectileTime = 0.0f;
    [SerializeField] private float projectileTime = 0.05f;
    [SerializeField] private GameObject projectilesFolder;

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
        CollisionDetection();

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            oneAtTime = !oneAtTime;
        }
    }

    private void GetInputs()
    {
        movements = inputs.movement;
        actionButton = inputs.actionPressed;
    }

    private void Actions()
    {
        rb.velocity = movements * speed;
        if (actionButton)
        {
            if(oneAtTime && bulletAvailable)
            {
                SpawnProjectile();
                bulletAvailable = false;
            }
            if(!oneAtTime && currentProjectileTime <=0)
            {
                SpawnProjectile();
                currentProjectileTime = projectileTime;
            }
        }
        currentProjectileTime -= Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        Script_Player_Bullet projectile = Instantiate(bulletPrefab).GetComponent<Script_Player_Bullet>();
        projectile.transform.position = transform.position;
        projectile.Init(this, projectileSpeed);
    }

    private void CollisionDetection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), hitboxSize);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider.transform.tag);
            if (collider.transform.tag == "Ennemie")
            {
                foreach (Transform child in projectilesFolder.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitboxSize);
    }
}
