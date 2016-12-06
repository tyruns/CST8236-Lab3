using UnityEngine;
using System.Collections;

public class Explode : MonoBehaviour 
{
    //These two should be declared
	private const float force = 150.0f;
	private const float radius = 500.0f;
    //start with null, can be checked after loading to see
    //if still null to determine if the load was okay
	private GameObject destroyed = null;

	void Start ()
	{

		destroyed = Resources.Load ("Prefabs/Broken") as GameObject;
		if (destroyed == null)
			throw new UnityException ("No destroyed prefab found");
	}

	void OnCollisionEnter (Collision collision)
	{
        //initialize
		var collidingBall = collision.gameObject;

		//found online - easy way to make sure two walls wont make eachother explode
		if (collidingBall.name != "Ball")
			return;
		
		var collidingWall = collidingBall.GetComponent <Rigidbody> ();
        //make sure it worked
		if (collidingWall == null)
			return;

        //Struggled a bit with spawning a new destroyed version of the wall
        //Currently it spawns the new wall, but the positioning is off
		Vector3 wallPosition = gameObject.transform.position;
		Destroy (gameObject);
		var destroyedWall = Instantiate (destroyed, wallPosition, Quaternion.identity) as GameObject;
		destroyedWall.transform.position = wallPosition; //I dont think this does what I want it to
		collidingWall.AddExplosionForce (force, collidingBall.transform.position, radius);

        //particle emitter code - should shoot out red particles
		var emitter = collidingBall.GetComponent <ParticleSystem> ();
		if (emitter == null)
			return;
		emitter.Play ();
        
        //Audio code - should play an explosion sound found online
		var audio = collidingBall.GetComponent <AudioSource> ();
		if (audio == null)
			return;
		audio.Play ();
	}
}
