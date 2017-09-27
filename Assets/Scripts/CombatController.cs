using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatController : MonoSingleton<CombatController> {
	public List<UnitController> redTeamList;
	public List<UnitController> blueTeamList;
	public Dictionary<string, List<UnitController>> teams;

	void Awake () {
		CheckIsSingleInScene ();
		teams = new Dictionary<string, List<UnitController>> ();
		foreach(string teamName in Enum.GetNames(typeof(Team))){
			List<UnitController> l = new List<UnitController> ();
			teams.Add( teamName,l);
		}
	}
	
	public void AddUnitToTeam(UnitController unit){
		teams [unit.team.ToString()].Add (unit);
	}

	public UnitController FindClosestEnemyInRange(UnitController unit, float range){
		List<UnitController> enemyList;
		enemyList = unit.team == Team.Red ? blueTeamList : redTeamList;
		float closestRange = Mathf.Infinity;
		float distance;
		UnitController target = null;
		foreach (string teamName in Enum.GetNames(typeof(Team))) {
			if(teamName != unit.team.ToString())
				foreach(var enemy in teams[teamName]){
					if(enemy.isAlive){
						distance = Vector3.Distance (enemy.transform.position, unit.transform.position);
						if (distance < range && distance < closestRange) {
							target = enemy;
							closestRange = distance;
						}
					}
				}
		}
		return closestRange < range ? target : null;
	}
}
