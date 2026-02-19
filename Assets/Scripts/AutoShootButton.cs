using UnityEngine;
using UnityEngine.EventSystems;

public class AutoShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressed = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private void Start()
    {
        attack = PlayerManager.Instance.GetPlayer().GetAttackScript();
    }

    SpaceshipAttack attack;

    private void Update()
    {
        if (isPressed)
        {
            attack.AutoShoot();
        }
    }

}
