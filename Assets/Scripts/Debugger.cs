using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Monitors and displays debug logs in a Unity application.
/// </summary>
public class Debugger : MonoBehaviour
{
    /// <summary>
    /// The maximum number of messages to keep in the log queue.
    /// </summary>
    private const uint QSize = 5;

    /// <summary>
    /// Queue to store recent log messages.
    /// </summary>
    private readonly Queue<string> _logQueue = new Queue<string>();

    /// <summary>
    /// Initializes logging system.
    /// </summary>
    void Start()
    {
        Debug.Log("Logging system initialized.");
        
        // Test logging system
        TestLogging();
    }

    /// <summary>
    /// Registers the log handler when the script is enabled.
    /// </summary>
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    /// <summary>
    /// Unregisters the log handler when the script is disabled.
    /// </summary>
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    /// <summary>
    /// Handles incoming log messages.
    /// </summary>
    /// <param name="logString">The log message.</param>
    /// <param name="stackTrace">The stack trace if available.</param>
    /// <param name="type">The type of log message (e.g., error, warning).</param>
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        string formattedLog = "[" + type + "] : " + logString;
        _logQueue.Enqueue(formattedLog);

        if (type == LogType.Exception)
            _logQueue.Enqueue(stackTrace);

        // Keep log queue size within limit
        while (_logQueue.Count > QSize)
            _logQueue.Dequeue();
    }

    /// <summary>
    /// Renders the debug log messages on the screen.
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
        GUILayout.Label("\n" + string.Join("\n", _logQueue.ToArray()));
        GUILayout.EndArea();
    }
    
    /// <summary>
    /// Tests the logging system.
    /// </summary>
    void TestLogging()
    {
        StartCoroutine(LogWithDelay());
    }
    
    private IEnumerator LogWithDelay()
    {
        Debug.Log("This is a test log message.");
        yield return new WaitForSeconds(0.5f);

        Debug.LogWarning("This is a test warning message.");
        yield return new WaitForSeconds(0.5f);

        Debug.LogError("This is a test error message.");
        yield return new WaitForSeconds(0.5f);

        Debug.LogException(new Exception("This is a test exception message."));
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 10; i++)
        {
            Debug.Log("This is log message #" + i);
            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("This is the final test log message.");
        yield return new WaitForSeconds(0.5f);
        
        _logQueue.Clear();
    }

}
