using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoSingleton<MenuManager>
{
    [Header("UI Components")]
    [SerializeField] private RectTransform _openTransform = null;
    [SerializeField] private RectTransform _hauntTransform = null;
    [SerializeField] private TextMeshProUGUI _startText = null;
    [SerializeField] private MenuGhost _ghost;

    [Header("Title Values")]
    [SerializeField] private List<RectTransform> _openTransformPositions; // 0 = On Screen, 1 = Off screen
    [Range(0.1f, 5.0f)] [SerializeField] private float _openMoveTime = 1.0f;
    [Range(0.1f, 5.0f)] [SerializeField] private float _hauntScaleTime = 1.0f;
    
    [Header("Start Text Values")]
    [Range(0.1f, 5.0f)] [SerializeField] private float _characterRevealTime = 1.0f;
    [SerializeField] private AnimationCurve _startTextYOffsetCurve = null;
    [Range(0.5f, 5.0f)] [SerializeField] private float _startTextYOffSetScale = 1.0f;
    [Range(1.0f, 10.0f)][SerializeField] private float _startTextMoveSpeed = 1.0f;
    
    [Header("Ghost Values")]
    [Range(0.0f, 3.0f)][SerializeField] private float _ghostStartDelay = 0.25f;

    // Coroutines
    private IEnumerator _startTextShakeRoutine = null;

    // Count
    private int numChars;



    public static IEnumerator OpenMenu()
    {
        // Set Values
        Instance.numChars = Instance._startText.textInfo.characterCount;
        Instance._startText.maxVisibleCharacters = 0;
        Instance._openTransform.anchoredPosition = Instance._openTransformPositions[1].anchoredPosition;
        Instance._hauntTransform.localScale = Vector3.zero;

        // Display Title
        Instance.StartCoroutine(Instance.RevealTitle());

        yield return new WaitForSeconds(Instance._ghostStartDelay);

        // Move Ghost
        Instance._ghost.StartMenuRoutine();

        yield return null;
    }

    public static IEnumerator RevealStartText()
    {
        // Display Start Text
        var delay = Instance._characterRevealTime / Instance.numChars;

        for (int i = 0; i < Instance.numChars + 1; i++)
        {
            Instance._startText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(delay);
        }

        Instance._startText.renderMode = TextRenderFlags.DontRender;

        // Shake start text
        Instance._startTextShakeRoutine = Instance.ShakeStartText();
        Instance.StartCoroutine(Instance._startTextShakeRoutine);

        yield return new WaitForSeconds(Instance._characterRevealTime);
    }

    private IEnumerator RevealTitle()
    {
        // Move in open image
        var dist = Vector2.Distance(_openTransformPositions[1].anchoredPosition, _openTransformPositions[0].anchoredPosition);
        var speed = dist / _openMoveTime;
        var elapsedTime = 0.0f;

        while (elapsedTime < _openMoveTime)
        {
            _openTransform.anchoredPosition = Vector2.Lerp(_openTransformPositions[1].anchoredPosition,                _openTransformPositions[0].anchoredPosition, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / _openMoveTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _openTransform.anchoredPosition = _openTransformPositions[0].anchoredPosition;

        // Fade in haunt image.. or something
        elapsedTime = 0.0f;

        while (elapsedTime < _hauntScaleTime)
        {
            _hauntTransform.localScale = Vector2.Lerp(Vector2.zero, Vector2.one, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / _hauntScaleTime));

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _hauntTransform.localScale = Vector2.one;

        yield return null;
    }

    private IEnumerator ShakeStartText()
    {
        Vector3[] verts;

        var times = new float[numChars];
        var endTime = _startTextYOffsetCurve[_startTextYOffsetCurve.length - 1].time;
        for (int i = 0; i < numChars; i++)
        {
            times[i] = Random.Range(0.0f, endTime);
        }

        while (Application.isPlaying)
        {
            Instance._startText.ForceMeshUpdate();
            verts = _startText.textInfo.meshInfo[0].vertices;

            TMP_CharacterInfo charInfo;

            for (int i = 0; i < numChars; i++)
            {
                charInfo = _startText.textInfo.characterInfo[i];

                // Skip if not visible
                if(!charInfo.isVisible)
                    continue;

                var offSetY = _startTextYOffsetCurve.Evaluate(times[i]) * _startTextYOffSetScale;

                int vertexIndex = charInfo.vertexIndex;
                verts[vertexIndex + 0].y += offSetY;
                verts[vertexIndex + 1].y += offSetY;
                verts[vertexIndex + 2].y += offSetY;
                verts[vertexIndex + 3].y += offSetY;

                times[i] += _startTextMoveSpeed * Time.deltaTime;

                if (times[i] > endTime)
                    times[i] = 0.0f;
            }

            _startText.textInfo.meshInfo[0].mesh.vertices = verts;
            _startText.textInfo.meshInfo[0].mesh.uv = _startText.textInfo.meshInfo[0].uvs0;
            _startText.textInfo.meshInfo[0].mesh.uv2 = _startText.textInfo.meshInfo[0].uvs2;
            _startText.textInfo.meshInfo[0].mesh.colors32 = _startText.textInfo.meshInfo[0].colors32;

            _startText.canvasRenderer.SetMesh(_startText.textInfo.meshInfo[0].mesh);

            yield return null;
        }

        yield return null;
    }


    public static IEnumerator CloseMenu()
    {


        yield return null;
    }
}
