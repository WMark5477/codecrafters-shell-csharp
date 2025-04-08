using System.Diagnostics;

public static class Utils
{
    public static bool IsExecutable(string filePath)
    {
        if (OperatingSystem.IsWindows())
        {
            string ext = Path.GetExtension(filePath).ToLower();
            return ext == ".exe" || ext == ".bat" || ext == ".cmd";
        }
        else
            return new FileInfo(filePath).Exists && (new FileInfo(filePath).Attributes & FileAttributes.Directory) == 0;
    }

    public static string? FindCommandInPath(string command)
    {
        var paths = Environment.GetEnvironmentVariable("PATH")?.Split(Path.PathSeparator) ?? Array.Empty<string>();

        foreach (var path in paths)
        {
            string fullPath = Path.Combine(path, command);
            if (File.Exists(fullPath) && IsExecutable(fullPath))
                return fullPath;
        }

        return null;
    }
    
    public static void RunProcess(string path, string[] parameters)
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = Path.GetFileName(path),
            Arguments = string.Join(" ", parameters),
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        var process = Process.Start(processInfo);
        if (process == null)
        {
            Console.WriteLine($"Failed to start process: {path}");
            return;
        }

        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        process.WaitForExit();

        if (!string.IsNullOrEmpty(output))
            Console.WriteLine(output);

        if (!string.IsNullOrEmpty(error))
            Console.WriteLine(error);
    }
}