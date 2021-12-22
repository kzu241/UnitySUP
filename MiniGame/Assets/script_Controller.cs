using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Controller : MonoBehaviour {
    GameObject player_game_object;
    Vector3 player_position = new Vector3(1.0f, 10.0f, 0.0f);
    Quaternion player_rotation = Quaternion.Euler(0f, 0f, 0f);
    void Start( ) {
        //player_game_object = GameObject.Find("Prefab/prefab_player");
        //Instantiate( player_game_object, player_position, player_rotation );
    }
    void Update( ) {
        
    }
}
