using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class script_createObject : MonoBehaviour {
    //player
    public Transform player;
    Vector3 player_position = new Vector3( 1.0f, 10.0f, 0.0f );
    Quaternion player_rotation = Quaternion.Euler( 0f, 0f, 0f );
    //item
    public Transform item;
    Vector3 item_position = new Vector3( 5.0f, 1.0f, 5f );
    Quaternion item_rotation = Quaternion.Euler( 0f, 0f, 45f );
    //stage
    public Transform stage;
    Vector3 stage_position = new Vector3( -5.0f, 0.0f, -5.0f );
    //camera
    public Transform main_camera;
    Vector3 camera_pos = new Vector3( 5.0f, 15.0f, -13.0f );
    Quaternion camera_rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );

    void Start( ) {
        createItem( );
        createStage( );
        createPlayer( );
        createCamera( );
    }
   void createPlayer( ) {
        //player = ( GameObject )Resources.Load( "Player/player" );
        //_player = Addressables.InstantiateAsync("Player/player", player_position, player_rotation );
        //await _player.Task;
        Instantiate( player, player_position, player_rotation );
    }
	void createItem( ) {
        Instantiate( item, item_position, item_rotation );
    }
    void createStage( ) {
        Instantiate( stage, stage_position, Quaternion.identity );
    }
    
    void createCamera( ) {
        Instantiate( main_camera, camera_pos, camera_rotation );
    }
}
