using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour {
    Vector3 pos;
    Transform player;
    Quaternion rotate;
    
    public GameObject item;

    float gravity_power = -0.02f;

    bool stop_left = false;
    bool stop_right = false;
    bool stop_back = false;
    bool stop_front = false;
    bool judge_coll_x = false;
    bool judge_coll_z = false;

    float angle = 1.0f;

    // Start is called before the first frame update

    void Start( ) {
        item = GameObject.FindGameObjectWithTag( "Item" );

    }

    // Update is called once per frame
    void Update( ) {
        setup( );
        gravity( );
        collisionJudgement( );
        move( );
        removeItem( );
    }

    void setup( ) {
        player = this.transform;
        pos = player.position;
    }

    void gravity( ){
        Vector3 ground_pos = new Vector3( 0.0f, 0.0f, 0.0f );
        float now_pos_y = pos.y;
        float ground_get_pos = ground_pos.y + 0.76f;
        if ( now_pos_y <= ground_get_pos ) {
            transform.Translate( 0.0f, 0.0f, 0.0f );
        } else {
            transform.Translate( 0.0f, gravity_power, 0.0f );
            gravity_power -= 0.01f;
        }
    }
        
    void move( ){
        if ( Input.GetKey( KeyCode.UpArrow ) && !stop_front ) {
            transform.Translate( 0f, 0f, 0.05f );
            //animationPlayer( );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !stop_back ) {
            transform.Translate( 0.0f, 0f, -0.05f );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !stop_left ) {
            transform.Translate( -0.05f, 0f, 0f );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !stop_right ) {
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
        } else {
            stop_back = false;
        }
        if( front_wall.transform.position.z - 2.0f <= now_pos_z ) {
            stop_front = true;
        } else {
            stop_front = false;
        }
    }
    void removeItem( ) {
        if( item == null ){ 
            return;
        }
        float now_pos_x = pos.x;
        float now_pos_z = pos.z;
        float item_pos_x = item.transform.position.x;
        float item_pos_z = item.transform.position.z;
        float coll_x = item_pos_x - now_pos_x;
        float coll_z = item_pos_z - now_pos_z;

        if( coll_z <= 1.0f && coll_z >= -1.0f ){
            judge_coll_z = true;
        } else {
            judge_coll_z = false;
        }
		if ( coll_x <= 1.0f && coll_x >= -1.0f ) {
			judge_coll_x = true;
		} else {
            judge_coll_x = false;
        }

		if ( judge_coll_z && judge_coll_x ){
            Destroy( item );
        }
    }
	void animationPlayer( ) {
       Quaternion q = this.transform.rotation;
       transform.Rotate( new Vector3( 0, 1, 0 ), angle );
    }
}
