////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Snake.cs
//
// summary:	Implements the snake class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Snake
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   Values that represent directions. </summary>
    ///
    /// <remarks>   Jonna, 03-Jul-18. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public enum Direction
    {
        /// <summary>   An enum constant representing the down option. </summary>
        Down,
        /// <summary>   An enum constant representing the up option. </summary>
        Up,
        /// <summary>   An enum constant representing the right option. </summary>
        Right,
        /// <summary>   An enum constant representing the left option. </summary>
        Left
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A snake. </summary>
    ///
    /// <remarks>   Jonna, 03-Jul-18. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public class Snake
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the head x coordinate. </summary>
        ///
        /// <value> The head x coordinate. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int HeadX => m_Pieces.Last().X;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the head y coordinate. </summary>
        ///
        /// <value> The head y coordinate. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int HeadY => m_Pieces.Last().Y;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the length of the score. </summary>
        ///
        /// <value> The length of the score. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public int ScoreLength => m_Pieces.Count - 2;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the direction. </summary>
        ///
        /// <value> The direction. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Direction Direction { get; set; }

        /// <summary>   The pieces. </summary>
        private readonly Queue<Piece> m_Pieces;
        /// <summary>   The color. </summary>
        private readonly Brush m_Color;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="color">    The color. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Snake(Brush color)
        {
            m_Color = color;
            m_Pieces = new Queue<Piece>();
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
            foreach (var piece in m_Pieces)
            {
                piece.Draw(g);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determine if we can eat. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="a">    An int to process. </param>
        /// <param name="b">    An int to process. </param>
        /// <param name="food"> The food. </param>
        ///
        /// <returns>   True if we can eat, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool CanEat(int a, int b, Piece food)
        {
            return food.X == HeadX + a && food.Y == HeadY + b;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines if we can eats itself. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool EatsItself()
        {
            var i = 0;
            return m_Pieces.Any(piece => i++ != m_Pieces.Count - 1 && HeadY == piece.Y && HeadX == piece.X);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Query if this object contains the given a. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="a">    An int to process. </param>
        /// <param name="b">    An int to process. </param>
        ///
        /// <returns>   True if the object is in this collection, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public bool Contains(int a, int b)
        {
            return m_Pieces.Any(piece => piece.X == a && piece.Y == b);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Eats the given food. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="food"> The food. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Eat(Piece food)
        {
            m_Pieces.Enqueue(new Piece(food.X, food.Y, m_Color));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clears this object to its blank/initial state. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void Clear()
        {
            m_Pieces.Clear();
            m_Pieces.Enqueue(new Piece(0, 0, m_Color));
            m_Pieces.Enqueue(new Piece(0, 1, m_Color));
            Direction = Direction.Down;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Move to. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="a">    An int to process. </param>
        /// <param name="b">    An int to process. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public void MoveTo(int a, int b)
        {
            m_Pieces.Enqueue(new Piece(HeadX + a, HeadY + b, m_Color));
            m_Pieces.Dequeue();
        }
    }
}
