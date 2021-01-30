using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public GameObject go;
    public float yAngle;


    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (go.transform.rotation.y >= 360) {
            go.transform.rotation.Set(0.0f,0.0f, 0.0f, 0.0f);
        }
        go.transform.Rotate(0.0f, yAngle, 0.0f, Space.Self);

    }
}
