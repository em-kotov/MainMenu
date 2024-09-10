using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour, IActivatablePanel
{
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private Image _scrollViewImage;
    [SerializeField] private Image _scrollBarImage;
    [SerializeField] private Image _handleImage;

    private readonly Color _transparentColor = new(0f, 0f, 0f, 0f);

    private float _startScrollValue = 1f;
    private float _endScrollValue = 0f;
    private float _scrollSpeed = 0.1f;
    private float _scrollManualSpeed = 0.05f;
    private bool _isAutoScrolling = false;
    private Coroutine _autoScrollCoroutine;

    private void Start()
    {
        _scrollViewImage.color = _transparentColor;
        _scrollBarImage.color = _transparentColor;
        _handleImage.color = _transparentColor;
    }

    private void Update()
    {
        HandleManualScroll();
    }

    public void OnPanelActivated()
    {
        _scrollBar.value = _startScrollValue;
        _scrollBar.onValueChanged.AddListener(OnScrollValueChanged);

        ActivateAutoScroll();
    }

    public void OnPanelDeactivated()
    {
        _scrollBar.onValueChanged.RemoveListener(OnScrollValueChanged);
        StopAutoScroll();
    }

    private void ActivateAutoScroll()
    {
        if (_autoScrollCoroutine != null)
        {
            StopCoroutine(_autoScrollCoroutine);
        }

        _autoScrollCoroutine = StartCoroutine(AutoScrollToBottom(_scrollSpeed));
    }

    private void StopAutoScroll()
    {
        if (_isAutoScrolling)
        {
            StopCoroutine(_autoScrollCoroutine);
            _isAutoScrolling = false;
        }
    }

    private void HandleManualScroll()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            float newScrollBarValue = Input.mouseScrollDelta.y * _scrollManualSpeed + _scrollBar.value;
            _scrollBar.value = Mathf.Clamp(newScrollBarValue, _endScrollValue, _startScrollValue);

            StopAutoScroll();
        }
    }

    private void OnScrollValueChanged(float value)
    {
        if (_isAutoScrolling == false && value > _endScrollValue)
        {
            ActivateAutoScroll();
        }
    }

    private IEnumerator AutoScrollToBottom(float scrollSpeed)
    {
        float startValue = _scrollBar.value;
        float remainingValue = startValue - _endScrollValue;
        float scrollSeconds = remainingValue / scrollSpeed;
        float elapsedTime = 0f;

        _isAutoScrolling = true;

        while (elapsedTime < scrollSeconds && _isAutoScrolling)
        {
            elapsedTime += Time.deltaTime;
            float scrollProgress = elapsedTime / scrollSeconds;
            _scrollBar.value = Mathf.Lerp(startValue, _endScrollValue, scrollProgress);

            yield return null;
        }

        if (_isAutoScrolling)
        {
            _scrollBar.value = _endScrollValue;
        }

        _isAutoScrolling = false;
    }
}
