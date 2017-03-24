using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP2PROF;
namespace TP2Tests
{
  [TestClass]
  public class TestsGrid
  {
        // ppoulin
        // A DECOMMENTER LORSQUE LE CODE AURA ÉTÉ PRODUIT.
        /// <summary>
        /// Teste si toutes les propriétés ont été correctement initialisées
        /// </summary>
        [TestMethod]
        public void TestConstructeur01()
        {
            // Mise en place des données
            // Appel de la méthode à tester
            Grid grid = new Grid();

            // Validation des résultats
            Assert.AreEqual(-1, grid.Width);
            Assert.AreEqual(-1, grid.Height);
            Assert.AreEqual(-1, grid.GhostCagePositionColumn);
            Assert.AreEqual(-1, grid.GhostCagePositionRow);
            // Clean-up
        }

        /// <summary>
        /// Teste si le chargement d'un fichier vide échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent01()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory("");
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }


        const string INVALID_LEVEL_01 = @"
    ddffd;
    dfaf;
    dfadf;
    ";
        /// <summary>
        /// Teste si le chargement d'un fichier au format totalement invalide échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent02()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_01);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_02 = @"
    1,1,1,1,1;
    1,2,1,2,1;
    1,2,1,2,1,
    ";
        /// <summary>
        /// Teste si le chargement d'un fichier au format invalide échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent03()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_02);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }


        const string INVALID_LEVEL_03 = @"
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
1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,4,1,4,4,4,4,4,3,4,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il manque une ligne échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent04()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_03);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }
        const string INVALID_LEVEL_04 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,0,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,1,2,2,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,1,4,4,4,4,4,3,4,4,4,4,4,1,4,4,1,1;
