using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace TP2PROF
{
  class Application
  {
    public const int TARGET_FPS = 30;
    private RenderWindow window = null;
    private PacmanGame game = null;
    private Keyboard.Key lastKeyPressed = Keyboard.Key.Space;
    private void OnClose(object sender, EventArgs e)
    {
      RenderWindow window = (RenderWindow)sender;
      window.Close();
    }
    
    void OnKeyPressed(object sender, KeyEventArgs e)
    {
      lastKeyPressed = e.Code;
    }
    void OnKeyReleased(object sender, KeyEventArgs e)
    {
      lastKeyPressed = Keyboard.Key.Space;
    }
    public Application(string windowTitle)
    {
      game = new PacmanGame();
      window = new RenderWindow(new SFML.Window.VideoMode(PacmanGame.DEFAULT_GAME_WIDTH*PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH, PacmanGame.DEFAULT_GAME_HEIGHT*PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT), windowTitle, Styles.Titlebar);
      window.Closed += new EventHandler(OnClose);
      window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);
      window.KeyReleased += new EventHandler<KeyEventArgs>(OnKeyReleased);
      window.SetFramerateLimit(TARGET_FPS);
    }


    public void Run()
    {
      if(true == game.LoadGrid("Levels/level1.txt"))
      {
        window.SetActive();
        while ((lastKeyPressed != Keyboard.Key.Escape) && window.IsOpen && (game.Update(lastKeyPressed) == EndGameResult.NotFinished))
        {
          window.Clear(Color.Black);
          window.DispatchEvents();
          game.Draw(window);
          window.Display();
        }
      }
      else
      {
        System.Windows.Forms.MessageBox.Show("Format de fichier invalide.\n\nL'application va se terminer", "Erreur lors du chargement");
      }
    }
  }
}
