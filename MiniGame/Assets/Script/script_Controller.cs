using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    GameObject _player_game_object;
    GameObject _item_game_object;

    GameObject loadPrefab( string data, Vector3 pos ) {
        GameObject prefab_data = ( GameObject )Resources.Load( data );
        return Instantiate( prefab_data, pos, Quaternion.identity );
    }
    void Start( ) {
        GameObject _floor_game_object = loadPrefab( "prefab_Floor", new Vector3( 5.0f, 0.5f, 5.0f ) );
        GameObject _front_game_object = loadPrefab( "prefab_FrontWall", new Vector3( 5.0f, 1.0f, 15.0f ) );
        GameObject _back_game_object = loadPrefab( "prefab_BackWall", new Vector3( 5.0f, 1.0f, -5.0f ) );
        GameObject _right_game_object = loadPrefab( "prefab_RightWall", new Vector3( 15.0f, 1.0f, 5.0f ) );
        GameObject _left_game_object = loadPrefab( "prefab_LeftWall", new Vector3( -5.0f, 1.0f, 5.0f ) );
        _player_game_object = loadPrefab( "prefab_Player", new Vector3( 1.0f, 5.0f, 0.0f ) );
        _item_game_object = loadPrefab( "prefab_Item", new Vector3( 5.0f, 1.5f, 5.0f ) );

        _player_game_object.GetComponent<Renderer>( ).material.color = Color.red;
        _front_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _back_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _right_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _left_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _item_game_object.GetComponent<Renderer>( ).material.color = Color.yellow;

        _left_game_object.transform.Rotate( 0.0f, 90.0f, 0.0f );
        _right_game_object.transform.Rotate( 0.0f, 90.0f, 0.0f );
        _item_game_object.transform.Rotate( 0.0f, 0.0f, 45.0f );
    }
   
    void Update( ) {
        updateItem( );
        movePlayer( );
    }
    void updateItem( ) {
        if( _item_game_object != null ){
            rotationItem( );
            checkRemoveItem( );
        }
    }
	void rotationItem( ) {
        Quaternion vec_rotation = Quaternion.Euler( 0f, Time.time * 50f, 45f);
        _item_game_object.transform.rotation = vec_rotation;
    }
    void movePlayer( ) {
        Rigidbody rb_player = _player_game_object.transform.GetComponent<Rigidbody>( );
        float speed = 20.0f * Time.deltaTime;
        float x = 0.0f;
        float z = 0.0f;

        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            z += speed;
        }
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            z += -speed;
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) ) {
            x += -speed;
        }
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            x += speed;
        }
        rb_player.AddForce( x, 0.0f, z, ForceMode.Impulse );
    }
    bool isOverlappedItem( ) {
        Vector3 player_pos = _player_game_object.transform.position;
        Vector3 item_pos = _item_game_object.transform.position;
        float dis = Vector3.Distance( player_pos, item_pos );
        if ( dis < 1.2f ) {
            return true;
        }
        return false;
    }
    void checkRemoveItem( ) {
        if ( isOverlappedItem( ) ) {
            Destroy( _item_game_object );
        }
    }
}
