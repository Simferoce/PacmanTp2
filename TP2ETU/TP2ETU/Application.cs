using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using System.Timers;
using SFML.Audio;
using SFML.Window;

namespace TP2PROF
{
  class Application
  {
    public const int TARGET_FPS = 30;
    private RenderWindow window = null;
    private PacmanGame game = null;

    // vbouchard et ppoulin
    // Booléen représentant le status de l'écran titre 
    private bool isTitleDrawn = false;
    // Proprités SFML Audio liés au son du début du jeu
    private static SoundBuffer beginning = new SoundBuffer("Assets/pacman_beginning.wav");
    private Sound beginningSound = new Sound(beginning);
    // Propriété SFML liés à l'écran titre du jeu
    private static Texture titleTexture = new Texture("Assets/Logo.bmp");
    private Sprite titleSprite =  new Sprite(titleTexture);

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
      // vbouchard et ppoulin

      // Fait jouer le son de début de jeu
      beginningSound.Play();
      if (true == game.LoadGrid("Levels/level1.txt"))
      {
        window.SetActive();
        while ((lastKeyPressed != Keyboard.Key.Escape) && window.IsOpen && (game.Update(lastKeyPressed) == EndGameResult.NotFinished))
        {
         
          window.Clear(Color.Black);
          window.DispatchEvents();
          game.Draw(window);
          window.Display();
         
          // Tant que le son de début de jeu n'est pas arrêter on affiche l'écran titre
          while (beginningSound.Status != SoundStatus.Stopped && window.IsOpen)
          {
            // Si l'écran titre n'a pas déjà été dessiner (pour ne pas l'afficher à chaque fois dans la boucle)
            if (isTitleDrawn == false)
            {
              // On affiche l'écran de jeu (sans ceci le labyrinthe et les pacmans ne s'affichent pas)
              window.Display();

              // On dessine l'écran titre sur le jeu
              window.Draw(titleSprite);

              // On affiche l'écran titre sur le jeu 
              window.Display();

              // L'écran titre à été afficher
              isTitleDrawn = true;
            }
            
            

          }
          // Le jeu à terminer d'avoir son écran de début et sa musique de début
          game.isBeginning = false;
        }

        // Si le jeu est terminer, onse débarasse du son de début (sans ceci nous avons une execption de mémoire de son perdue)
        if (game.Update(lastKeyPressed) != EndGameResult.NotFinished)
          beginningSound.Dispose();
      }
      else
      {
        System.Windows.Forms.MessageBox.Show("Format de fichier invalide.\n\nL'application va se terminer", "Erreur lors du chargement");
      }
    }

 
  }
}
