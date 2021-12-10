using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    Transform player;
    float gravity_power = -0.02f;
    const int KABE_LEFT = -4;
    bool stop = false;

    void Start( ) {
        //GameObject.Find("Player/player").transform.position = new Vector3( player.x, player.y, player.z);

    }

    // Update is called once per frame
    void Update( ) {
        gravity( );
        collisionJudgement( );
        move( );
    }

    void gravity( ){
        player = this.transform;
        pos = player.position;

        Vector3 ground_pos = new Vector3(0.0f, 0.0f, 0.0f);
        float now_pos_y = pos.y;
        float ground_get_pos = ground_pos.y;

        if ( now_pos_y <= ground_get_pos ) {
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
        if ( Input.GetKey( KeyCode.LeftArrow ) && stop == false) {
            transform.Translate( -0.1f, 0f, 0f );
        }
        if ( Input.GetKey( KeyCode.RightArrow )  ) {
            transform.Translate( 0.1f, 0f, 0f );
        }
    }
    void collisionJudgement( ){
        player = this.transform;
        pos = player.position;
        float now_pos_x = pos.x;
        Debug.Log( "pos_x", player );
        if( now_pos_x <= KABE_LEFT ){
            stop = true;
        } else {
            stop = false;
        }
    }
}
