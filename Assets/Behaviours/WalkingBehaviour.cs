using UnityEngine;

public class WalkingBehaviour : StateMachineBehaviour {
	UnitController unitController;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//Debug.Log (animator.gameObject.name+" started walking.");
		unitController = animator.GetComponent<UnitController> ();
		if (unitController.finish != null)
			unitController.SetMovementTarget (unitController.finish);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (unitController.isCarryingFlag)
			unitController.SetMovementTarget (unitController.spawner);
		else
			unitController.SetMovementTarget (unitController.flag.transform);
	}

	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//	unitController.StopMovement ();
	//}
}
