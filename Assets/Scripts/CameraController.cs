using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject ball;

    private float xOffset; 
    private float yOffset;
    private float zOffset;

	void Start () {
        xOffset = this.transform.position.x - ball.transform.position.x;
        yOffset = this.transform.position.y - ball.transform.position.y;
        zOffset = this.transform.position.z - ball.transform.position.z;
	}
	
	void Update () {
	    this.transform.position = new Vector3(ball.transform.position.x + xOffset,
                                              ball.transform.position.y + yOffset,
                                              ball.transform.position.z + zOffset);
	}
}
