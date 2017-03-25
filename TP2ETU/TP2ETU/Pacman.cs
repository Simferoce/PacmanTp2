using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace TP2PROF
{
  public class Pacman
  {
    /// <summary>
    /// Position du pacman
    /// </summary>
    private Vector2i positionPacman;
    /// <summary>
    /// Accesseur de la position en colonne
    /// Propriété C#
    /// </summary>
    public int Column { get { if (positionPacman == null) { return -1; } else { return this.positionPacman.X; } } }
    /// <summary>
    /// Accesseur de la position en ligne
    /// Propriété C#
    /// </summary>
    public int Row { get { if (positionPacman == null) { return -1; } else { return this.positionPacman.Y; } } }


    // Propriétés SFML pour l'affichage
    Texture pacmanTexture = new Texture("Assets/Pacman.bmp");
    Sprite pacmanSprite = null;

    /// <summary>
    /// Constructeur
    /// </summary>
    /// <param name="row">Ligne de départ du pacman</param>
    /// <param name="column">Colonne de départ du pacman</param>
    public Pacman(int row, int column)
    {

      // Affectation de la position du pacman 
      // Ne pas oublier de lancer une exception si les paramètres sont invalides
      try 
      {
        positionPacman.Y = row;
        positionPacman.X = column;
      }
      catch(ArgumentOutOfRangeException)
      {
        Console.WriteLine("fermeture inexpecté");
      }
      // Initialisation des propriétés SFML
      pacmanSprite = new Sprite(pacmanTexture);
      pacmanSprite.Origin = new Vector2f(pacmanTexture.Size.X/2, pacmanTexture.Size.Y / 2);
    }

    /// <summary>
    /// Déplace le pacman selon une direction donnée.
    /// </summary>
    /// <param name="direction">Direction dans laquelle on veut déplacer le pacman</param>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le pacman passe au travers des murs</param>
    // A COMPLETER MÉTHODE MOVE
    public void Move(Direction direction,Grid grid)
    {
      if (direction==Direction.North)
      {
        if (grid.GetGridElementAt(positionPacman.Y-1,positionPacman.X)!=PacmanElement.Mur)
        {
          grid.SetGridElementAt(positionPacman.Y - 1, positionPacman.X, PacmanElement.PacMan);
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X,PacmanElement.Rien);

        }
      }

      if (direction == Direction.South)
      {
        if (grid.GetGridElementAt(positionPacman.Y + 1, positionPacman.X) != PacmanElement.Mur)
        {
          grid.SetGridElementAt(positionPacman.Y + 1, positionPacman.X, PacmanElement.PacMan);
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X, PacmanElement.Rien);
        }
      }

      if (direction == Direction.East)
      {
        if (grid.GetGridElementAt(positionPacman.Y, positionPacman.X+1) != PacmanElement.Mur)
        {
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X+1, PacmanElement.PacMan);
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X, PacmanElement.Rien);
        }
      }

      if (direction == Direction.West)
      {
        if (grid.GetGridElementAt(positionPacman.Y, positionPacman.X-1) != PacmanElement.Mur)
        {
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X-1, PacmanElement.PacMan);
          grid.SetGridElementAt(positionPacman.Y, positionPacman.X, PacmanElement.Rien);
        }
      }
    }

    /// <summary>
    /// Affiche le pacman dans la fenêtre de rendu.
    /// </summary>
    /// <param name="window">Fenêtre de rendu</param>
    public void Draw(RenderWindow window)
    {
      // ppoulin
      // A décommenter lorsqu'il sera possible d'accéder aux propriétés Column et Row
      // du pacman  
      pacmanSprite.Position = new Vector2f(PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH* Column , 
                                          PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT*Row )+ pacmanSprite.Origin;
      window.Draw(pacmanSprite);
    }
  }
}
