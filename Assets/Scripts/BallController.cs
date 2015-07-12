using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public int jumpHeight;
    public float speed;

    private bool isTouchingPlatform;
    private Rigidbody rigidBody;
    private AudioSource jumpSound;
    private float jumpTime;
    private float timeSinceLastJump;

	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        jumpSound = GetComponent<AudioSource>();
        isTouchingPlatform = true;
        jumpTime = Time.time;
        renderSkin();
	}

	void Update () {
        float moveHorizontal = Input.acceleration.x;
        timeSinceLastJump = Time.time - jumpTime;

        if (canJump())
        {
            rigidBody.AddForce(new Vector3(0, jumpHeight, 0));
            if (PlayerPrefs.GetInt("muted") == 0) { 
                jumpSound.Play();
            }

            isTouchingPlatform = false;
            jumpTime = Time.time;
        }

        rigidBody.AddForce(new Vector3(0, 0, moveHorizontal * speed));
    }

    private bool canJump()
    {
        return Input.touchCount == 1 && isTouchingPlatform && timeSinceLastJump > 1;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Platform") 
        {
            isTouchingPlatform = true;
        }
    }

    private void renderSkin()
    {
        string skin = PlayerPrefs.GetString("skin");
        Material material = Resources.Load(skin, typeof(Material)) as Material;
        GetComponent<Renderer>().material = material;
    }
	
}
