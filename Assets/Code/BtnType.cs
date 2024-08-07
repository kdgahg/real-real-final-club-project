using UnityEngine;
using UnityEngine.EventSystems;

public class BtnType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    private Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.New:
                SceneLoad.LoadSceneHandle("Play",0);
                break;
            case BTNType.Continue:
                SceneLoad.LoadSceneHandle("Play",1);
                break;
            case BTNType.Option:
                Debug.Log("Option");
                break;
            case BTNType.Sound:
                Debug.Log("Sound");
                break;
            case BTNType.Back:
                Debug.Log("Back");
                break;
            case BTNType.Quit:
                Debug.Log("Quit");
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
