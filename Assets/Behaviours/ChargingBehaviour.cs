using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingBehaviour : StateMachineBehaviour {
	UnitController unitController;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log (animator.gameObject.name+" started charging.");
		unitController = animator.GetComponent<UnitController> ();
		unitController.SetMovementTarget (unitController.enemy.transform);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//	unitController.StopMovement ();
	//}
}
