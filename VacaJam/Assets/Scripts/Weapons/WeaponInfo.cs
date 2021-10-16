using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponInfo : ScriptableObject
{
    [Header("Weapon Info")]
    public GameObject BulletPrefab;

    public int Ammo;
    public float Damage;
    public float FireRate;
}
