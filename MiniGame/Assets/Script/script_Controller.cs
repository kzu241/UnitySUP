using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    float _speed = 0f;
    bool _stop_left = false;
    bool _stop_right = false;
    bool _stop_back = false;
    bool _stop_front = false;

    GameObject _player_game_object;
    GameObject _floor_game_object;
    GameObject _front_game_object;
    GameObject _back_game_object;
    GameObject _right_game_object;
    GameObject _left_game_object;
    GameObject _item_game_object;

    GameObject loadPrefab( string data, Vector3 pos ) {
        GameObject prefab = ( GameObject )Resources.Load( data );
        return Instantiate( prefab, pos, Quaternion.identity );
    }
    void Start( ) {
        _player_game_object = loadPrefab("prefab_player", new Vector3( 1.0f, 10.0f, 0.0f ) );
        _floor_game_object = loadPrefab("prefab_Floor", new Vector3( -5.0f, 0.0f, -5.0f ) );
        _front_game_object = loadPrefab("prefab_FrontWall", new Vector3( -5.0f, 0.0f, 15.0f ) );
        _back_game_object = loadPrefab("prefab_BackWall", new Vector3( -5.0f, 0.0f, -5.0f ) );
        _right_game_object = loadPrefab("prefab_RightWall", new Vector3( 15.0f, 0.0f, 15.0f ) );
        _left_game_object = loadPrefab("prefab_LeftWall", new Vector3( -4.0f, 0.0f, 15.0f ) );
        _item_game_object = loadPrefab("prefab_item", new Vector3( 5.0f, 1.0f, 5f ) );

        _right_game_object.transform.Rotate( 0f, 90f, 0f );
        _left_game_object.transform.Rotate( 0f, 90f, 0f );
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
        if ( Input.GetKey( KeyCode.UpArrow ) && !_stop_front ) {
            _speed = 7.0f * Time.deltaTime;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveForwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !_stop_back ) {
            _speed = -7.0f * Time.deltaTime;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveBackwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !_stop_left ) {
            _speed = -7.0f * Time.deltaTime;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveLeftAnimation( );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !_stop_right ) {
            _speed = 7.0f * Time.deltaTime;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveRightAnimation( );
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

    private void OnCollisionEnter(Collision collision)
    {
        //Sphereが衝突したオブジェクトがPlaneだった場合
        if ( collision.gameObject.name == "Plane") {
            Debug.Log( "きちゃー" );
        }
    }

    //Animation
    void moveForwardAnimation( ) {
        Quaternion z = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.right );
        Quaternion forward_move = z * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = forward_move;
    }
    void moveBackwardAnimation( ) {
        Quaternion z = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.right );
        Quaternion backward_move = z * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = backward_move;
    }
    void moveLeftAnimation( ) {
        Quaternion x = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.forward );
        Quaternion left_move = x * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = left_move;
    }
    void moveRightAnimation( ) {
        Quaternion x = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.forward );
        Quaternion right_move = x * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = right_move;
    }

}
