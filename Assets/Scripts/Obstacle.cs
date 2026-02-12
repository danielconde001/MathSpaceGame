using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int Damage = 10;
    [SerializeField] bool useDebug = false;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject != PlayerManager.Instance.GetPlayer().gameObject)
        {
            return;
        }

        if (useDebug == true)
        {
            Debug.Log("Player has made contact with Obstacle!");
        }

        col.gameObject.GetComponent<Damageable>().TakeDamage(Damage);
    }
}
