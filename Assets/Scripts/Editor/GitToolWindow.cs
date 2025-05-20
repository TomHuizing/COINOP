using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class GitToolWindow : EditorWindow
{
    private static readonly string projectFolder = Path.Combine( Application.dataPath, "../" );
    // private string output;

    // [MenuItem("Window/Git Tool")]
    // public static void ShowWindow()
    // {
    //     GetWindow<GitToolWindow>("Git Tool");
    // }

    // private void OnGUI()
    // {
    //     if (GUILayout.Button("Status"))
    //     {
    //         output = RunGitCommand("status"); // Example command
    //     }
    //     if (GUILayout.Button("Commit"))
    //     {
    //         TextInputPopup.Show("Commit", "Enter commit message:", "Message:", (message) =>
    //         {
    //             //output = RunGitCommand($"commit -m \"{message}\""); // Example command
    //             output = "COMMIT: " + message;
    //         });
    //     }


    //     GUILayout.Label("Git Output:");
    //     GUILayout.TextArea(output);
    // }

    [MenuItem("Tools/Git/Status", false, 0)]
    public static void GitStatus()
    {
        string output = RunGitCommand("status");
        EditorUtility.DisplayDialog("Git Status", output, "OK");
    }

    [MenuItem("Tools/Git/Init", false, 1)]
    public static void GitInit()
    {
        if(Directory.Exists(Path.Combine(projectFolder, ".git")))
        {
            EditorUtility.DisplayDialog("Git Init", "Git repository already initialized.", "OK");
            return;
        }
        string output = RunGitCommand("init");
        EditorUtility.DisplayDialog("Git Status", output, "OK");
    }

    [MenuItem("Tools/Git/Commit", false, 2)]
    public static void GitCommit()
    {
        string output = RunGitCommand("add .");
        output += "\n";
        TextInputPopup.Show("Commit", "Enter commit message:", "Message:", (message) =>
        {
            output += RunGitCommand($"commit -m \"{message}\"");
            EditorUtility.DisplayDialog("Git Commit", output, "OK");
        });
    }

    [MenuItem("Tools/Git/Push", false, 3)]
    public static void GitPush()
    {
        string output = RunGitCommand("push origin main");
        EditorUtility.DisplayDialog("Git Push", output, "OK");
    }

    [MenuItem("Tools/Git/Set Remote", false, 4)]
    public static void GitSetRemote()
    {
        TextInputPopup.Show("Set Remote", "Enter remote URL:", "URL:", (url) =>
        {
            string output = RunGitCommand($"remote add origin {url}");
            EditorUtility.DisplayDialog("Git Set Remote", output, "OK");
        });
    }

    private static string RunGitCommand(string command)
    {
        ProcessStartInfo startInfo = new()
        {
            FileName = "git",
            Arguments = command,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            WorkingDirectory = projectFolder // Set the working directory to the Unity project
        };

        using Process process = Process.Start(startInfo);
        string ret = process.StandardOutput.ReadToEnd();
        if(string.IsNullOrEmpty(ret))
            ret = process.StandardError.ReadToEnd();
        process.WaitForExit();
        return ret;
    }
}
