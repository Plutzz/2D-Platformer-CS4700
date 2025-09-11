using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private float endYValue = 0.1f;
    [SerializeField] private float animTime = 1;
    [SerializeField] private Ease ease;
    [SerializeField] private RectTransform gemOverlay;
    private RectTransform rectTransform;

    private bool canReset;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        if (canReset && (Input.anyKeyDown || Input.GetButton("Jump")))
        { 
            SceneManager.LoadScene(0);
        }
    }

    public void ShowMenu()
    {
        StartCoroutine(ShowGameOverMenuAnimation());
        PlayerPrefs.SetInt("unlockedHardMode", 1);
    }

    public IEnumerator ShowGameOverMenuAnimation()
    {
        yield return new WaitForSeconds(1f);
        
        gemOverlay.parent = transform;
        gemOverlay.localPosition = new Vector3(75, -75, 0);
        
        Timer.Instance.transform.parent = transform;
        Timer.Instance.transform.localPosition = new Vector3(0, 75, 0);
        Timer.Instance.timerActive = false;
        
        rectTransform.DOLocalMoveY(endYValue, animTime).SetEase(ease);
        yield return new WaitForSeconds(animTime);
        canReset = true;
    }
}