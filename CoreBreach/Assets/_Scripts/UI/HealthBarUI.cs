using UnityEngine;
using UnityEngine.UI;

// listen Core events and update hp bar on screen.
public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    [SerializeField] private Color fullColor =Color.green;
    [SerializeField] private Color lowColor =Color.red;

    private void Start()
    {
        if (Core.Instance!= null)
        {
            Core.Instance.OnHealthChanged+= UpdateBar;
        }
    }

    private void OnDisable()
    {
        if (Core.Instance!= null)
        {
            Core.Instance.OnHealthChanged-= UpdateBar;
        }
    }

    private void UpdateBar(int current,int max)
    {
        if (healthSlider == null) return;

        float ratio = (float)current /max;
        healthSlider.value =ratio;

        if (fillImage !=null)
        {
            fillImage.color= Color.Lerp(lowColor,fullColor,ratio);
        }
    }
}