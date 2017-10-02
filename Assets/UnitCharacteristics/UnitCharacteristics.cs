using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCharacteristics : ScriptableObject {
	public Team team;
	public float agroRange = 12f;
	public float hitRange = 2f;
	public int maxHP = 3;
	public int damage = 1;
	public float movingSpeed = 5;
}
