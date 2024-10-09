using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_AttackPaterns : MonoBehaviour
{
    [Header("Boss Characteristics")]
    [SerializeField] private bool isAttacking = false;
     private float currentRotation = 0;
    [SerializeField, Range(0, 180)] private int rotationSpeed = 0;
    private float currentAttackTime = 0;
    [SerializeField, Range(0.01f, 3f)] private float timeBtwAttacks = 0.1f;

    [Header("Bullet Characteristics")]
    [SerializeField] private GameObject bulletFolder;
    [SerializeField] private GameObject bulletPREFAB;
    [SerializeField, Range(2, 50)] private int bulletNbr;
    [SerializeField, Range(2, 20)] private int size;
    [SerializeField, Range(0.01f, 15f)] private float speed;
    [SerializeField, Range(0, 180)] private int rotation = 0;
    [SerializeField] private Color color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation += rotationSpeed * Time.deltaTime;
        if (currentAttackTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Keypad1) || isAttacking)
            {
                Attack();
                currentAttackTime = timeBtwAttacks;
            }
        }
        else
        {
            currentAttackTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            isAttacking = !isAttacking;
        }
    }

    void Attack()
    {
        for (int i = 0; i < bulletNbr; i++)
        {
            Script_Ennemie_Bullet bullet = Instantiate(bulletPREFAB,
                                                        transform.position,
                                                        Quaternion.Euler(new Vector3(0, 0, ((360 / bulletNbr) * i) + currentRotation)))
                                                        .GetComponent<Script_Ennemie_Bullet>();
            bullet.transform.SetParent(bulletFolder.transform);
            bullet.Init(speed, rotation, size, color);
        }
    }
}
