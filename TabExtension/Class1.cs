using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using VRC.UI.Elements;
using WorldLoader.Mods;
using Il2CppGen.Runtime.Attributes;
using Il2CppGen.Runtime.Injection;
using System.Collections;
using VRC.UI.Core.Styles;
using WorldLoader.Utils;
using WorldLoader.Attributes;
using Object = UnityEngine.Object;

namespace TabExtension
{

    [Mod("Bad Tab Extension", "1.0", "Cyconi")]
    public class AppStart : UnityMod
    {
        private static GridLayoutGroup _gridLayoutGroup = null;
        private static GameObject _horizontalLayoutGroup;

        public override void OnInject()
        {

            WaitForUI().Start();
            base.OnInject();
        }

        private static IEnumerator WaitForUI()
        {
            while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>() == null) 
                yield return null;
            while (Object.FindObjectOfType<VRC.UI.Elements.QuickMenu>().transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM") == null) 
                yield return null;

            GameObject quickMenu = GameObject.Find("Canvas_QuickMenu(Clone)").gameObject; // gets qm
            quickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM").gameObject.GetComponent<BoxCollider>().extents = new Vector3(500, 500, 0.5f);
            _horizontalLayoutGroup = quickMenu.transform.Find("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup").gameObject;
            Object.Destroy(_horizontalLayoutGroup.GetComponent<HorizontalLayoutGroup>());
            AddGrid().Start();
        }

        private static IEnumerator AddGrid()
        {
            while (_gridLayoutGroup == null)
            {
                _gridLayoutGroup = _horizontalLayoutGroup.AddComponent<GridLayoutGroup>();
                yield return new WaitForSeconds(0.25f);
            }

            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = 7;
        }
    }
}
       