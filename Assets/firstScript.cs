using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstScript : MonoBehaviour
{
    public float speed = 0.00000001f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 actualPosition = transform.position;
        
        actualPosition.x += speed;

        transform.position = actualPosition;
    }
}
