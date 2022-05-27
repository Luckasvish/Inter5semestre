using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave_", menuName = "Create New Wave")]
public class WaveConfig : ScriptableObject
{
    [Header("2 for Angry")]
    [Header("1 for Impatient")] 
    [Header("0 for Calm")]
    public int[] enemyToSpawn;
}
