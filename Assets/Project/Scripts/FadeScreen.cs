using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private Image Image;
    private float Alpha;
    private bool ToImage;
    private bool IsEnabled;
    public bool IsFinished;

    void Start()
    {
        Alpha = 1;
        FadeToImage();
    }

    void FixedUpdate()
    {
        if (IsEnabled)
        {
            if (ToImage)
            {
                Alpha = Mathf.MoveTowards(Alpha, 0, Time.fixedDeltaTime);
                if (Alpha == 0)
                {
                    IsEnabled = false;
                    IsFinished = true;
                }
            }
            else
            {
                Alpha = Mathf.MoveTowards(Alpha, 1, Time.fixedDeltaTime);
                if (Alpha == 1)
                {
                    IsEnabled = false;
                    IsFinished = true;
                }
            }
            Color color = Image.color;
            color.a = Alpha;
            Image.color = color;
        }
    }

    public void FadeToBlack()
    {
        ToImage = false;
        IsEnabled = true;
        IsFinished = false;
    }

    public void FadeToImage()
    {
        ToImage = true;
        IsEnabled = true;
        IsFinished = false;
    }
}
