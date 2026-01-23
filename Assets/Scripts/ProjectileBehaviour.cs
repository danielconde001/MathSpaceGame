using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public Vector3 moveDir;
    public float projectileSpeed;

    private void Start()
    {
        Destroy(gameObject, 5f);
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

        Destroy(gameObject);
    }
}
