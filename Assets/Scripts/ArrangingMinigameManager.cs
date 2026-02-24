using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ArrangingMinigameManager : MinigameManager
{
    public int minNumber = 10;
    public int maxNumber = 30;
    public int minStep = 1;
    public int maxStep = 2;
    public GameObject numberImage1;
    public GameObject numberImage2;
    public GameObject numberImage3;
    public GameObject numberImage4;
    public GameObject numberImage5;
    public Button submitButton;
    public Image panel;
    public GameObject instructionObject;
    public GameObject anchorObject;
    public Transform hideTransform;
    public Transform showTransform;
    public float yOffset = 250f;
    public float animationDuration = 0.5f;

    private int[] numbers = new int[5];
    private int[] shuffledIndices = new int[5];
    private GameObject selectedImage = null;
    private GameObject draggingImage = null;
    private Vector3 dragStartPos;
    private Vector3 selectedImageStartPos;
    private GameObject[] images;
    private Vector2[] defaultAnchoredPositions = new Vector2[5];

    private uint rounds = 0;
    private uint roundsPassed = 0;

    [SerializeField] bool useDebug = false;

    public override void InitializeMinigame(uint p_numberOfRounds = 7)
    {
        base.InitializeMinigame();

        LevelManager.Instance.LevelState = 1;

        UIActivationManager.Instance.DeactivateOtherUI();

        rounds = p_numberOfRounds;
        roundsPassed = 0;
        panel.enabled = true;
        submitButton.interactable = true;

        images = new GameObject[] { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        for (int i = 0; i < images.Length; i++)
        {
            RectTransform rt = images[i].GetComponent<RectTransform>();
            defaultAnchoredPositions[i] = rt.anchoredPosition;
        }

        GenerateAndShowNumbers();

        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
        foreach (var img in images)
        {
            UnityEngine.UI.Button btn = img.GetComponent<UnityEngine.UI.Button>();
            if (btn == null)
                btn = img.AddComponent<UnityEngine.UI.Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnImageClicked(img));
            // Add drag event handlers
            var dragHandler = img.GetComponent<ImageDragHandler>();
            if (dragHandler == null)
                img.AddComponent<ImageDragHandler>().manager = this;
            else
                dragHandler.manager = this;

            // Ensure all images have the same z position
            Vector3 pos = img.transform.localPosition;
            img.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        }
    }

    public override void EndMinigame()
    {
        base.EndMinigame();
        LevelManager.Instance.LevelState = 0;
        UIActivationManager.Instance.ActivateOtherUI();
        panel.enabled = false;
    }

    public void GenerateAndShowNumbers()
    {
        AnimateFields();

        // Always reset images array to original slot order to keep logic and visuals in sync
        images[0] = numberImage1;
        images[1] = numberImage2;
        images[2] = numberImage3;
        images[3] = numberImage4;
        images[4] = numberImage5;

        // Generate 5 small two-digit numbers with a simple pattern (step 1 or 2)
        int maxStart = maxNumber - 4 * maxStep; // ensure last number fits in range for max step
        int start = Random.Range(minNumber, maxStart + 1);
        int step = Random.Range(minStep, maxStep + 1); // step in [minStep, maxStep]
        for (int i = 0; i < 5; i++)
        {
            numbers[i] = start + i * step;
            shuffledIndices[i] = i;
        }

        // Shuffle the indices for random arrangement
        for (int i = 0; i < 5; i++)
        {
            int rnd = Random.Range(i, 5);
            int temp = shuffledIndices[i];
            shuffledIndices[i] = shuffledIndices[rnd];
            shuffledIndices[rnd] = temp;
        }

        // Set the numbers to the child TMP_Text of each image
        for (int i = 0; i < 5; i++)
        {
            TMP_Text txt = images[i].GetComponentInChildren<TMP_Text>();
            txt.text = numbers[shuffledIndices[i]].ToString();
            // Reset position to slot
            images[i].GetComponent<RectTransform>().anchoredPosition = defaultAnchoredPositions[i];
        }
    }

    void AnimateFields()
    {
        anchorObject.transform.DOMove(showTransform.position, animationDuration);
    }

    void OnSubmit()
    {
        // Check if the images are arranged in ascending order by their child text
        var sortedNumbers = numbers.OrderBy(x => x).ToArray();
        bool correct = true;
        string logMsg = "Checking order: ";
        for (int i = 0; i < 5; i++)
        {
            TMP_Text txt = images[i].GetComponentInChildren<TMP_Text>();
            int val;
            logMsg += $"[{txt.text} vs {sortedNumbers[i]}] ";
            if (!int.TryParse(txt.text, out val) || val != sortedNumbers[i])
            {
                correct = false;
                break;
            }
        }
        submitButton.interactable = false;
        if (correct)
        {
            if (useDebug) Debug.Log("Correct!");
            FeedbackCanvas.Instance.ShowCorrect();
            roundsPassed++;
            StartCoroutine(SlideUpAndGenerate());
        }
        else
        {
            if (useDebug) Debug.Log("Incorrect!");
            // Add failure logic here
            FeedbackCanvas.Instance.ShowWrong();
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
    }

    public System.Collections.IEnumerator SlideUpAndGenerate()
    {
        anchorObject.transform.DOMove(hideTransform.position, animationDuration);

        yield return new WaitForSeconds(animationDuration);

        if (roundsPassed >= rounds)
        {
            EndMinigame();
            yield break;
        }

        GenerateAndShowNumbers();
        submitButton.interactable = true;
    }

    private void OnImageClicked(GameObject img)
    {
        if (draggingImage != null) return; // Prevent click-to-swap if dragging
        int idxA = System.Array.IndexOf(images, selectedImage);
        int idxB = System.Array.IndexOf(images, img);
        if (selectedImage == null)
        {
            selectedImage = img;
            selectedImageStartPos = img.GetComponent<RectTransform>().anchoredPosition;
        }
        else if (selectedImage == img)
        {
            selectedImage = null;
        }
        else
        {
            if (idxA >= 0 && idxB >= 0)
            {
                // Animate both images to their own slot positions
                images[idxA].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxA], 0.25f);
                images[idxB].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxB], 0.25f);
                // Swap TMP_Text values
                TMP_Text txtA = images[idxA].GetComponentInChildren<TMP_Text>();
                TMP_Text txtB = images[idxB].GetComponentInChildren<TMP_Text>();
                string tempText = txtA.text;
                txtA.text = txtB.text;
                txtB.text = tempText;
                // Swap Image.sprite
                Image imgA = images[idxA].GetComponent<Image>();
                Image imgB = images[idxB].GetComponent<Image>();
                Sprite tempSprite = imgA.sprite;
                imgA.sprite = imgB.sprite;
                imgB.sprite = tempSprite;
            }
            selectedImage = null;
        }
    }

    public void OnBeginDrag(GameObject img)
    {
        draggingImage = img;
        dragStartPos = img.GetComponent<RectTransform>().anchoredPosition;
        selectedImage = null; // Disable click-to-swap while dragging
        // Bring dragged image to front (top of hierarchy)
        img.transform.SetAsLastSibling();
        // Ensure z is 0 for all images
        foreach (var image in images)
        {
            Vector3 pos = image.transform.localPosition;
            image.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        }
    }

    public void OnDrag(GameObject img, Vector3 pointerPos)
    {
        if (draggingImage == img)
        {
            // Convert screen position to local anchored position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                img.transform.parent as RectTransform,
                pointerPos,
                null,
                out Vector2 localPoint);
            img.GetComponent<RectTransform>().anchoredPosition = localPoint;
        }
    }

    public void OnEndDrag(GameObject img)
    {
        if (draggingImage == img)
        {
            // On drag end, check for overlap with any other image and swap if overlapping
            RectTransform draggedRect = img.GetComponent<RectTransform>();
            bool swapped = false;
            int idxA = System.Array.IndexOf(images, img);
            for (int i = 0; i < images.Length; i++)
            {
                var other = images[i];
                if (other == img) continue;
                RectTransform otherRect = other.GetComponent<RectTransform>();
                if (draggedRect != null && otherRect != null)
                {
                    Vector3[] draggedCorners = new Vector3[4];
                    Vector3[] otherCorners = new Vector3[4];
                    draggedRect.GetWorldCorners(draggedCorners);
                    otherRect.GetWorldCorners(otherCorners);
                    Rect draggedWorldRect = new Rect(draggedCorners[0], draggedCorners[2] - draggedCorners[0]);
                    Rect otherWorldRect = new Rect(otherCorners[0], otherCorners[2] - otherCorners[0]);
                    if (draggedWorldRect.Overlaps(otherWorldRect))
                    {
                        int idxB = i;
                        // Animate both images to their own slot positions
                        images[idxA].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxA], 0.25f);
                        images[idxB].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxB], 0.25f);
                        // Swap TMP_Text values
                        TMP_Text txtA = images[idxA].GetComponentInChildren<TMP_Text>();
                        TMP_Text txtB = images[idxB].GetComponentInChildren<TMP_Text>();
                        string tempText = txtA.text;
                        txtA.text = txtB.text;
                        txtB.text = tempText;
                        // Swap Image.sprite
                        Image imgA = images[idxA].GetComponent<Image>();
                        Image imgB = images[idxB].GetComponent<Image>();
                        Sprite tempSprite = imgA.sprite;
                        imgA.sprite = imgB.sprite;
                        imgB.sprite = tempSprite;
                        swapped = true;
                        break;
                    }
                }
            }
            if (!swapped)
            {
                // Return to original position with animation
                img.GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxA], 0.25f);
            }
            draggingImage = null;
            // Ensure z is 0 for all images
            foreach (var image in images)
            {
                Vector3 pos = image.transform.localPosition;
                image.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
            }
        }
    }

    public void OnDrop(GameObject dropped, GameObject target)
    {
        if (dropped != null && target != null && dropped != target)
        {
            int idxA = System.Array.IndexOf(images, dropped);
            int idxB = System.Array.IndexOf(images, target);
            if (idxA >= 0 && idxB >= 0)
            {
                // Animate both images to their own slot positions
                images[idxA].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxA], 0.25f);
                images[idxB].GetComponent<RectTransform>().DOAnchorPos(defaultAnchoredPositions[idxB], 0.25f);
                // Swap TMP_Text values
                TMP_Text txtA = images[idxA].GetComponentInChildren<TMP_Text>();
                TMP_Text txtB = images[idxB].GetComponentInChildren<TMP_Text>();
                string tempText = txtA.text;
                txtA.text = txtB.text;
                txtB.text = tempText;
                // Swap Image.sprite
                Image imgA = images[idxA].GetComponent<Image>();
                Image imgB = images[idxB].GetComponent<Image>();
                Sprite tempSprite = imgA.sprite;
                imgA.sprite = imgB.sprite;
                imgB.sprite = tempSprite;
            }
        }
        // Ensure z is 0 for all images
        foreach (var image in images)
        {
            Vector3 pos = image.transform.localPosition;
            image.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        }
    }
}

// Drag handler for images
public class ImageDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [HideInInspector] public ArrangingMinigameManager manager;
    public void OnBeginDrag(PointerEventData eventData)
    {
        manager.OnBeginDrag(gameObject);
    }
    public void OnDrag(PointerEventData eventData)
    {
        manager.OnDrag(gameObject, eventData.position);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        manager.OnEndDrag(gameObject);
    }
    public void OnDrop(PointerEventData eventData)
    {
        var dropped = eventData.pointerDrag;
        if (dropped != null && dropped != gameObject)
        {
            manager.OnDrop(dropped, gameObject);
        }
    }
}
