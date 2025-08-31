using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour{
    public static GameInput Instance => instance;

    private static GameInput instance;

    private InputSystem_Actions actions;

    public event Action OnLeftButtonDown;
    public event Action OnLeftButtonUp;

    public event Action<Vector2> OnLookPerformed;

    private void Awake(){
 
        if(instance != null){
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        actions = new InputSystem_Actions();
    }

    private void OnEnable(){

        actions.Player.Enable();

        actions.Player.Click.performed += RaiseLeftButtonDown;
        actions.Player.Click.canceled += RaiseLeftButtonUp;

        actions.Player.Look.performed += RaiseLookPerformed;

    }

    private void OnDisable(){

        actions.Player.Disable();

        actions.Player.Click.started -= RaiseLeftButtonDown;
        actions.Player.Click.canceled -= RaiseLeftButtonUp;

        actions.Player.Look.performed -= RaiseLookPerformed;
    }

    private void RaiseLeftButtonDown(InputAction.CallbackContext context){
        OnLeftButtonDown?.Invoke();
    }
    private void RaiseLeftButtonUp(InputAction.CallbackContext context){
        OnLeftButtonUp?.Invoke();
    }
    private void RaiseLookPerformed(InputAction.CallbackContext context){
        OnLookPerformed?.Invoke(context.ReadValue<Vector2>());
    }


}
