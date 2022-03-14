using System;
using System.Collections;
using UnityEngine;

public class ProjectileSpawner : ProjectilePool
{
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private float bulletSpeed = 200f;

    public void SpawnProjectile()
    {
        var projectile = ProjectilesPool.Get();
        projectile.Shoot(bulletSpeed);

        StartCoroutine(RemoveProjectileAfterThreeSeconds(projectile));
    }

    //TODO also add the relase from pool on collision
    IEnumerator RemoveProjectileAfterThreeSeconds(Projectile projectile)
    {
            yield return new WaitForSeconds(3f);

            ProjectilesPool.Release(projectile);
    }
}
