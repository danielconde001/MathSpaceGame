using UnityEngine;

public class SpaceshipAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float range = 25f;
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private float fireRate = .12f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask cursorRayMask;

    private float fireCooldown = 0;

    private void Update()
    {
        if (fireCooldown >= 0f) fireCooldown -= Time.deltaTime;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 aimPoint;

        RaycastHit hit;

        // if ray hits something
        if (Physics.Raycast(ray, out hit, range, cursorRayMask))
        {
            aimPoint = hit.point;
        }

        // if ray hits nothing
        else
        {
            aimPoint = ray.GetPoint(range);
        }

        Vector3 projectileDir = (aimPoint - transform.position).normalized;

        ProjectileBehaviour projectile = Instantiate(projectilePrefab, bulletSpawn.position, Quaternion.identity);
        projectile.moveDir = projectileDir;

        fireCooldown = fireRate;
    }

    public int GetDamage()
    {
        return damage;
    }
}
