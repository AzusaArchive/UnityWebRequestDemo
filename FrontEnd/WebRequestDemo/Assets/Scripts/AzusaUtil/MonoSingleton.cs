using UnityEngine;

namespace Azusa.Unity.Util
{
    /// <summary>
    /// Monobehaviour单例基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            //若实例不为空且不为this则销毁本物体，防止重复单例
            if (Instance && !ReferenceEquals(Instance, this))
            {
                Debug.LogError("单例物体发生多次实例化");
                Destroy(gameObject);
            }
            else
            {
                Instance = (T)this;
            }
        }
    }
}