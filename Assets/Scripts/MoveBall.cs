using UnityEngine;
using System.Collections;

public class MoveBall : MonoBehaviour 
{
	private Rigidbody ball = null;

	private const float force = 5.0f;

	void Start ()
	{
		ball = GetComponent <Rigidbody> ();
	}

	void Update () 
	{
        //create a vector to be modified by each key pressed
		Vector3 forceAndDirection = new Vector3 ();

		if (Input.GetKeyDown (KeyCode.W))
			forceAndDirection.Set (-force, 0.0f, 0.0f);
		
		else if (Input.GetKeyDown (KeyCode.A))
			forceAndDirection.Set (0.0f, 0.0f, -force);
		
		else if (Input.GetKeyDown (KeyCode.S))
			forceAndDirection.Set (force, 0.0f, 0.0f);
		
		else if (Input.GetKeyDown (KeyCode.D))
			forceAndDirection.Set (0.0f, 0.0f, force);
		
		else
			return;
		//apply force to the ball
		ball.AddForce (forceAndDirection, ForceMode.Impulse);
	}
}
