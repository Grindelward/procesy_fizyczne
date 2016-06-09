using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

    public Rigidbody rb;
    public float speed;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () { // before rendering a frame
	    
	}

    void FixedUpdate() // before physics code
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        movement = Random.insideUnitSphere * speed;
        movement.y = 0.0f;

        rb.AddForce(movement);
    }
}
