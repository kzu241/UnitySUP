using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Camera : MonoBehaviour {
    public GameObject player;
    Vector3 now_camera_pos;
    // Start is called before the first frame update
    void Start( ) {
        player = GameObject.FindGameObjectWithTag( "Player" );
    }

    // Update is called once per frame
    void Update( ) {
        //if ( player == null ) {
        //    return;
        //}
        now_camera_pos.z = player.transform.position.z - 15.0f;
        now_camera_pos.x = player.transform.position.x;
        now_camera_pos.y = player.transform.position.y + 15.0f;
        this.transform.position = now_camera_pos;

        //move_camera = this.transform;
        //pos = move_camera.position;
        //
        //now_camera_pos = player.transform.position;
        //pos = now_camera_pos;
    }
}
