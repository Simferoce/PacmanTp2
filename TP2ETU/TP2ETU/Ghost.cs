﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace TP2PROF
{
    //srobidas
  public class Ghost
  {
        /// <summary>
        /// Position du fantôme
        /// </summary>
        private Vector2i position;
        /// <summary>
        /// Accesseur de la position en colonne
        /// Propriété C#
        /// </summary>
        public int Column {get { if (position != null) return position.X; else return -1; } }
        /// <summary>
        /// Accesseur de la position en ligne
        /// Propriété C#
        /// </summary>
        public int Row
        { get { if (position != null) return position.Y; else return -1; } }
        /// <summary>
        /// Indique si le fantôme a été mangé par le pacman sans être
        /// retourné dans sa cage pour se régénérer
        /// </summary>
        private bool isWeak;
        /// <summary>
        /// Accesseur de la propriété isWeak
        /// Propriété C#
        /// </summary>
        public bool IsWeak { get { return isWeak; } }

        // Propriétés SFML pour l'affichage
        Texture ghostTextureNormal = new Texture("Assets/Ghost.bmp");
    Texture ghostTextureScared = new Texture("Assets/GhostScared.bmp");
    Texture ghostTextureWeak = new Texture("Assets/GhostWeak.bmp");
    Sprite ghostSprite = null;


    /// <summary>
    /// Identifiant du fantôme (entre 0 et 3 inclusivement) pour déterminer
    /// la couleur par défaut du fantôme dans la méthode Draw.
    /// </summary>
    private int ghostId = 0;

    /// <summary>
    /// Délai pour que le fantôme se mette en mouvement au début
    /// </summary>
    

    
    /// <summary>
    /// Pour l'affichage SFML
    /// </summary>
    static Color[] ghostColors = new Color[] { Color.Red, new Color(255, 192, 203), new Color(137, 207, 240), new Color(255, 127, 80) };
    
    
    /// <summary>
    /// Compteur du nombre d'instances de fantômes
    /// </summary>
    static int nbGhostCreated = 0;
    static Random rnd = new Random();

    public Ghost(int row, int column)
    {
            // Affectation de la position du fantôme  
            // Ne pas oublier de lancer une exception si les paramètres sont invalides
            if (row >= PacmanGame.DEFAULT_GAME_HEIGHT || column >= PacmanGame.DEFAULT_GAME_WIDTH || row < 0 || column < 0)
                throw new IndexOutOfRangeException();
            position.X = column;
            position.Y = row;
            // Affectation de la propriété ghostId.
            // Quelle serait la meilleure "valeur" à affecter ici???
            ghostId = nbGhostCreated;

            // Incrémenter ici la propriété servant à compter le nombre de fantômes créés
            // jusqu'à date
            nbGhostCreated++;

            // Initialisation SFML
            ghostSprite = new Sprite(ghostTextureNormal);
      ghostSprite.Origin = new Vector2f(ghostTextureNormal.Size.X / 2, ghostTextureNormal.Size.Y / 2);
    }

    /// <summary>
    /// Déplace le fantôme selon une direction donnée.
    /// </summary>
    /// <param name="direction">Direction dans laquelle on veut déplacer le fantôme</param>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le fantôme passe au travers des murs</param>
    // A COMPLETER MÉTHODE MOVE
    public bool Move(Direction direction, Grid grid)
        {
            bool fantomePeutSeDeplacer = true;
            int deplacemenEnY = direction == Direction.North ? -1 : direction == Direction.South ? 1: 0;
            int deplacementEnX = direction == Direction.East ? 1 : direction == Direction.West ? -1 :0;
            grid.SetGridElementAt(Row, Column, 0);
            if (grid.GetGridElementAt(Row + deplacemenEnY, Column + deplacementEnX) != PacmanElement.Mur)
            {
                grid.SetGridElementAt(Row + deplacemenEnY, Column + deplacementEnX, PacmanElement.Fantome);
                position.X = Column + deplacementEnX;
                position.Y = Row + deplacemenEnY;
            }
            else
            {
                fantomePeutSeDeplacer = false;
            }
                
            return fantomePeutSeDeplacer;
        }

    /// <summary>
    /// Affiche le fantôme dans la fenêtre de rendu.
    /// </summary>
    /// <param name="window">Fenêtre de rendu</param>
    /// <param name="isSuperPillActive">true si une super-pastille est active, false sinon</param>
    public void Draw(RenderWindow window, bool isSuperPillActive)
    {
      // Mise à jour de la texture du fantôme selon l'état du fantôme
      if (isSuperPillActive)
      {
        // État "effrayé", i.-e. le pacman a mangé une superpastille
        ghostSprite.Texture = ghostTextureWeak;
        ghostSprite.Color = Color.White;
      }
      else
      {
        // État "normal"
        ghostSprite.Texture = ghostTextureNormal;
        ghostSprite.Color = ghostColors[ghostId];
      }
      
      // ppoulin
      // A décommenter lorsqu'il sera possible d'accéder aux propriétés Column et Row
      // du fantôme
      ghostSprite.Position =    new Vector2f(PacmanGame.DEFAULT_GAME_ELEMENT_WIDTH * Column , 
                                         PacmanGame.DEFAULT_GAME_ELEMENT_HEIGHT * Row ) 
                            +  ghostSprite.Origin;
      window.Draw(ghostSprite);
    }

    /// <summary>
    /// Met à jour la position du fantôme
    /// </summary>
    /// <param name="grid">Grille de référence. Utilisée pour ne pas que le fantôme passe au travers des murs</param>
    /// <param name="pacmanPosition"></param>
    /// <param name="isSuperPillActive"></param>
    public void Update(Grid grid, Vector2i pacmanPosition, bool isSuperPillActive)
    {
            // ppoulin
            // A compléter 
            if (!isSuperPillActive)
                Move(PathFinder.FindShortestPath(grid, position.X, position.Y, pacmanPosition.X, pacmanPosition.Y), grid);
            //else TO DO
    }
  }
}
