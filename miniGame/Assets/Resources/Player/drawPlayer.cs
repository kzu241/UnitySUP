using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawPlayer : MonoBehaviour {
	Vector3 player_pos = new Vector3( 0.0f,  1.0f, 5.0f);
	GameObject player_circle;
	//Start is called before the first frame update
	void Start( ) {
		player_circle = (GameObject)Resources.Load("Player/circle");
		Instantiate(player_circle, player_pos, Quaternion.identity);

	}

	//Update is called once per frame
	void Update( ) {
		//move();
	}
	//float x = 0.0f;
	//float z = 0.0f;

	//void move( ){
	//	if (Input.GetKey(KeyCode.UpArrow))
	//	{
	//		z += 3.0f * Time.deltaTime;
	//	}
	//	if (Input.GetKey(KeyCode.DownArrow))
	//	{
	//		z -= 3.0f * Time.deltaTime;
	//	}
	//	if (Input.GetKey(KeyCode.RightArrow))
	//	{
	//		x += 3.0f * Time.deltaTime;
	//	}
	//	if (Input.GetKey(KeyCode.LeftArrow))
	//	{
	//		x -= 3.0f * Time.deltaTime;
	//	}
	//	transform.localPosition = new Vector3(x, 0, z);
	//}

}