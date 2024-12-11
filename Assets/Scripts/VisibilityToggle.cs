using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    [SerializeField] bool defaultState ;
    void Start()
    {
        gameObject.SetActive(defaultState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleVisibility(bool shouldActivate){
        if(shouldActivate){
            gameObject.SetActive(true) ;
        }else{
            gameObject.SetActive(false);
        }
    }
}
