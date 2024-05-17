using System.Runtime.InteropServices;
using SDL;

namespace Geist;

public sealed class Window : IDisposable {
    public unsafe readonly SDL_Window *NativeWindow;

    internal unsafe Window(GeistInit.InitOptions options) {
        NativeWindow = SDL3.SDL_CreateWindow(options.windowTitle, options.initialWidth, options.initialHeight, SDL_WindowFlags.SDL_WINDOW_VULKAN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

        SDL3.SDL_SetWindowTitle(NativeWindow, options.windowTitle);
    }

    public unsafe void Dispose() {
        SDL3.SDL_DestroyWindow(NativeWindow);
    }
}
