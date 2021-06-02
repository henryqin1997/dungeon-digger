using UnityEngine;

public class PlaceholderArtifact : Artifact
{
    protected override void OnPickUp()
    {
        Debug.Log("Picked up PlaceholderArtifact");
    }
}
