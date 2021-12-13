using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_item : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 pos;
    Transform item;
    void Start( ) {
    }

    // Update is called once per frame
    void Update( ) {
        transform.Rotate( new Vector3( 0.3f, 0.3f, 0f ) );
    }

    void removeItem( ) {
        item = this.transform;
        pos = item.position;
        GameObject player = GameObject.FindGameObjectWithTag(  "Player" );

        float now_pos_x = pos.x;
        float coll_x = player.transform.position.x - now_pos_x;
        if (coll_x < 0)
        {
            coll_x *= -1;
            if (coll_x <= 200)
            {
                Destroy( item ); //itemを消したときにエラーが出る。
            }
        }
    }
}
