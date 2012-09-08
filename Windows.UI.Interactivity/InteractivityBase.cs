﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Windows.UI.Interactivity
{
    public abstract class InteractivityBase : FrameworkElement, IAttachedObject
    {
        private FrameworkElement associatedObject;
        private Type associatedObjectTypeConstraint;
        //private TaskCompletionSource<object> tcs;

        //public static readonly DependencyProperty DataContextDetectorProperty =
        //        DependencyProperty.Register("DataContextDetector",
        //                            typeof(object),
        //                            typeof(InteractivityBase),
        //                            new PropertyMetadata(null, DataContextDetectorChanged));

        //public object DataContextDetector
        //{
        //    get { return GetValue(DataContextDetectorProperty); }
        //    set { this.SetValue(DataContextDetectorProperty, value); }
        //}

        //private static void DataContextDetectorChanged(object sender, DependencyPropertyChangedEventArgs e)
        //{
        //    InteractivityBase myControl = (InteractivityBase)sender;
        //    if (e.NewValue != null)
        //    {
        //        myControl.SetBinding(DataContextProperty,
        //                new Binding
        //                {
        //                    Path = new PropertyPath("DataContext"),
        //                    Source = myControl.AssociatedObject
        //                }
        //            );
        //    }
        //    if (myControl.tcs != null)
        //    {
        //        myControl.tcs.SetResult(e.NewValue);
        //    }
        //}

        #region Constructors

        public InteractivityBase()
        {
            
        }

        #endregion

        /// <summary>
        /// Gets the object to which this behavior is attached.
        /// 
        /// </summary>
        protected FrameworkElement AssociatedObject
        {
            get { return this.associatedObject; }
            set { this.associatedObject = value; }
        }

        FrameworkElement IAttachedObject.AssociatedObject
        {
            get { return this.AssociatedObject; }
        }

        internal event EventHandler AssociatedObjectChanged;

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Override this to hook up functionality to the AssociatedObject.
        /// </remarks>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// 
        /// </summary>
        /// 
        /// <remarks>
        /// Override this to unhook functionality from the AssociatedObject.
        /// </remarks>
        protected virtual void OnDetaching()
        {
        }

        protected void OnAssociatedObjectChanged()
        {
            //this.SetBinding(InteractivityBase.DataContextDetectorProperty, new Binding() { Source = this.AssociatedObject, Path = new PropertyPath("DataContext") });

            if (this.AssociatedObjectChanged == null)
            {
                return;
            }
            this.AssociatedObjectChanged(this, new EventArgs());
        }
        
        /// <summary>
        /// Gets the type constraint of the associated object.
        /// 
        /// </summary>
        /// 
        /// <value>
        /// The associated object type constraint.
        /// </value>
        protected virtual Type AssociatedObjectTypeConstraint
        {
            get { return this.associatedObjectTypeConstraint; }
            set { this.associatedObjectTypeConstraint = value; }
        }

        public abstract void Attach(FrameworkElement frameworkElement);

        public abstract void Detach();

        /// <summary>
        /// Configures data context. 
        /// </summary>
        protected async Task ConfigureDataContext()
        {
            //make sure the detector is disposed
            DataContextChangedDetector det = new DataContextChangedDetector(this, this.AssociatedObject);
            await det.WaitForDataContext();
        }
    }    
}
