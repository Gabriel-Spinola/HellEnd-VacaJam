using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected LayerMask WhatIsBlock;

    [SerializeField] private float _lifeTime = 3f;
    [SerializeField] public float Speed;

    [HideInInspector] public OptionalNonSerializable<GameObject> Owner;

    [HideInInspector] public float Damage;

    protected virtual IEnumerator DestroyProjectileOnTime()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}