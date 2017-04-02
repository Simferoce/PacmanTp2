using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2PROF
{
    public static class PathFinder
    {
      #region vbouchard
        /// <summary>
        /// Initialise le tableau des coûts de déplacements, Le tableau est 
        /// initialisé à int.MaxValue partout sauf à l'endroit où se trouve le
        /// fantôme où le tableau est initialisé à 0.
        /// </summary>
        /// <param name="aGrid">La grille du jeu: pour connaitre les dimensions attendues</param>
        /// <param name="fromX">La position du fantôme en colonne</param>
        /// <param name="fromY">La position du fantôme en ligne</param>
        /// <returns>Le tableau initialisé correctement</returns>
        static public int[,] InitCosts(Grid aGrid, int fromX, int fromY)
        {
          // Initialisation du tableau costs à la grandeur et à la hauteur de la grille           
          int[,] costs = new int[aGrid.Height, aGrid.Width];

          // On assigne 0 à la position du fantôme dans le tableau
          costs[fromY, fromX] = 0;

          // Pour chaques éléments dans le tableau
          for (int i = 0; i < costs.GetLength(0); i++)
          {
            for (int j = 0; j < costs.GetLength(1); j++)
            {
              // Si la position n'est pas celle du fantôme, On assigne l'infini à la position vérifier dans le tableau
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
          // On initialise le tableau des coûts 
          int[,] costs = InitCosts(aGrid, fromX, fromY);

          // On calcul le coût de chaques déplacements et on l'assigne dans le tableau des coûts
          ComputeCosts(aGrid, fromX, fromY, toX, toY, costs);

          // On retourne la direction que le fantôme doit prendre
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

          // Vers le bas 

          // Vérifie si la position en bas du fantôme en Y est bien comprise dans le tableau
          if ((costs.GetLength(0) - 1 >= fromY + 1))
          {
            // Vérifie si  la position en bas du fantôme en Y n'est pas à la même position qu'un mur 
            if (aGrid.GetGridElementAt(fromY + 1, fromX) != PacmanElement.Mur)
              // Le fantôme peut se déplacer vers le bas
              bas = true;
          }

          // Vers le haut
           
          // Vérifie si la position en haut du fantôme en Y est bien comprise dans le tableau
          if ((0 <= fromY - 1))
          {
             // Vérifie si  la position en haut du fantôme en Y n'est pas à la même position qu'un mur 
            if (aGrid.GetGridElementAt(fromY - 1, fromX) != PacmanElement.Mur)
              // Le fantôme peut se déplacer vers le haut 
              haut = true;
          }

          // Vers la droite

          // Vérifie si la position à droite du fantôme en X est bien comprise dans le tableau
          if ((costs.GetLength(1) - 1 >= fromX + 1))
          {
            // Vérifie si  la position à droite du fantôme en X n'est pas à la même position qu'un mur 
            if (aGrid.GetGridElementAt(fromY, fromX + 1) != PacmanElement.Mur)
              // Le fantôme peut se déplacer vers la droite
              droite = true;
          }

          // Vers la gauche

          // Vérifie si la position à gauche du fantôme en X est bien comprise dans le tableau
          if ((0 <= fromX - 1))
          {
            // Vérifie si  la position à droite du fantôme en X n'est pas à la même position qu'un mur 
            if (aGrid.GetGridElementAt(fromY, fromX - 1) != PacmanElement.Mur)
              // Le fantôme peut se déplacer vers la gauche
              gauche = true;
          }
          //Condition de Sortie (Quand le Fantôme à trouvé où est le Pacman)
          if ((fromX == toX) && (fromY == toY))
          {
            // Nous avons plus à vérifier les 4 directions dans ce tableau
            bas = false;
            haut = false;
            droite = false;
            gauche = false;
          }

          // Vérifie si la direction vers le bas est valide
          if (bas == true)
          {
            // Vérifie si le déplacement à la position du fantôme (ancienne position) augmenter de 1 est plus petit que
            // le déplacement vers la direction reçue (nouvelle position) (on cherche le plus petit déplacement)
            if ((costs[fromY, fromX] + 1 < costs[fromY + 1, fromX]))
            {
              // On assigne ce plus petit déplacement dans le tableau
              costs[fromY + 1, fromX] = (costs[fromY, fromX]) + 1;

              // Appel récursif pour découvrir un chemain pour une position potentiellement plus court
              ComputeCosts(aGrid, fromX, fromY + 1, toX, toY, costs);
            }
          }

          // Vérifie si la direction vers le haut est valide
          if (haut == true)
          {
            // Vérifie si le déplacement à la position du fantôme (ancienne position) augmenter de 1 est plus petit que
            // le déplacement vers la direction reçue (nouvelle position) (on cherche le plus petit déplacement)
            if ((costs[fromY, fromX] + 1 < costs[fromY - 1, fromX]))
            {
              // On assigne ce plus petit déplacement dans le tableau
              costs[fromY - 1, fromX] = costs[fromY, fromX] + 1;

              // Appel récursif pour découvrir un chemain pour une position potentiellement plus court
              ComputeCosts(aGrid, fromX, fromY - 1, toX, toY, costs);
            }
          }

          // Vérifie si la direction vers la droite est valide
          if (droite == true)
          {
            // Vérifie si le déplacement à la position du fantôme (ancienne position) augmenter de 1 est plus petit que
            // le déplacement vers la direction reçue (nouvelle position) (on cherche le plus petit déplacement)
            if (costs[fromY, fromX] + 1 < costs[fromY, fromX + 1])
            {
              // On assigne ce plus petit déplacement dans le tableau
              costs[fromY, fromX + 1] = costs[fromY, fromX] + 1;

              // Appel récursif pour découvrir un chemain pour une position potentiellement plus court
              ComputeCosts(aGrid, fromX + 1, fromY, toX, toY, costs);
            }
          }

          // Vérifie si la direction vers la gauche est valide
          if (gauche == true)
          {
            // Vérifie si le déplacement à la position du fantôme (ancienne position) augmenter de 1 est plus petit que
            // le déplacement vers la direction reçue (nouvelle position) (on cherche le plus petit déplacement)
            if (costs[fromY, fromX] + 1 < costs[fromY, fromX - 1])
            {
              // On assigne ce plus petit déplacement dans le tableau
              costs[fromY, fromX - 1] = costs[fromY, fromX] + 1;

              // Appel récursif pour découvrir un chemain pour une position potentiellement plus court
              ComputeCosts(aGrid, fromX - 1, fromY, toX, toY, costs);
            }
          }
        }
        #endregion

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
