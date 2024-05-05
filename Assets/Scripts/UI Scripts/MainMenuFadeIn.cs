using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class MainMenuFadeIn : MonoBehaviour
{
    [SerializeField] private float fadeInTime = 5f; // Time taken to fade in (in seconds)
    private Graphic[] graphics; // Array to store references to UI graphics components
    private TMP_Text[] texts; // Array to store references to TextMeshPro components
    private Button[] buttons; // Array to store references to Button components
    private float timer = 0f; // Timer to keep track of the fading time

    void Start()
    {
        // Get all the Graphic components in the children of this GameObject
        graphics = GetComponentsInChildren<Graphic>();

        // Get all the TextMeshPro components in the children of this GameObject
        texts = GetComponentsInChildren<TMP_Text>();

        // Get all the Button components in the children of this GameObject
        buttons = GetComponentsInChildren<Button>();

        // Set initial alpha to 0 for all graphics
        foreach (Graphic graphic in graphics)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, 0f);
        }

        // Set initial alpha to 0 for all TextMeshPro components
        foreach (TMP_Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        }

        // Set initial alpha to 0 for all Button components
        foreach (Button button in buttons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, 0f);
            button.colors = colors;
        }
    }

    void Update()
    {
        timer += Time.deltaTime * 2; // Increment timer

        // Calculate alpha value based on time elapsed
        float alpha = Mathf.Clamp01(timer / fadeInTime);

        // Set the alpha value of all graphics
        foreach (Graphic graphic in graphics)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, alpha);
        }

        // Set the alpha value of all TextMeshPro components
        foreach (TMP_Text text in texts)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }

        // Set the alpha value of all Button components
        foreach (Button button in buttons)
        {
            ColorBlock colors = button.colors;
            colors.normalColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b, alpha);
            button.colors = colors;

            button.interactable = alpha > 0.5f;

        }

        // Disable the script when the fading is complete
        if (timer >= fadeInTime)
        {
            Time.timeScale = 0f;
            enabled = false;
        }
    }
}
