using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    [Header("Centipede")]
    [SerializeField] private bool isMyTurn = false;
    [SerializeField] private GameObject[] centipedeSpawners = null;

    [Header("Blocs")]
    [SerializeField] private float offset = 0.4f;
    [SerializeField] private int nbr = 32;
    [SerializeField] private Vector2 path = new Vector2(20,15);
    [SerializeField] GameObject blockSpawner;
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
            SpawnBlocs();
        }
    }

    private void SpawnBlocs()
    {
        int current = 0;
        Vector3 pos = blockSpawner.transform.position;
        while (current != nbr)
        {
            blockSpawner.transform.position = pos;
            for (int j = 0; j < path.y; j++)
            {
                for(int i = 0; i < path.x; i++)
                {
                    if (current == nbr) { blockSpawner.transform.position = pos; return; }
                    if (Random.Range(0, 20) == 0)
                    {
                        if (!Physics.CheckBox(blockSpawner.transform.position, new Vector3(0.1f, 0.1f, 0.1f)))
                        {
                            Instantiate(blockPREFAB, blockSpawner.transform.position, Quaternion.identity);
                            current++;
                        }
                    }
                    blockSpawner.transform.position = pos + new Vector3(i,-j) * offset;
                }
            }
        }
        blockSpawner.transform.position = pos;
    }
}
