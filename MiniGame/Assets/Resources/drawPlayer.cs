using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    void Start( ) {
       player  = (GameObject)Resources.Load( "player" );
        GameObject instance = (GameObject)Instantiate( player, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity );
    }

    // Update is called once per frame
    void Update( ){
        
    }
}
