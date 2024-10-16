
using UnityEngine;
using UnityEngine.VFX;

public class Script_Ennemie_Centipede : MonoBehaviour
{
    [SerializeField] private bool isUp;
    private Rigidbody2D rb;
    private VisualEffect vfx;
    [SerializeField] private int currentElevation = 20;
    [SerializeField] private float speed = 5;
    [SerializeField] private float offset = 1;
    [SerializeField] private int life = 20;
    [SerializeField] private int maxLife = 20;
    [SerializeField] private GameObject blocPREFAB;
    public Vector2 direction = new Vector2(-1, 0);

    [Header("Colors")]
    [SerializeField, Range(0, 1)] private float currentColor = 0;
    [SerializeField] private Color from;
    [SerializeField] private Color to;

    Script_Player_Inputs playerInputs;
    Script_GameManager gameManager;
    Script_UI ui;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vfx = GetComponent<VisualEffect>();
        playerInputs = Script_Player_Inputs.instance;
        gameManager = Script_GameManager.instance;
        ui = Script_UI.instance;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    public void TakeDamage()
    {
        playerInputs.AddRumble(new Vector2(0.6f, 0.6f), 0.2f);
        gameManager.currentHealth--;
        life -= 1;
        currentColor += 1f / maxLife;
        GetComponent<SpriteRenderer>().color = Color.Lerp(from, to, currentColor);
        vfx.Play();
        Debug.Log(currentColor);
        if (life <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(blocPREFAB, transform.position, Quaternion.identity);
        ui.UpdateScore(150);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "OutOfBound" || collision.transform.tag == "Bloc")
        {
            if(!isUp && currentElevation - 1 < 0)
            {
                isUp = true;
                offset = -offset;            
            }
            else if (isUp && currentElevation + 1 > 6)
            {
                isUp = false;
                offset = -offset;
            }
            direction *= -1;
            transform.position += new Vector3(direction.x * speed * Time.deltaTime, -offset, 0);
            currentElevation -= offset > 0 ? 1 : -1;
        }
    }
}
