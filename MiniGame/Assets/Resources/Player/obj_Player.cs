using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start( ) {
       player = (GameObject)Resources.Load( "Player/player" );
        Instantiate( player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity );
    }

    // Update is called once per frame
    void Update( ){
        move( );
    }
    void  move( ) {
        if ( Input.GetKey(KeyCode.UpArrow ) ){
            transform.position = new Vector3( 0f, transform.position.x + 0.1f, 0f );
        } 
    }
}
