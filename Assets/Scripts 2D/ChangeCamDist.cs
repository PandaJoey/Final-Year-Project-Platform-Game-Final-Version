using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class to change the camera distance via the scroll bar in the UI
 */

public class ChangeCamDist : MonoBehaviour {

    public Camera cam;
    public float changeDist = 6f;

    public void ChangeDistance(float distance) {

        if (distance == 0.0f) {
            cam.orthographicSize = 0.6f * 10f;
        }
        else if (distance > 0.05f && distance < 0.17f) {
            cam.orthographicSize = 0.8f * 8.0f;
        }
        else if (distance > 0.17f && distance < 0.27f) {
            cam.orthographicSize = 1.0f * 8.0f;
        }
        else if (distance > 0.27f && distance < 0.38f) {
            cam.orthographicSize = 1.2f * 8.0f;
        }
        else if (distance > 0.38f && distance < 0.5f) {
            cam.orthographicSize = 1.4f * 8.0f;
        }
        else if (distance > 0.5f && distance < 0.61f) {
            cam.orthographicSize = 1.6f * 8.0f;
        }
        else if (distance > 0.61f && distance < 0.72f) {
            cam.orthographicSize = 1.8f * 8.0f;
        }
        else if (distance > 0.72f && distance < 0.83f) {
            cam.orthographicSize = 2.0f * 8.0f;
        }
        else if (distance > 0.83f && distance < 0.94f) {
            cam.orthographicSize = 2.2f * 8.0f;
        }
        else { 
            cam.orthographicSize = 2.4f * 8.0f;
        }
    }
}