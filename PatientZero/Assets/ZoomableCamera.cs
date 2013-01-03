using UnityEngine;
using System.Collections;

public class ZoomableCamera : MonoBehaviour {
	
	public int MaxZoom = 10;
	public int MinZoom = 1;
	public float ZoomAmount = .5f;
	
	// Update is called once per frame
	void Update () {
	if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize+ZoomAmount, MinZoom, MaxZoom);

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize-ZoomAmount, MinZoom, MaxZoom);
        }
	}
}
