using SS14.Launcher.Errors;

namespace SS14.Launcher;

/// <summary>
/// How far the launcher got with bringing up its UI.
/// </summary>
internal enum StartupPhase
{
    /// <summary>Before we try to start the UI framework.</summary>
    Early,

    /// <summary>The UI framework is starting, but the launcher hasn't begun starting yet.</summary>
    StartingUi,

    /// <summary>The UI framework is up</summary>
    Running
}

/// <summary>
/// Tracks the <see cref="StartupPhase"/>, so a crash can be told apart by when it happened.
/// </summary>
internal static class StartupState
{
    private static volatile StartupPhase _phase = StartupPhase.Early;

    /// <summary>
    /// Call just before the UI framework starts.
    /// </summary>
    public static void UiStarting() => _phase = StartupPhase.StartingUi;

    /// <summary>
    /// Call once the UI framework is up and the app began starting.
    /// </summary>
    public static void UiStarted() => _phase = StartupPhase.Running;

    /// <summary>
    /// The code for a crash nothing else identified
    /// </summary>
    public static ErrorCode CrashCode =>
        _phase == StartupPhase.StartingUi ? ErrorCodes.GenNoGraphics : ErrorCodes.GenCrash;
}
