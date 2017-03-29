using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace TP2PROF
{
    //srobidas
  public class Grid
  {
        /// <summary>
        /// Niveau chargé
        /// </summary>
        bool isLevelLoad = false;
        /// <summary>
        /// Grille logique du jeu.
        /// Tableau 2D de PacmanElement
        /// </summary>  
        private PacmanElement[,] elements;
        /// <summary>
        /// Position de la cage des fantômes
        /// </summary>
        private Vector2i ghostCagePosition;

        /// <summary>
        /// Accesseur du numéro de la ligne où se trouve la cage à fantômes
        /// Propriété C#
        /// </summary>
        public int GhostCagePositionRow
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return ghostCagePosition.Y;
            }
        }
        /// <summary>
        /// Accesseur du numéro de la colonne où se trouve la cage à fantômes
        /// Propriété C#
        /// </summary>
        public int GhostCagePositionColumn
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return ghostCagePosition.X;
            }
        }

        /// <summary>
        /// Position originale du pacman
        /// </summary>
        private Vector2i pacmanOriginalPosition;
        /// <summary>
        /// Accesseur du numéro de la ligne où se trouve le pacman au début
        /// Propriété c#
        /// </summary>
        public int PacmanOriginalPositionRow
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return pacmanOriginalPosition.Y;
            }
        }

        /// <summary>
        /// Accesseur du numéro de la colonne où se trouve le pacman au début
        /// Propriété C#
        /// </summary>
        public int PacmanOriginalPositionColumn
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return pacmanOriginalPosition.X;
            }
        }

        /// <summary>
        /// Accesseur de la hauteur
        /// Propriété C#
        /// </summary>
        public int Height
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return elements.GetLength(0);
            }
        }

        /// <summary>
        /// Accesseur de la largeur
        /// Propriété C#
        /// </summary>
        public int Width
        {
            get
            {
                if (!isLevelLoad)
                    return -1;
                return elements.GetLength(1);
            }
        }


    /// <summary>
    /// Constructeur sans paramètre
    /// </summary>
    public Grid()
        {
            elements = new PacmanElement[PacmanGame.DEFAULT_GAME_HEIGHT, PacmanGame.DEFAULT_GAME_WIDTH];
        }


    /// <summary>
    /// Charge un niveau à partir d'une chaine de caractères en mémoire.
    /// Voir l'énoncé du travail pour le format de la chaîne.
    /// </summary>
    /// <param name="content"> Le contenu du niveau en mémoire</param>
    /// <returns>true si le chargement est correct, false sinon</returns>
    public bool LoadFromMemory(string content)
    {
            isLevelLoad = true;
            for(int i =0; i < Height; i++)
            {
                for(int j =0; j < Width; j++)
                {
                    elements[i,j] = 0;
                }
            }
      bool retval = true;
            bool pacmanFound = false;
            bool cageFound = false;
            string[] temp1 = content.Split(';');
            string[][] temp2 = new string[temp1.Length][];
            try
            {

                for(int i = 0;i < temp1.Length; i++)
                {
                    temp2[i] = temp1[i].Split(',');
                }
                for(int i =0; i < temp1.Length; i++)
                {
                    for(int j = 0; j < temp2[0].Length; j++)
                    {
                        temp2[i][j] = temp2[i][j].Trim();
                        elements[i, j] = (PacmanElement)int.Parse(temp2[i][j]);
                        if(elements[i, j] == PacmanElement.PacMan)
                        {
                            if (!pacmanFound)
                            {
                                pacmanOriginalPosition = new Vector2i(j, i);
                                pacmanFound = true;
                            }
                            else
                            {
                                retval = false;
                            }
                        }
                        else if(elements[i, j] == PacmanElement.Cage)
                        {
                            if(!cageFound)
                            {
                                ghostCagePosition = new Vector2i(j, i);
                                cageFound = true;
                            }
                            else
                            {
                                retval = false;
                            }
                        }
                    }
                }
                if (pacmanOriginalPosition == null || ghostCagePosition == null)
                    retval = false;
                else if(!(temp1.Length == Height && temp2[0].Length == Width))
                    retval = false;
                else if(!pacmanFound)
                    retval = false;
                else if(!cageFound)
                    retval = false;
            }
            
            catch
            {
                retval = false;
            }

      return retval;
    }

    /// <summary>
    /// Retourne l'élément à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <returns>L'élément à la position spécifiée</returns>
    public PacmanElement GetGridElementAt(int row, int column)
        {
            if(row >= Height || column >= Width || row < 0 || column < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            return elements[row,column];
        }




    /// <summary>
    /// Modifie le contenu du tableau à la position spécifiée
    /// </summary>
    /// <param name="row">La ligne</param>
    /// <param name="column">La colonne</param>
    /// <param name="element">Le nouvel élément à spécifier</param>
    public void SetGridElementAt(int row, int column, PacmanElement element)
        {
            if (row >= Height || column >= Width || row < 0 || column < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            elements[row, column] = element;
        }


  }
}
