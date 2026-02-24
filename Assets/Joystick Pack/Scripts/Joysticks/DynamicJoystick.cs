using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DynamicJoystick : Joystick
{
    public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

    [SerializeField] private float moveThreshold = 1;

    Color initialBackgroundColor;
    Color initialHandleColor;

    [SerializeField] Image backgroundImg;
    [SerializeField] Image handleImg;

    private void Awake()
    {
        backgroundImg = background.GetComponent<Image>();
        handleImg = transform.GetChild(0).Find("Handle").GetComponent<Image>();
        initialBackgroundColor = backgroundImg.color;
        initialHandleColor = handleImg.color;
    }

    protected override void Start()
    {
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (LevelManager.Instance.LevelState == 2)
        {
            backgroundImg.color = new Color(1, 1, 1, 0);
            handleImg.color = new Color(1, 1, 1, 0);
        }
        else
        {
            backgroundImg.color = initialBackgroundColor;
            handleImg.color = initialHandleColor;
        }

        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        if (magnitude > moveThreshold)
        {
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }
        base.HandleInput(magnitude, normalised, radius, cam);
    }
}