using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 moveDir;
    public float projectileSpeed;
    [SerializeField] private float selfDestoryTimer = 5f;

    private void Start()
    {
        Destroy(gameObject, selfDestoryTimer);
    }

    void Update()
    {
        transform.position += moveDir * projectileSpeed * Time.deltaTime;    
    }

    private void OnTriggerEnter(Collider other)
    {
        Damageable hit;
        if (other.gameObject.TryGetComponent<Damageable>(out hit))
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
