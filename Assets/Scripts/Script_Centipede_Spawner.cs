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

    public IEnumerator SpawnCentipede(GameObject folder)
    {
        for (int i = 0; i < spawnNbr; i++)
        {
            GameObject centipede = Instantiate(centipedePREFAB);
            centipede.transform.position = transform.position;
            centipede.transform.SetParent(folder.transform);
            Script_Ennemie_Centipede centiScript = centipede.GetComponent<Script_Ennemie_Centipede>();
            centiScript.direction = direction;
            centipede.GetComponent<Script_Ennemie_AttackPaterns>().bulletFolder = projectilesFOLDER;
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position - new Vector3(0,0.4f * 18, 0));
    }
}
