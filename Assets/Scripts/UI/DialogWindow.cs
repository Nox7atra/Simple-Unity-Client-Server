using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Nox7atra.UI
{
    public class DialogWindow : MonoBehaviour
    {
        [SerializeField]
        private Text _Title;
        [SerializeField]
        private Text _Content;

        protected System.Action[] Actions;

        public virtual void Show(string title, string content, System.Action[] actions) // 0 - for OnClose, 1 - for OnOk
        {
            gameObject.SetActive(true);
            _Title.text = title;
            _Content.text = content;
            Actions = actions;
        }
        public virtual void OnOK()
        {
            if (Actions != null)
            {
                if (Actions[1] != null)
                {
                    Actions[1]();
                }
            }
            gameObject.SetActive(false);
        }
        public virtual void OnClose()
        {
            if (Actions != null)
            {
                if (Actions[0] != null)
                {
                    Actions[0]();
                }
            }
            gameObject.SetActive(false);
        }
    }
}