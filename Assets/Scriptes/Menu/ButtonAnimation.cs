using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Header("Sound Settings")]
    [SerializeField] private AudioClip hoverSound; // Звук наведения
    [SerializeField][Range(0, 1)] private float volume = 0.5f; // Громкость
    private AudioSource audioSource;


    [SerializeField] private Vector3 _originalScale; 
    [SerializeField] private float _hoverScale = 1.1f;
    [SerializeField] private float _animationDuration = 0.3f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();

        transform.DOScale(_originalScale * _hoverScale, _animationDuration)
            .SetEase(Ease.OutBack);

        if (hoverSound != null && audioSource != null) audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(_originalScale, _animationDuration)
            .SetEase(Ease.OutQuad);
    }

    void Start()
    {
        _originalScale = transform.localScale;


        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false;
        audioSource.volume = volume;
    }


    void Update()
    {
        
    } 

}
