using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int Damage = 10;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject != PlayerManager.Instance.GetPlayer().gameObject)
        {
            return;
        }

        print("OUCH!");
        col.gameObject.GetComponent<Damageable>().TakeDamage(Damage);
    }
}
