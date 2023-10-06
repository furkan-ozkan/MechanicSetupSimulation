using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Data" ,menuName = "Sanlab/Create/Data")]
public class DataSO : SerializedScriptableObject
{
    public Dictionary<string,bool> steps = new Dictionary<string,bool>();
}
