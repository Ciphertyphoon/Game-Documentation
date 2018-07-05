////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Window.cs
//
// summary:	Implements the window class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A window. </summary>
    ///
    /// <remarks>   Jonna, 03-Jul-18. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////

    public partial class Window : Form
    {
        /// <summary>   The width. </summary>
        private const int WIDTH = 12;
        /// <summary>   The height. </summary>
        private const int HEIGHT = 16;
        /// <summary>   The score string. </summary>
        private const string SCORE_STRING = "Score: {0}";
        /// <summary>   The background color. </summary>
        private readonly Color m_BackgroundColor = Color.CornflowerBlue;
        /// <summary>   The game. </summary>
        private readonly Game m_Game;
        /// <summary>   The game field. </summary>
        private readonly Bitmap m_GameField;
        /// <summary>   The game graphics. </summary>
        private readonly Graphics m_GameGraphics;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public Window()
        {
            InitializeComponent();
            m_GameField = new Bitmap(WIDTH * Piece.SIDE, HEIGHT * Piece.SIDE);
            m_GameGraphics = Graphics.FromImage(m_GameField); m_GameGraphics.PageUnit = GraphicsUnit.Pixel;
            ClientSize = new Size(m_GameField.Width, m_GameField.Height + m_RestartBtn.Height);
            m_Game = new Game(WIDTH, HEIGHT);
            m_Timer.Start();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the score. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void UpdateScore()
        {
            scoreLbl.Text = string.Format(SCORE_STRING, m_Game.GetScore());
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Raises the timer tick event. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnTimerTick(object sender, EventArgs e)
        {
            if (m_Game.SnakeHasGrown())
            {
                UpdateScore();
            }
            if (m_Game.Lost())
            {
                m_Timer.Stop();
                m_RestartBtn.Enabled = true;
            }
            Invalidate();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Raises the key event. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    m_Game.ChangeSnakeDIrection(Direction.Left);
                    break;
                case Keys.Right:
                    m_Game.ChangeSnakeDIrection(Direction.Right);
                    break;
                case Keys.Up:
                    m_Game.ChangeSnakeDIrection(Direction.Up);
                    break;
                case Keys.Down:
                    m_Game.ChangeSnakeDIrection(Direction.Down);
                    break;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Paints this window. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnPaint(object sender, PaintEventArgs e)
        {
            m_GameGraphics.Clear(m_BackgroundColor);
            m_Game.Draw(m_GameGraphics);
            e.Graphics.DrawImage(m_GameField, 0, m_RestartBtn.Height);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Raises the restart button click event. </summary>
        ///
        /// <remarks>   Jonna, 03-Jul-18. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information to send to registered event handlers. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        private void OnRestartBtnClick(object sender, EventArgs e)
        {
            m_RestartBtn.Enabled = false;
            m_Game.Restart();
            UpdateScore();
            m_Timer.Start();
        }
    }
}

