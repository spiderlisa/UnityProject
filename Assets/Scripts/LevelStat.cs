﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelStat {
	public bool hasCrystals = false;
	public bool hasAllFruits = false;
	public bool levelPassed = false;
	public List<int> collectedFruits = new List<int>();
	
	public static LevelStat ReadInfo(string level) {
		string code = PlayerPrefs.GetString(level, null);
		return JsonUtility.FromJson<LevelStat>(code);
	}
}
