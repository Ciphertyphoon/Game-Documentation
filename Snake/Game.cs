////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Game.cs
//
// summary:	Implements the game class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace Snake
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A game. </summary>
    ///
    /// <remarks>   Jonna, 03-Jul-18. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Game
    {
        /// <summary>   The width. </summary>
        private readonly int m_Width;
        /// <summary>   The height. </summary>
        private readonly int m_Height;
        /// <summary>   The snake. </summary>
        private readonly Snake m_Snake;
        /// <summary>   The food. </summary>
        private readonly Piece m_Food;
        /// <summary>   The random. </summary>
        private readonly Random m_Rnd;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="width">    The width. </param>
        /// <param name="height">   The height. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Game(int width, int height)
        {
            m_Width = width;
            m_Height = height;
            m_Snake = new Snake(Brushes.MidnightBlue);
            m_Rnd = new Random();
            m_Food = new Piece(0, 0, Brushes.Violet);
            Restart();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Restarts this object. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Restart()
        {
            m_Snake.Clear();
            GenerateFood();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Draws the given g. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="g">    The Graphics to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Draw(Graphics g)
        {
            m_Snake.Draw(g);
            m_Food.Draw(g);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the score. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <returns>   The score. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int GetScore()
        {
            return m_Snake.ScoreLength;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines if we can snake has grown. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool SnakeHasGrown()
        {
            switch (m_Snake.Direction)
            {
                case Direction.Down:
                    return TryEat(0, 1);
                case Direction.Up:
                    return TryEat(0, -1);
                case Direction.Right:
                    return TryEat(1, 0);
                case Direction.Left:
                    return TryEat(-1, 0);
            }
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Losts this object. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Lost()
        {
            return m_Snake.HeadX > m_Width || m_Snake.HeadX < 0 || m_Snake.HeadY > m_Height || m_Snake.HeadY < 0 || m_Snake.EatsItself();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Change snake d irection. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="direction">    The direction. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void ChangeSnakeDIrection(Direction direction)
        {
            m_Snake.Direction = direction;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Attempts to eat an int from the given int. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="a">    An int to process. </param>
        /// <param name="b">    An int to process. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private bool TryEat(int a, int b)
        {
            if (m_Snake.CanEat(a, b, m_Food))
            {
                m_Snake.Eat(m_Food);
                GenerateFood();
                return true;
            }
            m_Snake.MoveTo(a, b);
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Generates a food. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void GenerateFood()
        {
            var a = m_Rnd.Next(0, m_Width);
            var b = m_Rnd.Next(0, m_Height);
            if (m_Snake.Contains(a, b))
            {
                GenerateFood();
            }
            m_Food.X = a;
            m_Food.Y = b;
        }
    }
}
