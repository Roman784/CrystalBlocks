using UnityEngine;

public class AdOfferMenu : PanelMenu
{
    private bool _hasBeenOffered;
    [SerializeField] private DefeatMenu _defeatMenu;

    private void Start()
    {
        DefeatChecker.Defeated.AddListener(Offer);
    }

    private void Offer()
    {
        if (_hasBeenOffered)
            Cancel();
        else
            OpenPanel();
    }

    public void WatchAd()
    {
        _hasBeenOffered = true;

        Debug.Log("Ad offer");

        ClosePanel();
    }

    public void Cancel()
    {
        ClosePanel();
        _defeatMenu.OpenPanel();
    }
}
