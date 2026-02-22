using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class AutoShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed = false;
    bool buttonEnabled = true;
    float waitForSeconds = 0f;
    SpaceshipAttack attack;

    [SerializeField] float pressedScale = 1.6f;
    [SerializeField] float unpressedScale = 1.25f;
    [SerializeField] float buttonGrowDuration = 1f;
    [SerializeField] float buttonShrinkDuration = 1f;

    float elapsedTimeForPressing = 0;
    float elapsedTimeForUnpressing = 0;

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

        if (isPressed == true && buttonEnabled == true)
        {
            elapsedTimeForUnpressing = 0;
            elapsedTimeForPressing += Time.deltaTime;
            float percentageComplete = elapsedTimeForPressing / buttonGrowDuration;
            if (percentageComplete >= 1) percentageComplete = 1;

            transform.localScale = Vector3.Lerp
                (transform.localScale, Vector3.one * pressedScale, percentageComplete) ;
            attack.AutoShoot();
        }
        else 
        {
            elapsedTimeForPressing = 0;
            elapsedTimeForUnpressing += Time.deltaTime;
            float percentageComplete = elapsedTimeForUnpressing / buttonShrinkDuration;
            if (percentageComplete >= 1) percentageComplete = 1;

            transform.localScale = Vector3.Lerp
                (transform.localScale, Vector3.one * unpressedScale, percentageComplete);
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
        buttonEnabled = false;
        yield return new WaitUntil( () => (waitForSeconds <= 0) );
        buttonEnabled = true;
    }
}
