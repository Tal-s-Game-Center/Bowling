using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
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
