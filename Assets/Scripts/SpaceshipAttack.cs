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
    [SerializeField] private float minigameFireRate = .72f;
    [SerializeField] private ProjectileBehaviour minigameProjectilePrefab;
    private float minigameFireCooldown = 0;

    private void Update()
    {
        NormalShootingLogic();
        MinigameShootingLogic();
    }

    private void NormalShootingLogic()
    {
        if (MinigameManager.Instance.State != MinigameManager.MinigameState.None) return;

        if (fireCooldown >= 0f) fireCooldown -= Time.deltaTime;
        
        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            NormalShoot();
        }
    }

    private void MinigameShootingLogic()
    {
        if (MinigameManager.Instance.State != MinigameManager.MinigameState.StationaryShooting) return;

        if (minigameFireCooldown >= 0f) minigameFireCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && minigameFireCooldown <= 0f)
        {
            MinigameShoot();
        }
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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        AsteroidScript asteroid;

        RaycastHit hit;

        // if ray hits something
        if (Physics.Raycast(ray, out hit, range))
        {
            if (hit.collider.TryGetComponent<AsteroidScript>(out asteroid))
            {
                Vector3 projectileDir = (asteroid.transform.position - transform.position).normalized;

                ProjectileBehaviour projectile = Instantiate(minigameProjectilePrefab, bulletSpawn.position, Quaternion.identity);
                projectile.moveDir = projectileDir;

                minigameFireCooldown = minigameFireRate;
            }
        }
    }

    public int GetDamage()
    {
        return damage;
    }
}
