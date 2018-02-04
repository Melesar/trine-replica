using UnityEngine.EventSystems;

public interface IDeathListener : IEventSystemHandler
{
    void OnDeath ();
}