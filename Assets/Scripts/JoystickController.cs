using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class JoystickController : MonoBehaviour
{
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickButton;
    [SerializeField] private float moveFactor;

    private bool canControlJoystick;
    private Vector3 tapPosition;
    private Vector3 move;


    void Start()
    {
        HideJoystick();
    }

    void Update()
    {
        if (canControlJoystick)
        {
            ControlJoystick();
        }
    }

    // will be called with a event trigger in the inspector (OnPointerDown)
    public void TappedOnJoystickZone()
    {
        // to detect whether touched on the screen or not. Joystick will be showed with a touch        

        tapPosition = Input.mousePosition;
        joystickOutline.position = tapPosition;
        ShowJoystick();
    }

    private void ControlJoystick()
    {
        // to control player with joystick

        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - tapPosition; // last position - first position  

        float canvasYScale = GetComponentInParent<Canvas>().GetComponent<RectTransform>().localScale.y;
        float moveMagnitude = direction.magnitude * moveFactor * canvasYScale;
        float newWidth = (joystickOutline.rect.width / 2) * canvasYScale;

        moveMagnitude = Mathf.Min(newWidth, moveMagnitude);

        move = direction.normalized * moveMagnitude;
        Vector3 targetPos = tapPosition + move;

        joystickButton.position = targetPos;  // knob can move only in the bounders of button outline

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControlJoystick = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControlJoystick = false;
        move = Vector3.zero; // otherwise character will float in the movement direction 
    }

    public Vector3 GetMovePosition()
    {
        return move / 1.75f; // will be adjust according to player's movement speed
    }
}
