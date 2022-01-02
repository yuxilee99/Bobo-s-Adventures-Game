using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CameraFollow : MonoBehaviour
{
    public GameObject player;
 
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;
    private float yPadTop = 2.5f;
    private float yPadBot = 1.5f;
 
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerposition = player.transform.position;
        Vector3 cameraposition = transform.position;
 
        // check forward x position
        if (playerposition.x > cameraposition.x) { 
            cameraposition.x = Mathf.SmoothDamp(cameraposition.x, playerposition.x, ref xVelocity, 0.5f);
        }

        // check upward y position
        if (playerposition.y - yPadTop > cameraposition.y) { 
            cameraposition.y = Mathf.SmoothDamp(cameraposition.y, playerposition.y, ref yVelocity, 0.5f);
        }

        // check bottom y position
        if (playerposition.y + yPadBot < cameraposition.y) { 
            cameraposition.y = Mathf.SmoothDamp(cameraposition.y, playerposition.y, ref yVelocity, 0.5f);
        }
 
        transform.position = cameraposition;
    }
}