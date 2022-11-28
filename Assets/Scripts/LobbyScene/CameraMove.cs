using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate((target.position - transform.position)*Time.deltaTime);
        transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }
}
