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
  public class TestsPacman
  {
    // ppoulin
    // A DECOMMENTER LORSQUE LE CODE AURA ÉTÉ PRODUIT

    //    /// <summary>
    //    /// Teste la création d'un pacman à une position donnée
    //    /// </summary>
    //    [TestMethod]
    //    public void TestConstructeur01()
    //    {
    //      // Mise en place des données
    //      // Appel de la méthode à tester
    //      Pacman pacman = new Pacman(20, 10);

    //      // Validation des résultats
    //      Assert.AreEqual(10, pacman.Column);
    //      Assert.AreEqual(20, pacman.Row);
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
    //    /// Teste le déplacement invalide du pacman vers la droite
    //    /// </summary>
    //    [TestMethod]
    //    public void TestMoveInvalid01()
    //    {
    //      // Mise en place des données      
    //      Grid grid = new Grid();
    //      grid.LoadFromMemory(VALID_LEVEL_01);
    //      Pacman pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

    //      // Appel de la méthode à tester
    //      pacman.Move(Direction.East, grid);

    //      // Validation des résultats
    //      Assert.AreEqual(grid.PacmanOriginalPositionRow, pacman.Row);
    //      Assert.AreEqual(grid.PacmanOriginalPositionColumn, pacman.Column);
    //      // Clean-up
    //    }
    //    /// <summary>
    //    /// Teste le déplacement invalide du pacman vers le haut
    //    /// </summary>
    //    [TestMethod]
    //    public void TestMoveInvalid02()
    //    {
    //      // Mise en place des données      
    //      Grid grid = new Grid();
    //      grid.LoadFromMemory(VALID_LEVEL_01);
    //      Pacman pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

    //      // Appel de la méthode à tester
    //      pacman.Move(Direction.North, grid);

    //      // Validation des résultats
    //      Assert.AreEqual(grid.PacmanOriginalPositionRow, pacman.Row);
    //      Assert.AreEqual(grid.PacmanOriginalPositionColumn, pacman.Column);
    //      // Clean-up
    //    }
    //    /// <summary>
    //    /// Teste le déplacement invalide du pacman vers le bas
    //    /// </summary>
    //    [TestMethod]
    //    public void TestMoveInvalid03()
    //    {
    //      // Mise en place des données      
    //      Grid grid = new Grid();
    //      grid.LoadFromMemory(VALID_LEVEL_01);
    //      Pacman pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

    //      // Appel de la méthode à tester
    //      pacman.Move(Direction.South, grid);

    //      // Validation des résultats
    //      Assert.AreEqual(grid.PacmanOriginalPositionRow, pacman.Row);
    //      Assert.AreEqual(grid.PacmanOriginalPositionColumn, pacman.Column);
    //      // Clean-up
    //    }

    //    const string VALID_LEVEL_02 = @"
    //1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
    //1,1,0,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
    //1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
    //1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
    //1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
    //1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
    //1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
    //1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
    //1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
    //1,1,1,4,4,4,4,4,1,2,3,2,1,4,4,4,4,4,1,1,1;
    //1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
    //1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
    //1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
    //1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
    //1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
    //1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
    //1,1,4,4,1,4,4,4,4,4,4,1,4,4,4,4,1,4,4,1,1;
    //1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
    //1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
    //1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
    //1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
    //1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
    //    /// <summary>
    //    /// Teste le déplacement invalide du pacman vers la cage à fantômes
    //    /// </summary>
    //    [TestMethod]
    //    public void TestMoveInvalid04()
    //    {
    //      // Mise en place des données      
    //      Grid grid = new Grid();
    //      grid.LoadFromMemory(VALID_LEVEL_02);
    //      Pacman pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

    //      // Appel de la méthode à tester
    //      pacman.Move(Direction.South, grid);

    //      // Validation des résultats
    //      Assert.AreEqual(grid.PacmanOriginalPositionRow, pacman.Row);
    //      Assert.AreEqual(grid.PacmanOriginalPositionColumn, pacman.Column);
    //      // Clean-up
    //    }

    /// <summary>
    /// Teste le déplacement valide du pacman vers la gauche
    /// Vous devez positionner le pacman à un endroit où il lui
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


    /// <summary>
    /// Teste le déplacement valide du pacman vers un fantôme
    /// Vous devez positionner le pacman à côté d'un fantôme et appeler la 
    /// méthode Move avec la bonne direction pour "foncer" dans la fantôme
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
