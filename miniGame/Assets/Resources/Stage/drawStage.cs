using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawStage : MonoBehaviour
{
	// Start is called before the first frame update
	Vector3 stage_pos = new Vector3( -15.0f, 1.0f, 0.0f);
	GameObject stage;
	//Start is called before the first frame update
	void Start( ) {
		stage = (GameObject)Resources.Load("Stage/stage");
		Instantiate(stage, stage_pos, Quaternion.identity);

	}

	// Update is called once per frame
	void Update( ) {
        
    }
}
