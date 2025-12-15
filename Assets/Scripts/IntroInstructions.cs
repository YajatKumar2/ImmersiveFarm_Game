using UnityEngine;

public class IntroInstructions : MonoBehaviour
{
    public float displayTime = 5f;

    void Start()
    {
        Invoke(nameof(Hide), displayTime);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            Hide();
        }
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}
