using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private string targetMessage;
    public Color highlightColor = Color.cyan;

    private Vector3 _originalScale;

    void Start()
    {
        _originalScale = transform.localScale;
    }

    public void OnMouseEnter()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = _originalScale * 1.1f;
    }

    public void OnMouseUp()
    {
        transform.localScale = _originalScale;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }
}