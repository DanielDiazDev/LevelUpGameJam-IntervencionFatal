using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(() => gameObject.SetActive(false));
    }
}