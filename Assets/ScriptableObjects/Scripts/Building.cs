using System.Collections.Generic;
using UnityEngine;

public enum EResourceType// 나무, 물, 돌, 고기 
{
    Water = 104,
    Rock = 301,
    Log = 302,
    Meat = 303,
}

[CreateAssetMenu(fileName = "Default Building", menuName = "Building/Default Building")]
public class Building : ScriptableObject
{
    [Header("DefaultBuildingInfo")]
    public string buildingName;
    public string buildingDescription;
    public Dictionary<int, int> requiredResources;
    public GameObject buildingPrefab;
}
public class AttackableBuilding : Building
{
    public float maxHp;
    public float curHp;
    public float attackDamage;
    public GameObject projectilePrefab;
    public int level;
    public float damageIncreasePerLevel;
}