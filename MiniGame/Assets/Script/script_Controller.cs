using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    GameObject _player_game_object;
    GameObject _floor_game_object;
    GameObject _front_game_object;
    GameObject _back_game_object;
    GameObject _right_game_object;
    GameObject _left_game_object;
    GameObject _item_game_object;

    Rigidbody _rigid_body;

    GameObject loadPrefab( string data, Vector3 pos ) {
        GameObject prefab = ( GameObject )Resources.Load( data );
        return Instantiate( prefab, pos, Quaternion.identity );
    }
    void Start( ) {
        _player_game_object = loadPrefab( "prefab_player", new Vector3( 1f, 5f, 0f ) );
        _floor_game_object = loadPrefab( "prefab_Floor", new Vector3( 5f, 0f, 5f ) );
        _front_game_object = loadPrefab( "prefab_FrontWall", new Vector3( 5f, 1f, 15f ) );
        _back_game_object = loadPrefab( "prefab_BackWall", new Vector3( 5f, 1f, -5f ) );
        _right_game_object = loadPrefab( "prefab_RightWall", new Vector3( 15f, 1f, 5f ) );
        _left_game_object = loadPrefab( "prefab_LeftWall", new Vector3( -5f, 1f, 5f ) );
        _item_game_object = loadPrefab( "prefab_Item", new Vector3( 5f, 1f, 5f ) );

        _player_game_object.GetComponent<Renderer>( ).material.color = Color.red;
        _front_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _back_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _right_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _left_game_object.GetComponent<Renderer>( ).material.color = Color.cyan;
        _item_game_object.GetComponent<Renderer>( ).material.color = Color.yellow;

        _front_game_object.transform.Rotate( 0f, 90f, 0f );
        _back_game_object.transform.Rotate( 0f, 90f, 0f );
        _item_game_object.transform.Rotate( 0f, 0f, 45f );
        _rigid_body = _player_game_object.transform.GetComponent<Rigidbody>( );
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
        float _speed = 10f;
        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            _speed = 10f * Time.deltaTime;
            //_rigid_body.AddForce( transform.forward * _speed );
            _rigid_body.AddForce( 0f, 0f, _speed, ForceMode.Impulse );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            _speed = -1000f * Time.deltaTime;
            _rigid_body.AddForce(transform.forward * _speed);
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) ) {
            _speed = -1000f * Time.deltaTime;
            _rigid_body.AddForce( transform.right * _speed );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            _speed = 1000f * Time.deltaTime;
            _rigid_body.AddForce( transform.right * _speed );
        }
    }
    void removeItem( ) {
        float now_pos_x = _player_game_object.transform.position.x;
        float now_pos_z = _player_game_object.transform.position.z;
        float item_pos_x = _item_game_object.transform.position.x;
        float item_pos_z = _item_game_object.transform.position.z;
        float coll_x = changePositiveNumber( item_pos_x - now_pos_x );
        float coll_z = changePositiveNumber( item_pos_z - now_pos_z );
        if ( coll_z <= 1.0f && coll_x <= 1.0f ) {
            Destroy( _item_game_object );
        }
    }
    float changePositiveNumber( float coll ) {
        if ( coll < 0 ) {
            coll *= -1f;
        }
        return coll;
    }
}
