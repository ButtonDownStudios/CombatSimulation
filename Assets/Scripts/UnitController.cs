using UnityEngine;
using UnityEngine.AI;

public enum Team {Red, Blue};

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class UnitController : MonoBehaviour {
	public Team team;
	public float agroRange = 12f;
	public float hitRange = 2f;
	public int maxHP = 3;
	public Transform finish;
	public Projector agroRangeProjector;
	public Projector hitRangeProjector;
	public UnitController enemy;
	public HPBarController hpBarController;

	public bool isFighting = false;
	public bool isAlive = true;

	Animator animator;
	NavMeshAgent navMeshAgent;

	void Start () {
		CombatController.Instance.AddUnitToTeam (this);
		animator = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		agroRangeProjector.orthographicSize = agroRange;
		hitRangeProjector.orthographicSize = hitRange;
		hpBarController.InitHP (maxHP);
	}

	void Update(){
		if (isFighting == false) {
			enemy = CombatController.Instance.FindClosestEnemyInRange (this, agroRange);
			if (enemy == null) {
				animator.SetBool ("enemyInHitRange", false);
				animator.SetBool ("enemyInAgroRange", false);
			} else {
				//Debug.DrawLine (new Vector3 (transform.position.x, 1, transform.position.z),
				//	new Vector3 (enemy.transform.position.x, 1, enemy.transform.position.z), Color.red);
				animator.SetBool ("enemyInAgroRange", true);
				float distance = Vector3.Distance (transform.position, enemy.transform.position);
				animator.SetBool ("enemyInHitRange", distance <= hitRange);
				isFighting = distance <= hitRange;
			}
		}
	}

	public void SetMovementTarget(Transform movementTarget){
		navMeshAgent.destination = movementTarget.position;
	}

	public void StopMovement(){
		navMeshAgent.destination = transform.position;
		navMeshAgent.speed = 0;
		navMeshAgent.angularSpeed = 0;
	}
}
