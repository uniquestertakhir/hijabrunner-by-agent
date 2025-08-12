using System;
using System.Linq;
using UnityEditor;

public static class CIWebGLBuild
{
    // В Cloud Build мы вызовем этот метод: CIWebGLBuild.Build
    public static void Build()
    {
        try
        {
            // Берём список сцен из Build Settings
            var scenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Select(s => s.path)
                .ToArray();

            if (scenes.Length == 0)
                throw new Exception("No scenes in Build Settings. Add at least one scene.");

            // Папка вывода (Cloud Build сам упакует артефакты)
            const string output = "Builds/WebGL";

            var options = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = output,
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            };

            var report = BuildPipeline.BuildPlayer(options);
            var summary = report.summary;

            // Пишем в лог итог, чтобы в Cloud Build было видно
            Console.WriteLine($"[CI] Build result: {summary.result}, " +
                              $"duration: {summary.totalTime}, " +
                              $"errors: {summary.totalErrors}, warnings: {summary.totalWarnings}");

            if (summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
                throw new Exception("Unity Build failed. See errors above in the Editor log.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("[CI] FATAL: " + ex.Message);
            // Явно падаем, чтобы Cloud Build окрасил билд в красный и показал ошибки выше
            EditorApplication.Exit(1);
        }
    }
}
