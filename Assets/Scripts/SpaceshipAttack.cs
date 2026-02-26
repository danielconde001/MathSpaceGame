using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpaceshipAttack : MonoBehaviour
{
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private List<Transform> doubleBulletSpawns;
    [SerializeField] private float range = 1000f;
    [SerializeField] private ProjectileBehaviour projectilePrefab;
    [SerializeField] private ProjectileBehaviour blueProjectilePrefab;
    [SerializeField] private float fireRate = .12f;
    [SerializeField] private int damage = 10;
    [SerializeField] private LayerMask cursorRayMask;
    private float fireCooldown = 0;
    private SpaceshipLookAt spaceshipLookAt;
    private Transform autoTarget = null;

    [Header("Minigame Settings")]
    [SerializeField] private float minigameFireRate = .72f;
    [SerializeField] private ProjectileBehaviour minigameProjectilePrefab;
    private float minigameFireCooldown = 0;

    [Header("Debug")]
    [SerializeField] bool useDebug = false;


    private void Awake()
    {
        spaceshipLookAt = GetComponentInChildren<SpaceshipLookAt>();
    }

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
            case 2:
                MinigameShootingLogic();
                break;
            default:
                if (useDebug == true)
                {
                    Debug.LogWarning("No state exists for index: " + LevelManager.Instance.LevelState.ToString());
                }
                break;
        }
    }

    private void NormalShootingLogic()
    {
        if (fireCooldown >= 0f) fireCooldown -= Time.deltaTime;
        
        if (Input.GetMouseButton(0) && 
            fireCooldown <= 0f && 
            EventSystem.current.IsPointerOverGameObject() == false)
        {
            NormalShoot();
        }
    }

    private void MinigameShootingLogic()
    {
        if (minigameFireCooldown >= 0f) minigameFireCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && 
            minigameFireCooldown <= 0f)
        {
            MinigameShoot();
        }
    }

    private void NormalShoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 aimPoint;

        RaycastHit hit;
        bool hitSomething = false;

        // if ray hits something
        if (Physics.Raycast(ray, out hit, range, cursorRayMask))
        {
            hitSomething = true;
            aimPoint = hit.point;
            if (useDebug) Debug.Log(hit.collider.name + ", " + aimPoint + ", " + hit.collider.gameObject);
        }

        // if ray hits nothing
        else
        {
            hitSomething = false;
            aimPoint = ray.GetPoint(range);
            if (useDebug) Debug.Log("None");
        }

        Vector3 projectileDir = (aimPoint - bulletSpawn.position).normalized;
        ProjectileBehaviour projectile;

        if (PowerUpManager.Instance.HasDoubleBullets())
        {
            AudioManager.Instance.PlayPlayerDDShootSFX();
            projectile = Instantiate(blueProjectilePrefab, bulletSpawn.position, Quaternion.identity);
        }
        else
        {
            AudioManager.Instance.PlayPlayerShootSFX();
            projectile = Instantiate(projectilePrefab, bulletSpawn.position, Quaternion.identity);
        }

        if (hitSomething == true)
        {
            if (hit.collider.name != "Ground" && 
                hit.collider.GetComponent<Obstacle>()?.HasWeirdShape == false)
            {
                projectile.target = hit.transform;
            }
        }

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
                Vector3 projectileDir = 
                    (asteroid.transform.position + (transform.up * 7) - transform.position).normalized;

                AudioManager.Instance.PlayPlayerShootSFX();

                ProjectileBehaviour projectile = Instantiate(minigameProjectilePrefab, bulletSpawn.position, Quaternion.identity);
                projectile.moveDir = projectileDir;

                minigameFireCooldown = minigameFireRate;
            }
        }
    }

    public void AutoShoot()
    {
        // To fix that weird bug where you can shoot once while looking a Help Guide
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        // If no target is found
        if (autoTarget == null)
        {
            // Find one..
            autoTarget = PlayerVicinity.Instance.GetNearest()?.transform;
        }

        // If the auto target is now way behind the player..
        if (PlayerVicinity.Instance.ContainsTransform(autoTarget) == false)
        {
            // Remove that as the auto target..
            autoTarget = null;
        }

        // Do a special case for missiles, because the kids will cry if I don't
        if (PlayerVicinity.Instance.GetNearest()?.IsMissile == true)
        {
            // Remove that as the auto target..
            autoTarget = PlayerVicinity.Instance.GetNearest()?.transform;
        }

        // If you finally found one..
        if (autoTarget != null)
        {
            // Look at target instantly.
            spaceshipLookAt.LookAtPosition(autoTarget.position);
        }
        else
        {
            // If you still can't find one..
            // Do nothing but look at the center...
            spaceshipLookAt.transform.localRotation = 
            Quaternion.Slerp(
                spaceshipLookAt.transform.localRotation, 
                Quaternion.Euler(0,0,0), 
                Time.deltaTime * spaceshipLookAt.RotationSpeed);

            return;
        }

        // If you're still waiting on your fire rate..
        if (fireCooldown > 0f)
        {
            return;
        }

        // SHOOT - Startling line
        ProjectileBehaviour projectile;

        if (PowerUpManager.Instance.HasDoubleBullets())
        {
            AudioManager.Instance.PlayPlayerDDShootSFX();
            projectile = Instantiate(blueProjectilePrefab, bulletSpawn.position, Quaternion.identity);
        }
        else
        {
            AudioManager.Instance.PlayPlayerShootSFX();
            projectile = Instantiate(projectilePrefab, bulletSpawn.position, Quaternion.identity);
        }

        projectile.target = autoTarget;
        // SHOOT - Ending line

        // Reset fire cooldown. Set it to your Fire Rate..
        fireCooldown = fireRate;
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
