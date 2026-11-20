using System.Collections;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{
    [SerializeField] private GameObject cardBack;
    [SerializeField] private SceneController controller;

    private int _id;
    private bool _isFlipping = false;
    private Vector3 _originalScale;

    public int id
    {
        get { return _id; }
    }

    void Start()
    {
        _originalScale = transform.localScale;
    }

    public void SetCard(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseEnter()
    {
        if (cardBack.activeSelf && controller.canReveal && !_isFlipping)
        {
            transform.localScale = _originalScale * 1.06f;
        }
    }

    public void OnMouseExit()
    {
        if (!_isFlipping)
        {
            transform.localScale = _originalScale;
        }
    }

    public void OnMouseDown()
    {
        if (cardBack.activeSelf && controller.canReveal && !_isFlipping)
        {
            StartCoroutine(FlipCard(true));
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        StartCoroutine(FlipCard(false));
    }

    public void PlayMatchEffect()
    {
        StartCoroutine(MatchPopRoutine());
    }

    private IEnumerator FlipCard(bool showFace)
    {
        _isFlipping = true;
        float duration = 0.12f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scaleX = Mathf.Lerp(_originalScale.x, 0f, elapsed / duration);
            transform.localScale = new Vector3(scaleX, _originalScale.y, _originalScale.z);
            yield return null;
        }

        cardBack.SetActive(!showFace);

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scaleX = Mathf.Lerp(0f, _originalScale.x, elapsed / duration);
            transform.localScale = new Vector3(scaleX, _originalScale.y, _originalScale.z);
            yield return null;
        }

        transform.localScale = _originalScale;
        _isFlipping = false;
    }

    private IEnumerator MatchPopRoutine()
    {
        float duration = 0.1f;
        Vector3 bigScale = _originalScale * 1.15f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(_originalScale, bigScale, elapsed / duration);
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(bigScale, _originalScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = _originalScale;
    }
}