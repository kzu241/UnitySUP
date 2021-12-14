using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Camera : MonoBehaviour
{
    private GameObject player;
    private Vector3 now_camera_pos;

    GameObject main_camera;
    Vector3 camera_pos = new Vector3( 5.0f, 15.0f, -13.0f ) ;
    Quaternion rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );
    // Use this for initialization
    void Start( ) {
        main_camera = (GameObject)Resources.Load( "Camera/Main Camera" );
        Instantiate( main_camera, camera_pos, rotation );
        player = GameObject.FindGameObjectWithTag( "Player" );
    }

    void Update( ) {
        if( player == null ){
            return;
        }
        now_camera_pos = player.transform.position;
        camera_pos = now_camera_pos;
    }
}