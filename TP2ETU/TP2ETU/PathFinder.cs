using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2PROF
{
    public static class PathFinder
    {
        /// <summary>
        /// Initialise le tableau des coûts de déplacements, Le tableau est 
        /// initialisé à int.MaxValue partout sauf à l'endroit où se trouve le
        /// fantôme où le tableau est initialisé à 0.
        /// </summary>
        /// <param name="aGrid">La grille du jeu: pour connaitre les dimensions attendues</param>
        /// <param name="fromX">La position du pacman en colonne</param>
        /// <param name="fromY">La position du pacman en ligne</param>
        /// <returns>Le tableau initialisé correctement</returns>
        static public int[,] InitCosts(Grid aGrid, int fromX, int fromY)
        {

            int[,] costs = new int[aGrid.Height, aGrid.Width];
            costs[fromY, fromX] = 0;
            for (int i = 0; i < costs.GetLength(0); i++)
            {
                for (int j = 0; j < costs.GetLength(1); j++)
                {
                    if ((j != fromX) || (i != fromY))
                        costs[i, j] = int.MaxValue;
                }
            }

            return costs;
        }

        /// <summary>
        /// Détermine le premier déplacement nécessaire pour déplacer un objet de la position (fromX, fromY)
        /// vers la position (toX, toY). 
        /// <param name="aGrid">La grille du jeu: pour connaitre les positions des murs</param>
        /// <param name="fromX">La position de départ en colonne</param>
        /// <param name="fromY">La position de départ en ligne</param>
        /// <param name="toX">La position cible en colonne</param>
        /// <param name="toY">La position cible en ligne</param>
        /// <remark>Typiquement, la position (fromX, fromY) est celle du fantôme tandis 
        /// que la position (toX, toY) est celle du pacman.</remark>
        /// <returns>La direction dans laquelle on doit aller. Direction.None si l'on
        /// est déjà rendu ou Direction.Undefined s'il est impossible d'atteindre la cible</returns>
        /// </summary>
        public static Direction FindShortestPath(Grid aGrid, int fromX, int fromY, int toX, int toY)
        {

            int[,] costs = InitCosts(aGrid, fromX, fromY);

            ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

            return FindDirection(costs, toX, toY);
        }

        /// <summary>
        /// Calcule le nombre de déplacements requis pour aller de la position (fromX, fromY)
        /// vers la position (toX, toY). 
        /// <param name="aGrid">La grille du jeu: pour connaitre les positions des murs</param>
        /// <param name="fromX">La position de départ en colonne</param>
        /// <param name="fromY">La position de départ en ligne</param>
        /// <param name="toX">La position cible en colonne</param>
        /// <param name="toY">La position cible en ligne</param>
        /// <param name="costs">Le tableau des coûts à remplir</param>
        /// <remark>Typiquement, la position (fromX, fromY) est celle du fantôme tandis 
        /// que la position (toX, toY) est celle du pacman.</remark>
        /// <remark>Cette méthode est récursive</remark>
        /// </summary>
        static public void ComputeCosts(Grid aGrid, int fromX, int fromY, int toX, int toY, int[,] costs)
        {
            bool bas = false;
            bool haut = false;
            bool droite = false;
            bool gauche = false;


            if ((costs.GetLength(0) - 1 >= fromY + 1))
            {
                if (aGrid.GetGridElementAt(fromY + 1, fromX) != PacmanElement.Mur)
                    bas = true;
            }
            if ((0 <= fromY - 1))
            {
                if (aGrid.GetGridElementAt(fromY - 1, fromX) != PacmanElement.Mur)
                    haut = true;
            }
            if ((costs.GetLength(1) - 1 >= fromX + 1))
            {
                if (aGrid.GetGridElementAt(fromY, fromX + 1) != PacmanElement.Mur)
                    droite = true;
            }

            if ((0 <= fromX - 1))
            {
                if (aGrid.GetGridElementAt(fromY, fromX - 1) != PacmanElement.Mur)
                    gauche = true;
            }
            //Condition de Sortie (Quand le Fantome à trouver où est le Pacman)
            if ((fromX == toX) && (fromY == toY))
            {
                bas = false;
                haut = false;
                droite = false;
                gauche = false;

            }
            if (bas == true)
            {

                if ((costs[fromY, fromX] + 1 < costs[fromY + 1, fromX]))
                {

                    costs[fromY + 1, fromX] = (costs[fromY, fromX]) + 1;
                    ComputeCosts(aGrid, fromX, fromY + 1, toX, toY, costs);
                }

            }


            if (haut == true)
            {
                if ((costs[fromY, fromX] + 1 < costs[fromY - 1, fromX]))
                {

                    costs[fromY - 1, fromX] = costs[fromY, fromX] + 1;
                    ComputeCosts(aGrid, fromX, fromY - 1, toX, toY, costs);
                }
            }




            if (droite == true)
            {
                if (costs[fromY, fromX] + 1 < costs[fromY, fromX + 1])
                {
                    costs[fromY, fromX + 1] = costs[fromY, fromX] + 1;
                    ComputeCosts(aGrid, fromX + 1, fromY, toX, toY, costs);
                }
            }


            if (gauche == true)
            {
                if (costs[fromY, fromX] + 1 < costs[fromY, fromX - 1])
                {

                    costs[fromY, fromX - 1] = costs[fromY, fromX] + 1;
                    ComputeCosts(aGrid, fromX - 1, fromY, toX, toY, costs);
                }
            }


        }

        //srobidas
        /// <summary>
        /// Parcourt le tableau de coûts pour trouver le premier déplacement requis pour aller de la position (fromX, fromY)
        /// vers la position (toX, toY) (Methode englobante). 
        /// <param name="costs">Le tableau des coûts prédédemment calculés</param>
        /// <param name="targetX">La position cible en colonne</param>
        /// <param name="targetY">La position cible en ligne</param>
        /// <remark>Typiquement, la position (targetX, targetY) est celle du pacman.</remark>
        /// </summary>
        /// <returns>La direction dans laquelle on doit aller. Direction.None si l'on
        /// est déjà rendu ou Direction.Undefined s'il est impossible d'atteindre la cible</returns>
        public static Direction FindDirection(int[,] costs, int targetX, int targetY)
        {
            //Si le fantôme est déjà sur la case cible
            if (costs[targetY, targetX] == 0)
                return Direction.None;
            return RecurseFindDirection(costs, targetX, targetY);
        }
        /// <summary>
        /// Parcourt le tableau de coûts pour trouver le premier déplacement requis pour aller de la position (fromX, fromY)
        /// vers la position (toX, toY). 
        /// <param name="costs">Le tableau des coûts prédédemment calculés</param>
        /// <param name="targetX">La position cible en colonne</param>
        /// <param name="targetY">La position cible en ligne</param>
        /// <remark>Typiquement, la position (targetX, targetY) est celle du pacman.</remark>
        /// <remark>Cette méthode est récursive</remark>
        /// </summary>
        /// <returns>La direction dans laquelle on doit aller. Direction.None si l'on
        /// est déjà rendu ou Direction.Undefined s'il est impossible d'atteindre la cible</returns>
        private static Direction RecurseFindDirection(int[,] costs, int targetX, int targetY)
        {
            //Vérifie si la cause de gauche,droite,haut,bas est une case qui rapproche la cible du fantôme.
            if (targetX - 1 >= 0 && costs[targetY, targetX - 1] < costs[targetY, targetX])
            {
                //Si la case + le déplacement est égale à la position du fantôme, fini la recursivité et retourne le déplacement correspondant.
                if (costs[targetY, targetX - 1] == 0)
                    return Direction.East;
                else
                    return FindDirection(costs, targetX - 1, targetY);
            }
            if (targetX + 1 < costs.GetLength(1) && costs[targetY, targetX + 1] < costs[targetY, targetX])
            {
                if (costs[targetY, targetX + 1] == 0)
                    return Direction.West;
                else
                    return FindDirection(costs, targetX + 1, targetY);
            }
            if (targetY - 1 >= 0 && costs[targetY - 1, targetX] < costs[targetY, targetX])
            {
                if (costs[targetY - 1, targetX] == 0)
                    return Direction.South;
                else
                    return FindDirection(costs, targetX, targetY - 1);
            }
            if (targetY + 1 < costs.GetLength(0) && costs[targetY + 1, targetX] < costs[targetY, targetX])
            {
                if (costs[targetY + 1, targetX] == 0)
                    return Direction.North;
                else
                    return FindDirection(costs, targetX, targetY + 1);
            }
            //Si aucun chemin est valide.
            return Direction.Undefined;
        }
    }
}
