using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] int Damage = 10;
    [SerializeField] bool hasWeirdShape = false;
    [SerializeField] bool useDebug = false;

    public bool HasWeirdShape { get => hasWeirdShape; }

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

        col.gameObject.GetComponent<PlayerDamageable>().TakeDamageWithInvul(Damage, 4f);
    }
}
