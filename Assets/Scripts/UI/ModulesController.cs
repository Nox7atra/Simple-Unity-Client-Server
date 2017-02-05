using UnityEngine;
using System.Collections;
namespace Nox7atra.UI
{
    public class ModulesController
    {
        #region singletone description
        private static ModulesController _Instance;

        public static ModulesController Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new ModulesController();
                }
                return _Instance;
            }
        }

        private ModulesController()
        {

        }
        #endregion

        public Modules Modules;
    }
}