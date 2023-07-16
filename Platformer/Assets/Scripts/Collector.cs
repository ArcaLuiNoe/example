using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
	[SerializeField] Collectible[] _collectibles;

    void Update()
    {
        if (_collectibles.Any(t => t.gameObject.activeSelf == true))
            return;

        Debug.Log("Got All Gems!");
    }
}
