using UnityEngine.Events;

public class EventBus
{
    private static EventBus _instance;
    public static EventBus Instance
    {
        get
        {
            if (_instance == null)
                _instance = new EventBus();
            return _instance;
        }
    }

    public UnityEvent DataLoaded = new UnityEvent();

    public UnityEvent<int> BestScoreChanged = new UnityEvent<int>();

    public UnityEvent<float> VolumeChanged = new UnityEvent<float>();

    public UnityEvent AnyButtonClicked = new UnityEvent();

    public UnityEvent<Figure> FigurePlaced = new UnityEvent<Figure>();

    public UnityEvent GameDefeated = new UnityEvent();

    public UnityEvent FigureAvailabilityChecked = new UnityEvent();

    public UnityEvent<int> BlocksDestroyed = new UnityEvent<int>();
}
