using System.Collections;
using UnityEngine;

public class TransparentNearCamera : MonoBehaviour
{
    Camera mainCamera;
    Renderer rend;

    bool colorHasChanged = false;

    private void Awake()
    {
        mainCamera = Camera.main;
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float distToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        if (distToCamera < 12.5f)
        {
            
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (colorHasChanged == true)
        {
            return;
        }

        StartCoroutine(ChangeColorCoroutine());
    }

    IEnumerator ChangeColorCoroutine()
    {
        while (rend.material.color.a > 0)
        {
            float r = rend.material.color.r;
            float g = rend.material.color.g;
            float b = rend.material.color.b;
            float a = rend.material.color.a - .1f;

            rend.material.color = new Color(r, g, b, a);

            yield return new WaitForSeconds(.1f);
        }
    }
}
