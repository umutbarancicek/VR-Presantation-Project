using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandsControllerScript : MonoBehaviour
{

    [SerializeField] InputActionReference gripInputAction;
    [SerializeField] InputActionReference triggerInputAction;

    public event Action<float> OnTriggerPressed;
    public event Action<float> OnGripPressed;

    Animator handAnimator;


    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        gripInputAction.action.performed += GripPressed;
        triggerInputAction.action.performed += TriggerPressed;
    }

    public void TriggerPressed(InputAction.CallbackContext obj)
    {
    
        float triggerValue = obj.ReadValue<float>();
        Debug.Log(triggerValue);
        handAnimator.SetFloat("Trigger", triggerValue);

        // Invoke the OnTriggerPressed event
        OnTriggerPressed?.Invoke(triggerValue);

    }

    public void GripPressed(InputAction.CallbackContext obj)
    {
       float gripValue = obj.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);

        // Invoke the OnGripPressed event
        OnGripPressed?.Invoke(gripValue);

    }

    private void OnDisable()
    {
        gripInputAction.action.performed -= GripPressed;
        triggerInputAction.action.performed -= TriggerPressed;
    }




}
