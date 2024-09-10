using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadingDemo : MonoBehaviour, IActivatablePanel
{
    [SerializeField] private Slider _loadingSlider;

    private float _moveTimeDivider = 1f;
    private float _startValue = 0f;
    private float _endValue = 1f;
    private float _pauseValue1 = 0.2f;
    private float _pauseValue2 = 0.7f;
    private float _pauseWaitSeconds1 = 1.4f;
    private float _pauseWaitSeconds2 = 0.8f;

    public void OnPanelActivated()
    {
        _loadingSlider.value = _startValue;
        _loadingSlider.interactable = false;
        
        StartCoroutine(MoveSliderStartToEnd());
    }

    public void OnPanelDeactivated()
    {
        StopCoroutine(MoveSliderStartToEnd());
    }

    private IEnumerator MoveSliderStartToEnd()
    {
        yield return StartCoroutine(SmoothValueMove(_startValue, _pauseValue1));
        yield return new WaitForSeconds(_pauseWaitSeconds1);
        yield return StartCoroutine(SmoothValueMove(_pauseValue1, _pauseValue2));
        yield return new WaitForSeconds(_pauseWaitSeconds2);
        yield return StartCoroutine(SmoothValueMove(_pauseValue2, _endValue));
    }

    private IEnumerator SmoothValueMove(float startValue, float endValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < _moveTimeDivider)
        {
            _loadingSlider.value = Mathf.Lerp(startValue, endValue, elapsedTime / _moveTimeDivider);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _loadingSlider.value = endValue;
    }
}
