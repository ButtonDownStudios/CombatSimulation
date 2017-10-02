using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {

	[SerializeField]
	float spawnCD = 2f;
	[SerializeField]
	float spawnTime = 0;

	public UnitCharacteristics unitCharacteristics;
	public GameObject unitPrefab;
	public Transform finish;
	public Transform teamGroup;
	public Material teamColorMaterial;
	public GameObject flag;
	public Transform bodyPartsTransform;

	int numUnitsToSpawn = 0;

	public event Action<UnitController> OnUnitSpawned;

	void Start(){
		CombatController.Instance.AddSpawner (unitCharacteristics.team.ToString (), this);
	}

	void Update(){
		spawnTime += Time.deltaTime;
		if(numUnitsToSpawn !=0 && spawnTime>= spawnCD){
			SpawnUnit ();
			spawnTime = 0;
		}
	}

	void SpawnUnit(){
		GameObject newUnit = Instantiate (unitPrefab, teamGroup);
		UnitController unitController = newUnit.GetComponent<UnitController> ();
		unitController.unitCharacteristics = unitCharacteristics;
		unitController.InitUnit ();
		BodyController bodyController = newUnit.GetComponent<BodyController> ();
		bodyController.SetBodyMaterial (teamColorMaterial);
		newUnit.transform.position = transform.position;
		if (finish == null)
			Debug.LogError (gameObject.name + " finish is null.");
		else {
			unitController.finish = finish;		
			//unitController.SetMovementTarget (finish);
		}
		if (flag == null)
			Debug.LogError (gameObject.name + " flag is null.");
		else {
			unitController.flag = flag;
		}
		if (bodyPartsTransform == null)
			Debug.LogError (gameObject.name + " bodyPartsTransform is null.");
		else {
			bodyController.bodyPartsTransform = bodyPartsTransform;
		}
		unitController.spawner = transform;
		numUnitsToSpawn--;
		if (OnUnitSpawned != null)
			OnUnitSpawned (unitController);
	}

	public void AddUnitToSpawn(){
		numUnitsToSpawn++;
	}
}
