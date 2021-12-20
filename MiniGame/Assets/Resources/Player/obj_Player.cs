using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour {
    Vector3 pos;
    Transform player;
    public GameObject item;
    float gravity_power = -0.02f;
    bool stop_left = false;
    bool stop_right = false;
    bool stop_back = false;
    bool stop_front = false;
    float speed = 0f;

    void Start( ) {
        item = GameObject.FindGameObjectWithTag( "Item" );
    }

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

    void gravity( ) {
        Vector3 ground_pos = new Vector3( 0.0f, 0.0f, 0.0f );
        float now_pos_y = pos.y;
        float ground_get_pos = ground_pos.y + 0.97f;
        if ( now_pos_y <= ground_get_pos ) {
            transform.Translate( 0.0f, 0.0f, 0.0f, Space.World );
        } else {
            transform.Translate( 0.0f, gravity_power, 0.0f, Space.World );
            gravity_power -= 0.01f;
        }
    }
        
    void move( ) {
        if ( Input.GetKey( KeyCode.UpArrow ) && !stop_front ) {
            this.speed = 0.05f;
            transform.Translate( 0, 0, speed, Space.World );
            animationForwardMove( );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !stop_back ) {
            this.speed = -0.05f;
            transform.Translate( 0, 0, speed, Space.World );
            animationBackwardMove( );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !stop_left ) {
            this.speed = -0.05f;
            transform.Translate( speed, 0, 0, Space.World );
            animationLeftMove( );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !stop_right ) {
            this.speed = 0.05f;
            transform.Translate( speed, 0, 0, Space.World );
            animationRightMove( );
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
        if ( right_wall.transform.position.x - 2.0f <= now_pos_x ) {
            stop_right = true;
        } else {
            stop_right = false;
        }
        if ( back_wall.transform.position.z + 1.0f >= now_pos_z ) {
            stop_back = true;
        } else {
            stop_back = false;
        }
        if ( front_wall.transform.position.z - 2.0f <= now_pos_z ) {
            stop_front = true;
        } else {
            stop_front = false;
        }
    }
    void removeItem( ) {
        if ( item == null ) { 
            return;
        }
        float now_pos_x = pos.x;
        float now_pos_z = pos.z;
        float item_pos_x = item.transform.position.x;
        float item_pos_z = item.transform.position.z;

        float coll_x = conversion( item_pos_x - now_pos_x );
        float coll_z = conversion( item_pos_z - now_pos_z );

        if ( coll_z <= 1.0f && coll_x <= 1.0f ) {
            Destroy( item );
        }
    }

    float conversion( float coll ) {
        if( coll < 0 ){
            coll *= -1f;
        }
        return coll;
    }

    //Animation
    void animationForwardMove( ) {
        Quaternion z = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.right );
        Quaternion forward_move = z * this.transform.rotation;
        transform.rotation = forward_move;
    }
    void animationBackwardMove( ) {
        Quaternion z = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.right );
        Quaternion backward_move = z * this.transform.rotation;
        transform.rotation = backward_move;
    }
    void animationLeftMove( ) {
        Quaternion x = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.forward );
        Quaternion left_move = x * this.transform.rotation;
        transform.rotation = left_move;
    }
    void animationRightMove( ) {
        Quaternion x = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.forward );
        Quaternion right_move = x * this.transform.rotation;
        transform.rotation = right_move;
    }
}
