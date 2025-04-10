﻿using System.Diagnostics;
using System.Text;

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
        for (int i = 0; i < parameters.Length; i++)
        {
            if (parameters[i].Contains(' '))
            {
                parameters[i] = $"\'{parameters[i]}\'";
            }
        }
       
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

        Console.WriteLine("DEBUG1");
        Console.WriteLine($"null: {output == null} + string: {output == ""} + {output}");
        if (!string.IsNullOrEmpty(output))
            Console.WriteLine(output.Trim());
        Console.WriteLine("DEBUG2");
        process.WaitForExit();
    }

    public static string[] ParseInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<string>();
        input = input.Trim();
        var sb = new StringBuilder();
        var inSingleQuotes = false;
        var isSeparated = false;
        foreach (var c in input)
        {
            if (c == '\'')
            {
                isSeparated = false;
                inSingleQuotes = !inSingleQuotes;
                continue;
            }
            if (inSingleQuotes)
            {
                sb.Append(c);
            }
            else
            {
                if (c != ' ')
                {
                    isSeparated = false;
                    sb.Append(c);
                    continue;
                }
                if (!isSeparated)
                {
                    sb.Append('\n');
                    isSeparated = true;
                    continue;
                }
            }
        }

        if (inSingleQuotes)
        {
            return Array.Empty<string>();
        }
        var result = sb.ToString().Split('\n');
        return result;
    }
}