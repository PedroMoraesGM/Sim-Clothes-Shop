using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent InteractEvent, SelectEvent, UnSelectEvent;

    public void Interact()
    {
        if (InteractEvent != null)
            InteractEvent.Invoke();
    }

    public void Select()
    {
        if (SelectEvent != null)
            SelectEvent.Invoke();
    }

    public void Unselect()
    {
        if (UnSelectEvent != null)
            UnSelectEvent.Invoke();
    }
}
