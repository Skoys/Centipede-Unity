using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bonus : MonoBehaviour
{
    public int value = 1; 
    [SerializeField] private int speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(0,speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "OutOfBound")
        {
            Destroy(gameObject);
        }
    }
}
