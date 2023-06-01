using UnityEngine;

namespace SniperProject
{
    public class DebugHelper
    {
        public static void LogError(string errorMessage)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string className = callingFrame.GetMethod().DeclaringType.Name;
            string methodName = callingFrame.GetMethod().Name;

            string fullErrorMessage = string.Format("ERROR - {0} {1}(): {2}", className, methodName, errorMessage);

            Object context = callingFrame.GetMethod().DeclaringType == typeof(DebugHelper) ? null : GameObject.FindObjectOfType(callingFrame.GetMethod().DeclaringType);

            if (context == null)
            {
                Debug.LogError(fullErrorMessage);
                return;
            }

            Debug.LogError(fullErrorMessage, context);
        }

        public static void LogWarning(string warningMessage)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string className = callingFrame.GetMethod().DeclaringType.Name;
            string methodName = callingFrame.GetMethod().Name;

            string fullWarningMessage = string.Format("Warning - {0} {1}(): {2}", className, methodName, warningMessage);

            Object context = callingFrame.GetMethod().DeclaringType == typeof(DebugHelper) ? null : GameObject.FindObjectOfType(callingFrame.GetMethod().DeclaringType);

            if (context == null)
            {
                Debug.LogWarning(fullWarningMessage);
                return;
            }

            Debug.LogWarning(fullWarningMessage, context);
        }

        public static void Log(string message, bool showBreadcrumbs = false)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string className = callingFrame.GetMethod().DeclaringType.Name;
            string methodName = callingFrame.GetMethod().Name;

            string fullMessage = message;
            if (showBreadcrumbs)
            {
                fullMessage = string.Format("{0} {1}(): {2}", className, methodName, message);
            }

            Object context = callingFrame.GetMethod().DeclaringType == typeof(DebugHelper) ? null : GameObject.FindObjectOfType(callingFrame.GetMethod().DeclaringType);

            if (context == null)
            {
                Debug.Log(fullMessage);
                return;
            }

            Debug.Log(fullMessage, context);
        }
    }
}
