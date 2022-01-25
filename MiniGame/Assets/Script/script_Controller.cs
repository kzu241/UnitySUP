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
        GameObject _floor_game_object = loadPrefab( "prefab_Floor", new Vector3( 5f, 0.5f, 5f ) );
        GameObject _front_game_object = loadPrefab( "prefab_FrontWall", new Vector3( 5f, 1f, 15f ) );
        GameObject _back_game_object = loadPrefab( "prefab_BackWall", new Vector3( 5f, 1f, -5f ) );
        GameObject _right_game_object = loadPrefab( "prefab_RightWall", new Vector3( 15f, 1f, 5f ) );
        GameObject _left_game_object = loadPrefab( "prefab_LeftWall", new Vector3( -5f, 1f, 5f ) );
        _player_game_object = loadPrefab( "prefab_player", new Vector3( 1f, 5f, 0f ) );
        _item_game_object = loadPrefab( "prefab_Item", new Vector3( 5f, 1f, 5f ) );

        _player_game_object.GetComponent<Renderer>( ).material.color = Color.red;
        _front_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _back_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _right_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _left_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _item_game_object.GetComponent<Renderer>( ).material.color = Color.yellow;

        _left_game_object.transform.Rotate( 0f, 90f, 0f );
        _right_game_object.transform.Rotate( 0f, 90f, 0f );
        _item_game_object.transform.Rotate( 0f, 0f, 45f );
    }
   
    void Update( ) {
        processItem( );
        movePlayer( );
    }
    void processItem( ) {
        if( _item_game_object != null ){
            moveItem( );
            removeItem( );
        }
    }
	void moveItem( ) {
        Quaternion item_rotation = Quaternion.AngleAxis( 30.0f * Time.deltaTime, Vector3.up );
        Quaternion rotate_y = item_rotation * _item_game_object.transform.rotation;
        _item_game_object.transform.rotation = rotate_y;
    }
    void movePlayer( ) {
        float speed = 20f * Time.deltaTime;
        float x = 0f;
        float z = 0f;
        Rigidbody _rb_player = _player_game_object.transform.GetComponent<Rigidbody>( );

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
        _rb_player.AddForce( x, 0f, z, ForceMode.Impulse );
    }
    bool overlappedItem( ) {
        Vector3 player_pos = _player_game_object.transform.position;
        Vector3 item_pos = _item_game_object.transform.position;
        float dis = Vector3.Distance( player_pos, item_pos );
        if ( dis < 1.2f ) {
            return true;
        }
        return false;
    }
    void removeItem( ) {
        if ( overlappedItem( ) ) {
            Destroy( _item_game_object );
        }
    }
}
