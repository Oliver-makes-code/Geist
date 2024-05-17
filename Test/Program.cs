using Geist;
using SDL;

GeistInit.Setup();

var window = GeistInit.CreateWindow(new() {
    windowTitle = "yippee"
});

while (!GeistInit.ShouldQuit()) {
    GeistInit.Update();
}
