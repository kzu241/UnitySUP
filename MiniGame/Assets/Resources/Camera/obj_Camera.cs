using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Camera : MonoBehaviour
{
    private GameObject player;
    private Vector3 now_camera_pos;

    // Use this for initialization
    void Start( ) {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update( ) {
        now_camera_pos = player.transform.position;
       this.transform.position = now_camera_pos;
    }
}