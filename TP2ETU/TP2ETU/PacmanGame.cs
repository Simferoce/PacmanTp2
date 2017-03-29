using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
namespace TP2PROF
{
  public class PacmanGame
  {
    /// <summary>
    /// Nombre de cases en largeur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_WIDTH = 21;

    /// <summary>
    /// Nombre de cases en hauteur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_HEIGHT = 22;

    /// <summary>
    /// Largeur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_HEIGHT = 20;

    /// <summary>
    /// Hauteur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_WIDTH = 20;

    /// <summary>
    /// La grille principale de jeu. Elle est créée dans la méthode LoadGrid
    /// </summary>
    // A COMPLETER
    Grid grid = new Grid();

    /// <summary>
    /// Nombre de fantômes présents dans le jeu
    /// </summary>
    // A COMPLETER
    private const int NB_GHOSTS = 4;
    /// <summary>
    /// Les 4 fantômes du jeu
    /// </summary>
    // A COMPLETER
    private Ghost[] ghosts= new Ghost[NB_GHOSTS];
    /// <summary>
    /// Le pacman du jeu
    /// </summary>
    // A COMPLETER
    private Pacman pacman;
    /// <summary>
    /// Durée d'activation d'une superpastille (en secondes)
    /// </summary>
    private const int SUPERPILL_ACTIVATION_TIME = 5000;
    Timer tmSuperPastille = new Timer(SUPERPILL_ACTIVATION_TIME);
    int j = 0;


    /// <summary>
    /// Accesseur permettant de savoir si une super pastille est active
    /// Propriété C#
    /// </summary>
    // A COMPLETER
    private bool SuperPillActive = false;

    // Propriétés SFML pour l'affichage des pastilles et super-pastilles
    const float SMALL_PILL_RADIUS = DEFAULT_GAME_ELEMENT_HEIGHT/8;
    const float SUPER_PILL_RADIUS = 2 * SMALL_PILL_RADIUS;
    CircleShape smallPillShape = new CircleShape(SMALL_PILL_RADIUS);
    CircleShape superPillShape = new CircleShape(SUPER_PILL_RADIUS);

