using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour {
	void OnTriggerEnter(Collider collider){
		UnitController cc = collider.GetComponent<UnitController> ();
		if (cc != null)
			Debug.Log (collider.gameObject.name);
	}
}
