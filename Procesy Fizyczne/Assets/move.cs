using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class move : MonoBehaviour {

    public Rigidbody rb;
    public float speed;
    public float boost = 2;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () { // before rendering a frame
	    
	}

    void FixedUpdate() // before physics code
    {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;
        bool isBoosting = CrossPlatformInputManager.GetButton("Boost");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //movement = Random.insideUnitSphere * speed;
        //movement.y = 0.0f;

        if (isBoosting) rb.useGravity = false; else rb.useGravity = true;

        rb.AddForce(movement);
    }
}
