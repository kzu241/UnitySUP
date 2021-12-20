using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createObject : MonoBehaviour {
    //player
    public GameObject player;
    public Vector3 player_position = new Vector3( 1.0f, 10.0f, 0.0f );
    Quaternion player_rotation = Quaternion.Euler( 0f, 0f, 0f );
    //item
    public GameObject item;
    Vector3 item_position = new Vector3( 5.0f, 1.0f, 5f );
    Quaternion item_rotation = Quaternion.Euler( 0f, 0f, 45f );
    //stage
    GameObject stage;
    Vector3 stage_position = new Vector3( -5.0f, 0.0f, -5.0f );
    //camera
    GameObject main_camera;
    Vector3 camera_pos = new Vector3( 5.0f, 15.0f, -13.0f );
    Quaternion camera_rotation = Quaternion.Euler( 50.0f, 0.0f, 0.0f );
    void Start( ) {
        createItem( );
        createStage( );
        createPlayer( );
        createCamera( );
    }
   void createPlayer( ) {
        player = ( GameObject )Resources.Load( "Player/player" );
        Instantiate( player, player_position, player_rotation );
    }
    void createItem( ) {
        item = ( GameObject )Resources.Load( "Item/item" );
        Instantiate( item, item_position, item_rotation );
    }
    void createStage( ) {
        stage = ( GameObject )Resources.Load( "Stage/stage" );
        Instantiate( stage, stage_position, Quaternion.identity );
    }
    void createCamera( ) {
        main_camera = ( GameObject )Resources.Load( "Camera/Main Camera" );
        Instantiate( main_camera, camera_pos, camera_rotation );
    }
}
