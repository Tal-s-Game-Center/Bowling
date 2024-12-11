using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResetScript : MonoBehaviour
{
    [SerializeField] InputAction activationKey ;
    [SerializeField] String startSceneName ;
    void Start()
    {
        
    }
    public void OnEnable(){
        activationKey.Enable() ;
    }
    public void OnDisable(){
        activationKey.Disable() ;
    }
    // Update is called once per frame
    void Update()
    {
        if(activationKey.WasPressedThisFrame()){
            SceneManager.LoadScene(startSceneName) ;
        }
    }
}
