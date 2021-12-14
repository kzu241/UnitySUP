using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_createCamera : MonoBehaviour {
    GameObject main_camera;
    Vector3 camera_pos = new Vector3( 5.0f, 15.0f, -13.0f ) ;
    Quaternion rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );
    // Use this for initialization
    void Start( ) {
        main_camera = ( GameObject )Resources.Load( "Camera/Main Camera" );
        Instantiate( main_camera, camera_pos, rotation );
    }

    void Update( ) {
        
    }
}