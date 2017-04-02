using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using SFML.Audio;
namespace TP2PROF
{
    //srobidas et vvbouchard
    //vvbouchard
  public class PacmanGame
  {
    /// <summary>
    /// Nombre de cases en largeur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_WIDTH = 21;

    /// <summary>
    /// Nombre de cases en hauteur dans le jeu du Pacman
    /// </summary>
    public const int DEFAULT_GAME_HEIGHT = 22;

    /// <summary>
    /// Largeur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_HEIGHT = 20;

    /// <summary>
    /// Hauteur de rendu d'un élément de jeu
    /// </summary>
    public const int DEFAULT_GAME_ELEMENT_WIDTH = 20;

    /// <summary>
    /// La grille principale de jeu. Elle est créée dans la méthode LoadGrid
    /// </summary>
    Grid grid = new Grid();

    /// <summary>
    /// Nombre de fantômes présents dans le jeu
    /// </summary>
    private const int NB_GHOSTS = 4;
    /// <summary>
    /// Les 4 fantômes du jeu
    /// </summary>
    private Ghost[] ghosts= new Ghost[NB_GHOSTS];
    /// <summary>
    /// Le pacman du jeu
    /// </summary>
    private Pacman pacman;
    /// <summary>
    /// Durée d'activation d'une superpastille (en secondes)
    /// </summary>
    private const int SUPERPILL_ACTIVATION_TIME = 5000;
    /// <summary>
    /// Durée de la fréquence entre chaques mouvements du pacman (en millisecondes)
    /// </summary>
    private const int MOVE_FREQUENCY = 220;
    /// <summary>
    /// Timer de la durée de la super pastille
    /// </summary>
    private  Timer tmSuperPastille = new Timer(SUPERPILL_ACTIVATION_TIME);
    /// <summary>
    ///  Timer de l'intervalle de temps entre chaque mouvements obligatoires du pacman
    /// </summary>
    private Timer tmMoveFrequency = new Timer(MOVE_FREQUENCY);
    /// <summary>
    /// Compteur représentant la fréquence de mouvement de chaques fantômes
    /// </summary>
    private int ghostMoveFrequency = 0;

    /// <summary>
    /// Accesseur permettant de savoir si une super pastille est active
    /// Propriété C#
    /// </summary>
    private bool SuperPillActive = false;

    // Propriétés SFML pour l'affichage des pastilles et super-pastilles
    const float SMALL_PILL_RADIUS = DEFAULT_GAME_ELEMENT_HEIGHT/8;
    const float SUPER_PILL_RADIUS = 2 * SMALL_PILL_RADIUS;
    CircleShape smallPillShape = new CircleShape(SMALL_PILL_RADIUS);
    CircleShape superPillShape = new CircleShape(SUPER_PILL_RADIUS);

    // Propriétés SFML pour l'affichage du labyrinthe
    Texture wallTexture = new Texture("Assets/Wall.bmp");
    Sprite wallSprite = null;
    // Propriétés SFML Audio pour l'application des différents sons du jeu
    SoundBuffer chomp = new SoundBuffer("Assets/pacman_chomp.wav");
    Sound chompSound = null;
    SoundBuffer death = new SoundBuffer("Assets/pacman_death.wav");
    Sound deathSound = null;
    SoundBuffer eatGhost = new SoundBuffer("Assets/pacman_eatghost.wav");
    Sound eatGhostSound = null;

