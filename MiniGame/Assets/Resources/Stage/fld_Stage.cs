using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fld_Stage : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject stage;
    void Start()
    {
        stage = (GameObject)Resources.Load("Stage/stage");
        Instantiate(stage, new Vector3( -5.0f, 0.0f, -5.0f ), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
