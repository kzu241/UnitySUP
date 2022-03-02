using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Controller : MonoBehaviour {
    GameObject _player;
    GameObject _item;
    GameObject _camera;
    GameObject loadPrefab( string data, Vector3 pos ) {
        GameObject prefab_data = ( GameObject )Resources.Load( data );
        return Instantiate( prefab_data, pos, Quaternion.identity );
    }
    void Start( ) {
        GameObject floor = loadPrefab( "prefab_Floor", new Vector3( 5.0f, 0.5f, 5.0f ) );
        GameObject front = loadPrefab( "prefab_FrontWall", new Vector3( 5.0f, 1.0f, 15.0f ) );
        GameObject back = loadPrefab( "prefab_BackWall", new Vector3( 5.0f, 1.0f, -5.0f ) );
        GameObject right = loadPrefab( "prefab_RightWall", new Vector3( 15.0f, 1.0f, 5.0f ) );
        GameObject left = loadPrefab( "prefab_LeftWall", new Vector3( -5.0f, 1.0f, 5.0f ) );
        _player = loadPrefab( "prefab_Player", new Vector3( 1.0f, 5.0f, 0.0f ) );
        _item = loadPrefab( "prefab_Item", new Vector3( 5.0f, 1.5f, 5.0f ) );

        _camera = GameObject.Find( "MainCamera" );

        _player.GetComponent<Renderer>( ).material.color = Color.red;
        front.GetComponent<Renderer>( ).material.color = Color.cyan;
        back.GetComponent<Renderer>( ).material.color = Color.cyan;
        right.GetComponent<Renderer>( ).material.color = Color.cyan;
        left.GetComponent<Renderer>( ).material.color = Color.cyan;
        _item.GetComponent<Renderer>( ).material.color = Color.yellow;

        left.transform.Rotate( 0.0f, 90.0f, 0.0f );
        right.transform.Rotate( 0.0f, 90.0f, 0.0f );
        _item.transform.Rotate( 0.0f, 0.0f, 45.0f );
    }
   
    void Update( ) {
        updateItem( );
        updatePlayer( );
    }
    void updateItem( ) {
        rotateItem( );
    }
	void rotateItem( ) {
        if( _item != null ) {
            Quaternion vec_rotation = Quaternion.Euler( 0.0f, Time.time * 50.0f, 45.0f );
            _item.transform.rotation = vec_rotation;
        }
    }

    void updatePlayer( ) {
        float speed = 30.0f * Time.deltaTime;
        Vector3 player_vec = new Vector3( 0.0f, 0.0f, 0.0f );
        //ScaleÇégÇ¡Çƒ_cameraÇÃYÇÇOÇ…ÇµÇƒÇ¢ÇÈÅB
        Vector3 camera_forward = Vector3.Scale( _camera.transform.forward, new Vector3( 2.0f, 0.0f, 2.0f ) );
        Vector3 camera_right = Vector3.Scale( _camera.transform.right, new Vector3( 1.0f, 0.0f, 1.0f ) );
        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            player_vec += camera_forward * speed;
        }
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            player_vec += camera_forward * -speed;
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) ) {
            player_vec += camera_right * -speed;
        }
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            player_vec += camera_right * speed;
        }
        Rigidbody rb_player = _player.transform.GetComponent<Rigidbody>();
        rb_player.AddForce( player_vec, ForceMode.Impulse );
    }

	void removeItem( ) {
        if( _item != null ){
            return;
        }
        Destroy( _item );
    }
}
