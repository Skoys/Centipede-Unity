using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GameManager : MonoBehaviour
{
    [Header("Centipede")]
    [SerializeField] private GameObject ennemiesFolder;
    [SerializeField] private bool isMyTurn = false;
    [SerializeField] private GameObject[] centipedeSpawners = null;

    [Header("Blocs")]
    [SerializeField] private GameObject blocsFolder;
    [SerializeField] private float offset = 0.4f;
    [SerializeField] private int nbr = 32;
    [SerializeField] private Vector2 path = new Vector2(20,15);
    [SerializeField] GameObject blockSpawner;
    [SerializeField] GameObject blockPREFAB;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            IEnumerator coroutine = centipedeSpawners[isMyTurn?1:0].GetComponent<Script_Ennemie_Centipede_Spawner>().SpawnCentipede(ennemiesFolder);
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
            float x = Random.Range(0, path.x - 1);
            float y = Random.Range(0, path.y - 1);
            if (current == nbr) { blockSpawner.transform.position = pos; return; }
            blockSpawner.transform.position = pos + new Vector3(x,-y) * offset;
            if (!Physics.CheckBox(blockSpawner.transform.position, new Vector3(0.1f, 0.1f, 0.1f)))
            {
                GameObject bloc = Instantiate(blockPREFAB, blockSpawner.transform.position, Quaternion.identity);
                bloc.transform.SetParent(blocsFolder.transform);
                current++;
            }
        }
        blockSpawner.transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(blockSpawner.transform.position + new Vector3(path.x * offset * 0.5f, -path.y * offset * 0.5f, 1), new Vector3(path.x * offset, -path.y * offset, 1));
    }
}