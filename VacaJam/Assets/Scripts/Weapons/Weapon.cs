using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon References")]
    [SerializeField] protected Transform ShootTransform;
    [SerializeField] protected WeaponInfo WeaponInfo;

    [SerializeField] protected float BulletSpeed;

    [HideInInspector] public float LookAngle = 0f;

    protected int CurrentAmmo = 0;

    protected float _nextShoot = 0f;

    protected bool IsReloading = false;

    protected virtual void Start()
    {
        IsReloading = false;

        CurrentAmmo = WeaponInfo.Ammo;
    }

    // protected abstract void Shoot(OptionalNonSerializeble<GameObject> owner);
    // protected abstract void Shoot(OptionalNonSerializeble<GameObject> owner);

    protected abstract IEnumerator Reload(float time);
}