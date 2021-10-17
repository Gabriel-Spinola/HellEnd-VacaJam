using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class Shotgun : Weapon
{
    [SerializeField] private int _pelleteCount;

    private List<Quaternion> _pelleteRotations;

    private void Awake()
    {
        _pelleteRotations = new List<Quaternion>(_pelleteCount);

        for (int i = 0; i < _pelleteCount; i++) {
            _pelleteRotations.Add(Quaternion.Euler(Vector3.zero));
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, LookAngle);
    }

    protected sealed override void Shoot(OptionalNonSerializable<GameObject> owner)
    {
        ShootParticle.Play();

        for (int i = 0; i < _pelleteCount; i++) {
            _pelleteRotations[i] = Random.rotation;

            Projectile projectile = Instantiate(
                    original: WeaponInfo.BulletPrefab,
                    position: ShootTransform.position,
                    rotation: ShootTransform.rotation
                ).GetComponent<Projectile>();

            projectile.transform.rotation = Quaternion.RotateTowards(projectile.transform.rotation, _pelleteRotations[i], Spread);
            projectile.Damage = WeaponInfo.Damage;
            projectile.Owner = owner;

            if (BulletSpeed.Enabled) {
                projectile.Speed = BulletSpeed.Value;
            }

            if (Recoil.Enabled) {
                owner.Value.GetComponent<IShooteable>().ShootFeedback(Recoil.Value, LookAngle);
            }

            projectile.gameObject.SetActive(true);
        }
    }

    public sealed override void UseWeapon(PInputAction.PlayerActions inputAction, OptionalNonSerializable<GameObject> owner)
    {
        if (inputAction.ShootHold.ReadValue<float>() > 0f) {
            if (Time.time >= _nextShoot) {
                Shoot(owner);

                _nextShoot = Time.time + 1f / WeaponInfo.FireRate;
            }
        }
    }

    public sealed override void UseWeapon(OptionalNonSerializable<GameObject> owner)
    {
        if (Time.time >= _nextShoot) {
            Shoot(owner);

            _nextShoot = Time.time + 1f / WeaponInfo.FireRate;
        }
    }

    // # Not Implemented yet
    protected sealed override IEnumerator Reload(float time) => null;
}
