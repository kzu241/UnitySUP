using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class script_Controller : MonoBehaviour {
    float gravity_power = -0.02f;
    Vector3 _ground_pos = new Vector3( 0f, 0f, 0f );
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
        cameraMove( );
        itemMove( );
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

}
