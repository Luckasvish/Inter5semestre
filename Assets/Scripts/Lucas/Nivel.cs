using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nivel_", menuName = "Create New Nivel")]
public class Nivel : ScriptableObject
{
    public WaveConfig[] waves;
    public int[] countdownBetweenWaves;
}
