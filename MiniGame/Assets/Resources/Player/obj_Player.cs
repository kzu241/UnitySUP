using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 player_pos = new Vector3( 0.0f, 0.0f, 0.0f );
    Vector3 ground_pos = new Vector3( 0.0f, 0.0f, 0.0f );
    Vector3 now_pos;
    Transform player;
    float gravity_power = -0.02f;
    void Start( ) {
        //GameObject.Find("Player/player").transform.position = new Vector3( player.x, player.y, player.z);

    }

    // Update is called once per frame
    void Update( ) {
        gravity( );
        move( );
    }

    void gravity( ){
        player = this.transform;
        now_pos = player.position;
        float pos_y = now_pos.y;
        float ground_get_pos = ground_pos.y;
        if ( pos_y <= ground_get_pos ) {
            transform.Translate( 0.0f, 0.0f, 0.0f );
        } else {
            transform.Translate( 0.0f, gravity_power, 0.0f );
            gravity_power -= 0.01f;
        }
    }
        
    void move( ){
        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            transform.Translate( 0f, 0f, 0.1f );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            transform.Translate( 0.0f, 0f, -0.1f );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) ) {
            transform.Translate( -0.1f, 0f, 0f );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            transform.Translate( 0.1f, 0f, 0f );
        }
    }
}
