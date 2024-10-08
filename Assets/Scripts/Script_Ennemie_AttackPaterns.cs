using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_AttackPaterns : MonoBehaviour
{
    [Header("Boss Characteristics")]
    [SerializeField] private Vector3 currentRotation = Vector3.zero;

    [Header("Bullet Characteristics")]
    [SerializeField] private GameObject bulletPREFAB;
    [SerializeField, Range(2, 50)] private int bulletNbr;
    [SerializeField, Range(2, 20)] private int size;
    [SerializeField, Range(2, 50)] private int speed;
    [SerializeField] private Vector2 velocity = Vector2.down;
    [SerializeField] private int rotation = 0;
    [SerializeField] private Color color = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Attack();
        }
    }

    void Attack()
    {
        for (int i = 0; i < bulletNbr; i++)
        {
            Script_Ennemie_Bullet bullet = Instantiate(bulletPREFAB,
                                                        transform.position,
                                                        Quaternion.Euler(currentRotation + new Vector3(0, 0, (360 / bulletNbr) * i)))
                                                        .GetComponent<Script_Ennemie_Bullet>();
            Vector2 vel = Vector2.zero;
            vel.x = Mathf.Cos(((360 / bulletNbr) * i) * velocity.x) - Mathf.Sin(((360 / bulletNbr) * i) * velocity.y);
            vel.y = Mathf.Sin(((360 / bulletNbr) * i) * velocity.x) + Mathf.Cos(((360 / bulletNbr) * i) * velocity.y);
            bullet.Init(vel, rotation, size, color);
        }
    }
}
