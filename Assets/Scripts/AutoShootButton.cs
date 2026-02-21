using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    bool enableButton = true;
    float waitForSeconds = 0f;
    SpaceshipAttack attack;

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

    private void Update()
    {
        if (waitForSeconds > 0 && PauseManager.Instance.IsPaused == false)
        {
            waitForSeconds -= Time.deltaTime;
        }

        if (isPressed == true && enableButton == true)
        {
            attack.AutoShoot();
        }
    }

    public void DisableButtonForSeconds(float p_seconds)
    {
        waitForSeconds = p_seconds;

        if (transform.parent.gameObject.activeSelf == true)
        {
            StartCoroutine(DisableButton());
        }
    }

    IEnumerator DisableButton()
    {
        enableButton = false;
        yield return new WaitUntil( () => (waitForSeconds <= 0) );
        enableButton = true;
    }
}
