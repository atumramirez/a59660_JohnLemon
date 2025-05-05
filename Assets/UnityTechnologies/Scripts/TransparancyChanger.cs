using UnityEngine;

public class TransparencyChanger : MonoBehaviour
{
    [Range(0f, 1f)]
    public float transparency = 1f;

    private Material _material;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer == null)
        {
            Debug.LogError("No Renderer found on this GameObject.");
            return;
        }

        _material = renderer.material;
        SetMaterialTransparent();
        UpdateTransparency();
    }

    void Update()
    {
        UpdateTransparency(); // Optionally make this controllable elsewhere
    }

    void SetMaterialTransparent()
    {
        if (_material != null)
        {
            _material.SetFloat("_Mode", 3); // For legacy support, optional
            _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            _material.SetInt("_ZWrite", 0);
            _material.DisableKeyword("_ALPHATEST_ON");
            _material.EnableKeyword("_ALPHABLEND_ON");
            _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            _material.renderQueue = 3000;
        }
    }

    void UpdateTransparency()
    {
        if (_material != null)
        {
            Color color = _material.color;
            color.a = transparency;
            _material.color = color;
        }
    }
}