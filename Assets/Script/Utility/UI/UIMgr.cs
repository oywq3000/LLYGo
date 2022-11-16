
using Script.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    private IUIkit _uIkit;
    private void Start()
    {
        _uIkit = GameFacade.Instance.GetInstance<IUIkit>();
        _uIkit.OpenPanel("Login");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _uIkit.ClosePanel("Enroll");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            _uIkit.OpenPanel("Enroll");
        }
        
    }
}
