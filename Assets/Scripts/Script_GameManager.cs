using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    [SerializeField] private bool isMyTurn = false;
    [SerializeField] private GameObject[] centipedeSpawners = null;

    [SerializeField] private float offset = 0.4f;
    [SerializeField] private int nbr = 32;
    [SerializeField] GameObject blockPREFAB;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            IEnumerator coroutine = centipedeSpawners[isMyTurn?1:0].GetComponent<Script_Ennemie_Centipede_Spawner>().SpawnCentipede();
            StartCoroutine(coroutine);
            isMyTurn = !isMyTurn;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            SpawnBlocks();
        }
    }

    private void SpawnBlocks()
    {
        int current = 0;
        Vector3 pos = transform.position;
        while (current < nbr)
        {
            if (Random.Range(0, 5) == 0)
            {
                if (!Physics.CheckBox(transform.position, new Vector3(0.1f, 0.1f, 0.1f)))
                {
                    Instantiate(blockPREFAB, transform.position, Quaternion.identity);
                    current++;
                }
            }
        }
    }
}
