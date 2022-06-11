using SDL2;
using SandSimulator;

class Program
{
    static IntPtr window;
    static IntPtr renderer;
    static bool running = true;


    static WorldGrid worldGrid;

    public static void Main(string[] args)
    {
        worldGrid = new WorldGrid(20, 20);
        Setup();

        running = true;

        while (running)
        {
            PollEvent();
            Render();
            Thread.Sleep(500);
            worldGrid.Step();
        }
        CleanUp();
    }

    private static void CleanUp()
    {
        SDL.SDL_DestroyRenderer(renderer);
        SDL.SDL_DestroyWindow(window);
        SDL.SDL_Quit();
    }

    private static void Render()
    {
        SDL.SDL_SetRenderDrawColor(renderer, 122, 122, 122, 255);
        SDL.SDL_RenderClear(renderer);



        var w = worldGrid.Width;
        var h = worldGrid.Height;

        for(uint i =0; i < h; i++)
        {
            for(uint j =0; j < w; j++)
            {
                var cell = worldGrid.GetCell(i, j);
                var rect = new SDL.SDL_Rect
                {
                    x = (int)(i * 25),
                    y = (int)(j * 25),
                    w = 24,
                    h = 24
                };
                var colour = cell.GetColour();
                SDL.SDL_SetRenderDrawColor(renderer, colour[0], colour[1], colour[2], 255);
                // Draw a filled in rectangle.
                SDL.SDL_RenderFillRect(renderer, ref rect);
            }
        }
        SDL.SDL_RenderPresent(renderer);
    }

    private static void PollEvent()
    {
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
        {
            switch (e.type)
            {
                case SDL.SDL_EventType.SDL_QUIT:
                    running = false;
                    break;
            }
        }
    }

    private static void Setup()
    {
        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
        {
            Console.WriteLine($"There was an issue initilizing SDL. {SDL.SDL_GetError()}");
        }

        // Create a new window given a title, size, and passes it a flag indicating it should be shown.
        window = SDL.SDL_CreateWindow(
            "Piksel Pusha",
            SDL.SDL_WINDOWPOS_UNDEFINED,
            SDL.SDL_WINDOWPOS_UNDEFINED,
            500,
            500,
            SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

        if (window == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the window. {SDL.SDL_GetError()}");
        }

        // Creates a new SDL hardware renderer using the default graphics device with VSYNC enabled.
        renderer = SDL.SDL_CreateRenderer(window,
                                                -1,
                                                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                                                SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        if (renderer == IntPtr.Zero)
        {
            Console.WriteLine($"There was an issue creating the renderer. {SDL.SDL_GetError()}");
        }

        // Initilizes SDL_image for use with png files.
        if (SDL_image.IMG_Init(SDL_image.IMG_InitFlags.IMG_INIT_PNG) == 0)
        {
            Console.WriteLine($"There was an issue initilizing SDL2_Image {SDL_image.IMG_GetError()}");
        }
    }
}
