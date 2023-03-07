using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowsPlayers : MonoBehaviour
{

    public Transform player1;
    public Transform player2;
    public Camera cam;

    // Follow Two Transforms with a Fixed-Orientation Camera

    private void Update()
    {
        FixedCameraFollowSmooth();
    }

    public void FixedCameraFollowSmooth()
    {
        // How many units should we keep from the players
        float zoomFactor = 3f;
        float followTimeDelta = 0.8f;

        // Midpoint we're after
        Vector3 midpoint = (player1.position + player2.position) / 2f;

        // Distance between objects
        float distance = (player1.position - player2.position).magnitude;

        // Move camera a certain distance
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;

        // Adjust ortho size if we're using one of those
        if (cam.orthographic)
     {
            // The camera's forward vector is irrelevant, only t$$anonymous$$s size will matter
            cam.orthographicSize = distance;
        }
        // You specified to use MoveTowards instead of Slerp
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, followTimeDelta);

        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;
    }

}

