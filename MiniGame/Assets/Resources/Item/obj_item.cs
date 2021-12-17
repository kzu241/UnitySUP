using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_item : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos = new Vector3( 5.0f, 0f, 0f );
    Vector3 item_pos;
    Transform item;
    // Update is called once per frame
    void Update( ) {
        Quaternion y = Quaternion.AngleAxis( 30.0f * Time.deltaTime, Vector3.up );
        Quaternion rotate_y = y * this.transform.rotation;
        transform.rotation = rotate_y;
    }
}
