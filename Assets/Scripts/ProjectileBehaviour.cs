using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 moveDir;
    public float projectileSpeed;
    [SerializeField] protected float selfDestoryTimer = 5f;

    protected void Start()
    {
        Destroy(gameObject, selfDestoryTimer);
    }

    protected void Update()
    {
        transform.position += moveDir * projectileSpeed * Time.deltaTime;    
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        Damageable hit;
        if (other.gameObject.TryGetComponent(out hit))
        {
            hit.TakeDamage(PlayerManager.Instance.GetPlayer().GetDamage());
        }
        else if (other.gameObject.GetComponent<AsteroidScript>())
        {
            AsteroidScript asteroid = other.gameObject.GetComponent<AsteroidScript>();

            asteroid.OnShot();
        }

        Destroy(gameObject);
    }
}
