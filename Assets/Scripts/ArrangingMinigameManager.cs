using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ArrangingMinigameManager : MonoBehaviour
{
    public GameObject numberImage1;
    public GameObject numberImage2;
    public GameObject numberImage3;
    public GameObject numberImage4;
    public GameObject numberImage5;
    public Button submitButton;

    private int[] numbers = new int[5];
    private int[] shuffledIndices = new int[5];
    private GameObject selectedImage = null;
    private GameObject draggingImage = null;
    private Vector3 dragStartPos;

    void Start()
    {
        InitializeMinigame();
    }

    public void InitializeMinigame()
    {
        GenerateAndShowNumbers();
        submitButton.onClick.RemoveAllListeners();
        submitButton.onClick.AddListener(OnSubmit);
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        foreach (var img in images)
        {
            Button btn = img.GetComponent<Button>();
            if (btn == null)
                btn = img.AddComponent<Button>();
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

    public void GenerateAndShowNumbers()
    {
        AnimateFields();

        // Generate 5 small two-digit numbers with a simple pattern (step 1 or 2)
        int start = Random.Range(10, 26); // ensures last number is <= 30
        int step = Random.Range(1, 3); // step of 1 or 2
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
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        for (int i = 0; i < 5; i++)
        {
            TMP_Text txt = images[i].GetComponentInChildren<TMP_Text>();
            txt.text = numbers[shuffledIndices[i]].ToString();
        }
    }

    void AnimateFields()
    {
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        foreach (var img in images)
        {
            img.transform.DOMoveY(img.transform.position.y - 500f, 2f);
        }
        submitButton.transform.DOMoveY(submitButton.transform.position.y - 500f, 2f);
    }

    void OnSubmit()
    {
        // Check if the images are arranged in ascending order by their child text
        bool correct = true;
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        for (int i = 0; i < 5; i++)
        {
            TMP_Text txt = images[i].GetComponentInChildren<TMP_Text>();
            int val;
            if (!int.TryParse(txt.text, out val) || val != numbers[i])
            {
                correct = false;
                break;
            }
        }
        submitButton.interactable = false;
        if (correct)
        {
            Debug.Log("Correct!");
            StartCoroutine(SlideUpAndGenerate());
        }
        else
        {
            Debug.Log("Incorrect!");
            // Add failure logic here
            // Optionally re-enable if you want to allow retry on failure:
            submitButton.interactable = true;
        }
    }

    public System.Collections.IEnumerator SlideUpAndGenerate()
    {
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        foreach (var img in images)
        {
            img.transform.DOMoveY(img.transform.position.y + 500f, 2f);
        }
        submitButton.transform.DOMoveY(submitButton.transform.position.y + 500f, 2f);
        yield return new WaitForSeconds(2f);
        GenerateAndShowNumbers();
        submitButton.interactable = true;
    }

    private void OnImageClicked(GameObject img)
    {
        if (draggingImage != null) return; // Prevent click-to-swap if dragging
        if (selectedImage == null)
        {
            selectedImage = img;
            // Optionally highlight selected image
            // img.GetComponent<Image>().color = Color.yellow;
        }
        else if (selectedImage == img)
        {
            // Deselect if same image clicked again
            selectedImage = null;
            // img.GetComponent<Image>().color = Color.white;
        }
        else
        {
            // Swap TMP_Text values
            TMP_Text txtA = selectedImage.GetComponentInChildren<TMP_Text>();
            TMP_Text txtB = img.GetComponentInChildren<TMP_Text>();
            string temp = txtA.text;
            txtA.text = txtB.text;
            txtB.text = temp;
            // Optionally reset highlight
            // selectedImage.GetComponent<Image>().color = Color.white;
            selectedImage = null;
        }
    }

    public void OnBeginDrag(GameObject img)
    {
        draggingImage = img;
        dragStartPos = img.transform.position;
        selectedImage = null; // Disable click-to-swap while dragging
        // Bring dragged image to front (top of hierarchy)
        img.transform.SetAsLastSibling();
        // Ensure z is 0 for all images
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
        foreach (var image in images)
        {
            Vector3 pos = image.transform.localPosition;
            image.transform.localPosition = new Vector3(pos.x, pos.y, 0f);
        }
    }

    public void OnDrag(GameObject img, Vector3 pointerPos)
    {
        if (draggingImage == img)
            img.transform.position = pointerPos;
    }

    public void OnEndDrag(GameObject img)
    {
        if (draggingImage == img)
        {
            // On drag end, check for overlap with any other image and swap if overlapping
            GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
            RectTransform draggedRect = img.GetComponent<RectTransform>();
            foreach (var other in images)
            {
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
                        // Swap TMP_Text values
                        TMP_Text txtA = img.GetComponentInChildren<TMP_Text>();
                        TMP_Text txtB = other.GetComponentInChildren<TMP_Text>();
                        string temp = txtA.text;
                        txtA.text = txtB.text;
                        txtB.text = temp;
                        break;
                    }
                }
            }
            // Return to original position
            img.transform.position = dragStartPos;
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
            TMP_Text txtA = dropped.GetComponentInChildren<TMP_Text>();
            TMP_Text txtB = target.GetComponentInChildren<TMP_Text>();
            string temp = txtA.text;
            txtA.text = txtB.text;
            txtB.text = temp;
        }
        // Ensure z is 0 for all images
        GameObject[] images = { numberImage1, numberImage2, numberImage3, numberImage4, numberImage5 };
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
