using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Controller : MonoBehaviour {
    GameObject _player;
    GameObject _move_delete_item;
    GameObject _range_delete_item;
    GameObject _camera;

    Vector3 _first_item_pos;

    int _delete_item_timer = 0;
    int _delete_timer_count = 0;

    const float FOLLOW_RANGE = 20.0f;
    const float DELETE_RANGE = 2.0f;

    GameObject loadPrefab( string data, Vector3 pos ) {
        GameObject prefab_data = ( GameObject )Resources.Load( data );
        return Instantiate( prefab_data, pos, Quaternion.identity );
    }
    void Start( ) {
        Application.targetFrameRate = 60;

        GameObject floor = loadPrefab( "prefab_Floor", new Vector3( 5.0f, 0.5f, 5.0f ) );
        GameObject front = loadPrefab( "prefab_FrontWall", new Vector3( 5.0f, 1.0f, 15.0f ) );
        GameObject back = loadPrefab( "prefab_BackWall", new Vector3( 5.0f, 1.0f, -5.0f ) );
        GameObject right = loadPrefab( "prefab_RightWall", new Vector3( 15.0f, 1.0f, 5.0f ) );
        GameObject left = loadPrefab( "prefab_LeftWall", new Vector3( -5.0f, 1.0f, 5.0f ) );
        _player = loadPrefab( "prefab_Player", new Vector3( 1.0f, 5.0f, 0.0f ) );
        _move_delete_item = loadPrefab( "prefab_Item", new Vector3( 0.0f, 1.5f, 5.0f ) );
        _range_delete_item = loadPrefab( "prefab_Item", new Vector3( 10.0f, 1.5f, 5.0f ) );

        _camera = GameObject.Find( "MainCamera" );

        _player.GetComponent<Renderer>( ).material.color = Color.red;
        front.GetComponent<Renderer>( ).material.color = Color.cyan;
        back.GetComponent<Renderer>( ).material.color = Color.cyan;
        right.GetComponent<Renderer>( ).material.color = Color.cyan;
        left.GetComponent<Renderer>( ).material.color = Color.cyan;
        _move_delete_item.GetComponent<Renderer>( ).material.color = Color.yellow;
        _range_delete_item.GetComponent<Renderer>().material.color = Color.yellow;


        left.transform.Rotate( 0.0f, 90.0f, 0.0f );
        right.transform.Rotate( 0.0f, 90.0f, 0.0f );
        _move_delete_item.transform.Rotate( 0.0f, 0.0f, 45.0f );
        _range_delete_item.transform.Rotate( 0.0f, 0.0f, 45.0f );

        _first_item_pos = _move_delete_item.transform.position;
        _delete_item_timer = 3;
    }

    void Update( ) {
        updateItem( );
        updatePlayer( );
    }
    void updateItem( ) {
        rotateItem( );
        if( sensingSpeedItem( ) ) {
            if( deleteItemTime( ) == 0 ) {
                removeItem( _move_delete_item );
            }
        }
        if( sensingRange( ) ){
            followItem( );
            if( deleteRange( ) ){
                removeItem( _range_delete_item );
            }
        }
    }
	void rotateItem( ) {
        Quaternion vec_rotation = Quaternion.Euler( 0.0f, Time.time * 50.0f, 45.0f );
        if( _move_delete_item != null ) {
            _move_delete_item.transform.rotation = vec_rotation;
        }
        if( _range_delete_item != null ){
            _range_delete_item.transform.rotation = vec_rotation;
        }
    }

    void updatePlayer( ) {
        float speed = 30.0f * Time.deltaTime;
        Vector3 player_vec = new Vector3( 0.0f, 0.0f, 0.0f );
        //Scaleを使って_cameraのYを０にしている。
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
        Rigidbody rb_player = _player.transform.GetComponent<Rigidbody>( );
        rb_player.AddForce( player_vec, ForceMode.Impulse );
    }

    bool deleteRange( ) {
        // ItemがPlayerの指定の範囲に入ったらtrue、入らなかったらfalse;
        if ( _range_delete_item != null ) {
            Vector3 distance = _range_delete_item.transform.position - _player.transform.position;
            float collision = distance.magnitude;
            if ( collision < DELETE_RANGE ) {
                return true;
            }
        }
        return false;
    }

    void followItem( ) {
        //プレイヤーに向かう
        if( _range_delete_item != null ) {
            Vector3 distance = _range_delete_item.transform.position - _player.transform.position;
            _range_delete_item.transform.position -= distance / 30;
        }
    }

    bool sensingRange( ) {
        //ItemがPlayerの指定の範囲に入ったらtrue、入らなかったらfalse;
        if( _range_delete_item != null ) {
            float magnitude = Vector3.Distance( _range_delete_item.transform.position, _player.transform.position );
            if( magnitude < FOLLOW_RANGE ) {
                return true;
            }
        }
        return false;
    }

    int deleteItemTime( ) {
        //３秒後に消えるカウントダウン
        _delete_timer_count++;
        _delete_timer_count %= 60;
        if( _delete_timer_count == 0 ) {
            _delete_item_timer--;
        }
        return _delete_item_timer;
    }

    bool sensingSpeedItem( ) {
        //一番最初の位置と、現在の位置を引いて、2.0f以上ならtrue;
        if( _move_delete_item != null ) {
            Vector3 pos = _move_delete_item.transform.position;
            float vec = Vector3.Distance( _first_item_pos, pos );
            if( vec > DELETE_RANGE ) {
                return true;
            }
        }
        return false;
    }

	void removeItem( GameObject item ) {
        if( item == null ) {
            return;
        }
        Destroy( item );
    }
}
