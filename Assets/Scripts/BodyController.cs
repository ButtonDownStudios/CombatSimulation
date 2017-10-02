using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
	public Transform bodyPartsTransform;
	public GameObject head;
	public GameObject body;
	public GameObject leftArm;
	public GameObject rightArm;
	List<GameObject> bodyParts;

	void Start(){
		bodyParts = new List<GameObject> ();
		bodyParts.Add (head);
		bodyParts.Add (body);
		bodyParts.Add (leftArm);
		bodyParts.Add (rightArm);

	}

	public void Death(UnitController uC){
		foreach (GameObject bodyPart in bodyParts) {
			Rigidbody part = bodyPart.AddComponent<Rigidbody> ();
			part.transform.parent = bodyPartsTransform;
			part.useGravity = true;
			part.isKinematic = false;
		}
		//Rigidbody bodyRigid = body.GetComponent<Rigidbody>();
		//bodyRigid.AddExplosionForce (0.5f, new Vector3 (0, 0, 0.1f) + body.transform.position, 0.2f);
		
	}

	public void SetBodyMaterial(Material material){
		body.GetComponent<Renderer> ().material = material;
	}
}
