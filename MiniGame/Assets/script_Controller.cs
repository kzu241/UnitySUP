using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    float gravity_power = -0.02f;
    Vector3 _ground_pos = new Vector3( 0f, 0f, 0f );
    float speed = 0f;
    bool stop_left = false;
    bool stop_right = false;
    bool stop_back = false;
    bool stop_front = false;
    //Player
    GameObject _player_game_object;
    Vector3 _player_position = new Vector3( 1.0f, 10.0f, 0.0f );
    Quaternion _player_rotation = Quaternion.Euler( 0f, 0f, 0f );
    float now_player_pos;
                                //Stage
    GameObject _stage_game_object;
    Vector3 _stage_position = new Vector3( -5.0f, 0.0f, -5.0f );
                                //Item
    GameObject _item_game_object;
    Vector3 _item_position = new Vector3( 5.0f, 1.0f, 5f );
    Quaternion _item_rotation = Quaternion.Euler( 0f, 0f, 45f );
                                //Camera
    GameObject _camera_game_object;
    Vector3 _camera_position = new Vector3( 5.0f, 15.0f, -13.0f );
    Quaternion _camera_rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );
    Vector3 now_camera_pos;

    void Start( ) {
        createPlayer( );
        createStage( );
        createItem( );
        createCamera( );
    }
    void createPlayer( ) {
        Object player_data = AssetDatabase.LoadMainAssetAtPath("Assets/Prefab/prefab_player.prefab");
        _player_game_object = (GameObject)Instantiate( player_data, _player_position, _player_rotation );
    }
    void createStage( ) {
        Object stage_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_stage.prefab" );
        _stage_game_object = ( GameObject )Instantiate( stage_data, _stage_position, Quaternion.identity );
    }
    void createItem( ) {
        Object item_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_item.prefab" );
        _item_game_object = ( GameObject )Instantiate( item_data, _item_position, _item_rotation );
    }
    void createCamera( ) {
        Object camera_data = AssetDatabase.LoadMainAssetAtPath( "Assets/Prefab/prefab_camera.prefab" );
        _camera_game_object = ( GameObject )Instantiate( camera_data, _camera_position, _camera_rotation );
    }
    void Update( ) {
        gravity( );
        animationMove( );
        playerMove( );
        collisionJudgement( );
        
    }
    void animationMove( ) {
        cameraMove( );
        if( _item_game_object != null ){
            itemMove( );
            removeItem( );
        }
    }
    void itemMove( ){
        Quaternion y = Quaternion.AngleAxis( 30.0f * Time.deltaTime, Vector3.up );
        Quaternion rotate_y = y * _item_game_object.transform.rotation;
        _item_game_object.transform.rotation = rotate_y;
    }
    void cameraMove( ) {
        now_camera_pos.z = _player_game_object.transform.position.z - 15.0f;
        now_camera_pos.x = _player_game_object.transform.position.x;
        now_camera_pos.y = _player_game_object.transform.position.y + 15.0f;
        _camera_game_object.transform.position = now_camera_pos;
    }
    void gravity( ) {
        now_player_pos = _player_game_object.transform.position.y;
        if ( now_player_pos <= _ground_pos.y + 0.99f ) {
            _player_game_object.transform.Translate( 0.0f, 0.0f, 0.0f, Space.World );
        } else {
            gravity_power -= 0.01f;
            _player_game_object.transform.Translate( 0.0f, gravity_power, 0.0f, Space.World );
        }
    }

    void playerMove( ) {
        if ( Input.GetKey( KeyCode.UpArrow ) && !stop_front ) {
            speed = 0.05f;
            _player_game_object.transform.Translate(0, 0, speed, Space.World);
            animationForwardMove();
        }
        if ( Input.GetKey( KeyCode.DownArrow ) && !stop_back ) {
            speed = -0.05f;
            _player_game_object.transform.Translate( 0, 0, speed, Space.World );
            animationBackwardMove( );
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) && !stop_left ) {
            speed = -0.05f;
            _player_game_object.transform.Translate( speed, 0, 0, Space.World );
            animationLeftMove( );
        }
        if ( Input.GetKey( KeyCode.RightArrow ) && !stop_right ) {
            speed = 0.05f;
            _player_game_object.transform.Translate( speed, 0, 0, Space.World );
            animationRightMove( );
        }
    }

    void collisionJudgement( ) {
        GameObject left_wall = GameObject.FindGameObjectWithTag("LeftWall");
        GameObject right_wall = GameObject.FindGameObjectWithTag("RightWall");
        GameObject back_wall = GameObject.FindGameObjectWithTag("BackWall");
        GameObject front_wall = GameObject.FindGameObjectWithTag("FrontWall");
        float now_pos_z = _player_game_object.transform.position.z;
        float now_pos_x = _player_game_object.transform.position.x;
        if ( left_wall.transform.position.x + 0.9f >= now_pos_x ) {
            stop_left = true;
        } else {
            stop_left = false;
        }
        if ( right_wall.transform.position.x - 1.9f <= now_pos_x ) {
            stop_right = true;
        } else {
            stop_right = false;
        }
        if ( back_wall.transform.position.z + 0.9f >= now_pos_z ) {
            stop_back = true;
        } else {
            stop_back = false;
        }
        if ( front_wall.transform.position.z - 1.9f <= now_pos_z ) {
            stop_front = true;
        } else {
            stop_front = false;
        }
    }

    void removeItem( ) {
        float now_pos_x = _player_game_object.transform.position.x;
        float now_pos_z = _player_game_object.transform.position.z;
        float item_pos_x = _item_game_object.transform.position.x;
        float item_pos_z = _item_game_object.transform.position.z;

        float coll_x = conversion( item_pos_x - now_pos_x );
        float coll_z = conversion( item_pos_z - now_pos_z );

        if ( coll_z <= 1.0f && coll_x <= 1.0f )
        {
            Destroy( _item_game_object );
        }
    }

    float conversion(float coll)
    {
        if (coll < 0)
        {
            coll *= -1f;
        }
        return coll;
    }

    //Animation
    void animationForwardMove( ) {
        Quaternion z = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.right );
        Quaternion forward_move = z * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = forward_move;
    }
    void animationBackwardMove( ) {
        Quaternion z = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.right );
        Quaternion backward_move = z * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = backward_move;
    }
    void animationLeftMove( ) {
        Quaternion x = Quaternion.AngleAxis( 300.0f * Time.deltaTime, Vector3.forward );
        Quaternion left_move = x * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = left_move;
    }
    void animationRightMove( ) {
        Quaternion x = Quaternion.AngleAxis( -300.0f * Time.deltaTime, Vector3.forward );
        Quaternion right_move = x * _player_game_object.transform.rotation;
        _player_game_object.transform.rotation = right_move;
    }

}
