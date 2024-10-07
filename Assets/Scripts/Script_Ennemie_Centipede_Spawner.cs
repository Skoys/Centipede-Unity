using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Centipede_Spawner : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float spawnRate = 0.2f;
    [SerializeField] private int spawnNbr = 7;
    [SerializeField] private GameObject centipedePREFAB;

    public IEnumerator SpawnCentipede()
    {
        for (int i = 0; i < spawnNbr; i++)
        {
            GameObject centipede = Instantiate(centipedePREFAB);
            centipede.transform.position = transform.position;
            Script_Ennemie_Centipede centiScript = centipede.GetComponent<Script_Ennemie_Centipede>();
            centiScript.direction = direction;
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
