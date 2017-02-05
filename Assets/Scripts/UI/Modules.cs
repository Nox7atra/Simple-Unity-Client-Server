using UnityEngine;
using System.Collections.Generic;
namespace Nox7atra.UI
{
    public class Modules : MonoBehaviour
    {
        [SerializeField]
        List<GameObject> _ModulesPrefabs;
        [SerializeField]
        Transform _UIRoot;

        private List<GameObject> _ModulesPool;
        void Awake()
        {
            ModulesController.Instance.Modules = this;
            _ModulesPool = new List<GameObject>();
            for (int i = 0; i < _ModulesPrefabs.Count; i++)
            {
                _ModulesPool.Add(Instantiate(_ModulesPrefabs[i], _UIRoot, false));
            }
        }

        public T GetModuleByName<T>(string moduleName)
        {
            T mod = default(T);
            for (int i = 0; i < _ModulesPool.Count; i++)
            {
                if (moduleName == _ModulesPool[i].name)
                {
                    mod = _ModulesPool[i].GetComponent<T>();
                }
            }
            return mod;
        }
        public T GetModule<T>()
        {
            T mod = default(T);
            for (int i = 0; i < _ModulesPool.Count; i++)
            {
                mod = _ModulesPool[i].GetComponent<T>();
                if (mod != null)
                {
                    return mod;
                }
            }
            return mod;
        }
    }
}
