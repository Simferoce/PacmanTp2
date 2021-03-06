﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2PROF;
namespace TP2Tests
{
  /// <summary>
  /// Description résumée pour TestsPathFinder
  /// </summary>
  [TestClass]
  public class TestsPathFinder
  {
    const string VALID_LEVEL_01 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,0,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,4,1,2,2,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,4,1,4,4,4,4,4,3,4,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
    //vbouchard
    #region MANDAT1 vbouchard
    

    /// <summary>
    /// Test de l'initialisation des coûts.
    /// Vous devez vous assurer que la méthode InitCost initialise
    /// les valeurs du tableau à +infini partout sauf à l'endroit de départ
    /// (initialisation à 0)
    /// </summary>
    [TestMethod]
    public void TestInitCost_01()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
     
       // Appel de la méthode à tester
      int[,] costs= PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow);
     
       // Validations
      Assert.AreEqual(0, costs[aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow]);
      for (int i = 0; i < aGrid.Width-1; i++)
      {
        for (int j = 0; j < aGrid.Height-1; j++)
        {
          if ((i != aGrid.GhostCagePositionRow) && (j != aGrid.GhostCagePositionColumn))
            Assert.AreEqual(int.MaxValue,costs[i,j]);
        }
      }
    }

    /// <summary>
    /// Test de calcul des coûts dans la grille de base.
    /// Vous devez vous assurer que le calcul des coûts se
    /// fait correctement. Pour cela, faites l'appel à la méthode
    /// InitCosts puis ComputeCosts et faites quelques validations
    /// pour différents scénarios: chemins existants, chemins 
    /// inexistants (ex. à partie ou dans un mur!)
    /// </summary>
    [TestMethod]
    public void TestComputeCost_01()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      
      // Appel de la méthode à tester
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow);
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow, costs);

      // Validations
     
      for (int i = 0; i < costs.GetLength(0)-1; i++)
      {
        for (int j = 0; j < costs.GetLength(1)-1; j++)
        {

          // Chemins existants
          if (aGrid.GetGridElementAt(i, j) != PacmanElement.Mur)
          {
            Assert.AreNotEqual(int.MaxValue, costs[i, j]);
          }

          // Chemins inexistants
          if (aGrid.GetGridElementAt(i, j) == PacmanElement.Mur)
          {
            Assert.AreEqual(int.MaxValue, costs[i,j]);
          }


        }
      }    
    }
    
    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est le même que le point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_NoDisplacement()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow);
     
       // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow, costs);
      
      // Validations
      Assert.AreEqual(0, costs[aGrid.GhostCagePositionRow, aGrid.GhostCagePositionColumn]);
      for (int i = 0; i < aGrid.Width - 1; i++)
      {
        for (int j = 0; j < aGrid.Height - 1; j++)
        {
          if ((i != aGrid.GhostCagePositionRow) && (j != aGrid.GhostCagePositionColumn))
          {
            Assert.AreEqual(int.MaxValue, costs[i, j]);
          }
        }
      }
      
      // Cleanup
      costs = null;
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à gauche du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToEast()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow-1);
      // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow-1, aGrid.GhostCagePositionColumn+1, aGrid.GhostCagePositionRow-1, costs);
      // Validations
      Assert.AreEqual(1,costs[aGrid.GhostCagePositionRow - 1, aGrid.GhostCagePositionColumn + 1]);
      // Cleanup      
      costs = null;
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à droite du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToWest()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow - 1); ;
      // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow - 1, aGrid.GhostCagePositionColumn - 1, aGrid.GhostCagePositionRow - 1, costs);
      
      // Validations
      Assert.AreEqual(1, costs[aGrid.GhostCagePositionRow - 1, aGrid.GhostCagePositionColumn - 1]);
      
      // Cleanup
      costs = null;
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste en dessous du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToNorth()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow);
     
       // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow-1, costs);
     
       // Validations
      Assert.AreEqual(1, costs[aGrid.GhostCagePositionRow - 1, aGrid.GhostCagePositionColumn]);
      
      // Cleanup
      costs = null;
    }
    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste au dessus du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToSouth()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow-1);

      // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow-1, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow, costs);

      // Validations
      Assert.AreEqual(1, costs[aGrid.GhostCagePositionRow, aGrid.GhostCagePositionColumn]);

      // Cleanup
      costs = null;
    }
    

    
    /// <summary>
    /// Test de calcul d'une direction impossible (vers un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleToWall()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow);
      // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow+1, costs);
      // Validations
      Assert.AreEqual(int.MaxValue, costs[aGrid.GhostCagePositionRow+1, aGrid.GhostCagePositionColumn]);
      // Cleanup
      costs = null;
    }

    /// <summary>
    /// Test de calcul d'une direction impossible (à partie d'un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleFromWall()
    {
      // Mise en place des données
      Grid aGrid = new Grid();
      aGrid.LoadFromMemory(VALID_LEVEL_01);
      int[,] costs = PathFinder.InitCosts(aGrid, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow);
     
       // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid, aGrid.GhostCagePositionColumn, aGrid.GhostCagePositionRow+1, aGrid.PacmanOriginalPositionColumn, aGrid.PacmanOriginalPositionRow, costs);
      
      // Validations
      Assert.AreEqual(int.MinValue, costs[aGrid.GhostCagePositionRow, aGrid.GhostCagePositionColumn]);
     
       // Cleanup
      costs = null;
    }
    #endregion
    //srobids
    #region MANDAT2
    int[,] simpleCostArray1 = new int[,]{
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue },
      {int.MaxValue,  int.MaxValue, 7,            6,            7,            8,            int.MaxValue },
      {int.MaxValue,  3,            int.MaxValue, 5,            int.MaxValue, 9,            int.MaxValue },
      {int.MaxValue,  2,            int.MaxValue, 4,            int.MaxValue, int.MaxValue, int.MaxValue },
      {int.MaxValue,  1,            2,            3,            int.MaxValue, 7,            int.MaxValue },
      {int.MaxValue,  0,            int.MaxValue, 4,            int.MaxValue, 6,            int.MaxValue },
      {int.MaxValue,  1,            2,            3,            4,            5,            int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue }
    };
    /// <summary>
    /// Test de calcul du premier déplacement vers le nord.
    /// Vous devez aller vers la haut (ex. (x=1, y=4)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North01()
    {
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(simpleCostArray1, 1, 4));
    }
    /// <summary>
    /// Test de calcul du second déplacement vers le nord.
    /// Vous devez choisir une cible "vers le nord" plus complexe que celle juste 
    /// au-dessus.  La direction retournée par PathFinder.RecurseFindDirection 
    /// devrait être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North02()
    {
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(simpleCostArray1, 3, 4));
        }

    /// <summary>
    /// Test de calcul du troisième déplacement vers le nord
    /// Vous devez choisir une cible "vers le nord" plus complexe que celle juste 
    /// au-dessus et autre que pour le test précédent.  La direction 
    /// retournée par PathFinder.RecurseFindDirection devrait être le "nord".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_North03()
    {
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(simpleCostArray1, 2, 1));
        }
    /// <summary>
    /// Test de calcul du premier déplacement vers le sud
    /// Vous devez choisir une cible vers la bas (ex. (x=1, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South01()
    {
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(simpleCostArray1, 1, 6));
        }

    /// <summary>
    /// Test de calcul du second déplacement vers le sud
    /// Vous devez choisir une cible "vers le bas" plus complexe que celle juste 
    /// en-dessous.  La direction retournée par PathFinder.RecurseFindDirection 
    /// devrait être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.    
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South02()
    {
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(simpleCostArray1, 5, 4));
        }

    /// <summary>
    /// Test de calcul du troisième déplacement vers le sud
    /// Vous devez choisir une cible "vers le bas" plus complexe que celle juste 
    /// en-dessous et autre que pour le test précédent.  La direction 
    /// retournée par PathFinder.RecurseFindDirection devrait être le "sud".
    /// Utilisez le tableau simpleCostArray1 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_South03()
    {
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(simpleCostArray1, 3, 6));

        }

    int[,] simpleCostArray2 = new int[,]{
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,  int.MaxValue, int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, 7,            int.MaxValue, 13,           12,            11,           int.MaxValue },
      {int.MaxValue,  8,            int.MaxValue, 6,            int.MaxValue, 14,           int.MaxValue,  10,           int.MaxValue },
      {int.MaxValue,  7,            int.MaxValue, 5,            int.MaxValue, 15,           int.MaxValue,  9,            int.MaxValue },
      {int.MaxValue,  6,            5,            4,            int.MaxValue, 16,           int.MaxValue,  8,            int.MaxValue },
      {int.MaxValue,  5,            4,            3,            int.MaxValue, 17,           int.MaxValue,  7,            int.MaxValue },
      {int.MaxValue,  4,            int.MaxValue, 2,            int.MaxValue, 18,           int.MaxValue,  6,            int.MaxValue },
      {int.MaxValue,  3,            int.MaxValue, 1,            int.MaxValue, int.MaxValue, int.MaxValue,  5,            int.MaxValue },
      {int.MaxValue,  2,            1,            0,            1,            2,            3,             4,            int.MaxValue },
      {int.MaxValue,  int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue,  int.MaxValue, int.MaxValue }
    };
    /// <summary>
    /// Test de calcul du premier déplacement vers l'ouest
    /// Vous devez aller vers la gauche (ex. (x=1, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être l'"ouest".
    /// Utilisez le tableau simpleCostArray2 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_West01()
    {
            Assert.AreEqual(Direction.West, PathFinder.FindDirection(simpleCostArray2, 1, 6));


        }
    /// <summary>
    /// Test de calcul du premier déplacement vers l'est
    /// Vous devez aller vers la droite (ex. (x=5, y=6)).  La direction
    /// retournée par PathFinder.RecurseFindDirection devrait
    /// être l'"est".
    /// Utilisez le tableau simpleCostArray2 comme tableau des coûts.
    /// </summary>
    [TestMethod]
    public void TestRecurseFindDirection_East01()
    {

            Assert.AreEqual(Direction.East, PathFinder.FindDirection(simpleCostArray2, 5, 6));

        }


    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à gauche(x=2,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth01()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid,2,20);
            PathFinder.ComputeCosts(grid, 2, 20, 2, 2, cost);
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(cost, 2, 2));
        }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche(x=2,y=20) vers le haut au centre(x=11,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth02()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 20);
            PathFinder.ComputeCosts(grid, 2, 20, 11, 2, cost);
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(cost, 2, 2));

        }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à droite (x=18,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth03()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 20);
            PathFinder.ComputeCosts(grid, 2, 20, 18, 2, cost);
            Assert.AreEqual(Direction.North, PathFinder.FindDirection(cost, 2, 2));
        }

    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à gauche (x=2,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth01()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 2);
            PathFinder.ComputeCosts(grid, 2, 2, 2, 20, cost);
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(cost, 2, 20));


        }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas au centre (x=11,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth02()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 2);
            PathFinder.ComputeCosts(grid, 2, 2, 11, 20, cost);
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(cost, 11, 20));

        }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à droite(x=18,y=19).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth03()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 2);
            PathFinder.ComputeCosts(grid, 2, 2, 18, 19, cost);
            Assert.AreEqual(Direction.South, PathFinder.FindDirection(cost, 18, 19));
        }


    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche (x=3,y=3) vers la droite en haut (x=18,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast01()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 3, 3);
            PathFinder.ComputeCosts(grid, 3, 3, 18, 3, cost);
            Assert.AreEqual(Direction.East, PathFinder.FindDirection(cost, 18, 3));


        }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=3,y=3) au centre vertical (x=15,y=11).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast02()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 3, 3);
            PathFinder.ComputeCosts(grid, 3, 3, 15, 11, cost);
            Assert.AreEqual(Direction.East, PathFinder.FindDirection(cost, 15, 11));
        }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=2,y=3), vers le haut vers la droite (x=18,y=3)
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast03()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 2, 3);
            PathFinder.ComputeCosts(grid, 2, 3, 18, 3, cost);
            Assert.AreEqual(Direction.East, PathFinder.FindDirection(cost, 18, 3));
        }

    /// <summary>
    /// Test de calcul d'une direction vers l'ouest à partir 
    /// du haut de la grille à droite (x=18,y=3) vers la gauche en haut (x=2,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToWest01()
    {
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            int[,] cost = PathFinder.InitCosts(grid, 18, 3);
            PathFinder.ComputeCosts(grid, 18, 3, 2, 3, cost);
            Assert.AreEqual(Direction.West, PathFinder.FindDirection(cost, 2, 3));

        }
    
    #endregion
  }
}
