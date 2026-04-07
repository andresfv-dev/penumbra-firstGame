using UnityEngine;
using UnityEngine.Pool;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    private IObjectPool<GameObject> pool;

    void Awake()
    {
        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(projectilePrefab),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    public GameObject GetProjectile() => pool.Get();
    public void ReleaseProjectile(GameObject obj) => pool.Release(obj);
}