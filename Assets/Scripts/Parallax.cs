using UnityEngine;
using System;

public class Parallax : MonoBehaviour
{[SerializeField] private ParallaxElement[] parallaxElements;
    
    private Transform _mainCamera;
    private Vector3 _previousCameraPosition;
    private Vector3 _cameraDeltaMovement;

    private void Awake() {
        _mainCamera = Camera.main!.transform;
        
        foreach (var parallaxElement in parallaxElements) {
            parallaxElement.objectTransform = parallaxElement.gameObject.transform;
            parallaxElement.spriteRenderer = parallaxElement.gameObject.GetComponent<SpriteRenderer>();

            if (parallaxElement.infiniteScrollingXAxis) {
                SetInfiniteScrolling(parallaxElement);
            }
        }
    }

    private void LateUpdate() {
        _cameraDeltaMovement = _mainCamera.position - _previousCameraPosition;

        foreach (var parallaxElement in parallaxElements) {
            if (!parallaxElement.xAxisParallaxEnabled) continue;
            ApplyParallaxMovementXAxis(parallaxElement);

            if (!parallaxElement.infiniteScrollingXAxis) continue;
            ApplyInfiniteScrollingXAxis(parallaxElement);
        }
        
        _previousCameraPosition = _mainCamera.position;
    }

    private void SetInfiniteScrolling(ParallaxElement parallaxElement) {
        parallaxElement.spriteRenderer.drawMode = SpriteDrawMode.Tiled;
        parallaxElement.spriteRenderer.size = new Vector2(parallaxElement.spriteRenderer.size.x * 3, parallaxElement.spriteRenderer.size.y);
    }

    private void ApplyInfiniteScrollingXAxis(ParallaxElement parallaxElement) {
        var parallaxElementSprite = parallaxElement.spriteRenderer.sprite;
        var width = parallaxElementSprite.texture.width / parallaxElementSprite.pixelsPerUnit;

        if (_mainCamera.position.x - parallaxElement.objectTransform.position.x >= width) {
            parallaxElement.objectTransform.position += new Vector3(width, 0, 0);
        }
        
        if (_mainCamera.position.x - parallaxElement.objectTransform.position.x <= -width) {
            parallaxElement.objectTransform.position -= new Vector3(width, 0, 0);
        }
    }

    private void ApplyParallaxMovementXAxis(ParallaxElement parallaxElement) {
        var parallaxMovementX = _cameraDeltaMovement.x + _cameraDeltaMovement.x * parallaxElement.xAxisEffectMultiplier;
        
        parallaxElement.objectTransform.position += new Vector3(parallaxMovementX, 0, 0);
    }
    

    [Serializable] public class ParallaxElement {
        public GameObject gameObject;
        [HideInInspector] public Transform objectTransform;
        [HideInInspector] public SpriteRenderer spriteRenderer;
        
        [Header("X Axis Parallax")]
        public bool xAxisParallaxEnabled;
        public float xAxisEffectMultiplier;
        public bool infiniteScrollingXAxis;
    }
}