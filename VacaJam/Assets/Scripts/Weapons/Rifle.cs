using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Rifle : Weapon
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, LookAngle);
    }

    protected sealed override void Shoot(OptionalNonSerializable<GameObject> owner)
    {
        ShootParticle.Play();
        AudioManager._I.PlaySound2D("Rifle-Shoot");
        CinemachineShake.ShakeCamera(1.2f, .1f);

        Projectile projectile = Instantiate(
                original: WeaponInfo.BulletPrefab,
                position: ShootTransform.position,
                rotation: Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, LookAngle + Random.Range(-Spread, Spread))
            ).GetComponent<Projectile>();

        projectile.Damage = Damage;
        projectile.Owner = owner;

        if (BulletSpeed.Enabled) {
            projectile.Speed = BulletSpeed.Value;
        }

        if (Recoil.Enabled) {
            owner.Value.GetComponent<IShooteable>().ShootFeedback(Recoil.Value, LookAngle);
        }

        projectile.gameObject.SetActive(true);
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
