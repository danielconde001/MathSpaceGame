using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 moveDir;
    public float projectileSpeed;
    [SerializeField] protected float selfDestoryTimer = 5f;
    [SerializeField] protected bool useDebug = false;

    protected void Start()
    {
        Destroy(gameObject, selfDestoryTimer);
    }

    protected void Update()
    {
        if (PauseManager.Instance.IsPaused == true)
        {
            return;
        }

        transform.position += moveDir * projectileSpeed * Time.deltaTime;    
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Damageable hit;
        if (other.gameObject.TryGetComponent(out hit))
        {
            if (hit.gameObject.CompareTag("Enemy"))
            {
                if (PowerUpManager.Instance.HasHealPerHit())
                {
                    PlayerManager.Instance.GetPlayer().Health.AddHealth(1);

                    if (useDebug == true)
                    {
                        Debug.Log
                        (
                            "Hit has Heal! Player now has: " +
                            PlayerManager.Instance.GetPlayer().Health.value +
                            "/" +
                            PlayerManager.Instance.GetPlayer().GetMaxHealth()
                        );
                    }
                }
            }

            int playerDmg = PlayerManager.Instance.GetPlayer().GetDamage();

            if (PowerUpManager.Instance.HasDoubleBullets() == true)
            {
                playerDmg *= 2;
            }
        
            hit.TakeDamage(playerDmg);
        }
        else if (other.gameObject.GetComponent<AsteroidScript>())
        {
            AsteroidScript asteroid = other.gameObject.GetComponent<AsteroidScript>();

            asteroid.OnShot();
        }

        AudioManager.Instance.PlayHitSFXAtLocation(transform.position);

        Destroy(gameObject);
    }
}
