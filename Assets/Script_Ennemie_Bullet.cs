using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Ennemie_Bullet : MonoBehaviour
{
    private Vector3 velocity;
    private int rotation;

    void Update()
    {
        float rotDelta = rotation * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        velocity.x = Mathf.Cos(rotDelta * velocity.x) - Mathf.Sin(rotDelta * velocity.y);
        velocity.y = Mathf.Sin(rotDelta * velocity.x) + Mathf.Cos(rotDelta * velocity.y);
    }

    public void Init(Vector2 _velocity, int _rotation, int _size,Color _color)
    {
        velocity = _velocity;
        rotation = _rotation;
        transform.localScale = transform.localScale * _size;
        GetComponent<SpriteRenderer>().color = _color;
    }
}
