using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    Transform player;
    float gravity_power = -0.02f;
    bool stop_left = false;
    bool stop_right = false;
    bool stop_back = false;
    bool stop_front = false;

    void Start( ) {

    }

    // Update is called once per frame
    void Update( ) {
        setup( );
        gravity( );
        collisionJudgement( );
        move( );
        //removeItem( );
    }

    void setup( ) {
        player = this.transform;
        pos = player.position;
    }

    void gravity( ){
        Vector3 ground_pos = new Vector3( 0.0f, 0.0f, 0.0f );
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
        if ( Input.GetKey( KeyCode.UpArrow ) && stop_front == false ) {
            transform.Translate( 0f, 0f, 0.05f );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && stop_back == false ) {
            transform.Translate( 0.0f, 0f, -0.05f );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && stop_left == false ) {
            transform.Translate( -0.05f, 0f, 0f );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && stop_right == false ) {
            transform.Translate( 0.05f, 0f, 0f );
        }
    }
    void collisionJudgement( ) {
        GameObject left_wall   = GameObject.FindGameObjectWithTag( "LeftWall" );
        GameObject right_wall = GameObject.FindGameObjectWithTag( "RightWall" );
        GameObject back_wall = GameObject.FindGameObjectWithTag( "BackWall" );
        GameObject front_wall = GameObject.FindGameObjectWithTag( "FrontWall" );
        float now_pos_z = pos.z;
        float now_pos_x = pos.x;
        if ( left_wall.transform.position.x + 1.0f >= now_pos_x ) {
            stop_left = true;
        } else {
            stop_left = false;
        }
        if( right_wall.transform.position.x - 2.0f <= now_pos_x ) {
            stop_right = true;
        } else {
            stop_right = false;
        }
        if( back_wall.transform.position.z + 1.0f >= now_pos_z ) {
            stop_back = true;
        }else{
            stop_back = false;
        }
        if( front_wall.transform.position.z - 2.0f <= now_pos_z ) {
            stop_front = true;
        } else {
            stop_front = false;
        }
    }
    void removeItem( ) {
        GameObject item = GameObject.FindGameObjectWithTag( "Item" );
        float now_pos_x = pos.x;
        float coll_x = item.transform.position.x - now_pos_x;
		if ( coll_x < 0 ) {
            coll_x *= -1;
		    if ( coll_x <= 200 ) {
                Destroy( item ); //itemを消したときにエラーが出る。
            }
		}
        
    }
}
