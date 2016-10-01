using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace Rightpoint.Peeps.Client.Models
{
    public class DynamicCollection<T> : ViewModelBase where T : Entity
    {
        private readonly int _rotationExpiryMinimum;
        private readonly int _rotationExpiryMaximum;


        public DynamicCollection(int rotationExpiryMinimum = 5000, int rotationExpiryMaximum = 10000)
        {
            this._rotationExpiryMinimum = rotationExpiryMinimum;
            this._rotationExpiryMaximum = this._rotationExpiryMinimum >= rotationExpiryMaximum ? this._rotationExpiryMinimum : rotationExpiryMaximum;
        }

        public bool IsLoading { get; set; } = true;

        private T _currentItem;
        public T CurrentItem
        {
            get { return _currentItem; }
            set
            {
                if (_currentItem != value)
                {
                    _currentItem = value;
                    RaisePropertyChanged(() => CurrentItem);
                }
            }
        }

        private int _currentIndex;
        public int CurrentIndex
        {
            get { return _currentIndex; }
            set
            {
                if (_currentIndex != value)
                {
                    _currentIndex = value;
                    RaisePropertyChanged(() => CurrentIndex);
                }
            }
        }

        /// <summary>
        /// Number of millseconds before moving to the next item in the collection
        /// </summary>
        public int RotationExpiry { get; set; }

        public ObservableCollection<T> Collection { get; } = new ObservableCollection<T>();

        public void Initialize(List<T> TModelList)
        {
            foreach (var model in TModelList)
            {
                try
                {
                    this.Collection.Add(model);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

            }

            Random random = new Random();
            this.RotationExpiry = random.Next(_rotationExpiryMinimum, _rotationExpiryMaximum);

            this.IsLoading = false;
            this.InitializeTimer();
        }

        public DispatcherTimer CollectionTimer { get; set; }

        private void InitializeTimer()
        {
            //https://msdn.microsoft.com/en-us/library/system.windows.threading.dispatchertimer(v=vs.110).aspx
            this.CurrentIndex = 0;

            if (this.Collection.Count > 0)
            {
                this.CurrentItem = this.Collection[CurrentIndex];

                this.CollectionTimer = new DispatcherTimer();
                this.CollectionTimer.Tick += this.CollectionTimer_Tick;
                this.CollectionTimer.Interval = TimeSpan.FromMilliseconds(this.RotationExpiry);
                this.CollectionTimer.Start();
            }
        }

        /// <summary>
        /// Once the "tick" has been reached, increment the CurrentIndex or reset to 0
        /// </summary>
        private void CollectionTimer_Tick(object sender, object e)
        {
            if ((this.CurrentIndex + 1) < this.Collection.Count)
            {
                CurrentIndex++;
            }
            else
            {
                CurrentIndex = 0;
            }           

            Debug.WriteLine($"{DateTime.Now} after ({TimeSpan.FromMilliseconds(this.RotationExpiry).TotalMilliseconds}) milliseconds: Rotate for {CurrentItem.GetType().ToString()}");
        }

        public override void Cleanup()
        {
            // Stop the timer if needed
            if (this.CollectionTimer != null && this.CollectionTimer.IsEnabled)
            {
                this.CollectionTimer.Stop();
                this.CollectionTimer.Tick -= this.CollectionTimer_Tick;
            }

            base.Cleanup();
        }
    }
}
