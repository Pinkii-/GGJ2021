using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairScript : MonoBehaviour
{
    private MessageItemScript itemInCrosshair;

    public float RaycastDistance = 3.8f;
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
            MessageItemScript item = hit.transform.parent.GetComponent<MessageItemScript>();

            if (item == null) 
            {
                Debug.LogWarning("Item " + hit.transform.parent + " is in the message item layer but it doesn't have a  MessageItemScript attached. Please, fix it!");
                return;
            }

            // If a new item is crosshairs, we remove the old one and tell it we are no longer looking at it
            if (item != itemInCrosshair) 
            {
                if(itemInCrosshair != null)
                    itemInCrosshair.OutCrosshair();
                item.InCrosshair();
                itemInCrosshair = item;
            }

            Debug.DrawRay(FPSCamera.transform.position, FPSCamera.transform.forward * hit.distance, Color.green);
            //Debug.Log("Did Hit");
        }
        else
        {
            if (itemInCrosshair != null)
                itemInCrosshair.OutCrosshair();
            itemInCrosshair = null;
            
            Debug.DrawRay(FPSCamera.transform.position, FPSCamera.transform.forward * 1000, Color.red);
            //Debug.Log("Did not Hit");
        }

        // Clicked or pressed E
        if (Input.GetAxis("Fire1") > 0f)
        {
            if (itemInCrosshair != null)
                itemInCrosshair.Selected();
        }
    }

    public void Reset()
    {
        itemInCrosshair = null;
    }
}
