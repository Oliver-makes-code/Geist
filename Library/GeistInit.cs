using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL;

namespace Geist;

public static class GeistInit {
    public struct InitOptions {
        public bool vsync = true;
        public int initialWidth = 320;
        public int initialHeight = 240;
        public string windowTitle = "Geist";

        public InitOptions() {}
    }

    private const SDL_InitFlags DefaultFlags = 0
        | SDL_InitFlags.SDL_INIT_VIDEO
        | SDL_InitFlags.SDL_INIT_AUDIO
        | SDL_InitFlags.SDL_INIT_JOYSTICK
        | SDL_InitFlags.SDL_INIT_GAMEPAD;

    private static bool wasInit = false;
    private static bool shouldQuit = false;

    public static void Setup()
        => TryInit();

    public static bool ShouldQuit()
        => shouldQuit;

    public static Window CreateWindow(InitOptions options)
        => new(options);

    public static void Update()
        => SDL3.SDL_PumpEvents();

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe int Event(nint _, SDL_Event *ev) {
        Console.WriteLine(ev->Type);
        if (ev->Type == SDL_EventType.SDL_EVENT_QUIT) {
            shouldQuit = true;
            return 1;
        }
        return 0;
    }

    internal unsafe static void TryInit() {
        if (wasInit)
            return;
        int err = SDL3.SDL_Init(DefaultFlags);
        if (err != 0)
            ThrowError();
        
        SDL3.SDL_SetEventFilter(&Event, 0);

        wasInit = true;
    }

    internal static void ThrowError() {
        var err = SDL3.SDL_GetError();
        if (err != null)
            throw new SdlException(err);
    }
}

public class SdlException(string? err) : Exception($"SDL Error: {err}");
