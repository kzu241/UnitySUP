using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obj_createItem : MonoBehaviour {
    public GameObject item;
    Vector3 item_position = new Vector3( 5.0f, 1.0f, 5f );

    Quaternion rotation = Quaternion.Euler( 0, 0, 45 );
    void Start( ) {
        item = ( GameObject )Resources.Load( "Item/item" );
        Instantiate( item, item_position, rotation );
    }
}