    /// <summary>
    /// Booléen représentant le status de jeu (si le jeu est encore dans l'écran titre) 
    /// </summary>
    public bool isBeginning = true;
    /// <summary>
    /// Booléen représentant la fin de la trame sonore lors de la mort du Pacman
    /// </summary>
    private bool deadSoundWasPlayed = false;
    /// <summary>
    /// Booléen représentant la fin de l'affichage du Pacman lorsqu'il meurt
    /// </summary>
    private bool isDead = false;
    /// <summary>
    /// Direction représentant la direction présentement utilisée par le joueur
    /// </summary>
    Direction currentDirection=Direction.Undefined;
    /// <summary>
    /// Direction représentant la direction précédemment utilisée par le joueur
    /// </summary>
    Direction lastDirection = Direction.Undefined;
    /// <summary>
    /// Constructeur du jeu de Pacman
    /// </summary>
    public PacmanGame()
    { 
      // Initialisation des événements liés aux timers
      tmSuperPastille.Elapsed += new ElapsedEventHandler(SetSuperPastilleToFalse);
      tmMoveFrequency.Elapsed += new ElapsedEventHandler(UpdatePacman);

      // Initialisation SFML
      smallPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SMALL_PILL_RADIUS )/ 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SMALL_PILL_RADIUS )/ 2);
      superPillShape.Origin = new Vector2f((float)-(DEFAULT_GAME_ELEMENT_WIDTH- SUPER_PILL_RADIUS) / 2, -(float)(DEFAULT_GAME_ELEMENT_HEIGHT- SUPER_PILL_RADIUS) / 2);
      wallSprite = new Sprite(wallTexture);

      // Initialisation SFML Audio
      chompSound = new Sound(chomp);
      deathSound = new Sound(death);
      eatGhostSound = new Sound(eatGhost);

      // Départ du timer lié au mouvement du pacman
      tmMoveFrequency.Start();
      
      
      
    }
    #region vbouchard LoadGrid
    /// <summary>
    /// Charge un fichier de labyrinthe.
    /// </summary>
    /// <param name="path">Le fichier contenant la description du labyrinthe de jeu</param>
    /// <returns>true si le chargement s'est bien effectué, false sinon</returns>
    public bool LoadGrid(string path)
    {
      bool retval = System.IO.File.Exists(path);
      if (retval)
      {
        string fileContent = System.IO.File.ReadAllText(path);
        // Appelez la méthode LoadFromMemory ici
         retval = grid.LoadFromMemory(fileContent);

        // Si le chargement s'est correctement effectué
        if (true == retval)
        {
          int k = 0;
          for (int i = 0; i < grid.Height-1; i++)
          {
            for (int j = 0; j < grid.Width-1; j++)
            {
              // On parcourt la grille et, avec la méthode GetGridElementAt
              // On trouve les positions où il y a des fantômes
              if (grid.GetGridElementAt(i,j)== PacmanElement.Fantome)
              {
                // Quand on en trouve un, on crée le fantôme (new Ghost(...)) correspondant
                // et on l'enlève de la grille car dorénavant c'est l'objet de type Ghost
                // qui gère le déplacement
                ghosts[k] = new Ghost(i,j);
                grid.SetGridElementAt(i,j, PacmanElement.Rien);
                k++;
              }
            }
          }
          // Ensuite, on crée le pacman à la position spécifiée par la grille.
          pacman = new Pacman(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn);

          // Puis, comme pour les fantômes, on le retire de la grille. 
          // Sa position sera gérée par l'instance de la classe Pacman
          grid.SetGridElementAt(grid.PacmanOriginalPositionRow, grid.PacmanOriginalPositionColumn,PacmanElement.Rien);
        }
      }

      return retval;
    }
    #endregion
    /// <summary>
    /// Met à jour la logique de jeu
    /// </summary>
    /// <param name="key">La touche entrée par le joueur pour contrôle le pacman</param>
    /// <returns>EndGameResult.NotFinished si la partie est toujours en cours, EndGameResult.Win
    /// si le joueur a mangé toutes les pastilles ou EndGameResult.Losse si le joueur s'est fait
    /// mangé par un fantôme</returns>
    //vbouchard et srobidas
    public EndGameResult Update(Keyboard.Key key)
    {     
      #region vbouchard update 
      //
      if (isBeginning==false)
      { 
        ghostMoveFrequency++;
      
        // Déplacement du joueur
          if (currentDirection != lastDirection)
          {
            pacman.Move(currentDirection, grid);
            tmMoveFrequency.Start();
          }
        if (key == Keyboard.Key.Left)
          {
            if (grid.GetGridElementAt(pacman.Row, pacman.Column - 1) != PacmanElement.Mur )
              currentDirection = Direction.West;
          }
          else if (key == Keyboard.Key.Right)
          {
            if (grid.GetGridElementAt(pacman.Row,pacman.Column +1) != PacmanElement.Mur)
            currentDirection = Direction.East;
          }
          else if (key == Keyboard.Key.Up)
          {
            if (grid.GetGridElementAt(pacman.Row-1, pacman.Column) != PacmanElement.Mur)
              currentDirection = Direction.North;
          }
          else if (key == Keyboard.Key.Down)
          {
            if (grid.GetGridElementAt(pacman.Row+1, pacman.Column) != PacmanElement.Mur)
              currentDirection = Direction.South;
          }
    
          lastDirection = currentDirection;

     
        // Mise à jour des fantômes

        // Compteur représentant l'intervalle de temps après chaques mise à jours des fantômes
        if (ghostMoveFrequency==10)
          {
            //Va chercher chaques fantômes invidiuellement
            for (int i = 0; i < NB_GHOSTS; i++)
            {
              // Va mettre à jours chaque fantôme donnée à chaque fois que le compteur est rendu à une certaine fréquence 
              ghosts[i].Update(grid, new Vector2i(pacman.Column, pacman.Row), SuperPillActive, ghosts);

              // Va remettre le compteur à 0
              ghostMoveFrequency = 0;
            }
          }

        }
      #endregion

      // Gestion des collisions avec le pacman


      #region vBouchard pastille
      // Vérification du ramassage d'une pastille

      // Si la position du pacman est la même qu'une pastille
      if (grid.GetGridElementAt(pacman.Row, pacman.Column) == PacmanElement.Pastille)
      {
       // Vérifie que le son de mastication du pacman n'est pas arrêter avant de le jouer
       if( chompSound.Status == SoundStatus.Stopped)
       {
          chompSound.Play();         
       }
       // Enlève la pastille à la position du pacman comme si il l'avait mangé
       grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.Rien);
      }
      #endregion
      // Vérification de l'activation d'un superpill
      #region Superpill srobidas
      if (grid.GetGridElementAt(pacman.Row, pacman.Column) == PacmanElement.SuperPastille)
      {
        grid.SetGridElementAt(pacman.Row, pacman.Column, PacmanElement.Rien);
                SuperPillActive = true;
                tmSuperPastille.Start();
      }
      #endregion
      #region vbouchard fin partie et pacman mangé ou non

      // Validations de fin de partie

      //Il faut que la partie finisse s'il ne reste plus de pastille (le joueur à ganger)
      if ( CountNbPillsRemaining()==0)
      {
         return EndGameResult.Win;
      }

      // ou si le pacman a été mangé par un fantôme

      // Va chercher la position de chaques fantômes 
      for (int i = 0; i < NB_GHOSTS; i++)
      {
        // Va vérifier si le pacman est à la même positon que l'un des fantômes ou non
        if ((pacman.Column == ghosts[i].Column) && ( pacman.Row==ghosts[i].Row) ) 
        {
          // Va vérifier si le pacman à la posibilité de manger un fantôme (Superpastille)
          if (SuperPillActive)
          {
            // Vérifie que le son ne soit pas déjà entrain de jouer
            if (eatGhostSound.Status == SoundStatus.Stopped)
            {
              // Va joueur le son de la mastication du pacmana lorsqu'il mange un fantôme 
              eatGhostSound.Play();
              // (Remarque) la gestion de la faiblesse d'un fantôme est gérer à l'intérieur du fantôme      
            }
          }
          else 
          {
            //Arrête de faire fonctionner le timer pour que le pacman puisse arrêter de pouvoir bouger
            tmMoveFrequency.Close();

            //Fait disparaître le pacman puisqu'il est mort
            isDead = true;

            // Vérifie que le son ne soit pas déjà entrain de jouer (sans ceci le son jouerait sans cesse lors de l'update du jeu
            //  pusique la position  du pacman est la même que celle d'un fantôme)
            if (deadSoundWasPlayed==false)
            {
              // Fait jouer le son de mort du pacman
              deathSound.Play();

              // Le son de mort à bel et bien été joué
              deadSoundWasPlayed=true;            
            }

            //S'assure que le son de la mort du pacman soit bien completement terminer avant de gérer une fin de partie perdue
            if (deathSound.Status == SoundStatus.Stopped)


            return EndGameResult.Losse;
          }
        }
      }
      #endregion
      return EndGameResult.NotFinished;
    }


    /// <summary>
    /// Calcule le nombre de pastille non encore ramassées par le pacman
    /// </summary>
    /// <returns>Le nombre de pastille non encore ramassées</returns>
    #region vbouchard  CountPillsRemaining  
    private int CountNbPillsRemaining()
    {
      // le nombre de pastille restante sur la grille de jeu
      int nbpillsRemaining=0;
      // Va chercher le nombre de pastille non accumulé en vérifiant chaque élément de la grille de jeu
      for (int i = 0; i < grid.Height; i++)
      {
        for (int j = 0; j < grid.Width; j++)
        {
            // Si l'élément dans la grille correspond à une pastille
            if(grid.GetGridElementAt(i,j)==PacmanElement.Pastille)
            {
              // On ajoute 1 au compteur de pastille restante sur la grille de jeu
              nbpillsRemaining++;
            }
        }
      }
      return nbpillsRemaining;
    }
    #endregion

    /// <summary>
    /// Dessine les éléments du jeu à l'écran
    /// </summary>
    /// <param name="window">Le contexte de rendu</param>
    public void Draw(RenderWindow window)
    {
      for (int row = 0; row < grid.Height; row++)
      {
        for (int col = 0; col < grid.Width; col++)
        {
          // Pastille régulière
          if (grid.GetGridElementAt(row, col)==PacmanElement.Pastille)                  
          {
            smallPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(smallPillShape);     
          }
          // Super pastille
          else if (grid.GetGridElementAt(row, col) == PacmanElement.SuperPastille)
          {

            superPillShape.Radius =  SUPER_PILL_RADIUS;
            superPillShape.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(superPillShape);
          }
          // Mur
          else if (grid.GetGridElementAt(row, col) == PacmanElement.Mur)
          {
            wallSprite.Position = new Vector2f(col * DEFAULT_GAME_ELEMENT_WIDTH, row * DEFAULT_GAME_ELEMENT_HEIGHT);
            window.Draw(wallSprite);
          }
        }
      }

      // Les 4 fantômes
      for (int i = 0; i < NB_GHOSTS; i++)
      {
        if (ghosts[i] != null)        
        ghosts[i].Draw(window, SuperPillActive);
      }

      // Le pacman
      // si le pacman est mort on ne le dessine pas 
      if (null != pacman && isDead==false)
        pacman.Draw(window);
        
    }

    /// <summary>
        /// Met l'effet de la super pastille à nulle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
    //srobidas
    private void SetSuperPastilleToFalse(object sender, ElapsedEventArgs e)
        {
            SuperPillActive = false;
            tmSuperPastille.Stop();
        }
    /// <summary>
    /// Fait déplacer le pacman sur la grille de jeu 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdatePacman(object sender, ElapsedEventArgs e)
    {
      // Fait bouger le pacman dans la dernière direction obligatoirement après un certain temps
      pacman.Move(currentDirection, grid);  
    }
   
  }
}
