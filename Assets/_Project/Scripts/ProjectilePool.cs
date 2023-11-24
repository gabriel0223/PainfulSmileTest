using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private CannonProjectile _projectilePrefab;

    private ObjectPool<CannonProjectile> _projectilePool;

    // Start is called before the first frame update
    void Start()
    {
        _projectilePool = new ObjectPool<CannonProjectile>(() => Instantiate(_projectilePrefab), 
            projectile =>
            {
                projectile.gameObject.SetActive(true);
            }, projectile =>
            {
                projectile.gameObject.SetActive(false);
            }, projectile =>
            {
                Destroy(projectile.gameObject);
            }, false, 30, 70);
    }

    public CannonProjectile GetProjectile()
    {
        return _projectilePool.Get();
    }

    public void ReturnProjectile(CannonProjectile projectile)
    {
        _projectilePool.Release(projectile);
    }
}
