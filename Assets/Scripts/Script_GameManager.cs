using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Script_GameManager : MonoBehaviour
{
    public static Script_GameManager instance;

    [Header("GAME")]
    [Range(0, 10)] public int gamePhase = 0;

    [Header("Centipede")]
    [SerializeField] private GameObject ennemiesFolder;
    [SerializeField] private bool watchForCentipedes = false;
    [SerializeField] private bool isMyTurn = false;
    [SerializeField] private GameObject[] centipedeSpawners = null;
    [SerializeField, Range(-1, 19)] private int centipedeLevel = -1;

    [Header("Blocs")]
    [SerializeField] private GameObject blocsFolder;
    [SerializeField] private float offset = 0.4f;
    [SerializeField] private int nbr = 32;
    [SerializeField] private Vector2 path = new Vector2(20,15);
    [SerializeField] GameObject blockSpawner;
    [SerializeField] GameObject blockPREFAB;

    [Header("UI")]
     public int currentHealth = 0;
     public int maxHealth = 100;
    public Slider healthUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IEnumerator coroutine = NextGamePhase(true);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        if (watchForCentipedes)
        {
            if(ennemiesFolder.transform.childCount == 0)
            {
                watchForCentipedes = false ;
                IEnumerator coroutine = NextGamePhase(true);
                StartCoroutine(coroutine);
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            SpawnCentipede();
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            SpawnBlocs();
        }
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            IEnumerator coroutine = NextGamePhase(true);
            StartCoroutine(coroutine);
        }

        healthUI.value = currentHealth;
    }

    private void SpawnCentipede()
    {
        IEnumerator coroutine = centipedeSpawners[isMyTurn ? 1 : 0].GetComponent<Script_Ennemie_Centipede_Spawner>().SpawnCentipede(ennemiesFolder, centipedeLevel);
        StartCoroutine(coroutine);
        coroutine = AddLife();
        StartCoroutine(coroutine);
        isMyTurn = !isMyTurn;
    }

    private void SpawnBlocs()
    {
        int current = 0;
        Vector3 pos = blockSpawner.transform.position;
        GetComponent<VisualEffect>().Play();
        int debug = 0;  
        while (current != nbr || blocsFolder.transform.childCount > 40 || debug > 100)
        {
            blockSpawner.transform.position = pos;
            float x = Mathf.Ceil(Random.Range(0, path.x - 1));
            float y = Mathf.Ceil(Random.Range(0, path.y - 1));
            if (current == nbr) { blockSpawner.transform.position = pos; return; }
            blockSpawner.transform.position = pos + new Vector3(x,-y) * offset;
            Collider2D[] test = Physics2D.OverlapBoxAll(blockSpawner.transform.position, new Vector2(offset, offset), 0);
            if (test.Length == 1)
            {
                GameObject bloc = Instantiate(blockPREFAB, blockSpawner.transform.position, Quaternion.identity);
                bloc.transform.SetParent(blocsFolder.transform);
                current++;
            }
            debug ++;
        }
        blockSpawner.transform.position = pos;
    }

    public IEnumerator NextGamePhase(bool wait)
    {
        currentHealth = 0;
        yield return new WaitForSeconds(wait?5:0);
        centipedeLevel++;
        watchForCentipedes = true;
        gamePhase += 1;
        switch (gamePhase)
        {
            case 1:
                SpawnCentipede();
                yield break;

            case 2:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 3:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 4:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 5:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 6:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 7:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 8:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 9:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

            case 10:
                SpawnCentipede();
                SpawnBlocs();
                yield break;

        }
    }

    private IEnumerator AddLife()
    {
        healthUI.minValue = 0;
        healthUI.maxValue = maxHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            currentHealth++;
            yield return new WaitForSeconds(0.01f);
            healthUI.value = currentHealth;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(blockSpawner.transform.position + new Vector3(path.x * offset * 0.5f, -path.y * offset * 0.5f, 1), new Vector3(path.x * offset, -path.y * offset, 1));
    }
}