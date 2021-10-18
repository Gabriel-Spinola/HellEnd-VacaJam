using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils;

public class Pistol : Weapon
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _spriteRenderer.flipY = LookAngle < 90 && LookAngle > -90 ? false : true;
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, LookAngle);
    }

    protected sealed override void Shoot(OptionalNonSerializable<GameObject> owner)
    {
        ShootParticle.Play();
        AudioManager._I.PlaySound2D("Pistol-Shoot");
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
        if (inputAction.Shoot.triggered) {
            Shoot(owner);
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
