using Photon.Pun;
using UnityEngine;

namespace GhostReplays
{
    public class ReplayGhost : MonoBehaviour
    {
        private GameObject replayGhost;

        private void Awake()
        {
            Plugin.instantiatedObjects.Add(this);

            //replayGhost = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            //Plugin.instantiatedObjects.Add(replayGhost);
            //replayGhost.transform.position = new Vector3(-10f, 1.8f, 70.50f);
            //replayGhost.GetComponent<Renderer>().material.color = new Color(1f, 0.5f, 0f); // Orange

            var prefabPool = new DefaultPool();
            replayGhost = prefabPool.Instantiate("Player", new Vector3(0f, 3f, 0f), Quaternion.identity);
        }
    }
}