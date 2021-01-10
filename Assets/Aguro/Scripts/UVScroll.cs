using UnityEngine;
 
public class UVScroll : MonoBehaviour
{
    [SerializeField]
    private Material _targetMaterial;
 
    [SerializeField]
    private float _scrollX;
    [SerializeField]
    private float _scrollY;
 
    private Vector2 offset;
    private void Awake()
    {
        offset = _targetMaterial.mainTextureOffset;
    }
 
    private void Update()
    {
        // offset.x += _scrollX * Time.deltaTime;
        // offset.y += _scrollY * Time.deltaTime;
        offset.x += _scrollX * 0.01f;
        offset.y += _scrollY * 0.01f;
        _targetMaterial.mainTextureOffset = offset;
    }
}