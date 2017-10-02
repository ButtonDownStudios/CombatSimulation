using UnityEngine;
using System;
using UnityEngine.AI;

public enum Team {Red, Blue, Yellow, Black};

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BodyController))]
public class UnitController : MonoBehaviour {

	public UnitCharacteristics unitCharacteristics;
	public Transform finish;
	public Projector agroRangeProjector;
	public Projector hitRangeProjector;
	public UnitController enemy;
	public HPBarController hpBarController;
	public GameObject flag;
	public Transform spawner;

	public bool isFighting = false;
	public bool isAlive = true;
	public bool isCarryingFlag = false;

	public event Action<UnitController> OnDeath;


	Animator animator;
	NavMeshAgent navMeshAgent;
	BodyController bodyController;

	[HideInInspector]
	public Team team;
	float agroRange;
	float hitRange;
	int maxHP;
	int damage;
	float movingSpeed;

	void Start () {
		bodyController = GetComponent<BodyController> ();
		CombatController.Instance.AddUnitToTeam (this);
		animator = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		OnDeath += bodyController.Death;
		OnDeath += hpBarController.Death;
		OnDeath += Death;
		InitUnit ();
	}

	public void InitUnit(){
		team = unitCharacteristics.team;
		agroRange = unitCharacteristics.agroRange;
		hitRange = unitCharacteristics.hitRange;
		maxHP = unitCharacteristics.maxHP;
		damage = unitCharacteristics.damage;
		movingSpeed = unitCharacteristics.movingSpeed;
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
				animator.SetBool ("enemyInAgroRange", true);
				float distance = Vector3.Distance (transform.position, enemy.transform.position);
				animator.SetBool ("enemyInHitRange", distance <= hitRange);
				isFighting = distance <= hitRange;
			}
		}
	}

	public void SetMovementTarget(Transform movementTarget){
		if (navMeshAgent == null)
			return;
		navMeshAgent.speed = movingSpeed;
		navMeshAgent.destination = movementTarget.position;
	}

	public void StopMovement(){
		navMeshAgent.destination = transform.position;
		navMeshAgent.speed = 0;
		//navMeshAgent.angularSpeed = 0;
	}

	public void AttackEnemy(){
		//Debug.Log (gameObject.name + " :: attack");
		if (isAlive == false)
			return;
		enemy.TakeDamage (damage);
		isFighting = enemy.isAlive;
	}

	public void TakeDamage(int damageTaken){
		if (isAlive == false)
			return;
		hpBarController.currentHP -= damageTaken;
		if (hpBarController.currentHP == 0){
			if (OnDeath != null)
				OnDeath (this);
		}	
	}

	void Death(UnitController t){
		isAlive = false;
		navMeshAgent.enabled = false;
		Invoke ("DestroyUnit", 2);
	}

	void DestroyUnit(){
		Destroy (gameObject);
	}
}
