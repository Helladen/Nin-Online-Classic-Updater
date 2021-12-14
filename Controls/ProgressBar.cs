using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Updater.Controls
{
    public class ProgressBar : Control
    {
        #region Field Region

        private int _maximum = 100;
        private int _value;

        private Image _backgroundImage;
        private Image _progressImage;

        #endregion

        #region Property Region

        [Category("Appearance")]
        [Description("The background image used for the control.")]
        public new Image BackgroundImage
        {
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The progress bar image used for the control.")]
        public Image ProgressImage
        {
            get { return _progressImage; }
            set
            {
                _progressImage = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("The maximum value the progress bar should represent.")]
        [DefaultValue(100)]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        [Description("The current value that the progress bar represents.")]
        [DefaultValue(0)]
        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public ProgressBar()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            if (_backgroundImage == null || _progressImage == null)
            {
                base.OnPaint(e);
                return;
            }
            
            g.DrawImageUnscaled(_backgroundImage, 0, 0);

            var offset = 3; // Defines how much distance is shown between the control edges and the bar contents

            var progressWidth = _progressImage.Width;

            if (Maximum > 0)
                progressWidth = (int)(_progressImage.Width * ((float)Value / (float)Maximum));

            g.DrawImage(_progressImage, offset, offset, progressWidth, _progressImage.Height);
        }

        #endregion
    }
}
