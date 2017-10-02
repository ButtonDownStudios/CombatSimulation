using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CombatController : MonoSingleton<CombatController> {
	public List<UnitController> redTeamList;
	public List<UnitController> blueTeamList;
	public Dictionary<string, List<UnitController>> teams;
	public Dictionary<string, SpawnerController> spawners;

	void Awake () {
		CheckIsSingleInScene ();
		teams = new Dictionary<string, List<UnitController>> ();
		foreach(string teamName in Enum.GetNames(typeof(Team))){
			List<UnitController> l = new List<UnitController> ();
			teams.Add( teamName,l);
		}
		spawners = new Dictionary<string, SpawnerController> ();
	}
	
	public void AddUnitToTeam(UnitController unit){
		teams [unit.team.ToString()].Add (unit);
		unit.OnDeath += OnUnitDeath;
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

	void OnUnitDeath(UnitController unitController){
		string unitTeam = unitController.team.ToString();
		teams [unitTeam].Remove (unitController);
		if (spawners.ContainsKey (unitTeam))
			spawners [unitTeam].AddUnitToSpawn ();
	}

	void OnUnitSpawed(UnitController unitController){
		string unitTeam = unitController.team.ToString();
		teams [unitTeam].Add (unitController);
	}

	public void AddSpawner(string team, SpawnerController spawnerController){
		spawners.Add (team, spawnerController);
		spawnerController.OnUnitSpawned += OnUnitSpawed;
	}
}
