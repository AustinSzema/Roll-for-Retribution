using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MemoryRoom : MonoBehaviour
{
    [SerializeField] private float _roomFadeDuration = 5.0f;
    [SerializeField] private int _maxFadeablePriority = 4;
    [SerializeField] private bool _startHidden = false;
    private List<ObjectFade> _allObjectFades;
    public List<List<ObjectFade>> ObjectFadesByPriority;

    public int MinPriority { get; private set; }
    public int MaxPriority { get; private set; }

    private void Start()
    {
        if (_startHidden)
        {
            HideAll();
        }
        else
        {
            ShowAll();
        }
    }

    public int GetFadeObjectCount()
    {
        AggregateObjectFades();
        int fadeObjectCount = 0;
        for (int i = MinPriority; i <= MaxPriority; i++)
        {
            fadeObjectCount += ObjectFadesByPriority[i].Count;
        }
        return fadeObjectCount;
    }

    public void FadeInAll()
    {
        AggregateObjectFades();
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.In(_roomFadeDuration);
        }
    }

    public void FadeInAllInPriority(int priority)
    {
        AggregateObjectFades();
        foreach (ObjectFade objectFade in ObjectFadesByPriority[priority])
        {
            objectFade.In(_roomFadeDuration);
        }
    }

    public void FadeOutAll()
    {
        AggregateObjectFades();
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Out(_roomFadeDuration);
        }
    }

    public bool FadeOutRandomObjectInPriority(int priority)
    {
        int priorityFinal = Mathf.Clamp(priority, MinPriority, MaxPriority);

        List<ObjectFade> priorityList = ObjectFadesByPriority[priorityFinal];

        if (priorityList.Count <= 0)
        {
            return false;
        }

        int randomIndex = Random.Range(0, priorityList.Count);
        ObjectFade selectedObjectFade = priorityList[randomIndex];

        selectedObjectFade.Out();

        ObjectFadesByPriority[priorityFinal].Remove(selectedObjectFade);

        return true;
    }

    public bool FadeOutRandomObjectInPriority(int priority, float fadeDuration)
    {
        int priorityFinal = Mathf.Clamp(priority, MinPriority, MaxPriority);

        List<ObjectFade> priorityList = ObjectFadesByPriority[priorityFinal];

        if (priorityList.Count <= 0)
        {
            return false;
        }

        int randomIndex = Random.Range(0, priorityList.Count);
        ObjectFade selectedObjectFade = priorityList[randomIndex];

        selectedObjectFade.Out(fadeDuration);

        ObjectFadesByPriority[priorityFinal].Remove(selectedObjectFade);

        return true;
    }

    public void HideAll()
    {
        AggregateObjectFades();
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Hide();
        }
    }

    public void ShowAll()
    {
        AggregateObjectFades();
        foreach (ObjectFade objectFade in _allObjectFades)
        {
            objectFade.Show();
        }
    }

    private void AggregateObjectFades()
    {
        _allObjectFades = GetComponentsInChildren<ObjectFade>().ToList();
        ObjectFadesByPriority = new List<List<ObjectFade>>();

        MaxPriority = Mathf.Clamp(_allObjectFades.Max(objectFade =>
            objectFade.FadePriority), 0, _maxFadeablePriority);
        MinPriority = _allObjectFades.Min(objectFade =>
            objectFade.FadePriority);

        for (int i = MinPriority; i <= MaxPriority; i++)
        {
            List<ObjectFade> priorityList =
                _allObjectFades.Where(objectFade =>
                objectFade.FadePriority == i).ToList();
            ObjectFadesByPriority.Add(priorityList);
        }
    }
}
