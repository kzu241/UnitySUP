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
    GameObject _stage_game_object;
    Vector3 _stage_position = new Vector3( -5.0f, 0.0f, -5.0f );
                                //Child
    GameObject _child_back_object;
    GameObject _child_front_object;
    GameObject _child_left_object;
    GameObject _child_right_object;
                                //Item
    GameObject _item_game_object;
    Vector3 _item_position = new Vector3( 5.0f, 1.0f, 5f );

    void Start( ) {
        createPlayer( );
        createStage( );
        createItem( );
        createChild( );
    }
    void createPlayer( ) {
        Object player_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_player.prefab" );
        _player_game_object = (GameObject)Instantiate( player_data, _player_position, Quaternion.identity );
    }
    void createStage( ) {
        Object stage_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_stage.prefab" );
        _stage_game_object = ( GameObject )Instantiate( stage_data, _stage_position, Quaternion.identity );
    }
    void createChild( ) {
        _child_back_object = _stage_game_object.transform.Find( "BackWall" ).gameObject;
        _child_front_object = _stage_game_object.transform.Find( "FrontWall" ).gameObject;
        _child_right_object = _stage_game_object.transform.Find( "RightWall" ).gameObject;
        _child_left_object = _stage_game_object.transform.Find( "LeftWall" ).gameObject;
    }
    void createItem( ) {
        Object item_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_item.prefab" );
        _item_game_object = ( GameObject )Instantiate( item_data, _item_position, Quaternion.identity );
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
            _gravity_power -= 0.01f;
            _player_game_object.transform.Translate( 0.0f, _gravity_power, 0.0f, Space.World );
        }
    }

    void movePlayer( ) {
        if ( Input.GetKey( KeyCode.UpArrow ) && !_stop_front ) {
            _speed = 0.05f;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveForwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !_stop_back ) {
            _speed = -0.05f;
            _player_game_object.transform.Translate( 0, 0, _speed, Space.World );
            moveBackwardAnimation( );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !_stop_left ) {
            _speed = -0.05f;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveLeftAnimation( );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !_stop_right ) {
            _speed = 0.05f;
            _player_game_object.transform.Translate( _speed, 0, 0, Space.World );
            moveRightAnimation( );
        }
    }

    void takeCollision( ) {
        float now_pos_z = _player_game_object.transform.position.z;
        float now_pos_x = _player_game_object.transform.position.x;
        if ( _child_left_object.transform.position.x + 0.9f >= now_pos_x ) {
            _stop_left = true;
        } else {
            _stop_left = false;
        }
        if ( _child_right_object.transform.position.x - 1.9f <= now_pos_x ) {
            _stop_right = true;
        } else {
            _stop_right = false;
        }
        if ( _child_back_object.transform.position.z + 0.9f >= now_pos_z ) {
            _stop_back = true;
        } else {
            _stop_back = false;
        }
        if ( _child_front_object.transform.position.z - 1.9f <= now_pos_z ) {
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
