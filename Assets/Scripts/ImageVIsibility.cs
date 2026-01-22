using UnityEngine;

public class ImageVIsibility : MonoBehaviour
{

    public void SetImageVisibility()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }
}
