using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Script_Ennemie_AttackPaterns : MonoBehaviour
{
    [Header("Boss Characteristics")]
    [SerializeField] private bool isAttacking = false;
     private float currentRotation = 0;
    [SerializeField, Range(-180, 180)] private int rotationSpeed = 0;
    private float currentAttackTime = 0;
    private float currentPauseTime = 0;

    [Header("Bullet Characteristics")]
    public GameObject bulletFolder;
    [SerializeField] private GameObject bulletPREFAB;
    [SerializeField, Range(0.01f, 3f)] private float timeBtwAttacks = 0.1f;
    [SerializeField, Range(2, 50)] private int totalNbr;
    [SerializeField, Range(0.01f, 5f)] private float timePause = 3f;
    [SerializeField, Range(2, 50)] private int bulletNbr;
    [SerializeField, Range(1, 20)] private int size;
    [SerializeField, Range(0.01f, 15f)] private float speed;
    [SerializeField, Range(-180, 180)] private int rotation = 0;
    [SerializeField] private Color color = Color.white;

    [Header("Attack Storage")]
    [Range(0, 19)] public int currentAttack = 0;
    private int oldCurrentAttack = 0;

    [System.Serializable]
    public class AttackListClass
    {
        public List<float> subAttack;
        public List<float> subAttack2;
        public List<float> subAttack3;
    }
    public List<AttackListClass> attacks;

    void Start()
    {
        oldCurrentAttack = currentAttack;
        ChangeVariables();
    }

    void Update()
    {
        if (oldCurrentAttack != currentAttack) { oldCurrentAttack = currentAttack; ChangeVariables(); }

        UpdateTheAttack();

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
        for (int i = 0; i < attacks[currentAttack].subAttack[3]; i++)
        {
            Script_Ennemie_Bullet bullet = Instantiate(bulletPREFAB,
                                                        transform.position,
                                                        Quaternion.Euler(new Vector3(0, 0, ((360 / attacks[currentAttack].subAttack[3]) * i) + currentRotation)))
                                                        .GetComponent<Script_Ennemie_Bullet>();
            bullet.transform.SetParent(bulletFolder.transform);
            bullet.Init(attacks[currentAttack].subAttack[5],
                            (int)attacks[currentAttack].subAttack[6],
                            (int)attacks[currentAttack].subAttack[4],
                            new Color(attacks[currentAttack].subAttack[7],
                                      attacks[currentAttack].subAttack[8],
                                      attacks[currentAttack].subAttack[9]));
        }

        if (attacks[currentAttack].subAttack2.Count != 0)
        {
            for (int i = 0; i < attacks[currentAttack].subAttack2[3]; i++)
            {
                Script_Ennemie_Bullet bullet = Instantiate(bulletPREFAB,
                                                            transform.position,
                                                            Quaternion.Euler(new Vector3(0, 0, ((360 / attacks[currentAttack].subAttack2[3]) * i) + currentRotation)))
                                                            .GetComponent<Script_Ennemie_Bullet>();
                bullet.transform.SetParent(bulletFolder.transform);
                bullet.Init(attacks[currentAttack].subAttack2[5],
                                (int)attacks[currentAttack].subAttack2[6],
                                (int)attacks[currentAttack].subAttack2[4],
                                new Color(attacks[currentAttack].subAttack2[7],
                                          attacks[currentAttack].subAttack2[8],
                                          attacks[currentAttack].subAttack2[9]));
            }
        }
        if (attacks[currentAttack].subAttack3.Count != 0)
        {
            for (int i = 0; i < attacks[currentAttack].subAttack3[3]; i++)
            {
                Script_Ennemie_Bullet bullet = Instantiate(bulletPREFAB,
                                                            transform.position,
                                                            Quaternion.Euler(new Vector3(0, 0, ((360 / attacks[currentAttack].subAttack3[3]) * i) + currentRotation)))
                                                            .GetComponent<Script_Ennemie_Bullet>();
                bullet.transform.SetParent(bulletFolder.transform);
                bullet.Init(attacks[currentAttack].subAttack3[5],
                                (int)attacks[currentAttack].subAttack3[6],
                                (int)attacks[currentAttack].subAttack3[4],
                                new Color(attacks[currentAttack].subAttack3[7],
                                          attacks[currentAttack].subAttack3[8],
                                          attacks[currentAttack].subAttack3[9]));
            }
        }
    }

    private void ChangeVariables()
    {
        timeBtwAttacks = attacks[currentAttack].subAttack[0];
        totalNbr = (int)attacks[currentAttack].subAttack[1];
        timePause = attacks[currentAttack].subAttack[2];
        bulletNbr = (int)attacks[currentAttack].subAttack[3];
        size = (int)attacks[currentAttack].subAttack[4];
        speed = attacks[currentAttack].subAttack[5];
        rotation = (int)attacks[currentAttack].subAttack[6];
        color = new Color(attacks[currentAttack].subAttack[7],
                          attacks[currentAttack].subAttack[8],
                          attacks[currentAttack].subAttack[9]);
    }

    void UpdateTheAttack()
    {
        attacks[currentAttack].subAttack[0] = timeBtwAttacks;
        attacks[currentAttack].subAttack[1] = totalNbr;
        attacks[currentAttack].subAttack[2] = timePause;
        attacks[currentAttack].subAttack[3] = bulletNbr;
        attacks[currentAttack].subAttack[4] = size;
        attacks[currentAttack].subAttack[5] = speed;
        attacks[currentAttack].subAttack[6] = rotation;
        attacks[currentAttack].subAttack[7] = color.r;
        attacks[currentAttack].subAttack[8] = color.g;
        attacks[currentAttack].subAttack[9] = color.b;
    }
}
