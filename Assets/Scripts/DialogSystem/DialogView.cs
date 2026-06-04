using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogView : MonoBehaviour
{
    public Image Portret;
    public TextMeshProUGUI Text;
    public Button Scip;
    public RectTransform DialogBackground;
    private Vector3 _hidenPanelPosition;
    public void ShowDialogPanel()
    {
        _hidenPanelPosition = DialogBackground.anchoredPosition;
        DialogBackground.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
    }

    public void PutAwayDialogPanel()
    {
        DialogBackground.DOAnchorPos(_hidenPanelPosition, 1f).SetEase(Ease.InOutSine);
    }
}



