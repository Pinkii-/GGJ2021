using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    public float RaycastDistance = 3.5f;
    public Camera FPSCamera;

    private void Awake()
    {
        if (FPSCamera == null)
            Debug.LogWarning("FPSCamera isn't assigned to the crosshair script! Do it.");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (FPSCamera == null) 
        {
            Debug.LogWarning("FPSCamera has been removed, we can't do any raycasting without it. Reassign it!");
            return;
        }

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, RaycastDistance, layerMask))
        {
            Debug.DrawRay(FPSCamera.transform.position, FPSCamera.transform.forward * hit.distance, Color.green);
            //Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(FPSCamera.transform.position, FPSCamera.transform.forward * 1000, Color.red);
            //Debug.Log("Did not Hit");
        }
    }
}