    // Propriétés SFML pour l'affichage du labyrinthe
    Texture wallTexture = new Texture("Assets/Wall.bmp");
    Sprite wallSprite = null;
    SoundBuffer chomp = new SoundBuffer("Assets/pacman_chomp.wav");
    Sound chompSound = null;
    SoundBuffer death = new SoundBuffer("Assets/pacman_death.wav");
    Sound deathSound = null;
    SoundBuffer eatGhost = new SoundBuffer("Assets/pacman_eatghost.wav");
    Sound eatGhostSound = null;
    SoundBuffer beginning = new SoundBuffer("Assets/pacman_beginning.wav");
    public Sound beginningSound = null;
    bool deadSoundStopped = false;
    Direction currentDirection=Direction.Undefined;
    /// <summary>
    /// Constructeur du jeu de Pacman
    /// </summary>
    public PacmanGame()
    {
            // A COMPLETER   


            tmSuperPastille.Elapsed += new ElapsedEventHandler(SetSuperPastilleToFalse);
      // Initialisation SFML
      smallPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SMALL_PILL_RADIUS )/ 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SMALL_PILL_RADIUS )/ 2);
      superPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SUPER_PILL_RADIUS) / 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SUPER_PILL_RADIUS) / 2);
      wallSprite = new Sprite(wallTexture);
      chompSound = new Sound(chomp);
      deathSound = new Sound(death);
      eatGhostSound = new Sound(eatGhost);
      beginningSound = new Sound(beginning);
    }
    #region vbouchard LoadGrid
    /// <summary>
    /// Charge un fichier de labyrinthe.
    /// </summary>
    /// <param name="path">Le fichier contenant la description du labyrinthe de jeu</param>
    /// <returns>true si le chargement s'est bien effectué, false sinon</returns>
    public bool LoadGrid(string path)
    {
      bool retval = System.IO.File.Exists(path);
      if (retval)
      {
        string fileContent = System.IO.File.ReadAllText(path);
        // Appelez la méthode LoadFromMemory ici
        // A COMPLETER
         retval = grid.LoadFromMemory(fileContent);

        // Si le chargement s'est correctement effectué
        if (true == retval)
        {
          int k = 0;
          for (int i = 0; i < grid.Height-1; i++)
          {
            for (int j = 0; j < grid.Width-1; j++)
            {
              // On parcourt la grille et, avec la méthode GetGridElementAt
              // On trouve les positions où il y a des fantômes
              if (grid.GetGridElementAt(i,j)== PacmanElement.Fantome)
              {
                // Quand on en trouve un, on crée le fantôme (new Ghost(...)) correspondant
                // et on l'enlève de la grille car dorénavant c'est l'objet de type Ghost
                // qui gère le déplacement
                ghosts[k] = new Ghost(i,j);
                grid.SetGridElementAt(i,j, PacmanElement.Rien);
                k++;
              }
            }
          }
          // Ensuite, on crée le pacman à la position spécifiée par la grille.
          pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

          // Puis, comme pour les fantômes, on le retire de la grille. 
          // Sa position sera gérée par l'instance de la classe Pacman
          grid.SetGridElementAt(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn,PacmanElement.Rien);
        }
      }

      return retval;
    }
    #endregion
    /// <summary>
    /// Met à jour la logique de jeu
    /// </summary>
    /// <param name="key">La touche entrée par le joueur pour contrôle le pacman</param>
    /// <returns>EndGameResult.NotFinished si la partie est toujours en cours, EndGameResult.Win
    /// si le joueur a mangé toutes les pastilles ou EndGameResult.Losse si le joueur s'est fait
    /// mangé par un fantôme</returns>
    public EndGameResult Update(Keyboard.Key key)
    {
      #region vbouchard update (
      j++;
      // Déplacement du joueur
      if (deadSoundStopped == false)
      {
        if (key == Keyboard.Key.Left)
        {
          if (grid.GetGridElementAt(pacman.Row, pacman.Column - 1) != PacmanElement.Mur)
            currentDirection = Direction.West;
        }
        else if (key == Keyboard.Key.Right)
        {
          if (grid.GetGridElementAt(pacman.Row,pacman.Column +1) != PacmanElement.Mur)
          currentDirection = Direction.East;
        }
        else if (key == Keyboard.Key.Up)
        {
          if (grid.GetGridElementAt(pacman.Row-1, pacman.Column) != PacmanElement.Mur)
            currentDirection = Direction.North;
        }
        else if (key == Keyboard.Key.Down)
        {
          if (grid.GetGridElementAt(pacman.Row+1, pacman.Column) != PacmanElement.Mur)
            currentDirection = Direction.South;
        }
        
        pacman.Move(currentDirection, grid);
      }
      #endregion
      //vbouchard
      // Mise à jour des fantômes
      if (j==10)
        {
          for (int i = 0; i < NB_GHOSTS; i++)
          {
            
            ghosts[i].Update(grid, new Vector2i(pacman.Column, pacman.Row), SuperPillActive);
            j = 0;
          }
        }




      // Gestion des collisions avec le pacman

      #region vBouchard pastille
      // Vérification du ramassage d'une pastille
      if (grid.GetGridElementAt(pacman.Row, pacman.Column) == PacmanElement.Pastille)
      {
       if( chompSound.Status == SoundStatus.Stopped)
       {
          chompSound.Play();         
       }
       grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.Rien);
      }
      #endregion
      // Vérification de l'activation d'un superpill
      #region Superpill srobidas
      if (grid.GetGridElementAt(pacman.Row, pacman.Column) == PacmanElement.SuperPastille)
      {
        grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.Rien);
                SuperPillActive = true;
                tmSuperPastille.Start();
      }
      #endregion
      #region vbouchard fin partie et pacman mangé ou non
      // Validations de fin de partie
      //Il faut que la partie finisse s'il ne reste plus de pastille
      if ( CountNbPillsRemaining()==0)
      {
         return EndGameResult.Win;
      }

      // ou si le pacman a été mangé par un fantôme
      for (int i = 0; i < NB_GHOSTS-1; i++)
      {
        if ((pacman.Column == ghosts[i].Column) && pacman.Row==ghosts[i].Row) 
        {
          if (SuperPillActive)
          {
            if (eatGhostSound.Status == SoundStatus.Stopped)
            {
              eatGhostSound.Play();

            }
          }
          else 
          {
            if ((deathSound.Status == SoundStatus.Stopped) && deadSoundStopped==false)
            {
              deathSound.Play();
              deadSoundStopped=true;
              
            }
            if (deathSound.Status == SoundStatus.Stopped)
            return EndGameResult.Losse;
          }
        }
      }
      #endregion
      return EndGameResult.NotFinished;
    }


    /// <summary>
    /// Calcule le nombre de pastille non encore ramassées par le pacman
    /// </summary>
    /// <returns>Le nombre de pastille non encore ramassées</returns>
    #region vbouchard  CountPillsRemaining  
    private int CountNbPillsRemaining()
    {
      int nbpillsRemaining=0;
      for (int i = 0; i < grid.Height-1; i++)
      {
        for (int j = 0; j < grid.Width-1; j++)
        {
            if(grid.GetGridElementAt(i,j)==PacmanElement.Pastille)
            {
              nbpillsRemaining++;
            }
        }
      }
      return nbpillsRemaining;
    }
    #endregion

    /// <summary>
    /// Dessine les éléments du jeu à l'écran
    /// </summary>
    /// <param name="window">Le contexte de rendu</param>
    public void Draw(RenderWindow window)
    {
      // PPOULIN
      // A DECOMMENTER LORSQUE LES CLASSES AURONT ÉTÉ CODÉES
      for (int row = 0; row < grid.Height; row++)
      {
        for (int col = 0; col < grid.Width; col++)
        {
          // Pastille régulière
          if (grid.GetGridElementAt(row, col)==PacmanElement.Pastille)                  
          {
            smallPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(smallPillShape);     
          }
          // Super pastille
          else if (grid.GetGridElementAt(row, col) == PacmanElement.SuperPastille)
          {

            superPillShape.Radius =  SUPER_PILL_RADIUS;
            superPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(superPillShape);
          }
          // Mur
          else if (grid.GetGridElementAt(row, col) == PacmanElement.Mur)
          {
            wallSprite.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(wallSprite);
          }
        }
      }

      // Les 4 fantômes
      for (int i = 0; i < NB_GHOSTS; i++)
      {
        if (ghosts[i] != null)        
        ghosts[i].Draw(window, SuperPillActive);
      }

      // Le pacman
      if (null != pacman)
        pacman.Draw(window);
        
    }
       private void SetSuperPastilleToFalse(object sender, ElapsedEventArgs e)
        {
            SuperPillActive = false;
            tmSuperPastille.Stop();
        }
  }
}
