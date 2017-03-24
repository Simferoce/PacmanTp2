using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace TP2PROF
{
  public class Grid
  {
        /// <summary>
        /// Grille logique du jeu.
        /// Tableau 2D de PacmanElement
        /// </summary>  
        // A compléter
        private PacmanElement[,] elements;
        /// <summary>
        /// Position de la cage des fantômes
        /// </summary>
        // A compléter
        private Vector2i ghostCagePosition;

        /// <summary>
        /// Accesseur du numéro de la ligne où se trouve la cage à fantômes
        /// Propriété C#
        /// </summary>
        // A compléter
        public int GhostCagePositionRow
        {
            get
            {
                if (elements == null)
                    return -1;
                return ghostCagePosition.Y;
            }
        }
        /// <summary>
        /// Accesseur du numéro de la colonne où se trouve la cage à fantômes
        /// Propriété C#
        /// </summary>
        // A compléter
        public int GhostCagePositionColumn
        {
            get
            {
                if (elements == null)
                    return -1;
                return ghostCagePosition.X;
            }
        }

        /// <summary>
        /// Position originale du pacman
        /// </summary>
        // A compléter
        private Vector2i pacmanOriginPosition;
        /// <summary>
        /// Accesseur du numéro de la ligne où se trouve le pacman au début
        /// Propriété c#
        /// </summary>
        // A compléter
        public int PacmanOriginPositionRow
        {
            get
            {
                if (elements == null)
                    return -1;
                return pacmanOriginPosition.Y;
            }
        }

        /// <summary>
        /// Accesseur du numéro de la colonne où se trouve le pacman au début
        /// Propriété C#
        /// </summary>
        // A compléter
        public int PacmanOriginPositionColumn
        {
            get
            {
                if (elements == null)
                    return -1;
                return pacmanOriginPosition.X;
            }
        }

        /// <summary>
        /// Accesseur de la hauteur
        /// Propriété C#
        /// </summary>
        // A compléter
        public int Height
        {
            get
            {
                return elements.GetLength(0);
            }
        }

        /// <summary>
        /// Accesseur de la largeur
        /// Propriété C#
        /// </summary>
        // A compléter
        public int Width { get { return elements.GetLength(1); } }


    /// <summary>
    /// Constructeur sans paramètre
    /// </summary>
    // A compléter
    public Grid()
        {
        }


    /// <summary>
    /// Charge un niveau à partir d'une chaine de caractères en mémoire.
    /// Voir l'énoncé du travail pour le format de la chaîne.
    /// </summary>
    /// <param name="content"> Le contenu du niveau en mémoire</param>
    /// <returns>true si le chargement est correct, false sinon</returns>
    public bool LoadFromMemory(string content)
    {
      bool retval = true;
            bool pacmanFound = false;
            bool cageFound = false;
            // A compléter selon les spécifications du travail
            try
            {
                string[] temp1 = content.Split(';');
                string[][] temp2 = new string[temp1.Length][];
                for(int i = 0;i < temp1.Length; i++)
                {
                    temp2[i] = temp1[i].Split(',');
                }
                for(int i =0; i < temp1.Length; i++)
                {
                    for(int j = 0; j < temp2[0].Length; j++)
                    {
                        elements[i, j] = (PacmanElement)int.Parse(temp2[i][j]);
                        if(elements[i, j] == (PacmanElement)3)
                        {
                            if (!pacmanFound)
                            {
                                pacmanOriginPosition = new Vector2i(i, j);
                                pacmanFound = true;
                            }
                            else
                            {
                                retval = false;
                            }
                        }
                        else if(elements[i, j] == (PacmanElement)6)
                        {
                            if(!cageFound)
                            {
                                ghostCagePosition = new Vector2i(i, j);
                                cageFound = true;
                            }
                            else
                            {
                                retval = false;
                            }
                        }
                    }
                }
            }
            catch
            {
                retval = false;
            }
            if (pacmanOriginPosition == null || ghostCagePosition == null)
            {
                retval = false;
            }
            else if(!(Height == 22 && Width == 21))
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
    // A compléter
    public PacmanElement GetGridElementAt(int row, int column)
        {
            if(row >= Height || column >= Width)
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
    // A compléter
    public void SetGridElementAt(int row, int column, PacmanElement element)
        {
            if (row >= Height || column >= Width)
            {
                throw new ArgumentOutOfRangeException();
            }
            elements[row, column] = element;
        }


  }
}
