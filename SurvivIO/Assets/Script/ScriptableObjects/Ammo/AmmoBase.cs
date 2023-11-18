using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Ammo")]
public class AmmoBase : ScriptableObject
{
    public GameObject _prefab;
    public string _name;
    public int _minReplenish;
    public int _maxReplenish;
}
