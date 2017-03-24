using System;
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
    #region MANDAT1
    

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
     int[,] costs= PathFinder.InitCosts(aGrid, 0, 0);
      // Validations
      Assert.AreEqual(0, costs[0, 0]);
      for (int i = 0; i < aGrid.Width-1; i++)
      {
        for (int j = 0; j < aGrid.Height-1; j++)
        {
          if ((i != 0) && (j != 0))
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
      int[,] costs= PathFinder.InitCosts(aGrid, 0, 0);

      // Appel de la méthode à tester
      PathFinder.ComputeCosts(aGrid,0,0,0,0,costs);

      // Validations
      // Chemins existants
      Assert.AreEqual(PacmanElement.None, costs[,] );




      // Chemins inexistants
       Assert.AreEqual(PacmanElement.Wall, PathFinder.costs[,]);


      
    }
    
    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est le même que le point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_NoDisplacement()
    {
      // Mise en place des données
      
      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à gauche du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToEast()
    {
      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup      

    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// juste à droite du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToWest()
    {
      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste en dessous du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToNorth()
    {
      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }
    /// <summary>
    /// Test de calcul d'une direction lorsque le point de départ
    /// est juste au dessus du point d'arrivée.
    /// </summary>
    [TestMethod]
    public void TestFindPath_ToSouth()
    {
      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }
    

    
    /// <summary>
    /// Test de calcul d'une direction impossible (vers un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleToWall()
    {
      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }

    /// <summary>
    /// Test de calcul d'une direction impossible (à partie d'un mur).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ImpossibleFromWall()
    {      // Mise en place des données

      // Appel de la méthode à tester

      // Validations

      // Cleanup
    }
    #endregion
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



    }


    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à gauche(x=2,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth01()
    {
      


    }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche(x=2,y=20) vers le haut au centre(x=11,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth02()
    {


    }
    /// <summary>
    /// Test de calcul d'une direction vers le nord à partir 
    /// du bas de la grille à gauche (x=2,y=20) vers le haut à droite (x=18,y=2).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToNorth03()
    {
      


    }

    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à gauche (x=2,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth01()
    {
      


    }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas au centre (x=11,y=20).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth02()
    {
      

    }
    /// <summary>
    /// Test de calcul d'une direction vers le sud à partir 
    /// du haut de la grille à gauche (x=2,y=2) vers le bas à droite(x=18,y=19).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToSouth03()
    {
      
    }


    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche (x=3,y=3) vers la droite en haut (x=18,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast01()
    {
      


    }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=3,y=3) au centre vertical (x=15,y=11).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast02()
    {



    }
    /// <summary>
    /// Test de calcul d'une direction vers l'est à partir 
    /// du haut de la grille à gauche vers la gauche (x=2,y=3), vers le haut vers la droite (x=18,y=3)
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToEast03()
    {
      
    }

    /// <summary>
    /// Test de calcul d'une direction vers l'ouest à partir 
    /// du haut de la grille à droite (x=18,y=3) vers la gauche en haut (x=2,y=3).
    /// </summary>
    [TestMethod]
    public void TestFindPath_ComplexToWest01()
    {


    }
    
    #endregion
  }
}
