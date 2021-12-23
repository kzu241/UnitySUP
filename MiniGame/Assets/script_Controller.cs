using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script_Controller : MonoBehaviour {
    GameObject player_game_object;
    Vector3 player_position = new Vector3(1.0f, 10.0f, 0.0f);
    Quaternion player_rotation = Quaternion.Euler(0f, 0f, 0f);
    float speed = 0f;
    void Start( ) {
        player_game_object = GameObject.Find("/Prefab/prefab_player");
        Instantiate( player_game_object, player_position, player_rotation );
    }
    void Update( ) {
        move( );
    }
    void move()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.speed = 0.05f;
            transform.Translate(0, 0, speed, Space.World);
            animationForwardMove();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.speed = -0.05f;
            transform.Translate(0, 0, speed, Space.World);
            animationBackwardMove();
        }
        if (Input.GetKey(KeyCode.LeftArrow) )
        {
            this.speed = -0.05f;
            transform.Translate(speed, 0, 0, Space.World);
            animationLeftMove();
        }
        if (Input.GetKey(KeyCode.RightArrow) )
        {
            this.speed = 0.05f;
            transform.Translate(speed, 0, 0, Space.World);
            animationRightMove();
        }
    }
    void animationForwardMove()
    {
        Quaternion z = Quaternion.AngleAxis(300.0f * Time.deltaTime, Vector3.right);
        Quaternion forward_move = z * this.transform.rotation;
        transform.rotation = forward_move;
    }
    void animationBackwardMove()
    {
        Quaternion z = Quaternion.AngleAxis(-300.0f * Time.deltaTime, Vector3.right);
        Quaternion backward_move = z * this.transform.rotation;
        transform.rotation = backward_move;
    }
    void animationLeftMove()
    {
        Quaternion x = Quaternion.AngleAxis(300.0f * Time.deltaTime, Vector3.forward);
        Quaternion left_move = x * this.transform.rotation;
        transform.rotation = left_move;
    }
    void animationRightMove()
    {
        Quaternion x = Quaternion.AngleAxis(-300.0f * Time.deltaTime, Vector3.forward);
        Quaternion right_move = x * this.transform.rotation;
        transform.rotation = right_move;
    }
}
