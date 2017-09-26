using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoSingleton<CombatController> {
	public List<UnitController> redTeamList;
	public List<UnitController> blueTeamList;

	void Start () {
		CheckIsSingleInScene ();
	}
	
	public void AddUnitToTeam(UnitController unit){
		if (unit.team == Team.Red)
			redTeamList.Add (unit);
		else
			blueTeamList.Add (unit);
	}

	public UnitController FindClosestEnemyInRange(UnitController unit, float range){
		List<UnitController> enemyList;
		enemyList = unit.team == Team.Red ? blueTeamList : redTeamList;
		float closestRange = Mathf.Infinity;
		float distance;
		UnitController target = null;
		foreach(var enemy in enemyList){
			distance = Vector3.Distance (enemy.transform.position, unit.transform.position);
			if(distance < range && distance < closestRange){
				target = enemy;
				closestRange = distance;
			}
		}
		//Debug.Log (closestRange < range ? target : null);
		return closestRange < range ? target : null;
	}
}
