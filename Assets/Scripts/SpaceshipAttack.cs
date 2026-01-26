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

    [Header("Minigame Settings")]
    [SerializeField] private float minigamFireRate = .72f;
    [SerializeField] private ProjectileBehaviour minigameProjectilePrefab;
    private float minigameFireCooldown = 0;

    private void Update()
    {
        if (fireCooldown >= 0f) fireCooldown -= Time.deltaTime;

        NormalShootingLogic();
        MinigameShootingLogic();
    }

    private void NormalShootingLogic()
    {
        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            NormalShoot();
        }
    }

    private void MinigameShootingLogic()
    {
        
    }

    private void NormalShoot()
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

    private void MinigameShoot()
    {
        
    }

    public int GetDamage()
    {
        return damage;
    }
}
