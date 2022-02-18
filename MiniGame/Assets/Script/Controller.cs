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
        float speed = 20.0f * Time.deltaTime;
        float x = 0.0f;
        Vector3 player_vec = new Vector3( 0f, 0f, 0f );
        //カメラが見ている方向を見て移動する。
        //カメラの正面を取得する。
        Vector3 camera_direction = _camera.transform.forward;
        //そのカメラが見ている方向から上方向にベクトルを動かす。
        if ( Input.GetKey( KeyCode.UpArrow ) ) {
            player_vec += camera_direction.normalized * speed;
            //z += speed;
        }
        //カメラの正面を取得して動かそうとしているから後ろに行くとき、カメラが空中にあるためボールも空中に浮く。
        //Xを動かさないようにしてもいいが、そうしたら次にLeftとRightの時に積む。
        if ( Input.GetKey( KeyCode.DownArrow ) ) {
            player_vec += camera_direction.normalized * -speed;
        }
        if ( Input.GetKey( KeyCode.LeftArrow ) ) {
            x += -speed;
        }
        if ( Input.GetKey( KeyCode.RightArrow ) ) {
            x += speed;
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
