using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    #region Private Fields

    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Vector3 originalScale;
    private int currentState;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private Vector2 cardPlayPosition;
    private Vector3 playPosition;
    private GameObject glowEffect;
    private GameObject playArrow;
    private float lerpFactor;
    private int cardPlayDivider;
    private float cardPlayMultiplier;
    private bool needUpdateCardPlayPosition;
    private int playPositionYDivider;
    private float playPositionYMultiplier;
    private int playPositionXDivider;
    private float playPositionXMultiplier;
    private bool needUpdatePlayPosition;

    #endregion

    #region Serialized Fields

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlayArea;
    [SerializeField] private Vector3 playPositionOffset;
    [SerializeField] private GameObject cardPlayAreaGlowEffect;
    [SerializeField] private GameObject playArrowObject;
    [SerializeField] private float lerpSpeed = 10f;
    [SerializeField] private int cardPlayAreaDivider = 4;
    [SerializeField] private float cardPlayAreaMultiplier = 1f;
    [SerializeField] private bool updateCardPlayPositionOnStart = false;
    [SerializeField] private int playPositionYDividerValue = 2;
    [SerializeField] private float playPositionYMultiplierValue = 1f;
    [SerializeField] private int playPositionXDividerValue = 4;
    [SerializeField] private float playPositionXMultiplierValue = 1f;
    [SerializeField] private bool updatePlayPositionOnStart = false;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        UpdateCardPlayPosition();
        UpdatePlayPosition();

        if (updateCardPlayPositionOnStart)
        {
            UpdateCardPlayPosition();
        }

        if (updatePlayPositionOnStart)
        {
            UpdatePlayPosition();
        }
    }

    private void Update()
    {
        if (needUpdateCardPlayPosition)
        {
            UpdateCardPlayPosition();
        }

        if (needUpdatePlayPosition)
        {
            UpdatePlayPosition();
        }

        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0)) //Check if mouse button is released
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                if (!Input.GetMouseButton(0)) //Check if mouse button is released
                {
                    TransitionToState0();
                }
                break;
        }
    }

    #endregion

    #region Private Methods

    private void TransitionToState0()
    {
        currentState = 0;
        rectTransform.localScale = originalScale; //Reset Scale
        rectTransform.localRotation = originalRotation; //Reset Rotation
        rectTransform.localPosition = originalPosition; //Reset Position
        glowEffect.SetActive(false); //Disable glow effect
        playArrow.SetActive(false); //Disable playArrow
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            if (eventData.position.y > cardPlayArea.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, playPosition, lerpFactor * lerpSpeed * Time.deltaTime);
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //Set the card's rotation to zero
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.localPosition = Vector3.Lerp(rectTransform.localPosition, eventData.position, lerpFactor * lerpSpeed * Time.deltaTime);
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
       
