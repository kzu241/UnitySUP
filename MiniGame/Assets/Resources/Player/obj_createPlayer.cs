using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_createPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    Vector3 clone_position = new Vector3( 1.0f, 10.0f, 0.0f );
    void Start( ) {
       player = (GameObject)Resources.Load( "Player/player" );
        Instantiate( player, clone_position, Quaternion.identity );
    }

    // Update is called once per frame
    void Update( ){
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(0f, 0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(0.0f, 0f, 1.0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(-1.0f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(1.0f, 0f, 0f);
        }
    }
}
