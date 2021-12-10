using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_Player : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 player_pos = new Vector3( 0f, 0f, 0f );
    void Start( ) {
    }

    // Update is called once per frame
    void Update( ) {
        player_pos = GameObject.Find("Player/player").transform.position;
        //GameObject.Find("Player/player").transform.position = new Vector3( player.x, player.y, player.z);
        if ( player_pos.y <= 0 ){
            transform.Translate( 0f, 0f, 0f );
        }else{
            transform.Translate( 0.0f, -0.2f, 0.0f );
        }
        move( );
    }
        
    void move( ){
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0f, 0f, 0.1f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0.0f, 0f, -0.1f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-0.1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.1f, 0f, 0f);
        }
    }
}
