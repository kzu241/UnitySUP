using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_createPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public Vector3 clone_position = new Vector3( 1.0f, 30.0f, 0.0f );
    Quaternion rotation = Quaternion.Euler( 0, 0, 0 );
    void Start( ) {
       player = (GameObject)Resources.Load( "Player/player" );
        Instantiate( player, clone_position, rotation );
    }
}
