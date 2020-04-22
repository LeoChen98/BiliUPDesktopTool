using System.Drawing;
using System.Linq;

using Forms = System.Windows.Forms;

namespace BiliUPDesktopTool
{
    internal class ScreenHelper
    {
        #region Private Fields

        private static Screen[] screens;

        #endregion Private Fields

        #region Public Methods

        public static Screen FindScreenByPoint(Point pos)
        {
            if (screens == null) GetScreens();
            return screens.FirstOrDefault(s => (s.startPoint.X < pos.X && s.startPoint.X + s.size.X > pos.X) && (s.startPoint.Y < pos.Y && s.startPoint.Y + s.size.Y > pos.Y));
        }

        public static Screen FindScreenByPoint(int x, int y)
        {
            return FindScreenByPoint(new Point(x, y));
        }

        public static Point GetFullSize()
        {
            if (screens == null) GetScreens();
            Point sp = new Point(), ep = new Point();
            sp = GetStartPoint();
            foreach (Screen s in screens)
            {
                if (s.startPoint.X + s.size.X > ep.X) ep.X = s.startPoint.X + s.size.X;
                if (s.startPoint.Y + s.size.Y > ep.Y) ep.Y = s.startPoint.Y + s.size.Y;
            }
            return new Point(ep.X - sp.X, ep.Y - sp.Y);
        }

        public static int GetScreenIdByPoint(Point pos)
        {
            return screens.ToList().IndexOf(FindScreenByPoint(pos));
        }

        public static int GetScreenIdByPoint(int x, int y)
        {
            return GetScreenIdByPoint(new Point(x, y));
        }

        public static Point GetStartPoint()
        {
            if (screens == null) GetScreens();
            Point rs = new Point();
            foreach (Screen s in screens)
            {
                if (s.startPoint.X < rs.X) rs.X = s.startPoint.X;
                if (s.startPoint.Y < rs.Y) rs.Y = s.startPoint.Y;
            }
            return rs;
        }

        #endregion Public Methods

        #region Private Methods

        private static void GetScreens()
        {
            screens = new Screen[Forms.Screen.AllScreens.Length];
            for (int i = 0; i < Forms.Screen.AllScreens.Length; i++)
            {
                Forms.Screen screen = Forms.Screen.AllScreens[i];
                screens[i] = new Screen(i, screen.DeviceName, new Point(screen.WorkingArea.Width, screen.WorkingArea.Height), new Point(screen.WorkingArea.X, screen.WorkingArea.Y));
            }
        }

        #endregion Private Methods

        #region Public Classes

        public class Screen
        {
            #region Public Fields

            public int id;
            public string name;
            public Point size;
            public Point startPoint;

            #endregion Public Fields

            #region Public Constructors

            public Screen(int id, string name, Point size, Point startPoint)
            {
                this.id = id;
                this.name = name;
                this.size = size;
                this.startPoint = startPoint;
            }

            #endregion Public Constructors
        }

        #endregion Public Classes
    }
}