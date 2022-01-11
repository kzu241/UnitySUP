using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    float _gravity_power = -0.02f;
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

    GameObject prefabLoad( string data, Vector3 pos ) {
        GameObject prefab = ( GameObject )Resources.Load( data );
        return Instantiate( prefab, pos, Quaternion.identity );
    }
    void Start( ) {
        _player_game_object = prefabLoad("prefab_player", new Vector3( 1.0f, 10.0f, 0.0f ) );
        _floor_game_object = prefabLoad("prefab_Floor", new Vector3( -5.0f, 0.0f, -5.0f ) );
        _front_game_object = prefabLoad("prefab_FrontWall", new Vector3( -5.0f, 0.0f, 15.0f ) );
        _back_game_object = prefabLoad("prefab_BackWall", new Vector3( -5.0f, 0.0f, -5.0f ) );
        _right_game_object = prefabLoad("prefab_RightWall", new Vector3( 15.0f, 0.0f, 15.0f ) );
        _left_game_object = prefabLoad("prefab_LeftWall", new Vector3( -4.0f, 0.0f, 15.0f ) );
        _item_game_object = prefabLoad("prefab_item", new Vector3( 5.0f, 1.0f, 5f ) );

        _right_game_object.transform.Rotate( 0f, 90f, 0f );
        _left_game_object.transform.Rotate( 0f, 90f, 0f );
        _item_game_object.transform.Rotate( 0f, 0f, 45f );
    }
   
    void Update( ) {
        addGravity( );
        processItem( );
        movePlayer( );
        takeCollision( );
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
    void addGravity( ) {
        if (_player_game_object.transform.position.y <= _floor_game_object.transform.position.y + 0.94f ) {
            _player_game_object.transform.Translate( 0.0f, 0.0f, 0.0f, Space.World );
        } else {
            _gravity_power -= 0.5f * Time.deltaTime;
            _player_game_object.transform.Translate( 0.0f, _gravity_power, 0.0f, Space.World );
        }
    }

    void movePlayer( ) {
        if ( Input.GetKey( KeyCode.UpArrow ) && !_stop_front ) {
            _speed = 10.0f * Time.deltaTime;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveForwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !_stop_back ) {
            _speed = -10.0f * Time.deltaTime;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveBackwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !_stop_left ) {
            _speed = -10.0f * Time.deltaTime;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveLeftAnimation( );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !_stop_right ) {
            _speed = 10.0f * Time.deltaTime;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveRightAnimation( );
        }
    }

    void takeCollision( ) {
        float now_pos_z = _player_game_object.transform.position.z;
        float now_pos_x = _player_game_object.transform.position.x;
        if ( _left_game_object.transform.position.x + 0.9f >= now_pos_x ) {
            _stop_left = true;
        } else {
            _stop_left = false;
        }
        if ( _right_game_object.transform.position.x - 1.9f <= now_pos_x ) {
            _stop_right = true;
        } else {
            _stop_right = false;
        }
        if ( _back_game_object.transform.position.z + 0.9f >= now_pos_z ) {
            _stop_back = true;
        } else {
            _stop_back = false;
        }
        if ( _front_game_object.transform.position.z - 1.9f <= now_pos_z ) {
            _stop_front = true;
        } else {
            _stop_front = false;
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
