using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Bullet : MonoBehaviour
{
    private float speed;
    private Vector3 rotation;

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        transform.localEulerAngles += rotation * Time.deltaTime;
        rotation.z = Mathf.Lerp(rotation.z, 0, Time.deltaTime);
    }

    public void Init(float _speed, int _rotation, int _size,Color _color)
    {
        speed = _speed;
        rotation = new Vector3(0,0,_rotation);
        transform.localScale = transform.localScale * _size;
        GetComponent<SpriteRenderer>().color = _color;
        GetComponentInChildren<SpriteRenderer>().color = _color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "OutOfBound")
        {
            Destroy(gameObject);
        }
    }
}
