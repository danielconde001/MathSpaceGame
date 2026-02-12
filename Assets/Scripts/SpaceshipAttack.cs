using System.Collections.Generic;
using UnityEngine;

public class SpaceshipAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private List<Transform> doubleBulletSpawns;
    [SerializeField] private float range = 1000f;
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private float fireRate = .12f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask cursorRayMask;
    private float fireCooldown = 0;

    [Header("Minigame Settings")]
    [SerializeField] private float minigameFireRate = .72f;
    [SerializeField] private ProjectileBehaviour minigameProjectilePrefab;
    private float minigameFireCooldown = 0;

    [Header("Debug")]
    [SerializeField] bool useDebug = false;

    private void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        switch (LevelManager.Instance.LevelState)
        {
            case 0:
                NormalShootingLogic();
                break;
            case 1:
                MinigameShootingLogic();
                break;
            default:
                NormalShootingLogic();
                Debug.LogWarning("No state exists for index: " + LevelManager.Instance.LevelState.ToString());
                break;
        }
    }

    private void NormalShootingLogic()
    {
        if (fireCooldown >= 0f) fireCooldown -= Time.deltaTime;
        
        if (Input.GetMouseButton(0) && fireCooldown <= 0f)
        {
            NormalShoot();
        }
    }

    private void MinigameShootingLogic()
    {
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
            if (useDebug) Debug.Log(hit.collider.name);
            aimPoint = hit.point;
        }

        // if ray hits nothing
        else
        {
            if (useDebug) Debug.Log("None");
            aimPoint = ray.GetPoint(range);
        }



        if (PowerUpManager.Instance.HasDoubleBullets() == true)
        {
            for (int i = 0; i < doubleBulletSpawns.Count; i++)
            {
                Vector3 projectileDir = (aimPoint - doubleBulletSpawns[i].position).normalized;
                ProjectileBehaviour projectile = Instantiate(projectilePrefab, doubleBulletSpawns[i].position, Quaternion.identity);
                projectile.moveDir = projectileDir;
            }
        }
        else
        {
            Vector3 projectileDir = (aimPoint - bulletSpawn.position).normalized;
            ProjectileBehaviour projectile = Instantiate(projectilePrefab, bulletSpawn.position, Quaternion.identity);
            projectile.moveDir = projectileDir;
        }

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

    public void SetFireRate(float p_newFireRate)
    {
        fireRate = p_newFireRate;
    }
}
