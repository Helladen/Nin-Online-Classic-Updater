using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Updater.Core;

namespace Updater.Controls
{
    public class ImageButton : Control
    {
        #region Field Region

        private Image _imageNormal;
        private Image _imageHover;
        private Image _imageClicked;

        private ControlState _controlState;

        #endregion

        #region Property Region

        [Category("Appearance")]
        [Description("The image used by the control.")]
        public Image ButtonImage
        {
            get { return _imageNormal; }
            set
            {
                _imageNormal = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The image used by the control when hovered.")]
        public Image ButtonImageHover
        {
            get { return _imageHover; }
            set
            {
                _imageHover = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        [Description("The image used by the control when clicked.")]
        public Image ButtonImageClicked
        {
            get { return _imageClicked; }
            set
            {
                _imageClicked = value;
                Invalidate();
            }
        }

        #endregion

        #region Constructor Region

        public ImageButton()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.UserPaint, true);

            SetControlState(ControlState.Normal);
        }

        #endregion

        #region Method Region

        private void SetControlState(ControlState controlState)
        {
            _controlState = controlState;
            Invalidate();
        }

        #endregion

        #region Event Handler Region

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button != MouseButtons.Left)
                SetControlState(ControlState.Hover);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            SetControlState(ControlState.Click);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (ClientRectangle.Contains(Cursor.Position))
                SetControlState(ControlState.Hover);
            else
                SetControlState(ControlState.Normal);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            SetControlState(ControlState.Normal);
        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            base.OnMouseCaptureChanged(e);

            if (!ClientRectangle.Contains(Cursor.Position))
                SetControlState(ControlState.Normal);
        }

        #endregion

        #region Paint Region

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            Image stateImage = null;

            switch (_controlState)
            {
                case ControlState.Normal:
                    stateImage = _imageNormal;
                    break;
                case ControlState.Hover:
                    stateImage = _imageHover;
                    break;
                case ControlState.Click:
                    stateImage = _imageClicked;
                    break;
            }

            if (stateImage == null)
            {
                base.OnPaint(e);
                return;
            }

            g.DrawImage(stateImage, new Point(0, 0));
        }

        #endregion
    }
}
