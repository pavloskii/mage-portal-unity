using System;
using System.Collections;
using UnityEngine;

public class ProjectileSpawner : ProjectilePool
{
    [SerializeField]
    private float fireRate = 3f;
    [SerializeField]
    private float bulletSpeed = 200f;
    public ParticleSystem fireball;

    private void Awake()
    {
        fireball.transform.position = transform.position;
    }

    public void SpawnProjectile()
    {
        fireball.Play();
        //var projectile = ProjectilesPool.Get();
        //projectile.Shoot(bulletSpeed);
        //Instantiate(fireball, transform.position, Quaternion.identity);
        //StartCoroutine(RemoveProjectileAfterThreeSeconds(projectile));
        StartCoroutine(RemoveProjectileAfterThreeSeconds(fireball));
    }
    IEnumerator RemoveProjectileAfterThreeSeconds(ParticleSystem projectile)
    {
        yield return new WaitForSeconds(3f);

        projectile.Stop();
    }

    //TODO also add the relase from pool on collision
    IEnumerator RemoveProjectileAfterThreeSeconds(Projectile projectile)
    {
            yield return new WaitForSeconds(3f);

            ProjectilesPool.Release(projectile);
    }
}
