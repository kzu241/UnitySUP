using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    float _gravity_power = -0.02f;
    Vector3 _ground_pos = new Vector3( 0f, 0f, 0f );
    float _speed = 0f;
    bool _stop_left = false;
    bool _stop_right = false;
    bool _stop_back = false;
    bool _stop_front = false;

                                //Player
    GameObject _player_game_object;
    Vector3 _player_position = new Vector3( 1.0f, 10.0f, 0.0f );
    float _now_player_pos;
                                //Stage
    GameObject _floor_game_object;
    Vector3 _floor_position = new Vector3( -5.0f, 0.0f, -5.0f );
    GameObject _front_game_object;
    Vector3 _front_positiont = new Vector3( -5.0f, 0.0f, 15.0f);
    GameObject _back_game_object;
    Vector3 _back_position = new Vector3( -5.0f, 0.0f, -5.0f );
    GameObject _right_game_object;
    Vector3 _right_position = new Vector3( 15.0f, 0.0f, 15.0f );
    GameObject _left_game_object;
    Vector3 _left_position = new Vector3( -4.0f, 0.0f, 15.0f );
                                //Item
    GameObject _item_game_object;
    Vector3 _item_position = new Vector3( 5.0f, 1.0f, 5f );

    void Start( ) {
        createPlayer( );
        createStage( );
        createItem( );
    }
    void createPlayer( ) {
        Object player_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_player.prefab" );
        _player_game_object = (GameObject)Instantiate( player_data, _player_position, Quaternion.identity );
    }
    void createStage( ) {
        Object floor_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_Floor.prefab" );
        _floor_game_object = ( GameObject )Instantiate( floor_data, _floor_position, Quaternion.identity );
        Object front_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_FrontWall.prefab" );
        _front_game_object = ( GameObject )Instantiate( front_data, _front_positiont, Quaternion.identity );
        Object back_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_BackWall.prefab" );
        _back_game_object = ( GameObject )Instantiate( back_data, _back_position, Quaternion.identity );
        Object right_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_RightWall.prefab" );
        _right_game_object = ( GameObject )Instantiate( right_data, _right_position, Quaternion.identity );
        _right_game_object.transform.Rotate( 0f, 90f, 0 );
        Object left_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_LeftWall.prefab" );
        _left_game_object = ( GameObject )Instantiate( left_data, _left_position, Quaternion.identity );
        _left_game_object.transform.Rotate( 0f, 90f, 0f );
    }
    void createItem( ) {
        Object item_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_item.prefab" );
        _item_game_object = ( GameObject )Instantiate( item_data, _item_position, Quaternion.identity );
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
        _now_player_pos = _player_game_object.transform.position.y;
        if ( _now_player_pos <= _ground_pos.y + 0.99f ) {
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
