using UnityEngine;

namespace SniperProject
{
    public class DebugHelper
    {
        private enum MessageType
        {
            Default,
            Warning,
            Error
        }


        #region Messages
        public static void LogError(string message)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string fullMessage = BuildMessage(callingFrame, message, MessageType.Error);

            Object context = GetContextFromCallingFrame(callingFrame);

            if (context == null)
            {
                Debug.LogError(fullMessage);
                return;
            }

            Debug.LogError(fullMessage, context);
        }

        public static void LogWarning(string message)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string fullMessage = BuildMessage(callingFrame, message, MessageType.Warning);

            Object context = GetContextFromCallingFrame(callingFrame);

            if (context == null)
            {
                Debug.LogWarning(fullMessage);
                return;
            }

            Debug.LogWarning(fullMessage, context);
        }

        public static void Log(string message, bool showBreadcrumbs = false)
        {
            var stackTrace = new System.Diagnostics.StackTrace();
            var callingFrame = stackTrace.GetFrame(1); // Skip current frame

            string fullMessage = message;
            if (showBreadcrumbs)
            {
                fullMessage = BuildMessage(callingFrame, message, MessageType.Warning);
            }

            Object context = GetContextFromCallingFrame(callingFrame);

            if (context == null)
            {
                Debug.Log(fullMessage);
                return;
            }

            Debug.Log(fullMessage, context);
        }
        #endregion 


        #region Utilties
        private static Object GetContextFromCallingFrame(System.Diagnostics.StackFrame callingFrame)
        {
            System.Type declaringType = callingFrame.GetMethod().DeclaringType;
            
            if (declaringType == typeof(DebugHelper))
            {
                return null;
            }

            if (!typeof(Object).IsAssignableFrom(declaringType))
            {
                return null;
            }

            Object context = GameObject.FindObjectOfType(declaringType);
            return context;
        }

        private static string BuildMessage(System.Diagnostics.StackFrame callingFrame, string message, MessageType messageType = MessageType.Default)
        {
            string className = callingFrame.GetMethod().DeclaringType.Name;
            string methodName = callingFrame.GetMethod().Name;

            string prefix;
            switch (messageType)
            {
                case MessageType.Error:
                    prefix = "ERROR";
                    break;
                case MessageType.Warning:
                    prefix = "Warning";
                    break;
                default:
                    prefix = "";
                    break;
            }

            string fullMessage = string.Format("{0} - {1} {2}(): {3}", prefix, className, methodName, message);
            return fullMessage;
        }
        #endregion
    }
}
