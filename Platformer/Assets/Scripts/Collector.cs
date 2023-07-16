using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
	[SerializeField] Collectible[] _collectibles;

    void Update()
    {
        foreach (var collectible in _collectibles)
        {
            if (collectible.isActiveAndEnabled)
                return;
        }

        Debug.Log("Got All Gems!");
    }
}
