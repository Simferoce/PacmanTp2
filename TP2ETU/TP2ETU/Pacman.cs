using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Timers;
using SFML.Graphics;
namespace TP2PROF
{
  //vbouchard
  public class Pacman
  {
    /// <summary>
    /// Position du pacman
    /// </summary>
    private Vector2i position;
    /// <summary>
    /// Accesseur de la position en colonne
    /// Propriété C#
    /// </summary>
    public int Column { get { if (position == null) { return -1; } else { return this.position.X; } } }
    /// <summary>
    /// Accesseur de la position en ligne
    /// Propriété C#
    /// </summary>
    public int Row { get { if (position == null) { return -1; } else { return this.position.Y; } } }

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
        position.Y = row;
        position.X = column;
      }
      catch (ArgumentOutOfRangeException)
      {
        if (row>PacmanGame.DEFAULT_GAME_HEIGHT  )
        {
          position.Y = PacmanGame.DEFAULT_GAME_HEIGHT;
        }

        if (row < 0)
        {
          position.Y = 0;
        }

        if (column> PacmanGame.DEFAULT_GAME_WIDTH)
        {
          position.X = PacmanGame.DEFAULT_GAME_WIDTH;
        }

        if (column < 0)
        {
          position.X = 0;
        }
      }
      // Initialisation des propriétés SFML
      pacmanSprite = new Sprite(pacmanTexture);
      pacmanSprite.Origin = new Vector2f(pacmanTexture.Size.X / 2, pacmanTexture.Size.Y / 2);
    }

    /// <summary>
    /// Déplace le pacman selon une direction donnée.
    /// </summary>
    /// <param name="direction">Direction dans laquelle on veut déplacer le pacman</param>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le pacman passe au travers des murs</param>
    public void Move(Direction direction, Grid grid)
    {
      // Si la direction est vers le nord
      if (direction == Direction.North)
      {
        // On s'assure que la direction vers le nord est bien disponible (qu'il n'y a pas de mur ou de cage à fantôme)
        if ((grid.GetGridElementAt(position.Y - 1, position.X) != PacmanElement.Mur) && (grid.GetGridElementAt(position.Y - 1, position.X) != PacmanElement.Cage))
        {
          // On change la position du pacman d'une case vers le nord
          position.Y = position.Y - 1;

          // On tourne le pacman pour qu'il fasse face à la direction qu'il à pris
          pacmanSprite.Rotation = -90;
        }
      }

      // Si la direction est vers le sud
      if (direction == Direction.South)
      {
        // On s'assure que la direction vers le sud est bien disponible (qu'il n'y a pas de mur ou de cage à fantôme)
        if ((grid.GetGridElementAt(position.Y + 1, position.X) != PacmanElement.Mur) && (grid.GetGridElementAt(position.Y + 1, position.X) != PacmanElement.Cage))
        {
          // On change la position du pacman d'une case vers le sud
          position.Y = position.Y + 1;

          // On tourne le pacman pour qu'il fasse face à la direction qu'il à pris
          pacmanSprite.Rotation = 90;
        }
      }

      // Si la direction est vers l'est
      if (direction == Direction.East)
      {
        // On s'assure que la direction vers l'est est bien disponible (qu'il n'y a pas de mur ou de cage à fantôme)
        if ((grid.GetGridElementAt(position.Y, position.X + 1) != PacmanElement.Mur) && (grid.GetGridElementAt(position.Y, position.X + 1) != PacmanElement.Cage))
        {
          // On change la position du pacman d'une case vers l'est
          position.X = position.X + 1;

          // On tourne le pacman pour qu'il fasse face à la direction qu'il à pris
          pacmanSprite.Rotation = 0;
        }
      }

      // Si la direction est vers l'ouest
      if (direction == Direction.West)
      {
        // On s'assure que la direction vers l'ouest est bien disponible (qu'il n'y a pas de mur ou de cage à fantôme)
        if ((grid.GetGridElementAt(position.Y, position.X - 1) != PacmanElement.Mur) && (grid.GetGridElementAt(position.Y, position.X - 1) != PacmanElement.Cage))
        {
          // On change la position du pacman d'une case vers l'ouest
          position.X = position.X - 1;

          // On tourne le pacman pour qu'il fasse face à la direction qu'il à pris
          pacmanSprite.Rotation = -180;
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
      pacmanSprite.Position = new Vector2f(PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH * Column,
                                          PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT * Row) + pacmanSprite.Origin;
      window.Draw(pacmanSprite);
    }

  }
}
