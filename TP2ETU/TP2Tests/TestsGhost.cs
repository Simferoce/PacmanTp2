using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2PROF;
namespace TP2Tests
{
  /// <summary>
  /// Description résumée pour TestsPacman
  /// </summary>
  [TestClass]
  public class TestsGhost
  {
// ppoulin
// A DECOMMENTER LORSQUE LE CODE AURA ÉTÉ PRODUIT    
//    /// <summary>
//    /// Teste la création d'un fantôme
//    /// </summary>
//    [TestMethod]
//    public void TestConstructeur01()
//    {
//      // Mise en place des données
//      // Appel de la méthode à tester
//      Ghost ghost = new Ghost(20, 10);

//      // Validation des résultats
//      Assert.AreEqual(10, ghost.Column);
//      Assert.AreEqual(20, ghost.Row);
//      // Clean-up
//    }

//    const string VALID_LEVEL_01 = @"
//1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
//1,1,0,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
//1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
//1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
//1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
//1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
//1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
//1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
//1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
//1,1,1,4,4,4,4,4,1,2,2,2,1,4,4,4,4,4,1,1,1;
//1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
//1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
//1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
//1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
//1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
//1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
//1,1,4,4,1,4,4,4,4,4,3,1,4,4,4,4,1,4,4,1,1;
//1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
//1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
//1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
//1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
//1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";

//    /// <summary>
//    /// Teste le déplacement invalide du fantôme vers la droite
//    /// </summary>
//    [TestMethod]
//    public void TestMoveInvalid01()
//    {
//      // Mise en place des données      
//      Grid grid = new Grid();
//      grid.LoadFromMemory(VALID_LEVEL_01);
//      Ghost ghost = new Ghost(1,18 );

//      // Appel de la méthode à tester
//      ghost.Move(Direction.East, grid);

//      // Validation des résultats
//      Assert.AreEqual(1, ghost.Row);
//      Assert.AreEqual(18, ghost.Column);
//      // Clean-up
//    }
//    /// <summary>
//    /// Teste le déplacement invalide du fantôme vers le haut
//    /// </summary>
//    [TestMethod]
//    public void TestMoveInvalid02()
//    {
//      // Mise en place des données      
//      Grid grid = new Grid();
//      grid.LoadFromMemory(VALID_LEVEL_01);
//      Ghost ghost = new Ghost(1, 18);

//      // Appel de la méthode à tester
//      ghost.Move(Direction.North, grid);

//      // Validation des résultats
//      Assert.AreEqual(1, ghost.Row);
//      Assert.AreEqual(18, ghost.Column);
//      // Clean-up
//    }
//    /// <summary>
//    /// Teste le déplacement invalide du fantôme vers le bas
//    /// </summary>
//    [TestMethod]
//    public void TestMoveInvalid03()
//    {
//      // Mise en place des données      
//      Grid grid = new Grid();
//      grid.LoadFromMemory(VALID_LEVEL_01);
//      Ghost ghost = new Ghost(20, 3);

//      // Appel de la méthode à tester
//      ghost.Move(Direction.South, grid);

//      // Validation des résultats
//      Assert.AreEqual(20, ghost.Row);
//      Assert.AreEqual(3, ghost.Column);
//      // Clean-up
//    }
    

    /// <summary>
    /// Teste le déplacement valide du fantôme vers la gauche.
    /// Vous devez positionner le fantôme à un endroit où il lui
    /// sera possible d'aller vers la gauche puis appeler la 
    /// méthode Move avec la bonne direction (West).
    /// </summary>
    [TestMethod]
    public void TestMoveValid01()
    {
      // Mise en place des données      



      // Appel de la méthode à tester
      

      // Validation des résultats
      


      // Clean-up
    }
    const string VALID_LEVEL_02 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,0,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,4,1,2,3,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,4,1,4,4,4,4,4,4,1,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";


    /// <summary>
    /// Teste le déplacement valide du fantôme vers un pacman
    /// Vous devez positionner le fantôme à côté du pacman et appeler la 
    /// méthode Move avec la bonne direction pour "foncer" dans la pacman
    /// </summary>
    [TestMethod]
    public void TestMoveValid02()
    {
      // Mise en place des données      



      // Appel de la méthode à tester
      

      // Validation des résultats
      

      // Clean-up
    }
  }
}
