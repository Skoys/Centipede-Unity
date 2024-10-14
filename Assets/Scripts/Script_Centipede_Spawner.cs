using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Centipede_Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float spawnRate = 0.2f;
    [SerializeField] private int spawnNbr = 7;
    [SerializeField] private GameObject centipedePREFAB;
    [SerializeField] private GameObject projectilesFOLDER;

    Script_GameManager gameManager;

    private void Start()
    {
        gameManager = Script_GameManager.instance;
        gameManager.maxHealth = 20 * spawnNbr;
    }

    public IEnumerator SpawnCentipede(GameObject folder, int level)
    {
        
        for (int i = 0; i < spawnNbr; i++)
        {
            GameObject centipede = Instantiate(centipedePREFAB);
            centipede.transform.position = transform.position;
            centipede.transform.SetParent(folder.transform);
            Script_Ennemie_Centipede centiScript = centipede.GetComponent<Script_Ennemie_Centipede>();
            centiScript.direction = direction;
            centipede.GetComponent<Script_Ennemie_AttackPaterns>().bulletFolder = projectilesFOLDER;
            centipede.GetComponent<Script_Ennemie_AttackPaterns>().currentAttack = level;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0,0.4f * 18, 0));
    }
}
