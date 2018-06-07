using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Huffman.Common.Views
{
    /// <summary>
    /// Base class for splash screens featuring an image placeholder, a marquee progress bar,
    /// and a background threaded status text queuing facility that supports minimum display
    /// timespans for text.
    /// </summary>
    public partial class SplashForm : Form
    {
        #region Private Data
        /// <summary>
        /// Synchronisation primitive for thread safe access to splash status text item queue.
        /// </summary>
        private object synchronisationObject = new Object();

        /// <summary>
        /// Event which is set when all queued status text has been processed and the form is closing.
        /// Allows the application main form to wait until the splash is ready before display itself.
        /// </summary>
        private ManualResetEvent splashClosedEvent = new ManualResetEvent(false);

        /// <summary>
        /// Semaphore that allows the background worker to wait for splash items to be enqueued.
        /// Initially zero, with the potential to grow up to Int32.MaxValue.
        /// </summary>
        private Semaphore splashItemsSemaphore = new Semaphore(0, Int32.MaxValue);

        /// <summary>
        /// Boolean flag that indicates that the background worker should now exit when all status text items
        /// have been displayed.
        /// </summary>
        private bool allowToClose;

        /// <summary>
        /// Strongly typed queue of <see cref="SplashStatusTextItem"/>'s.
        /// </summary>
        private Queue<SplashStatusTextItem> splashTextQueue = new Queue<SplashStatusTextItem>();
        #endregion

        #region Public Properties
        /// <summary>
        /// Public accessor to get or set the splash image.
        /// </summary>
        public Image SplashImage
        {
            get { return _panelBody.BackgroundImage; }
            set { _panelBody.BackgroundImage = value; }
        }

        /// <summary>
        /// Public accessor which returns the <see cref="ProgressBar"/>, which is configured for marquee
        /// mode display by default.
        /// </summary>
        public ProgressBar ProgressBar
        {
            get { return this.progressBar; }
        }

        /// <summary>
        /// Wait handle which is set when all queued status text has been processed and the form is closing.
        /// Allows the application main form to wait until the splash is ready before display itself.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public WaitHandle SplashClosedWaitHandle
        {
            get { return this.splashClosedEvent; }
        }
        #endregion

        #region Constructors/Destructor
        public SplashForm(string subject)
        {
            InitializeComponent();

            _labelSubject.Text = subject;
        }
        #endregion

        #region Public Methods

        #region AllowToClose
        /// <summary>
        /// Indicates to the background worker should now exit when all status text items
        /// have been displayed. The <see cref="Enqueue"/> methods cannot be called after
        /// calling this method.
        /// </summary>
        public void AllowToClose()
        {
            this.allowToClose = true;

            // Perform a release on the semaphore in case the background worker is currently sleeping.
            this.splashItemsSemaphore.Release();
        }
        #endregion

        #region EnqueueStatusText
        /// <summary>
        /// Enqueues the supplied text for display in the status area of the splash screen.
        /// No minimum time is specified for the display of this text item when this overload is called.
        /// </summary>
        /// <param name="text">Represents a <see cref="System.String"/> containing the text.</param>
        public void EnqueueStatusText(string text)
        {
            // If allow to close is set then we must not enqueue - return immediately.
            if (this.allowToClose)
                return;

            // Make sure that the background worker is not concurrently accessing the queue whilst we're working with it.
            lock (this.synchronisationObject)
            {
                // Enqueue the new status text item.
                this.splashTextQueue.Enqueue(new SplashStatusTextItem(text));

                // And perform a single release on the semaphore. If the background worker was sleeping on the
                // semaphore it will now wake up.
                this.splashItemsSemaphore.Release();
            }
        }
        #endregion

        #region EnqueueStatusText
        /// <summary>
        /// Enqueues the supplied text for display in the status area of the splash screen.
        /// No minimum time is specified for the display of this text item when this overload is called.
        /// </summary>
        /// <param name="text">Represents a <see cref="System.String"/> containing the text.</param>
        /// <param name="minimumTimeToDisplayInMilliseconds">The minimum time to display the status text in milliseconds.</param>
        public void EnqueueStatusText(string text, int minimumTimeToDisplayInMilliseconds)
        {
            // If allow to close is set then we must not enqueue - return immediately.
            if (this.allowToClose)
                return;

            // Make sure that the background worker is not concurrently accessing the queue whilst we're working with it.
            lock (this.synchronisationObject)
            {
                // Enqueue the new status text item.
                this.splashTextQueue.Enqueue(new SplashStatusTextItem(text, new TimeSpan(0, 0, 0, 0, minimumTimeToDisplayInMilliseconds)));

                // And perform a single release on the semaphore.
                // If the background worker was sleeping on the semaphore it will now wake up.
                this.splashItemsSemaphore.Release();
            }
        }
        #endregion

        #endregion

        #region Protected Methods
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Start the background worker to process status text items.
            if (!this.DesignMode)
                this.backgroundWorker.RunWorkerAsync();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Set the splash closed wait handle so that the shell form can now go visible, if it was waiting.
            this.splashClosedEvent.Set();
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The <see cref="BackgroundWorker.DoWork"/> event handler here waits on a semaphore if there
        /// are not status text items yet to process. If the semaphore has a non-zero value and we haven't
        /// been asked to close yet and there are pending status text items to be displayed, then we safely
        /// dequeue one and use the <see cref="BackgroundWorker.ReportProgress"/> event to notify the UI
        /// thread for this instance of the status text to display. We then wait for the minimum display
        /// timespan specified for the status text item before iterating.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                // Wait on the semaphore, which will return if/as soon as there are status text items in
                // the queue or we've been allowed to close, otherwise will block indefinitely (no timeout).
                splashItemsSemaphore.WaitOne();

                SplashStatusTextItem statusTextItem = null;

                // Safely attempt to retrieve an item from the queue.
                lock (this.synchronisationObject)
                {
                    // If we're now allowed to close and there's nothing left in the queue then it's time to break.
                    if (this.allowToClose && this.splashTextQueue.Count == 0)
                        break;

                    statusTextItem = this.splashTextQueue.Dequeue();
                }

                // Use the report progress facility to communicate to this control's UI thread with the new status
                // text item to display.
                this.backgroundWorker.ReportProgress(0, statusTextItem);

                // If there's a non-zero timespan to keep the text displayed then sleep for that duration before continuing.
                if (statusTextItem.MinimumTimeSpanDisplayed > TimeSpan.Zero)
                    Thread.Sleep(statusTextItem.MinimumTimeSpanDisplayed);
            }
        }

        /// <summary>
        /// <see cref="BackgroundWorker.ProgressChanged"/> event handler which updates the status text label
        /// with the supplied status text as provided from the background worker thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            SplashStatusTextItem statusTextItem = (SplashStatusTextItem)e.UserState;
            this.labelStatus.Text = statusTextItem.Text;
        }

        /// <summary>
        /// <see cref="BackgroundWorker.RunWorkerCompleted"/> event handler which is invoked when the 
        /// background worker completes, it's time to close ourself.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // When the background worker completes, it's time to close ourself.
            this.Close();
        }
        #endregion
    }

    #region SplashStatusTextItem
    /// <summary>
    /// This class encapsulates a single request to display an item of text in the splash status text area.
    /// </summary>
    public class SplashStatusTextItem
    {
        public readonly TimeSpan MinimumTimeSpanDisplayed;
        public readonly string Text;

        /// <summary>
        /// SplashStatusTextItem class constructor requiring the status text to be displayed.
        /// </summary>
        /// <param name="text">Represents a <see cref="System.String"/> containing the text.</param>
        public SplashStatusTextItem(string text)
            : this(text, TimeSpan.Zero)
        {
        }

        /// <summary>
        /// SplashStatusTextItem class constructor requiring the status text to be displayed and minimum time for display.
        /// </summary>
        /// <param name="text">Represents a <see cref="System.String"/> containing the text.</param>
        /// <param name="minimumTimeSpanDisplayed">Represents the minimum time span to display the status text.</param>
        public SplashStatusTextItem(string text, TimeSpan minimumTimeSpanDisplayed)
        {
            this.Text = text;
            this.MinimumTimeSpanDisplayed = minimumTimeSpanDisplayed;
        }
    }
    #endregion

    public class LonelySplashViewer
    {
        private LonelySplashViewer() { }
        private static SplashForm _splashForm = null;

        public static void Start(string title, string message)
        {
            if (_splashForm != null) return;

            _splashForm = new SplashForm(title);
            Thread splashThread = new Thread(new ThreadStart(SplashThreadMain));

            splashThread.Priority = ThreadPriority.AboveNormal;
            splashThread.Start();

            _splashForm.EnqueueStatusText(message);
        }

        public static void Stop()
        {
            if (_splashForm == null) return;
            _splashForm.AllowToClose();
        }

        private static void SplashThreadMain()
        {
            if (_splashForm == null) return;

            _splashForm.StartPosition = FormStartPosition.CenterScreen;
            _splashForm.ShowDialog();

            _splashForm.Dispose();
            _splashForm = null;
        }
    }

}