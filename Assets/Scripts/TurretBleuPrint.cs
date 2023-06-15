using UnityEngine;

[System.Serializable]
public class gameTurretBluePrint
{
    public GameObject prefab;
    public int cost;
    public GameObject upgradedprefab;
    public int upgradeCost;
    public int index;

    public gameTurretBluePrint(GameObject _prefab, int _cost, int _upgradeCost, int _index)
    {
        prefab = _prefab;
        cost = _cost;
        upgradedprefab = null;
        upgradeCost = _upgradeCost;
        index = _index;
    }

    public gameTurretBluePrint(GameObject _prefab, int _cost, GameObject _upgradedprefab, int _upgradeCost, int _index)
    {
        prefab = _prefab;
        cost = _cost;
        upgradedprefab = _upgradedprefab;
        upgradeCost = _upgradeCost;
        index = _index;
    }
}