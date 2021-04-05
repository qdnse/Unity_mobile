using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletCollider : MonoBehaviour
{
    public Vector3 dir;
    public ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = gameObject.transform.position;
        p.x += dir.x;
        p.z += dir.z;
        gameObject.transform.position = p;
    }

    public void BulletDestroy()
    {
        Destroy(gameObject);
        ParticleSystem clone = Instantiate(particle, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyManager>().TakeDamage(PlayerManager.instance.Damage);
        }
        BulletDestroy();
    }
}
