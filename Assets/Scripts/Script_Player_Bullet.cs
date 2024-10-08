using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Script_Player_Bullet : MonoBehaviour
{
    public Script_Player player;

    [SerializeField] float speed = 1;
    [SerializeField] Vector2 direction = new Vector2(0,1);
    [SerializeField] private VisualEffect vfx;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        vfx = GetComponent<VisualEffect>();
    }
    void Update()
    {
        rb.velocity = direction * speed;
    }

    private IEnumerator Die()
    {
        player.bulletAvailable = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        direction = Vector2.zero;
        vfx.Play();
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.transform.tag);
        if (other.transform.tag == "OutOfBound")
        {
            StartCoroutine(Die());
        }
        if (other.transform.tag == "Bloc")
        {
            Destroy(other.gameObject);
            StartCoroutine(Die());
        }
        if (other.transform.tag == "Ennemie")
        {
            if (other.GetComponent<Script_Ennemie_Centipede>() != null)
            {
                other.GetComponent<Script_Ennemie_Centipede>().Die();
                StartCoroutine(Die());
            }
        }
    }
}
