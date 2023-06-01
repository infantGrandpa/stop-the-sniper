using UnityEngine;

namespace SniperProject
{
    public class DialogBoxCanvasBehaviour : MonoBehaviour
    {
        [SerializeField] RectTransform dialogBox;

        public static DialogBoxCanvasBehaviour Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType(typeof(DialogBoxCanvasBehaviour)) as DialogBoxCanvasBehaviour;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static DialogBoxCanvasBehaviour _instance;

        public bool CreateDialogBox()
        {
            return true;
        }
    }
}