1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il manque une colonne échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent05()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_04);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_05 = @"
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
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il manque un élément dans une colonne échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent06()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_05);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_06 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,1,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
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
1,1,4,4,1,4,4,4,4,4,0,4,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,1,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il n'y a pas de Pacman échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent07()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_06);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_07 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,1,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
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
1,1,1,4,1,4,1,4,1,1,3,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il y a 2 Pacman échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent08()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_07);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_08 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,1,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,4,1,2,2,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
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
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il n'y a pas de cage à fantômes échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent09()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_08);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }

        const string INVALID_LEVEL_09 = @"
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1;
1,1,1,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,5,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,4,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,1,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,2,1,1,4,1,4,1,1,1,1,1;
1,1,1,4,4,4,4,4,1,2,6,2,1,4,4,4,4,4,1,1,1;
1,1,1,1,1,4,1,4,1,1,6,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,4,4,4,4,4,4,1,4,1,1,1,1,1;
1,1,1,1,1,4,1,4,1,1,1,1,1,4,1,4,1,1,1,1,1;
1,1,4,4,4,4,4,4,4,4,1,4,4,4,4,4,4,4,4,1,1;
1,1,4,1,1,4,1,1,1,4,1,4,1,1,1,4,1,1,4,1,1;
1,1,4,4,1,4,4,4,4,4,1,4,4,4,4,4,1,4,4,1,1;
1,1,1,4,1,4,1,4,1,1,3,1,1,4,1,4,1,4,1,1,1;
1,1,4,4,4,4,1,4,4,4,1,4,4,4,1,4,4,4,4,1,1;
1,1,5,1,1,1,1,1,1,4,1,4,1,1,1,1,1,1,5,1,1;
1,1,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,4,1,1;
1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
        /// <summary>
        /// Teste si le chargement d'un fichier dans lequel il y a 2 cages à fantômes échoue
        /// </summary>
        [TestMethod]
        public void TestLoadInvalidContent10()
        {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(INVALID_LEVEL_07);
            // Validation des résultats
            Assert.IsFalse(result);
            // Clean-up
        }
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

    /// <summary>
    /// Test de chargement d'un niveau valide, sans erreur
    /// Utilisez la constante VALID_LEVEL_01 définie juste avant
    /// </summary>
    [TestMethod]
    public void TestLoadValidContent01()
    {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            bool result = grid.LoadFromMemory(VALID_LEVEL_01);
            // Validation des résultats
            Assert.IsTrue(result);
      // Clean-up
    }

    /// <summary>
    /// Teste si les éléments chargés sont corrects
    /// Ici la position de la cage des fantômes 
    /// à l'aide de la méthode GetGridElementAt
    /// Vous devez valider que la cage des fantômes est
    /// au bon endroit
    /// </summary>
    [TestMethod]
    public void TestGetElementAt01()
    {
            // Mise en place des données
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            // Appel de la méthode à tester

            // Validation des résultats
            Assert.AreEqual(6,grid.GetGridElementAt(11,10));
      // Clean-up
    }


    /// <summary>
    /// Teste si les éléments chargés sont corrects
    /// Ici quelques super-pills à l'aide de GetGridElementAt
    /// Vous devez vous assurer que les super-pastilles sont aux bons endroits
    /// à l'aide de la méthode GetGridElementAt
    /// </summary>
    [TestMethod]
    public void TestGetElementAt02()
    {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            // Validation des résultats
            Assert.AreEqual(5, grid.GetGridElementAt(2,2));
            Assert.AreEqual(5, grid.GetGridElementAt(2, 18));
            Assert.AreEqual(5, grid.GetGridElementAt(19, 2));
            Assert.AreEqual(5, grid.GetGridElementAt(19, 18));

            // Clean-up
        }

    /// <summary>
    /// Teste si les éléments chargés sont corrects
    /// Ici  quelques pastilles "régulières".
    /// Vous devez vous assurer que les pastilles sont aux bons endroits
    /// à l'aide de la méthode GetGridElementAt. Faites 5-4 vérifications.
    /// </summary>
    [TestMethod]
    public void TestGetElementAt03()
    {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            grid.LoadFromMemory(VALID_LEVEL_01);
            // Validation des résultats
            Assert.AreEqual(4, grid.GetGridElementAt(1,3));
            Assert.AreEqual(4, grid.GetGridElementAt(1, 4));
            Assert.AreEqual(4, grid.GetGridElementAt(1, 5));
            Assert.AreEqual(4, grid.GetGridElementAt(1, 6));
            Assert.AreEqual(4, grid.GetGridElementAt(1, 7));
            // Clean-up
        }



    /// <summary>
    /// Teste l'accès à un élément inexistant à l'extérieur de la grille
    ///                                       --------------------------
    /// Attention!  Votre méthode GetGridElementAt doit lancer une exception
    /// de type ArgumentOutOfRangeException si l'on tente d'accéder à un élément
    /// à l'extérieur de la grille. 
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestGetElementAt04()
    {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            grid.LoadFromMemory(VALID_LEVEL_01);
            // Validation des résultats
            grid.GetGridElementAt(3000,3000);

      // Clean-up
    }


    /// <summary>
    /// Teste la modification valide d'un élément de votre
    /// choix avec la méthode SetGridElementAt.
    /// Vous devez faire un SetGridElementAt avec une position
    /// et un type d'élément de votre choix et vous assurer
    /// par la suite que la modification a été faite dans la grille.
    /// </summary>
    [TestMethod]
    public void TestSetElementAt01()
    {
            // Mise en place des données
            Grid grid = new Grid();
            grid.LoadFromMemory(VALID_LEVEL_01);
            // Appel de la méthode à tester
            grid.SetGridElementAt(0,0,(PacmanElement)5);
            // Validation des résultats
            Assert.AreEqual(5, grid.GetGridElementAt(0,0));
      // Clean-up
    }
    /// <summary>
    /// Teste la modification invalide d'un élément à l'extérieur de la grille
    ///                                               ------------------------
    /// Attention!  Votre méthode SetGridElementAt doit lancer une exception
    /// de type ArgumentOutOfRangeException si l'on tente d'accéder à un élément
    /// à l'extérieur de la grille.                                              
    /// </summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TestSetElementAt02()
    {
            // Mise en place des données
            Grid grid = new Grid();
            // Appel de la méthode à tester
            grid.LoadFromMemory(VALID_LEVEL_01);
            // Validation des résultats
            grid.SetGridElementAt(3000, 3000, (PacmanElement)5);
            // Clean-up
        }
  }
}
