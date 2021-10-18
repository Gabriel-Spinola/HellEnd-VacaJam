using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponInfo : ScriptableObject
{
    [Header("Weapon Info")]
    public GameObject BulletPrefab;

    public int Ammo;
    public float FireRate;
}
