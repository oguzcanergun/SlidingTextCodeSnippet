using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SlidingText : MonoBehaviour
{
    [SerializeField] private TMP_Text slidingText;
    [SerializeField] private string textToSlide = "Sliding text.";
    [SerializeField] private float waitTimeBetweenCharactersInSeconds = 1.0f;
    [SerializeField] private int numberOfMaximumOccurence = 20;

    private WaitForSeconds waitTimeBetweenCharacters;
    private IEnumerator textSliderCoroutine;
    
    void Start()
    {
        waitTimeBetweenCharacters = new WaitForSeconds(waitTimeBetweenCharactersInSeconds);
        textSliderCoroutine = TextSlider();
        StartCoroutine(textSliderCoroutine);
    }

    private IEnumerator TextSlider()
    {
        var textHolder = textToSlide;
        var maximumCharCheckValue = textToSlide.Length * numberOfMaximumOccurence;
        int slideCharacterIndex = 0;
        while (true)
        {
            textHolder = string.Format("{0}{1}", textToSlide[textToSlide.Length - 1 - slideCharacterIndex], textHolder);
            slidingText.text = textHolder;
            
            yield return waitTimeBetweenCharacters;
            slideCharacterIndex++;
            if (slideCharacterIndex >= textToSlide.Length)
            {
                slideCharacterIndex = 0;
            }

            if (textHolder.Length >= maximumCharCheckValue)
            {
                textHolder = textHolder.Remove(textHolder.IndexOf(textToSlide), textToSlide.Length);
            }
        }
    }

    private void OnDisable()
    {
        StopCoroutine(textSliderCoroutine);
    }

    private void OnDestroy()
    {
        StopCoroutine(textSliderCoroutine);
    }
}
