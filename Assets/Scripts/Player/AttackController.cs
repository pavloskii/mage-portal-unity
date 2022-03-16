using System;
using System.Collections;
using UnityEngine;

public class AttackController : ParticleSystemPool
{
    [Obsolete]
    public GameObject target;

    public void Fire()
    {
        var fireBall = Pool.Get();
        //fireBall.transform.position = transform.position;
        //fireBall.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 180, 90));
        fireBall.Play();
        StartCoroutine(RemoveProjectileAfterThreeSeconds(fireBall));
    }

    IEnumerator RemoveProjectileAfterThreeSeconds(ParticleSystem projectile)
    {
        yield return new WaitForSeconds(3f);

        projectile.Stop();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
