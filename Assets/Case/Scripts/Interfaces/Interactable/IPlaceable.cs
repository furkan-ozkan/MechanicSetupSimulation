using UnityEngine;

public interface IPlaceable
{
    public void PlaceObject(Vector3 pos, Vector3 rot);
    public void UnplaceObject();
}