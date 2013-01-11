using UnityEngine;
using System.Collections;

public class RotateAroundScript : MonoBehaviour {

	public Vector3 RotateDirection = Vector3.left+Vector3.up;
	public GameObject Target;
	public int Speed = 150;

	// Update is called once per frame
	void Update () {
   	 	transform.RotateAround (Target.transform.position, RotateDirection*Random.Range(1,Speed), Speed * Time.deltaTime);
	}
}
