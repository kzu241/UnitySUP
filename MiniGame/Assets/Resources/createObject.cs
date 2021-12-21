using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class createObject : MonoBehaviour {
    //player
    public GameObject player;
    public Vector3 player_position = new Vector3( 1.0f, 10.0f, 0.0f );
    Quaternion player_rotation = Quaternion.Euler( 0f, 0f, 0f );
    private AsyncOperationHandle<GameObject> _player;
    //item
    public GameObject item;
    Vector3 item_position = new Vector3( 5.0f, 1.0f, 5f );
    Quaternion item_rotation = Quaternion.Euler( 0f, 0f, 45f );
    //stage
    GameObject stage;
    Vector3 stage_position = new Vector3( -5.0f, 0.0f, -5.0f );
    private AsyncOperationHandle<GameObject> _stage;
    //camera
    GameObject main_camera;
    Vector3 camera_pos = new Vector3( 5.0f, 15.0f, -13.0f );
    Quaternion camera_rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );

    void Start( ) {
        //createItem( );
        createStage( );
        createPlayer( );
        //createCamera( );
    }
   private async void createPlayer( ) {
        //player = ( GameObject )Resources.Load( "Player/player" );
        _player = Addressables.InstantiateAsync("Player/player", player_position, player_rotation );
        await _player.Task;
        Instantiate( player, player_position, player_rotation );
    }
	private void view_player( ) {
        Addressables.ReleaseInstance( _player );
    }
	void createItem( ) {
        item = ( GameObject )Resources.Load( "Item/item" );
        Instantiate( item, item_position, item_rotation );
    }
    private async void createStage( ) {
        stage = ( GameObject )Resources.Load( "Stage/stage" );
        
        _stage = Addressables.InstantiateAsync("Stage/stage", stage_position, Quaternion.identity );
        await _stage.Task;
    }
    private void view_stage( ) {
        Addressables.ReleaseInstance( _stage );
    }
    void createCamera( ) {
        main_camera = ( GameObject )Resources.Load( "Camera/Main Camera" );
        Instantiate( main_camera, camera_pos, camera_rotation );
    }
}
