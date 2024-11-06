using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResourceQuantityPair
{
    public int resourceId;
    public int quantity;
}

[CreateAssetMenu(fileName = "Default Structure", menuName = "Structure/Default Structure")]
public class Structure : ScriptableObject
{
    [Header("Default Structure Info")]
    public int structureId;
    public string structureName;
    public string structureDescription;
    public GameObject structurePrefab;
    public float maxHp;
    public float curHp;
    public int level;
    public float hpIncreasePerLevel;

    [Header("Default Passive Info")]
    public bool hasPassive;
    public float passiveRepairValue;
    public float passiveRepairDelay;
    public float passiveIncreasePerLevel;

    [Header("Default Required Resources Info")]
    public List<ResourceQuantityPair> resourceQuantityPair;
    public Dictionary<int, int> cachedRequiredResources;
    public Dictionary<int, int> GetRequiredResources()
    {
        if (cachedRequiredResources == null)
        {
            cachedRequiredResources = new Dictionary<int, int>();
            foreach(ResourceQuantityPair pair in resourceQuantityPair)
            {
                cachedRequiredResources[pair.resourceId] = pair.quantity;
            }
        }
        return cachedRequiredResources;
    }
}
