using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SamGui
{

    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("Scroll")]
    public partial class CustomSliderLeft : Control
    {

        #region Events

        /// <summary>
        /// Fires when Slider position has changed
        /// </summary>
        [Description("Event fires when the Value property changes")]
        [Category("Action")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Fires when user scrolls the LeftSlider
        /// </summary>
        [Description("Event fires when the Right Slider position is changed")]
        [Category("Behavior")]
        public event ScrollEventHandler ScrollLeft;

        /// <summary>
        /// Fires when user scrolls the RightSlider
        /// </summary>
        [Description("Event fires when the Right Slider position is changed")]
        [Category("Behavior")]
        public event ScrollEventHandler ScrollRight;

        #endregion

        #region Properties
        private Rectangle progressBarLeft, progressBarRight;
        private Rectangle thumbLeft, thumbRight;
        private bool isW = false, isS = false, isI = false, isK = false;
        private bool backgroundDrawn = false;


        private int thumbSize = 40;
        /// <summary>
        /// Gets or sets Distance between Sliders
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Get Diameter of SliderThumb")]
        [Category("CustomSlider")]
        [DefaultValue(30)]
        public int TMBsize
        {
            get { return thumbSize; }
            set
            {
                if (value >= 0)
                {
                    thumbSize = value;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Must be greater that 0)");
            }
        }

        private int sliderWidth = 60;
        /// <summary>
        /// Gets or sets Distance between Sliders
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Get Diameter of Slider")]
        [Category("CustomSlider")]
        [DefaultValue(60)]
        public int sliderwidth
        {
            get { return sliderWidth; }
            set
            {
                if (value >= 0 && value > thumbSize)
                {
                    sliderWidth = value;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Must be greater that 0)");
            }
        }


        private int trackerValueLeft = 90;
        /// <summary>
        /// Gets or sets the value of Left Slider.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Set Left Slider value")]
        [Category("CustomSlider")]
        [DefaultValue(90)]
        public int ValueLeft
        {
            get { return trackerValueLeft; }
            set
            {
                if (value >= barMinimumLeft & value <= barMaximumLeft)
                {
                    trackerValueLeft = value;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }
        private int trackerValueRight = 90;
        /// <summary>
        /// Gets or sets the value of Right Slider.
        /// </summary>
        /// <value>The value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when value is outside appropriate range (min, max)</exception>
        [Description("Set Right Slider value")]
        [Category("CustomSlider")]
        [DefaultValue(90)]
        public int ValueRight
        {
            get { return trackerValueRight; }
            set
            {
                if (value >= barMinimumRight & value <= barMaximumRight)
                {
                    trackerValueRight = value;
                    if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Value is outside appropriate range (min, max)");
            }
        }

        private int barMinimumLeft = 0;
        /// <summary>
        /// Gets or sets the minimum value for left slider.
        /// </summary>
        /// <value>The minimum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when minimal value is greather than maximal one</exception>
        [Description("Set Left Slider minimal point")]
        [Category("CustomSlider")]
        [DefaultValue(0)]
        public int MinimumLeft
        {
            get { return barMinimumLeft; }
            set
            {
                if (value < barMaximumLeft)
                {
                    barMinimumLeft = value;
                    if (trackerValueLeft < barMinimumLeft)
                    {
                        trackerValueLeft = barMinimumLeft;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }

        private int barMinimumRight = 0;
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when minimal value is greather than maximal one</exception>
        [Description("Set Slider minimal point")]
        [Category("CustomSlider")]
        [DefaultValue(0)]
        public int MinimumRight
        {
            get { return barMinimumRight; }
            set
            {
                if (value < barMaximumRight)
                {
                    barMinimumRight = value;
                    if (trackerValueLeft < barMinimumRight)
                    {
                        trackerValueLeft = barMinimumRight;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Minimal value is greather than maximal one");
            }
        }


        private int barMaximumLeft = 180;
        /// <summary>
        /// Gets or sets the left slidermaximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when maximal value is lower than minimal one</exception>
        [Description("Set Slider maximal point")]
        [Category("CustomSlider")]
        [DefaultValue(180)]
        public int MaximumLeft
        {
            get { return barMaximumLeft; }
            set
            {
                if (value > barMinimumLeft)
                {
                    barMaximumLeft = value;
                    if (trackerValueLeft > barMaximumLeft)
                    {
                        trackerValueLeft = barMaximumLeft;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }

        private int barMaximumRight = 180;
        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        /// <exception cref="T:System.ArgumentOutOfRangeException">exception thrown when maximal value is lower than minimal one</exception>
        [Description("Set Slider maximal point")]
        [Category("CustomSlider")]
        [DefaultValue(180)]
        public int MaximumRight
        {
            get { return barMaximumRight; }
            set
            {
                if (value > barMinimumRight)
                {
                    barMaximumRight = value;
                    if (trackerValueLeft > barMaximumRight)
                    {
                        trackerValueLeft = barMaximumRight;
                        if (ValueChanged != null) ValueChanged(this, new EventArgs());
                    }
                    Invalidate();
                }
                else throw new ArgumentOutOfRangeException("Maximal value is lower than minimal one");
            }
        }
        //Thumb and ProgressBar image
        Image thumbImage = Image.FromFile("C:\\Users\\user\\Desktop\\SamGUI\\CustomSeekbar\\Resources\\sliderthumb.png");
        Image progressBarImage = Image.FromFile("C:\\Users\\user\\Desktop\\SamGUI\\CustomSeekbar\\Resources\\seekbarbackground.png");

        #endregion

        #region Paint

        /// <summary>
        /// Draws the colorslider control using passed colors.
        /// </summary>
        /// <param name="e">The <see cref="T:System.Windows.Forms.PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                //Setting up SlidersBackgrounds
                double padding = sliderWidth/2 ;//distance that left rectange pads the client rectangle
                if (!backgroundDrawn)
                {
                    progressBarLeft = new Rectangle((int)padding, thumbSize / 2, sliderWidth, ClientRectangle.Height - thumbSize);
                    progressBarRight = new Rectangle(ClientRectangle.Width - (int)padding - sliderWidth, thumbSize / 2, sliderWidth, ClientRectangle.Height - thumbSize);
                    e.Graphics.DrawImage(progressBarImage, progressBarLeft);
                    e.Graphics.DrawImage(progressBarImage, progressBarRight);
                    backgroundDrawn = false;
                }

                //Drawing thumbs
                double thumbDistanceLeft = (trackerValueLeft - barMinimumLeft) * progressBarLeft.Height / (barMaximumLeft - barMinimumLeft);
                thumbLeft = new Rectangle((int) padding + sliderWidth/2 - thumbSize/2, (int)thumbDistanceLeft, thumbSize, thumbSize);      
                double thumbDistanceRight = (trackerValueRight - barMinimumRight) * progressBarRight.Height / (barMaximumRight - barMinimumRight);
                thumbRight = new Rectangle(ClientRectangle.Width - (int) padding - (sliderWidth/2 - thumbSize/2) - thumbSize, (int)thumbDistanceRight, thumbSize, thumbSize);
                e.Graphics.DrawImage(thumbImage, thumbLeft);
                e.Graphics.DrawImage(thumbImage, thumbRight);

            }
            catch (Exception Err)
            {
                Console.WriteLine("DrawBackGround Error in " + Name + ":" + Err.Message);
            }
        }
       /*protected override void OnPaintBackground(PaintEventArgs e)
        {
            double padding = sliderWidth /2;
            progressBarLeft = new Rectangle((int)padding, thumbSize / 2, sliderWidth, ClientRectangle.Height - thumbSize);
            progressBarRight = new Rectangle(ClientRectangle.Width - (int)padding - sliderWidth, thumbSize / 2, sliderWidth, ClientRectangle.Height - thumbSize);
            e.Graphics.DrawImage(progressBarImage, progressBarLeft);
            e.Graphics.DrawImage(progressBarImage, progressBarRight);
        }*/


        #endregion

        #region Overrides
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
                       
            if (e.KeyCode == Keys.W) { isW = true; }
            if (e.KeyCode == Keys.S) { isS = true; }
            if (e.KeyCode == Keys.I) { isI = true; }
            if (e.KeyCode == Keys.K) { isK = true; }

            if (isW && isI)
            {
                SetProperValueLeft(ValueLeft - 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueLeft));
                SetProperValueRight(ValueRight - 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueRight));
            }
            if (isW && isK)
            {
                SetProperValueLeft(ValueLeft - 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueLeft));
                SetProperValueRight(ValueRight + 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueRight));
            }
            if (isS && isI)
            {
                SetProperValueLeft(ValueLeft + 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueLeft));
                SetProperValueRight(ValueRight - 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueRight));
            }
            if (isS && isK)
            {
                SetProperValueLeft(ValueLeft + 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueLeft));
                SetProperValueRight(ValueRight + 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueRight));
            }
            if (isW)
            {
                SetProperValueLeft(ValueLeft - 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueLeft));
            }
            if (isS)
            {
                SetProperValueLeft(ValueLeft + 5);
                if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueLeft));
            }
            if (isI)
            {
                SetProperValueRight(ValueRight - 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueRight));
            }
            if (isK)
            {
                SetProperValueRight(ValueRight + 5);
                if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueRight));
            }
            /*switch (e.KeyCode)
            {
                case Keys.W:
                    SetProperValueLeft(ValueLeft - 5);
                    if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueLeft));
                    break;
                case Keys.S:
                    SetProperValueLeft(ValueLeft + 5);
                    if (ScrollLeft != null) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueLeft));
                    break;
                case Keys.I:
                    SetProperValueRight(ValueRight - 5);
                    if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallDecrement, ValueRight));
                    break;
                case Keys.K:
                    SetProperValueRight(ValueRight + 5);
                    if (ScrollRight != null) ScrollRight(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, ValueRight));
                    break;

            }*/
            if (ScrollLeft != null && ValueLeft == barMinimumLeft) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.First, ValueLeft));
            if (ScrollLeft != null && ValueLeft == barMaximumLeft) ScrollLeft(this, new ScrollEventArgs(ScrollEventType.Last, ValueLeft));
            if (ScrollRight != null && ValueRight == barMinimumRight) ScrollRight(this, new ScrollEventArgs(ScrollEventType.First, ValueRight));
            if (ScrollRight != null && ValueRight == barMaximumRight) ScrollRight(this, new ScrollEventArgs(ScrollEventType.Last, ValueRight));
            //Invalidate();
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);        
            if (e.KeyCode == Keys.W) { isW = false; }
            if (e.KeyCode == Keys.S) { isS = false; }
            if (e.KeyCode == Keys.I) { isI = false; }
            if (e.KeyCode == Keys.K) { isK = false; }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.LostFocus"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> that contains the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }
        
        #endregion

        #region Help Routines
        public void SetProperValueLeft(int val)
        {
            if (val < barMinimumLeft) ValueLeft = barMinimumLeft;
            else if (val > barMaximumLeft) ValueLeft = barMaximumLeft;
            else ValueLeft = val;
        }
        public void SetProperValueRight(int val)
        {
            if (val < barMinimumRight) ValueRight = barMinimumRight;
            else if (val > barMaximumRight) ValueRight = barMaximumRight;
            else ValueRight = val;
        }
        #endregion
    }
}

