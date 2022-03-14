using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //TODO ontrigger release into pool
    public void Shoot(float speed)
    {
        //TODO check this
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(Vector3.forward * speed);
    }
}
