using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] float destroyTimer = 3f;

    void Start()
    {
        Destroy(this.gameObject, destroyTimer);
    }
}
