using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts
{
    public class DebugUtil
    {
        public static GameObject CreateDot(GameObject prefab, Vector3 position)
        {
            var res = Object.Instantiate(prefab);
            res.transform.SetPositionAndRotation(position, res.transform.rotation);
            return res;
        }
    }
}
